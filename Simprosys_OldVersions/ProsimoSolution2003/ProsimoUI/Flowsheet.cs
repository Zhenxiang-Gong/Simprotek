using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
   public class Flowsheet : System.Windows.Forms.Panel, INumericFormat
   {
      public event NumericFormatStringChangedEventHandler NumericFormatStringChanged;
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

      private string currentUnitSystemName;
      public string CurrentUnitSystemName
      {
         get {return currentUnitSystemName;}
         set {currentUnitSystemName = value;}
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

      private NewSystemPreferences newSystemPrefs;
      public NewSystemPreferences NewSystemPrefs
      {
         get {return newSystemPrefs;}
      }

      private string decimalPlaces;
      public string DecimalPlaces
      {
         get {return decimalPlaces;}
         set
         {
            decimalPlaces = value;
            this.OnNumericFormatStringChanged(this);
         }
      }

      private NumericFormat numericFormat;
      public NumericFormat NumericFormat
      {
         get {return numericFormat;}
         set
         {
            numericFormat = value;
            this.OnNumericFormatStringChanged(this);
         }
      }

      public string NumericFormatString
      {
         get
         {
            if (this.NumericFormat.Equals(NumericFormat.FixedPoint))
            {
               return UI.FIXED_POINT + this.DecimalPlaces;
            }
            else if (this.NumericFormat.Equals(NumericFormat.Scientific))
            {
               return UI.SCIENTIFIC + this.DecimalPlaces;
            }
            else
               return UI.FIXED_POINT + "2";
         }
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

      public Flowsheet(NewSystemPreferences newSystemPrefs, EvaporationAndDryingSystem system)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.newSystemPrefs = newSystemPrefs;
         UnitSystemCatalog catalog = UnitSystemService.GetInstance().GetUnitSystemCatalog();
         UnitSystem us = catalog.Get("SI-2");
         UnitSystemService.GetInstance().CurrentUnitSystem = us;
         this.currentUnitSystemName = us.Name;

         this.MultipleSelection = false;
         this.Activity = FlowsheetActivity.Default;

         if (system == null)
         {
            DryingGas dg = DryingGasCatalog.GetInstance().GetDryingGas(newSystemPrefs.DryingGasName);
            if (dg == null)
               dg = DryingGasCatalog.GetInstance().GetDryingGas("Air");

            DryingMaterial dm = DryingMaterialCatalog.GetInstance().GetDryingMaterial(newSystemPrefs.DryingMaterialName);
            if (dm == null)
               dm = DryingMaterialCatalog.GetInstance().GetDryingMaterial("Generic Material");
            
            this.evapAndDryingSystem = new EvaporationAndDryingSystem("New System", dm, dg);
         }
         else
         {
            this.evapAndDryingSystem = system;
         }
         
         this.evapAndDryingSystem.SystemChanged += new SystemChangedEventHandler(evapAndDryingSystem_SystemChanged);
         this.evapAndDryingSystem.CalculationStarted += new CalculationStartedEventHandler(evapAndDryingSystem_CalculationStarted);
         this.evapAndDryingSystem.CalculationEnded += new CalculationEndedEventHandler(evapAndDryingSystem_CalculationEnded);

         this.connectionManager = new ConnectionManager(this);
         this.customEditor = new ProsimoUI.CustomEditor.CustomEditor(this);
         this.streamManager = new StreamManager(this);
         this.unitOpManager = new UnitOpManager(this);
         UI ui = new UI();
         this.BackColor = ui.FLOWSHEET_COLOR;
         this.NumericFormat = NumericFormat.FixedPoint;
         this.DecimalPlaces = "3";
         this.Version = new FlowsheetVersion();
         
         this.KeyDown += new KeyEventHandler(Flowsheet_KeyDown);
         this.KeyUp += new KeyEventHandler(Flowsheet_KeyUp);
         UnitSystemService.GetInstance().CurrentUnitSystemChanged += new CurrentUnitSystemChangedEventHandler(Flowsheet_CurrentUnitSystemChanged);
         this.IsDirty = false;
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         UnitSystemService.GetInstance().CurrentUnitSystemChanged -= new CurrentUnitSystemChangedEventHandler(Flowsheet_CurrentUnitSystemChanged);

         if (this.evapAndDryingSystem != null)
         {
            this.evapAndDryingSystem.SystemChanged -= new SystemChangedEventHandler(evapAndDryingSystem_SystemChanged);
            this.evapAndDryingSystem.CalculationStarted -= new CalculationStartedEventHandler(evapAndDryingSystem_CalculationStarted);
            this.evapAndDryingSystem.CalculationEnded -= new CalculationEndedEventHandler(evapAndDryingSystem_CalculationEnded);
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
         // 
         // contextMenu
         // 
         this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {this.menuItemPopupSelect, this.menuItemPopupFind});
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
         this.BackColor = System.Drawing.Color.White;
         this.ClientSize = new System.Drawing.Size(508, 377);
         this.ContextMenu = this.contextMenu;
         this.Name = "Flowsheet";
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseDown);
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseUp);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.Flowsheet_Paint);
         this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseMove);

      }
      #endregion

      private void OnNumericFormatStringChanged(INumericFormat iNumericFormat) 
      {
         if (NumericFormatStringChanged != null) 
         {
            NumericFormatStringChanged(iNumericFormat);
         }
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
            Form mainForm = (Form)this.Parent;
            this.toolbox.Owner = mainForm;
            this.toolbox.Show();
            this.toolbox.Location = location;
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
               this.ShowConnections(p);
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Left && this.Activity == FlowsheetActivity.SelectingSnapshot ||
            e.Button == System.Windows.Forms.MouseButtons.Left && this.Activity == FlowsheetActivity.Default)
         {
            this.snapX = e.X;
            this.snapY = e.Y;
            this.Invalidate();
         }
      }

      private void ShowConnections(Point p)
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

            if (this.Activity == FlowsheetActivity.DeletingConnection)
            {
               this.connectionManager.DeleteConnection(new Point(e.X, e.Y));
               this.ResetActivity();
            }
            else if (this.Activity == FlowsheetActivity.SelectingSnapshot)
            {
               // do not reset activity here
            }
            else if (this.Activity == FlowsheetActivity.Default)
            {
               this.Invalidate();
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
            IEnumerator en = this.StreamManager.GetStreamControls().GetEnumerator();
            while (en.MoveNext())
            {
               SolvableControl ctrl = (SolvableControl)en.Current;
               if (ctrl.Location.X > initialX && ctrl.Location.X < finalX &&
                  ctrl.Location.Y > initialY && ctrl.Location.Y < finalY)
                  ctrl.IsSelected = true;
            }
            en = this.UnitOpManager.GetUnitOpControls().GetEnumerator();
            while (en.MoveNext())
            {
               SolvableControl ctrl = (SolvableControl)en.Current;
               if (ctrl.Location.X > initialX && ctrl.Location.X < finalX &&
                  ctrl.Location.Y > initialY && ctrl.Location.Y < finalY)
                  ctrl.IsSelected = true;
            }
         }
      }

      private void Flowsheet_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
      {
         this.ConnectionManager.DrawConnections(); // this clears first the flowsheet
         if (this.Activity == FlowsheetActivity.SelectingSnapshot ||
            this.Activity == FlowsheetActivity.Default)
         {
            Pen p = new Pen(Color.Black, 1.0f);
            p.DashStyle = DashStyle.Dot;
            Graphics g = this.CreateGraphics();
            Rectangle r = new Rectangle(this.X, this.Y, this.snapX-this.X, this.snapY-this.Y);
            g.DrawRectangle(p, r);
         }
      }

      public void ShowCustomEditor()
      {
         CustomEditorForm customEditorsForm = new CustomEditorForm(this.customEditor);
         customEditorsForm.ShowDialog();
      }

      public void ShowFormulaEditor()
      {
         FormulaEditorForm formulaEditorForm = new FormulaEditorForm(this.EvaporationAndDryingSystem);
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

      private void Flowsheet_CurrentUnitSystemChanged(UnitSystem unitSystem)
      {
         this.currentUnitSystemName = unitSystem.Name;
         this.IsDirty = true;
      }

      public void InitializeCurrentUnitSystem(string name)
      {
         UnitSystemCatalog catalog = UnitSystemService.GetInstance().GetUnitSystemCatalog();
         UnitSystem us = catalog.Get(name);
         if (us != null)
            UnitSystemService.GetInstance().CurrentUnitSystem = us;
      }

      private void evapAndDryingSystem_CalculationStarted(object sender)
      {
         this.SetCursorOnUI(Cursors.WaitCursor);
      }

      private void evapAndDryingSystem_CalculationEnded(object sender)
      {
         this.SetCursorOnUI(Cursors.Default);
      }

      private void SetCursorOnUI(Cursor cursor)
      {
         this.Cursor = cursor;
         IEnumerator e = this.streamManager.GetStreamControls().GetEnumerator();
         while (e.MoveNext())
         {
            SolvableControl ctrl = (SolvableControl)e.Current;
            ctrl.Cursor = cursor;
            if (ctrl.Editor != null)
               ctrl.Editor.Cursor = cursor;
         }
         e = this.unitOpManager.GetUnitOpControls().GetEnumerator();
         while (e.MoveNext())
         {
            SolvableControl ctrl = (SolvableControl)e.Current;
            ctrl.Cursor = cursor;
            if (ctrl.Editor != null)
               ctrl.Editor.Cursor = cursor;
         }
      }

      private void evapAndDryingSystem_SystemChanged(object sender)
      {
         this.IsDirty = true;
      }

      private void editor_Disposed(object sender, EventArgs e)
      {
         this.editor.Disposed -= new EventHandler(editor_Disposed);
         this.editor = null;
      }
   }
}
