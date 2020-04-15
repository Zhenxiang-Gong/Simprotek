using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Crownwood.Magic.Common;
using Crownwood.Magic.Controls;
using Crownwood.Magic.Docking;
using Crownwood.Magic.Menus;

using Prosimo.UnitOperations;
using ProsimoUI.HumidityChart;
using ProsimoUI.ProcessStreamsUI;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;
using Prosimo.UnitSystems;
using ProsimoUI.GlobalEditor;
using ProsimoUI.CustomEditor;
using ProsimoUI.FormulaEditor;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo;
using Prosimo.Materials;
using Prosimo.SubstanceLibrary;

namespace ProsimoUI
{
   /// <summary>
   /// Summary description for Flowsheet.
   /// </summary>
   public class Flowsheet : ProsimoUI.ScrollablePanel
   {
      public event ActivityChangedEventHandler ActivityChanged;
      public event SaveFlowsheetEventHandler SaveFlowsheet;
      public event SnapshotTakenEventHandler SnapshotTaken;
      public event ToolboxAliveChangedEventHandler ToolboxAliveChanged;
      public event ToolboxLocationChangedEventHandler ToolboxLocationChanged;
      public event ToolboxVisibleChangedEventHandler ToolboxVisibleChanged;

      public int X;
      public int Y;
      private int snapX;
      private int snapY;

       private Content cA;

      public SolvableControl firstStepCtrl;
      public int attachIndex;


      private FindForm findForm;
      public FindForm FindBox
      {
         get {return findForm;}
         set {findForm = value;}
      }

      private FlowsheetVersion version;
      public FlowsheetVersion Version
      {
         get {return version;}
         set {version = value;}
      }

      private Bitmap bitmap;
      public Bitmap Image
      {
         get {return bitmap;}
         set {bitmap = value;}
      }

      private Toolbox toolbox;
      public Toolbox Toolbox
      {
         get {return toolbox;}
         set
         {
            toolbox = value;
            this.OnToolboxAliveChanged(value != null);
         }
      }

      public Point ToolboxLocation
      {
         set
         {
            this.OnToolboxLocationChanged(value);
         }
      }

      private NewProcessSettings newProcessSettings;
      public NewProcessSettings NewProcessSettings
      {
         get {return newProcessSettings;}
      }

      private ApplicationPreferences appPrefs;
      public ApplicationPreferences ApplicationPrefs
      {
         get { return appPrefs; }
      }

      private EvaporationAndDryingSystem evapAndDryingSystem;
      public EvaporationAndDryingSystem EvaporationAndDryingSystem
      {
         get {return evapAndDryingSystem;}
      }

      private bool isDirty;
      public bool IsDirty
      {
         get {return isDirty;}
         set
         {
            isDirty = value;
            if (this.Parent != null)
               this.SetDirtyCaptionFormat();
         }
      }

      private bool multipleSelection;
      public bool MultipleSelection
      {
         get {return multipleSelection;}
         set {multipleSelection = value;}
      }

      private FlowsheetActivity activity;
      public FlowsheetActivity Activity
      {
         get {return activity;}
         set
         {
            activity = value;
            if (activity == FlowsheetActivity.Default)
            {
               this.Cursor = Cursors.Default;
            }
            else if (activity == FlowsheetActivity.AddingSolidDryer ||
               activity == FlowsheetActivity.AddingCompressor ||
               activity == FlowsheetActivity.AddingLiquidDryer ||
               activity == FlowsheetActivity.AddingHeatExchanger ||
               activity == FlowsheetActivity.AddingCooler ||
               activity == FlowsheetActivity.AddingElectrostaticPrecipitator ||
               activity == FlowsheetActivity.AddingCyclone ||
               activity == FlowsheetActivity.AddingEjector ||
               activity == FlowsheetActivity.AddingWetScrubber ||
               activity == FlowsheetActivity.AddingScrubberCondenser ||
               activity == FlowsheetActivity.AddingMixer ||
               activity == FlowsheetActivity.AddingTee ||
               activity == FlowsheetActivity.AddingFlashTank ||
               activity == FlowsheetActivity.AddingFan ||
               activity == FlowsheetActivity.AddingValve ||
               activity == FlowsheetActivity.AddingBagFilter ||
               activity == FlowsheetActivity.AddingAirFilter ||
               activity == FlowsheetActivity.AddingGasStream ||
               activity == FlowsheetActivity.AddingHeater ||
               activity == FlowsheetActivity.AddingLiquidMaterialStream ||
               activity == FlowsheetActivity.AddingSolidMaterialStream ||
               activity == FlowsheetActivity.AddingProcessStream ||
               activity == FlowsheetActivity.AddingPump ||
               activity == FlowsheetActivity.AddingRecycle
               )
            {
               this.Cursor = Cursors.Cross;
               this.SetSolvableControlsSelection(false);
            }
            else if (activity == FlowsheetActivity.AddingConnStepOne ||
                     activity == FlowsheetActivity.AddingConnStepTwo ||
                     activity == FlowsheetActivity.DeletingConnection
               )
            {
               this.Cursor = Cursors.No;
               this.SetSolvableControlsSelection(false);
            }
            else if (activity == FlowsheetActivity.SelectingSnapshot)
            {
               this.Cursor = Cursors.Default;
               this.SetSolvableControlsSelection(false);
            }
            this.OnActivityChanged(activity);
         }
      }

      private ProsimoUI.CustomEditor.CustomEditor customEditor;
      public ProsimoUI.CustomEditor.CustomEditor CustomEditor
      {
         get {return customEditor;}
      }

      private ConnectionManager connectionManager;
      public ConnectionManager ConnectionManager
      {
         get {return connectionManager;}
      }

      private StreamManager streamManager;
      public StreamManager StreamManager
      {
         get {return streamManager;}
      }

      private UnitOpManager unitOpManager;
      public UnitOpManager UnitOpManager
      {
         get {return unitOpManager;}
      }

      private SystemEditor editor;
      public SystemEditor Editor
      {
         get {return editor;}
         set {editor = value;}
      }
      
      private System.Windows.Forms.SaveFileDialog saveFileDialog;
      private System.Windows.Forms.ContextMenu contextMenu;
      private System.Windows.Forms.MenuItem menuItemPopupSelect;
      private System.Windows.Forms.MenuItem menuItemPopupFind;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public Flowsheet()
      {
      }

      public Flowsheet(NewProcessSettings newProcessSettings, ApplicationPreferences appPrefs, EvaporationAndDryingSystem system)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.newProcessSettings = newProcessSettings;
         this.appPrefs = appPrefs;

         this.MultipleSelection = false;
         this.Activity = FlowsheetActivity.Default;

         if (system == null)
         {
            DryingGas dg = DryingGasCatalog.GetInstance().GetDryingGas(newProcessSettings.DryingGasName);
            if (dg == null)
            {
//               string message = "You need to set a drying gas in Materials / New Process Settings first!";
//               MessageBox.Show(message, "New Flowsheet Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               dg = DryingGasCatalog.GetInstance().GetDryingGas("Air");
            }

            DryingMaterial dm = DryingMaterialCatalog.GetInstance().GetDryingMaterial(newProcessSettings.DryingMaterialName);
            if (dm == null)
            {
               string message = "You need to choose a drying material for the new flowsheet to be created first! (go to Materials / New Flowsheet Settings)";
               MessageBox.Show(message, "New Flowsheet Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            this.evapAndDryingSystem = new EvaporationAndDryingSystem(UI.NEW_SYSTEM, dm, dg);
         }
         else
         {
            this.evapAndDryingSystem = system;
         }
         
         this.evapAndDryingSystem.SystemChanged += new SystemChangedEventHandler(evapAndDryingSystem_SystemChanged);
         this.evapAndDryingSystem.CalculationStarted += new CalculationStartedEventHandler(evapAndDryingSystem_CalculationStarted);
         this.evapAndDryingSystem.CalculationEnded += new CalculationEndedEventHandler(evapAndDryingSystem_CalculationEnded);
         this.evapAndDryingSystem.NameChanged += new NameChangedEventHandler(evapAndDryingSystem_NameChanged);

         this.connectionManager = new ConnectionManager(this);
         this.customEditor = new ProsimoUI.CustomEditor.CustomEditor(this);
         this.streamManager = new StreamManager(this);
         this.unitOpManager = new UnitOpManager(this);
         this.BackColor = Color.White;
         this.Version = new FlowsheetVersion();

         this.IsDirty = false;
      }

      void evapAndDryingSystem_NameChanged(object sender, string name, string oldName)
      {
         this.Parent.Text = ApplicationInformation.PRODUCT + " - " + name;
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         if (this.evapAndDryingSystem != null)
         {
            this.evapAndDryingSystem.SystemChanged -= new SystemChangedEventHandler(evapAndDryingSystem_SystemChanged);
            this.evapAndDryingSystem.CalculationStarted -= new CalculationStartedEventHandler(evapAndDryingSystem_CalculationStarted);
            this.evapAndDryingSystem.CalculationEnded -= new CalculationEndedEventHandler(evapAndDryingSystem_CalculationEnded);
            this.evapAndDryingSystem.NameChanged -= new NameChangedEventHandler(evapAndDryingSystem_NameChanged);
         }
         
         if( disposing )
         {
            if(components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose( disposing );
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
          this.contextMenu = new System.Windows.Forms.ContextMenu();
          this.menuItemPopupSelect = new System.Windows.Forms.MenuItem();
          this.menuItemPopupFind = new System.Windows.Forms.MenuItem();
          this.SuspendLayout();
          // 
          // contextMenu
          // 
          this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemPopupSelect,
            this.menuItemPopupFind});
          // 
          // menuItemPopupSelect
          // 
          this.menuItemPopupSelect.Index = 0;
          this.menuItemPopupSelect.Text = "Select All";
          this.menuItemPopupSelect.Click += new System.EventHandler(this.menuItemPopupSelect_Click);
          // 
          // menuItemPopupFind
          // 
          this.menuItemPopupFind.Index = 1;
          this.menuItemPopupFind.Text = "Find...";
          this.menuItemPopupFind.Click += new System.EventHandler(this.menuItemPopupFind_Click);
          // 
          // Flowsheet
          // 
          this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.BackColor = System.Drawing.Color.White;
          this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.ContextMenu = this.contextMenu;
          this.Enter += new System.EventHandler(this.Flowsheet_Enter);
          this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseMove);
          this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Flowsheet_KeyUp);
          this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseUp);
          this.SizeChanged += new System.EventHandler(this.Flowsheet_SizeChanged);
          this.DragLeave += new System.EventHandler(this.Flowsheet_DragLeave);
          this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseDown);
          this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Flowsheet_KeyDown);
          this.ResumeLayout(false);

      }

      #endregion

      void Flowsheet_Enter(object sender, EventArgs e)
      {
         this.ConnectionManager.DrawConnections();
         
      }

      void Flowsheet_SizeChanged(object sender, EventArgs e)
      {
         this.ConnectionManager.DrawConnections();
      }

      void Flowsheet_DragLeave(object sender, EventArgs e)
      {
         this.ConnectionManager.DrawConnections();
        
      }

      private void OnActivityChanged(FlowsheetActivity flowsheetActivity) 
      {
         if (ActivityChanged != null) 
         {
            ActivityChanged(flowsheetActivity);
         }
      }

      private void OnToolboxAliveChanged(bool alive) 
      {
         if (ToolboxAliveChanged != null) 
         {
            ToolboxAliveChanged(alive);
         }
      }

      private void OnToolboxVisibleChanged(bool visible) 
      {
         if (ToolboxVisibleChanged != null) 
         {
            ToolboxVisibleChanged(visible);
         }
      }

      private void OnToolboxLocationChanged(Point location) 
      {
         if (ToolboxLocationChanged != null) 
         {
            ToolboxLocationChanged(location);
         }
      }

      private void OnSnapshotTaken(Bitmap image) 
      {
         if (SnapshotTaken != null) 
         {
            SnapshotTaken(image);
         }
      }

      private void OnSaveFlowsheet(Flowsheet flowsheet) 
      {
         if (SaveFlowsheet != null) 
         {
            SaveFlowsheet(flowsheet);
         }
      }

      public void PrepareForTheMoveWithoutSelection(SolvableControl feCtrl, int x, int y)
      {
         if (this.Controls.Count > 0) 
         {
            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is SolvableControl)
               {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected && !ctrl.Equals(feCtrl))
                  {
                     ctrl.PrepareForTheMoveWithoutSelection(x, y);
                  }
               }
            }
         }
      }

      public void ChangeLocation(SolvableControl feCtrl, Point p)
      {
         if (this.Controls.Count > 0) 
         {
            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is SolvableControl)
               {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected && !ctrl.Equals(feCtrl))
                  {
                     ctrl.ChangeLocation(p);
                  }
               }
            }
         }
      }

      private void SetDirtyCaptionFormat()
      {
         if (this.isDirty) 
         {
            if (!this.Parent.Text.EndsWith(UI.DIRTY))
               this.Parent.Text = this.Parent.Text + UI.DIRTY;
         }
         else 
         {
            if (this.Parent.Text.EndsWith(UI.DIRTY))
               this.Parent.Text = this.Parent.Text.Substring(0, this.Parent.Text.Length-1);
         }
      }

      public void MakeNondirtyAll()
      {
         this.IsDirty = false;
         IEnumerator e = this.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is SolvableControl)
               ((SolvableControl)e.Current).IsDirty = false;
         }
      }

      public void EditSystem()
      {
         if (this.editor == null)
         {
            this.editor = new SystemEditor(this);
            // we need to subscribe to Dispose() and not to Close()
            // see SystemEditor.menuItemClose_Click() for details
            this.editor.Disposed += new EventHandler(editor_Disposed);
            this.editor.Owner = (Form)this.Parent;
            this.editor.Show();
         }
         else
         {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            this.editor.Activate();
         }
      }

      public void AddGasStream()
      {
         this.Activity = FlowsheetActivity.AddingGasStream;
      }
   
      public void AddLiquidMaterialStream()
      {
         this.Activity = FlowsheetActivity.AddingLiquidMaterialStream;
      }

      public void AddSolidMaterialStream()
      {
         this.Activity = FlowsheetActivity.AddingSolidMaterialStream;
      }

      public void AddProcessStream()
      {
         this.Activity = FlowsheetActivity.AddingProcessStream;
      }

      public void AddRecycle()
      {
         this.Activity = FlowsheetActivity.AddingRecycle;
      }

      public void AddLiquidDryer()
      {
         this.Activity = FlowsheetActivity.AddingLiquidDryer;
      }

      public void AddSolidDryer()
      {
         this.Activity = FlowsheetActivity.AddingSolidDryer;
      }

      public void AddCyclone()
      {
         this.Activity = FlowsheetActivity.AddingCyclone;
      }

      public void AddEjector()
      {
         this.Activity = FlowsheetActivity.AddingEjector;
      }

      public void AddWetScrubber()
      {
         this.Activity = FlowsheetActivity.AddingWetScrubber;
      }

      public void AddScrubberCondenser()
      {
         this.Activity = FlowsheetActivity.AddingScrubberCondenser;
      }

      public void AddHeatExchanger()
      {
         this.Activity = FlowsheetActivity.AddingHeatExchanger;
      }

      public void AddMixer()
      {
         this.Activity = FlowsheetActivity.AddingMixer;
      }

      public void AddTee()
      {
         this.Activity = FlowsheetActivity.AddingTee;
      }

      public void AddFlashTank()
      {
         this.Activity = FlowsheetActivity.AddingFlashTank;
      }

      public void AddFan()
      {
         this.Activity = FlowsheetActivity.AddingFan;
      }

      public void AddValve()
      {
         this.Activity = FlowsheetActivity.AddingValve;
      }

      public void AddBagFilter()
      {
         this.Activity = FlowsheetActivity.AddingBagFilter;
      }

      public void AddAirFilter()
      {
         this.Activity = FlowsheetActivity.AddingAirFilter;
      }

      public void AddCompressor()
      {
         this.Activity = FlowsheetActivity.AddingCompressor;
      }

      public void AddHeater()
      {
         this.Activity = FlowsheetActivity.AddingHeater;
      }

      public void AddElectrostaticPrecipitator()
      {
         this.Activity = FlowsheetActivity.AddingElectrostaticPrecipitator;
      }

      public void AddCooler()
      {
         this.Activity = FlowsheetActivity.AddingCooler;
      }

      public void AddPump()
      {
         this.Activity = FlowsheetActivity.AddingPump;
      }

      public void DeleteSelectedSolvables()
      {
         if (this.Controls.Count > 0) 
         {
            ArrayList toDeleteControls = new ArrayList();

            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is SolvableControl)
               {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected)
                  {
                     toDeleteControls.Add(ctrl);
                  }
               }
            }

            if (toDeleteControls.Count > 0)
            {
               string message = "Are you sure that you want to delete the selected items?";
               DialogResult dr = MessageBox.Show(this, message, "Delete: " + this.Text,
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

               switch (dr)
               {
                  case System.Windows.Forms.DialogResult.Yes:
                     IEnumerator e2 = toDeleteControls.GetEnumerator();
                     while (e2.MoveNext())
                     {
                        SolvableControl ctrl = (SolvableControl)e2.Current;
                        // delete from the model, the UI will be updated in the event listener
                        if (ctrl is UnitOpControl)
                        {
                           UnitOperation unitOp = ((UnitOpControl)ctrl).UnitOperation;
                           this.evapAndDryingSystem.DeleteUnitOperation(unitOp);
                        }
                        else if (ctrl is ProcessStreamBaseControl)
                        {
                           ProcessStreamBase processStream = ((ProcessStreamBaseControl)ctrl).ProcessStreamBase;
                           this.evapAndDryingSystem.DeleteStream(processStream);
                        }
                     }
                     break;
                  case System.Windows.Forms.DialogResult.No:
                     break;
               }
            }
         }
      }

      private void EditSelectedControl()
      {
         if (this.Controls.Count > 0) 
         {   
            ArrayList toEditControls = new ArrayList();

            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is SolvableControl)
               {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected)
                  {
                     toEditControls.Add(ctrl);
                  }
               }
            }

            if (toEditControls.Count < 1)
            { 
               string message = "Please select an item.";
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (toEditControls.Count > 1)
            {
               string message = "Please select only one item."; 
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               ((IEditable)toEditControls[0]).Edit();
            }
         }
      }

      private void CloseDependantsOfFlowsheet()
      {
         if (this.Editor != null)
         {
            this.Editor.Close();
            this.Editor = null;
         }
         if (this.toolbox != null)
         {
            this.toolbox.Close();
         }
      }

      private void CloseDependantsOfSolvables()
      {
         IEnumerator e = this.Controls.GetEnumerator();
         while (e.MoveNext())
         {
            if (e.Current is SolvableControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.Editor != null)
               {
                  ctrl.Editor.Close();
                  ctrl.Editor = null;
               }
            }
         }
      }

      public void AddConnection()
      {
         this.Activity = FlowsheetActivity.AddingConnStepOne;
         this.firstStepCtrl = null;
         this.attachIndex = -1;
      }

      public void ResetActivity()
      {
         this.Activity = FlowsheetActivity.Default;
         this.firstStepCtrl = null;
         this.attachIndex = -1;
      }

      public void SetSolvableControlsSelection(bool isSelected)
      {
         // clear selection of flowsheet elements
         IEnumerator e2 = this.Controls.GetEnumerator();
         while (e2.MoveNext()) 
         {
            if (e2.Current is SolvableControl)
            {
               ((SolvableControl)e2.Current).IsSelected = isSelected;
            }
         }
      }

      public void DeleteSolvable(SolvableControl ctrl)
      {
         if (ctrl != null) 
         {
            if (ctrl is UnitOpControl)
            {
               UnitOperation unitOp = ((UnitOpControl)ctrl).UnitOperation;
               this.evapAndDryingSystem.DeleteUnitOperation(unitOp);
            }
            else if (ctrl is ProcessStreamBaseControl)
            {
               ProcessStreamBase processStream = ((ProcessStreamBaseControl)ctrl).ProcessStreamBase;
               this.evapAndDryingSystem.DeleteStream(processStream);
            }
         }
      }
      
      public void RemoveSolvableControl(SolvableControl ctrl)
      {
         // remove dependents
         if (ctrl.Editor != null)
         {
            ctrl.Editor.Close();
            ctrl.Editor = null;
         }
         
         // remove the connections of this control
         this.ConnectionManager.RemoveConnections(ctrl.Solvable.Name);

         this.Controls.Remove(ctrl);
         ctrl.Dispose();
      }

      private void Flowsheet_KeyDown(object sender, KeyEventArgs e)
      {
          if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
              this.MultipleSelection = true;
          else if (e.KeyCode == Keys.Delete)
              DeleteSelectedSolvables();

      }

      private void Flowsheet_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            this.MultipleSelection = false;
      }


      private void menuItemPopupSystem_Click(object sender, System.EventArgs e)
      {
         this.EditSystem();
      }

      private void menuItemPopupSelect_Click(object sender, System.EventArgs e)
      {
         this.SetSolvableControlsSelection(true);
      }

      private void menuItemPopupFind_Click(object sender, System.EventArgs e)
      {
         this.ShowFindForm();
      }

      public void CreateHumidityChart()
      {
         PsychrometricChartModel psychrometricChartModel = this.EvaporationAndDryingSystem.GetPsychrometricChartModel();
         HumidityChartForm humidityChartForm = new HumidityChartForm(this, psychrometricChartModel);
         humidityChartForm.ShowDialog();
      }
      public void ShowToolbox(Point location)
      {
         if (this.toolbox == null)
         {
            this.Toolbox = new Toolbox(this);
            MainForm mainForm = (MainForm)this.Parent;
            this.toolbox.Owner = mainForm;
            // Create three Content objects, one of each type
            cA = mainForm._manager.Contents.Add(this.toolbox, this.toolbox.Text);
            mainForm._manager.ContentHidden += new DockingManager.ContentHandler(this.toolbox.Toolbox_FormClosed);
            // Define the initial floating form size
            cA.FloatingSize = new Size(78, 427);
            if (location != null)
                cA.DisplayLocation = location;
            //cA.FloatingSize = this.toolbox.toolBoxPanel.Size;
           // cA.DisplaySize = this.toolbox.toolBoxPanel.Size;
            cA.CloseOnHide = true;            
            // Request a new Docking window be created for the first content on the bottom edge
            WindowContent wc = mainForm._manager.AddContentWithState(cA, State.Floating) as WindowContent;
            // Make the content with title 'Notepad'
            // visible
            mainForm._manager.ShowContent(cA);
            this.ResetActivity(); // this call is to uncheck the misterious liquid dryer radio buton selection
            this.OnToolboxVisibleChanged(true);
         }
         else
         {
             if (this.toolbox.WindowState.Equals(FormWindowState.Minimized))
                 this.toolbox.WindowState = FormWindowState.Normal;
             this.toolbox.Activate();
         }
      }

      public void ShowFindForm()
      {
         if (this.findForm == null)
         {
            this.findForm = new FindForm(this);
            Form mainForm = (Form)this.Parent;
            this.findForm.Owner = mainForm;
            this.findForm.Show();
         }
         else
         {
            if (this.findForm.WindowState.Equals(FormWindowState.Minimized))
               this.findForm.WindowState = FormWindowState.Normal;
            this.findForm.Activate();
         }
      }

      public void CutConnection()
      {
         this.Activity = FlowsheetActivity.DeletingConnection;
      }

      public void SelectSnapshot()
      {
         this.Activity = FlowsheetActivity.SelectingSnapshot;
      }

      private void Flowsheet_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         Point p = new Point(e.X, e.Y);
         if (e.Button == System.Windows.Forms.MouseButtons.None)
         {
            if (this.activity == FlowsheetActivity.Default)
               this.Cursor = Cursors.Default;
            else if (this.Activity == FlowsheetActivity.DeletingConnection)
               this.ChangeCursorOverConnection(p);
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Left && this.Activity == FlowsheetActivity.SelectingSnapshot ||
            e.Button == System.Windows.Forms.MouseButtons.Left && this.Activity == FlowsheetActivity.Default)
         {
            this.snapX = e.X;
            this.snapY = e.Y;
//            this.Invalidate();
            this.ConnectionManager.DrawConnections(); // this clears first the flowsheet
            this.DrawSelection();
         }
      }

      private void DrawSelection()
      {
         if (this.Activity == FlowsheetActivity.SelectingSnapshot ||
            this.Activity == FlowsheetActivity.Default)
         {
            Pen p = new Pen(Color.Black, 1.0f);
            p.DashStyle = DashStyle.Dot;
            Graphics g = this.CreateGraphics();
            Rectangle r = new Rectangle(this.X, this.Y, this.snapX - this.X, this.snapY - this.Y);
            g.DrawRectangle(p, r);
         }
      }

      private void ChangeCursorOverConnection(Point p)
      {
         this.Cursor = Cursors.No;
         if (this.connectionManager.Connections.Count > 0) 
         {
            IEnumerator e = this.connectionManager.Connections.GetEnumerator();
            while (e.MoveNext())
            {
               SolvableConnection conn = (SolvableConnection)e.Current;
               if (conn.HitTest(p))
               {
                  this.Cursor = Cursors.Cross;
                  break;
               }
            }
         }
      }

      public bool Close()
      {
         bool cancel = false;
         if (this.isDirty)
         {
            string message = "Do you want to save the current changes?";
            DialogResult dr2 = MessageBox.Show(this, message, "Save Changes: " + this.Text,
               MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (dr2)
            {
               case System.Windows.Forms.DialogResult.Yes:

                  this.OnSaveFlowsheet(this);
                  break;
               case System.Windows.Forms.DialogResult.No:
                  break;
               case System.Windows.Forms.DialogResult.Cancel:
                  cancel = true;
                  break;
            }
            if (!cancel)
            {
               this.CloseDefinitively();
            }
         }
         else
         {
            this.CloseDefinitively();
         }
         bool close = !cancel;
         return close;
      }

      private void CloseDefinitively()
      {
          if (this.toolbox != null)
            ((MainForm)this.Parent)._manager.Contents.Remove(cA);
         this.OnToolboxVisibleChanged(this.toolbox != null);
         this.CloseDependantsOfSolvables();
         this.CloseDependantsOfFlowsheet();
         this.Activity = FlowsheetActivity.Default;
      }

      private void Flowsheet_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if(e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            // first remember the position
            this.X = e.X;
            this.Y = e.Y;
            this.snapX = e.X;
            this.snapY = e.Y;

            //if (this.Activity == FlowsheetActivity.DeletingConnection)
            //{
            //   this.connectionManager.DeleteConnection(new Point(e.X, e.Y));
            //   this.ResetActivity();
            //}
            if (this.Activity == FlowsheetActivity.SelectingSnapshot)
            {
               // do not reset activity here
            }
            else if (this.Activity == FlowsheetActivity.Default)
            {
//               this.Invalidate();
                this.ConnectionManager.DrawConnections();
               this.SetSolvableControlsSelection(false);
               this.ResetActivity();
            }
            else
            {
               if (this.Activity == FlowsheetActivity.AddingGasStream)
               {
                  // update the model
                  // the UI is updated in the event listener
                  evapAndDryingSystem.CreateStream(typeof(DryingGasStream));
               }
               else if (this.Activity == FlowsheetActivity.AddingSolidMaterialStream)
               {
                  evapAndDryingSystem.CreateSolidMaterialStream();
               }
               else if (this.Activity == FlowsheetActivity.AddingLiquidMaterialStream)
               {
                  evapAndDryingSystem.CreateLiquidMaterialStream();
               }
               else if (this.Activity == FlowsheetActivity.AddingProcessStream)
               {
                  evapAndDryingSystem.CreateStream(typeof(ProcessStream));
               }
               else if (this.Activity == FlowsheetActivity.AddingRecycle)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Recycle));
               }
               else if (this.Activity == FlowsheetActivity.AddingLiquidDryer)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(LiquidDryer));
               }
               else if (this.Activity == FlowsheetActivity.AddingSolidDryer)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(SolidDryer));
               }
               else if (this.Activity == FlowsheetActivity.AddingHeatExchanger)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(HeatExchanger));
               }
               else if (this.Activity == FlowsheetActivity.AddingCyclone)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Cyclone));
               }
               else if (this.Activity == FlowsheetActivity.AddingEjector)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Ejector));
               }
               else if (this.Activity == FlowsheetActivity.AddingWetScrubber)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(WetScrubber));
               }
               else if (this.Activity == FlowsheetActivity.AddingScrubberCondenser)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(ScrubberCondenser));
               }
               else if (this.Activity == FlowsheetActivity.AddingMixer)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Mixer));
               }
               else if (this.Activity == FlowsheetActivity.AddingTee)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Tee));
               }
               else if (this.Activity == FlowsheetActivity.AddingFlashTank)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(FlashTank));
               }
               else if (this.Activity == FlowsheetActivity.AddingFan)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Fan));
               }
               else if (this.Activity == FlowsheetActivity.AddingValve)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Valve));
               }
               else if (this.Activity == FlowsheetActivity.AddingBagFilter)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(BagFilter));
               }
               else if (this.Activity == FlowsheetActivity.AddingAirFilter)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(AirFilter));
               }
               else if (this.Activity == FlowsheetActivity.AddingCompressor)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Compressor));
               }
               else if (this.Activity == FlowsheetActivity.AddingHeater)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Heater));
               }
               else if (this.Activity == FlowsheetActivity.AddingCooler)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Cooler));
               }
               else if (this.Activity == FlowsheetActivity.AddingElectrostaticPrecipitator)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(ElectrostaticPrecipitator));
               }
               else if (this.Activity == FlowsheetActivity.AddingPump)
               {
                  evapAndDryingSystem.CreateUnitOperation(typeof(Pump));
               }
               this.ResetActivity();
            }
         }
         else if(e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            this.SetSolvableControlsSelection(false);
            this.ResetActivity();
         }
      }

      private void Flowsheet_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if(e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            int endX = e.X;
            int endY = e.Y;

            if (this.Activity == FlowsheetActivity.SelectingSnapshot)
            {
               this.SaveSnapshot(this.X, this.Y, endX, endY);
               this.X = 0;
               this.Y = 0;
               this.snapX = 0;
               this.snapY = 0;
            }
            else if (this.Activity == FlowsheetActivity.Default)
            {
                this.ConnectionManager.DrawConnections(); // this clears first the flowsheet
                this.SelectElements(this.X, this.Y, endX, endY);
                this.X = 0;
                this.Y = 0;
                this.snapX = 0;
                this.snapY = 0;
            }
         }
         else if(e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            this.SetSolvableControlsSelection(false);
            this.ResetActivity();
         }
      }

      private void SelectElements(int initialX, int initialY, int finalX, int finalY)
      {
         if (finalX>initialX && finalY>initialY)
         {
            IEnumerator<ProcessStreamBaseControl> en = this.StreamManager.GetStreamControls<ProcessStreamBaseControl>().GetEnumerator();
            while (en.MoveNext())
            {
               ProcessStreamBaseControl ctrl = en.Current;
               if (ctrl.Location.X > initialX && ctrl.Location.X < finalX &&
                  ctrl.Location.Y > initialY && ctrl.Location.Y < finalY)
                  ctrl.IsSelected = true;
            }
            IEnumerator<UnitOpControl> en2 = this.UnitOpManager.GetUnitOpControls<UnitOpControl>().GetEnumerator();
            while (en2.MoveNext())
            {
               UnitOpControl ctrl = en2.Current;
               if (ctrl.Location.X > initialX && ctrl.Location.X < finalX &&
                  ctrl.Location.Y > initialY && ctrl.Location.Y < finalY)
                  ctrl.IsSelected = true;
            }
         }
      }

      //private void Flowsheet_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
      //{
      //   this.ConnectionManager.DrawConnections(); // this clears first the flowsheet
      //   if (this.Activity == FlowsheetActivity.SelectingSnapshot ||
      //      this.Activity == FlowsheetActivity.Default)
      //   {
      //      Pen p = new Pen(Color.Black, 1.0f);
      //      p.DashStyle = DashStyle.Dot;
      //      Graphics g = this.CreateGraphics();
      //      Rectangle r = new Rectangle(this.X, this.Y, this.snapX - this.X, this.snapY - this.Y);
      //      g.DrawRectangle(p, r);
      //   }
      //}

      public void ShowCustomEditor()
      {
         CustomEditorForm customEditorsForm = new CustomEditorForm(this, this.customEditor);
         customEditorsForm.ShowDialog();
      }

      public void ShowFormulaEditor()
      {
         FormulaEditorForm formulaEditorForm = new FormulaEditorForm(this, this.EvaporationAndDryingSystem);
         formulaEditorForm.ShowDialog();
      }

      private void SaveSnapshot(int x1, int y1, int x2, int y2)
      {
         if (x2>x1 && y2>y1)
         {
            this.CaptureImage(x1,y1,x2,y2);
            this.OnSnapshotTaken(this.Image);
         }
      }

      public void CaptureImage(int x1, int y1, int x2, int y2)
      {
         // x1, y1 = starting point
         // x2, y2 = ending point
         int w = x2-x1;
         int h = y2-y1;
         Graphics sourceGraphics = this.CreateGraphics();
         this.Image = new Bitmap(w, h, sourceGraphics);
         Graphics destinGraphics = Graphics.FromImage(this.Image);
         
         IntPtr dcSource = sourceGraphics.GetHdc();
         IntPtr dcDestin = destinGraphics.GetHdc();
         
         BitBlt(dcDestin, 0, 0, w, h, dcSource, x1, y1, 13369376); //13369376 is SRCCOPY = 0xCC0020
         
         sourceGraphics.ReleaseHdc(dcSource);
         destinGraphics.ReleaseHdc(dcDestin);
         sourceGraphics.Dispose();
         destinGraphics.Dispose();
      }

      [System.Runtime.InteropServices.DllImport("gdi32.dll")]
      public static extern long BitBlt (IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

      private void evapAndDryingSystem_CalculationStarted(object sender)
      {
         this.SetCursorOnUI(Cursors.WaitCursor);
      }

      private void evapAndDryingSystem_CalculationEnded(object sender)
      {
         this.SetCursorOnUI(Cursors.Default);
         this.connectionManager.DrawConnections();
      }

      private void SetCursorOnUI(Cursor cursor)
      {
         this.Cursor = cursor;
         IEnumerator<ProcessStreamBaseControl> e = this.streamManager.GetStreamControls<ProcessStreamBaseControl>().GetEnumerator();
         while (e.MoveNext())
         {
            ProcessStreamBaseControl ctrl = e.Current;
            ctrl.Cursor = cursor;
            if (ctrl.Editor != null)
               ctrl.Editor.Cursor = cursor;
         }
         IEnumerator<UnitOpControl> e2 = this.unitOpManager.GetUnitOpControls<UnitOpControl>().GetEnumerator();
         while (e2.MoveNext())
         {
            UnitOpControl ctrl = e2.Current;
            ctrl.Cursor = cursor;
            if (ctrl.Editor != null)
               ctrl.Editor.Cursor = cursor;
         }
      }

      private void evapAndDryingSystem_SystemChanged(object sender)
      {
         this.IsDirty = true;
         this.connectionManager.DrawConnections();
      }

      private void editor_Disposed(object sender, EventArgs e)
      {
         this.editor.Disposed -= new EventHandler(editor_Disposed);
         this.editor = null;
      }
   }
}
