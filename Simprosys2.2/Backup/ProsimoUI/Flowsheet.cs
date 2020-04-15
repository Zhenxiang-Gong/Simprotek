using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;

using Prosimo;
using Prosimo.Materials;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.CustomEditor;
using ProsimoUI.FormulaEditor;
using ProsimoUI.GlobalEditor;
using ProsimoUI.HumidityChart;
using ProsimoUI.ProcessStreamsUI;
using ProsimoUI.UnitOperationsUI;

namespace ProsimoUI {
   
   public enum FlowsheetActivity {
      Default = 0,
      AddingSolvable,

      AddingConnStepOne,
      AddingConnStepTwo,
      DeletingConnection,
      SelectingSnapshot
   }

   /// <summary>
   /// Summary description for Flowsheet.
   /// </summary>
   [Serializable]
   public class Flowsheet : Panel, ISerializable {
      private const int CLASS_PERSISTENCE_VERSION = 2;
      
      private StreamingContext context;
      private SerializationInfo info;
      
      public event ActivityChangedEventHandler<ActivityChangedEventArgs> ActivityChanged;
      public event SaveFlowsheetEventHandler SaveFlowsheet;
      public event SnapshotTakenEventHandler SnapshotTaken;

      public int X = -1;
      public int Y = -1;
      private int snapX;
      private int snapY;

      //private Content cA;

      public SolvableControl firstStepCtrl;
      public int attachIndex;

      private FindForm findForm;
      public FindForm FindBox {
         get { return findForm; }
         set { findForm = value; }
      }

      private FlowsheetVersion version;
      public FlowsheetVersion Version {
         get { return version; }
         set { version = value; }
      }

      private Bitmap bitmap;
      public Bitmap Image {
         get { return bitmap; }
         set { bitmap = value; }
      }

      private ApplicationPreferences appPrefs;
      public ApplicationPreferences ApplicationPrefs {
         get { return appPrefs; }
         set { appPrefs = value; }
      }

      private FlowsheetPreferences flowsheetPrefs;
      internal FlowsheetPreferences FlowsheetPrefs {
         get { return flowsheetPrefs; }
      }

      private EvaporationAndDryingSystem evapAndDryingSystem;
      public EvaporationAndDryingSystem EvaporationAndDryingSystem {
         get { return evapAndDryingSystem; }
      }

      private bool isDirty;
      public bool IsDirty {
         get { return isDirty; }
         set {
            isDirty = value;
            if (this.Parent != null)
               this.SetDirtyCaptionFormat();
         }
      }

      private bool multipleSelection;
      public bool MultipleSelection {
         get { return multipleSelection; }
         set { multipleSelection = value; }
      }

      private FlowsheetActivity activity;
      public FlowsheetActivity Activity {
         get { return activity; }
      }

      private Type solvableTypeBeingAdded;
      public Type SolvableTypeBeingAdded {
         get { return solvableTypeBeingAdded; }
      }

      private ProsimoUI.CustomEditor.CustomEditor customEditor;
      public ProsimoUI.CustomEditor.CustomEditor CustomEditor {
         get { return customEditor; }
      }

      private ProsimoUI.CustomEditor.CustomEditorForm customEditorForm;
      internal ProsimoUI.CustomEditor.CustomEditorForm CustomEditorForm {
         get { return customEditorForm; }
      }

      private ConnectionManager connectionManager;
      public ConnectionManager ConnectionManager {
         get { return connectionManager; }
      }

      private StreamManager streamManager;
      public StreamManager StreamManager {
         get { return streamManager; }
      }

      private UnitOpManager unitOpManager;
      public UnitOpManager UnitOpManager {
         get { return unitOpManager; }
      }

      private SystemEditor editor;
      internal SystemEditor Editor {
         get { return editor; }
      }

      private System.Windows.Forms.SaveFileDialog saveFileDialog;
      private System.Windows.Forms.ContextMenu contextMenu;
      private System.Windows.Forms.MenuItem menuItemPopupSelect;
      private System.Windows.Forms.MenuItem menuItemPopupFind;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public Flowsheet() {
         InitializeComponent();
      }

      public Flowsheet(FlowsheetSettings newFlowsheetSettings, FlowsheetPreferences flowsheetPrefs, ApplicationPreferences appPrefs) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.flowsheetPrefs = flowsheetPrefs;
         this.appPrefs = appPrefs;

         DryingGas dg = DryingGasCatalog.Instance.GetDryingGas(newFlowsheetSettings.DryingGasName);
         if (dg == null) {
            //string message = "You need to set a drying gas in Materials / New Process Settings first!";
            //MessageBox.Show(message, "New Flowsheet Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            dg = DryingGasCatalog.Instance.GetDryingGas("Air");
         }

         DryingMaterial dm = DryingMaterialCatalog.Instance.GetDryingMaterial(newFlowsheetSettings.DryingMaterialName);
         if (dm == null) {
            string message = "You need to choose a drying material for the new flowsheet to be created first! (go to Materials / Set Default Flowsheet Settings)";
            MessageBox.Show(message, "New Flowsheet Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }

         this.evapAndDryingSystem = new EvaporationAndDryingSystem(UI.NEW_SYSTEM, dm, dg);

         this.connectionManager = new ConnectionManager(this);
         this.customEditor = new ProsimoUI.CustomEditor.CustomEditor(this);

         Init();
      }

      public Flowsheet(EvaporationAndDryingSystem system, ApplicationPreferences appPrefs) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.evapAndDryingSystem = system;
         this.appPrefs = appPrefs;

         this.connectionManager = new ConnectionManager(this);
         this.customEditor = new ProsimoUI.CustomEditor.CustomEditor(this);

         Init();
      }

      private void Init() {
         this.MultipleSelection = false;
         SetFlowsheetActivity(FlowsheetActivity.Default);

         this.evapAndDryingSystem.SystemChanged += new SystemChangedEventHandler(evapAndDryingSystem_SystemChanged);
         this.evapAndDryingSystem.CalculationStarted += new CalculationStartedEventHandler(evapAndDryingSystem_CalculationStarted);
         this.evapAndDryingSystem.CalculationEnded += new CalculationEndedEventHandler(evapAndDryingSystem_CalculationEnded);
         this.evapAndDryingSystem.NameChanged += new NameChangedEventHandler(evapAndDryingSystem_NameChanged);

         this.streamManager = new StreamManager(this);
         this.unitOpManager = new UnitOpManager(this);
         this.BackColor = Color.White;
         this.Version = new FlowsheetVersion();

         this.IsDirty = false;

         //InitializeComponent();
      }

      void evapAndDryingSystem_NameChanged(object sender, string name, string oldName) {
         this.Parent.Text = ((MainForm)this.Parent).GetMainFormText(name, evapAndDryingSystem);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.evapAndDryingSystem != null) {
            this.evapAndDryingSystem.SystemChanged -= new SystemChangedEventHandler(evapAndDryingSystem_SystemChanged);
            this.evapAndDryingSystem.CalculationStarted -= new CalculationStartedEventHandler(evapAndDryingSystem_CalculationStarted);
            this.evapAndDryingSystem.CalculationEnded -= new CalculationEndedEventHandler(evapAndDryingSystem_CalculationEnded);
            this.evapAndDryingSystem.NameChanged -= new NameChangedEventHandler(evapAndDryingSystem_NameChanged);
         }

         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
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
         this.AutoScroll = true;
         this.BackColor = System.Drawing.Color.White;
         this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.ContextMenu = this.contextMenu;
         this.Enter += new System.EventHandler(this.Flowsheet_Enter);
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseDown);
         this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseMove);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Flowsheet_KeyUp);
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Flowsheet_MouseUp);
         this.SizeChanged += new System.EventHandler(this.Flowsheet_SizeChanged);
         this.DragLeave += new System.EventHandler(this.Flowsheet_DragLeave);
         this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Flowsheet_KeyDown);
         this.ResumeLayout();
      }

      #endregion

      void Flowsheet_Enter(object sender, EventArgs e) {
         this.ConnectionManager.DrawConnections();
      }

      void Flowsheet_SizeChanged(object sender, EventArgs e) {
         this.ConnectionManager.DrawConnections();
      }

      void Flowsheet_DragLeave(object sender, EventArgs e) {
         this.ConnectionManager.DrawConnections();
      }

      private void OnActivityChanged(object sender, ActivityChangedEventArgs eventArgs) {
         if (ActivityChanged != null) {
            ActivityChanged(sender, eventArgs);
         }
      }

      private void OnSnapshotTaken(Bitmap image) {
         if (SnapshotTaken != null) {
            SnapshotTaken(image);
         }
      }

      private void OnSaveFlowsheet(Flowsheet flowsheet) {
         if (SaveFlowsheet != null) {
            SaveFlowsheet(flowsheet);
         }
      }

      public void PrepareForTheMoveWithoutSelection(SolvableControl feCtrl, int x, int y) {
         if (this.Controls.Count > 0) {
            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) {
               if (e.Current is SolvableControl) {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected && !ctrl.Equals(feCtrl)) {
                     ctrl.PrepareForTheMoveWithoutSelection(x, y);
                  }
               }
            }
         }
      }

      public void ChangeLocation(SolvableControl feCtrl, Point p) {
         if (this.Controls.Count > 0) {
            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) {
               if (e.Current is SolvableControl) {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected && !ctrl.Equals(feCtrl)) {
                     ctrl.ChangeLocation(p);
                  }
               }
            }
         }
      }

      private void SetDirtyCaptionFormat() {
         if (this.isDirty) {
            if (!this.Parent.Text.EndsWith(UI.DIRTY))
               this.Parent.Text = this.Parent.Text + UI.DIRTY;
         }
         else {
            if (this.Parent.Text.EndsWith(UI.DIRTY))
               this.Parent.Text = this.Parent.Text.Substring(0, this.Parent.Text.Length - 1);
         }
      }

      public void MakeNondirtyAll() {
         this.IsDirty = false;
         IEnumerator e = this.Controls.GetEnumerator();
         while (e.MoveNext()) {
            if (e.Current is SolvableControl)
               ((SolvableControl)e.Current).IsDirty = false;
         }
      }

      public void AddSolvable(Type type) {
         this.solvableTypeBeingAdded = type;
         SetFlowsheetActivity(FlowsheetActivity.AddingSolvable);
      }

      public void SetFlowsheetActivity(FlowsheetActivity flowsheetActivity) {
         this.activity = flowsheetActivity;
         if (activity == FlowsheetActivity.Default) {
            this.Cursor = Cursors.Default;
         }
         else if (activity == FlowsheetActivity.AddingSolvable) {
            this.Cursor = Cursors.Cross;
            this.SetSolvableControlsSelection(false);
         }
         else if (activity == FlowsheetActivity.AddingConnStepOne ||
                  activity == FlowsheetActivity.AddingConnStepTwo ||
                  activity == FlowsheetActivity.DeletingConnection) {
            this.Cursor = Cursors.No;
            this.SetSolvableControlsSelection(false);
         }
         else if (activity == FlowsheetActivity.SelectingSnapshot) {
            this.Cursor = Cursors.Default;
            this.SetSolvableControlsSelection(false);
         }
         this.OnActivityChanged(this, new ActivityChangedEventArgs(activity, solvableTypeBeingAdded));
      }

      public void DeleteSelectedSolvables() {
         if (this.Controls.Count > 0) {
            ArrayList toDeleteControls = new ArrayList();

            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) {
               if (e.Current is SolvableControl) {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected) {
                     toDeleteControls.Add(ctrl);
                  }
               }
            }

            if (toDeleteControls.Count > 0) {
               string message = "Are you sure that you want to delete the selected items?";
               DialogResult dr = MessageBox.Show(this, message, "Delete: " + this.Text,
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

               if (dr == System.Windows.Forms.DialogResult.Yes) {
                  foreach (SolvableControl ctrl in toDeleteControls) {
                     // delete from the model, the UI will be updated in the event listener
                     if (ctrl is UnitOpControl) {
                        UnitOperation unitOp = ((UnitOpControl)ctrl).UnitOperation;
                        this.evapAndDryingSystem.DeleteUnitOperation(unitOp);
                     }
                     else if (ctrl is ProcessStreamBaseControl) {
                        ProcessStreamBase processStream = ((ProcessStreamBaseControl)ctrl).ProcessStreamBase;
                        this.evapAndDryingSystem.DeleteStream(processStream);
                     }
                  }
               }
            }
         }
      }

      private void EditSelectedControl() {
         if (this.Controls.Count > 0) {
            ArrayList toEditControls = new ArrayList();

            IEnumerator e = this.Controls.GetEnumerator();
            while (e.MoveNext()) {
               if (e.Current is SolvableControl) {
                  SolvableControl ctrl = (SolvableControl)e.Current;
                  if (ctrl.IsSelected) {
                     toEditControls.Add(ctrl);
                  }
               }
            }

            if (toEditControls.Count < 1) {
               string message = "Please select an item.";
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (toEditControls.Count > 1) {
               string message = "Please select only one item.";
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
               ((IEditable)toEditControls[0]).Edit();
            }
         }
      }

      public void AddConnection() {
         SetFlowsheetActivity(FlowsheetActivity.AddingConnStepOne);
         this.firstStepCtrl = null;
         this.attachIndex = -1;
      }

      public void ResetActivity() {
         SetFlowsheetActivity(FlowsheetActivity.Default);
         this.firstStepCtrl = null;
         this.attachIndex = -1;
      }

      public void SetSolvableControlsSelection(bool isSelected) {
         // clear selection of flowsheet elements
         IEnumerator e2 = this.Controls.GetEnumerator();
         while (e2.MoveNext()) {
            if (e2.Current is SolvableControl) {
               ((SolvableControl)e2.Current).IsSelected = isSelected;
            }
         }
      }

      public void DeleteSolvable(SolvableControl ctrl) {
         if (ctrl != null) {
            if (ctrl is UnitOpControl) {
               UnitOperation unitOp = ((UnitOpControl)ctrl).UnitOperation;
               this.evapAndDryingSystem.DeleteUnitOperation(unitOp);
            }
            else if (ctrl is ProcessStreamBaseControl) {
               ProcessStreamBase processStream = ((ProcessStreamBaseControl)ctrl).ProcessStreamBase;
               this.evapAndDryingSystem.DeleteStream(processStream);
            }
         }
      }

      public void RemoveSolvableControl(SolvableControl ctrl) {
         // remove dependents
         if (ctrl.Editor != null) {
            ctrl.Editor.Close();
            ctrl.Editor = null;
         }

         // remove the connections of this control
         this.ConnectionManager.RemoveConnections(ctrl.Solvable.Name);

         this.Controls.Remove(ctrl);
         ctrl.Dispose();
      }

      private void Flowsheet_KeyDown(object sender, KeyEventArgs e) {
         if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            this.MultipleSelection = true;
         else if (e.KeyCode == Keys.Delete)
            DeleteSelectedSolvables();
      }

      private void Flowsheet_KeyUp(object sender, KeyEventArgs e) {
         if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            this.MultipleSelection = false;
      }

      private void menuItemPopupSystem_Click(object sender, System.EventArgs e) {
         this.EditSystem();
      }

      private void menuItemPopupSelect_Click(object sender, System.EventArgs e) {
         this.SetSolvableControlsSelection(true);
      }

      private void menuItemPopupFind_Click(object sender, System.EventArgs e) {
         this.ShowFindForm();
      }

      public void CreateHumidityChart() {
         PsychrometricChartModel psychrometricChartModel = this.EvaporationAndDryingSystem.GetPsychrometricChartModel();
         HumidityChartForm humidityChartForm = new HumidityChartForm(this, psychrometricChartModel);
         humidityChartForm.ShowDialog();
      }

      public void ShowFindForm() {
         if (this.findForm == null) {
            this.findForm = new FindForm(this);
            Form mainForm = (Form)this.Parent;
            this.findForm.Owner = mainForm;
            this.findForm.Show();
         }
         else {
            if (this.findForm.WindowState.Equals(FormWindowState.Minimized))
               this.findForm.WindowState = FormWindowState.Normal;
            this.findForm.Activate();
         }
      }

      public void EditSystem() {
         if (this.editor == null) {
            this.editor = new SystemEditor(this);
            if (flowsheetPrefs != null) {
               flowsheetPrefs.RestoreGlobalEditorPrefs(this.editor);
            }
            // we need to subscribe to Dispose() and not to Close()
            // see SystemEditor.menuItemClose_Click() for details
            this.editor.Disposed += new EventHandler(editor_Disposed);
            this.editor.Owner = (Form)this.Parent;
            this.editor.Show();
         }
         else {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            //this.editor.Activate();
            this.editor.Visible = true;
         }
      }

      public void ShowCustomEditor() {
         if (customEditorForm == null) {
            this.customEditorForm = new CustomEditorForm(this, this.customEditor);
            if (flowsheetPrefs != null) {
               flowsheetPrefs.RestoreCustomEditorPrefs(this.customEditorForm);
            }
            customEditorForm.Owner = this.Parent as Form;
            customEditorForm.Show();
         }
         else {
            this.customEditorForm.Visible = true;
         }
      }

      public void ShowFormulaEditor() {
         FormulaEditorForm formulaEditorForm = new FormulaEditorForm(this, this.EvaporationAndDryingSystem);
         formulaEditorForm.ShowDialog();
      }

      public void CutConnection() {
         SetFlowsheetActivity(FlowsheetActivity.DeletingConnection);
      }

      public void SelectSnapshot() {
         SetFlowsheetActivity(FlowsheetActivity.SelectingSnapshot);
      }

      private void Flowsheet_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
         Point p = new Point(e.X, e.Y);
         if (e.Button == System.Windows.Forms.MouseButtons.None) {
            if (this.activity == FlowsheetActivity.Default)
               this.Cursor = Cursors.Default;
            else if (this.Activity == FlowsheetActivity.DeletingConnection)
               this.ChangeCursorOverConnection(p);
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Left && this.Activity == FlowsheetActivity.SelectingSnapshot ||
            e.Button == System.Windows.Forms.MouseButtons.Left && this.Activity == FlowsheetActivity.Default) {
            this.snapX = e.X;
            this.snapY = e.Y;
            this.ConnectionManager.DrawConnections(); // this clears first the flowsheet
            //this.Refresh();
            this.DrawSelection();
            this.Select();//Has to call select here. Otherwise focus is not on this flowsheet control some times.
         }
      }

      private void DrawSelection() {
         if ((this.Activity == FlowsheetActivity.SelectingSnapshot ||
            this.Activity == FlowsheetActivity.Default)
            /*&& this.X != -1*/) { //.Net 2.0 triggers some unnecessary event when double click on an being openned file
                               //Have to add this X != -1 condition to prevent from drawing selection 
            Pen p = new Pen(Color.Black, 1.0f);
            p.DashStyle = DashStyle.Dot;
            Graphics g = this.CreateGraphics();
            Rectangle r = new Rectangle(this.X, this.Y, this.snapX - this.X, this.snapY - this.Y);
            g.DrawRectangle(p, r);

            p.Dispose();
            g.Dispose();
         }
      }

      private void ChangeCursorOverConnection(Point p) {
         this.Cursor = Cursors.No;
         if (this.connectionManager.Connections.Count > 0) {
            IEnumerator e = this.connectionManager.Connections.GetEnumerator();
            while (e.MoveNext()) {
               SolvableConnection conn = (SolvableConnection)e.Current;
               if (conn.HitTest(p)) {
                  this.Cursor = Cursors.Cross;
                  break;
               }
            }
         }
      }

      public bool Close() {
         bool cancel = false;
         if (this.isDirty) {
            string message = "Do you want to save the current changes?";
            DialogResult dr2 = MessageBox.Show(this, message, "Save Changes: " + this.Text,
               MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (dr2) {
               case System.Windows.Forms.DialogResult.Yes:

                  this.OnSaveFlowsheet(this);
                  break;
               case System.Windows.Forms.DialogResult.No:
                  break;
               case System.Windows.Forms.DialogResult.Cancel:
                  cancel = true;
                  break;
            }
            if (!cancel) {
               this.CloseDefinitively();
            }
         }
         else {
            this.CloseDefinitively();
         }
         bool close = !cancel;
         return close;
      }

      private void CloseDefinitively() {
         this.CloseDependantsOfSolvables();
         this.CloseDependantsOfFlowsheet();
         SetFlowsheetActivity(FlowsheetActivity.Default);
      }

      private void CloseDependantsOfFlowsheet() {
         if (this.editor != null) {
            this.editor.Close();
            this.editor.Dispose();
            this.editor = null;
         }

         if (this.customEditorForm != null) {
            this.customEditorForm.Close();
            this.customEditorForm.Dispose();
            this.customEditorForm = null;
         }
      }

      private void CloseDependantsOfSolvables() {
         IEnumerator e = this.Controls.GetEnumerator();
         while (e.MoveNext()) {
            if (e.Current is SolvableControl) {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.Editor != null) {
                  ctrl.Editor.Close();
                  ctrl.Editor = null;
               }
            }
         }
      }

      private void Flowsheet_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
         if (e.Button == System.Windows.Forms.MouseButtons.Left) {
            // first remember the position
            this.X = e.X;
            this.Y = e.Y;
            this.snapX = e.X;
            this.snapY = e.Y;

            if (this.Activity == FlowsheetActivity.DeletingConnection) {
               //this.connectionManager.DeleteConnection(new Point(e.X, e.Y));
               //this.ResetActivity();
            }
            else if (this.Activity == FlowsheetActivity.SelectingSnapshot) {
               // do not reset activity here
            }
            else if (this.Activity == FlowsheetActivity.Default) {
               //               this.Invalidate();
               this.ConnectionManager.DrawConnections();
               this.SetSolvableControlsSelection(false);
               this.ResetActivity();
            }
            else if (this.Activity == FlowsheetActivity.AddingSolvable) {
               evapAndDryingSystem.CreateSolvable(solvableTypeBeingAdded);
               this.ResetActivity();
            }
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right) {
            this.SetSolvableControlsSelection(false);
            this.ResetActivity();
         }
      }

      private void Flowsheet_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
         if (e.Button == System.Windows.Forms.MouseButtons.Left) {
            int endX = e.X;
            int endY = e.Y;

            if (this.Activity == FlowsheetActivity.SelectingSnapshot) {
               this.SaveSnapshot(this.X, this.Y, endX, endY);
               this.X = 0;
               this.Y = 0;
               this.snapX = 0;
               this.snapY = 0;
            }
            else if (this.Activity == FlowsheetActivity.Default
               && this.X != -1) { //.Net 2.0 triggers some unnecessary event when double click on an being openned file
                                  //Have to add this X != -1 condition to prevent from drawing selection 
               this.ConnectionManager.DrawConnections(); // this clears first the flowsheet
               this.SelectElements(this.X, this.Y, endX, endY);
               this.X = 0;
               this.Y = 0;
               this.snapX = 0;
               this.snapY = 0;
            }
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right) {
            this.SetSolvableControlsSelection(false);
            this.ResetActivity();
         }
      }

      private void SelectElements(int initialX, int initialY, int finalX, int finalY) {
         if (finalX > initialX && finalY > initialY) {
            IEnumerator<ProcessStreamBaseControl> en = this.StreamManager.GetStreamControls<ProcessStreamBaseControl>().GetEnumerator();
            while (en.MoveNext()) {
               ProcessStreamBaseControl ctrl = en.Current;
               if (ctrl.Location.X > initialX && ctrl.Location.X < finalX &&
                  ctrl.Location.Y > initialY && ctrl.Location.Y < finalY)
                  ctrl.IsSelected = true;
            }
            IEnumerator<UnitOpControl> en2 = this.UnitOpManager.GetUnitOpControls<UnitOpControl>().GetEnumerator();
            while (en2.MoveNext()) {
               UnitOpControl ctrl = en2.Current;
               if (ctrl.Location.X > initialX && ctrl.Location.X < finalX &&
                  ctrl.Location.Y > initialY && ctrl.Location.Y < finalY)
                  ctrl.IsSelected = true;
            }
         }
      }

      private void SaveSnapshot(int x1, int y1, int x2, int y2) {
         if (x2 > x1 && y2 > y1) {
            this.CaptureImage(x1, y1, x2, y2);
            this.OnSnapshotTaken(this.Image);
         }
      }

      public void CaptureImage(int x1, int y1, int x2, int y2) {
         // x1, y1 = starting point
         // x2, y2 = ending point
         int w = x2 - x1;
         int h = y2 - y1;
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
      public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

      private void evapAndDryingSystem_CalculationStarted(object sender) {
         this.SetCursorOnUI(Cursors.WaitCursor);
      }

      private void evapAndDryingSystem_CalculationEnded(object sender) {
         this.SetCursorOnUI(Cursors.Default);
         this.connectionManager.DrawConnections();
      }

      private void SetCursorOnUI(Cursor cursor) {
         this.Cursor = cursor;
         //IEnumerator<ProcessStreamBaseControl> e = this.streamManager.GetStreamControls<ProcessStreamBaseControl>().GetEnumerator();
         //while (e.MoveNext()) {
         //   ProcessStreamBaseControl ctrl = e.Current;
         IList<ProcessStreamBaseControl> scList = this.streamManager.GetStreamControls<ProcessStreamBaseControl>();
         foreach(ProcessStreamBaseControl ctrl in scList) {
            ctrl.Cursor = cursor;
            if (ctrl.Editor != null)
               ctrl.Editor.Cursor = cursor;
         }
         //IEnumerator<UnitOpControl> e2 = this.unitOpManager.GetUnitOpControls<UnitOpControl>().GetEnumerator();
         //while (e2.MoveNext()) {
         //   UnitOpControl ctrl = e2.Current;
         IList<UnitOpControl> ucList = this.unitOpManager.GetUnitOpControls<UnitOpControl>();
         foreach (UnitOpControl ctrl in ucList) {
            ctrl.Cursor = cursor;
            if (ctrl.Editor != null)
               ctrl.Editor.Cursor = cursor;
         }
      }

      private void evapAndDryingSystem_SystemChanged(object sender) {
         this.IsDirty = true;
         this.connectionManager.DrawConnections();
      }

      private void editor_Disposed(object sender, EventArgs e) {
         this.editor.Disposed -= new EventHandler(editor_Disposed);
         this.editor = null;
      }

      protected Flowsheet(SerializationInfo info, StreamingContext context) {
         this.info = info;
         this.context = context;

         //InitializeComponent();
         //SetObjectData();
      }

      public virtual void SetObjectData() {
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionFlowsheet");
         this.evapAndDryingSystem = (EvaporationAndDryingSystem) Storable.RecallStorableObject(info, context, "EvaporationAndDryingSystem", typeof(EvaporationAndDryingSystem));
         //this.appPrefs = (ApplicationPreferences)Storable.RecallStorableObject(info, context, "ApplicationPreferences", typeof(ApplicationPreferences));
         this.customEditor = (ProsimoUI.CustomEditor.CustomEditor)Storable.RecallStorableObject(info, context, "CustomEditor", typeof(ProsimoUI.CustomEditor.CustomEditor));
         //this.connectionManager = (ConnectionManager)Storable.RecallStorableObject(info, context, "ConnectionManager", typeof(ConnectionManager));
         ArrayList connections = info.GetValue("Connections", typeof(ArrayList)) as ArrayList;
         if (connections != null) {
            foreach (SolvableConnection sc in connections) {
               sc.SetObjectData();
            }
         }
         this.flowsheetPrefs = (FlowsheetPreferences)Storable.RecallStorableObject(info, context, "FlowsheetPrefs", typeof(FlowsheetPreferences));

         ArrayList persistedControls = info.GetValue("PersistedControls", typeof(ArrayList)) as ArrayList;
         RestoreSolvableControls(persistedControls, connections);
         InitializeComponent();
         Init();

         if (persistedClassVersion <= 1) {
            RecallInitialization();
         }
      }

      private void RecallInitialization() {
         ArrayList scrubberCondenserList = this.evapAndDryingSystem.GetSolvableList(typeof(ScrubberCondenser));
         foreach (ScrubberCondenser sc in scrubberCondenserList) {
            ProcessStreamBase gasOutlet = sc.GasOutlet;
            if (gasOutlet != null) {
               connectionManager.DetachStreamFromUnitOp(sc, gasOutlet);
            }
            ProcessStreamBase liquidOutlet = sc.LiquidOutlet;
            if (liquidOutlet != null) {
               connectionManager.DetachStreamFromUnitOp(sc, liquidOutlet);
            }
            sc.AttachStream(gasOutlet, ScrubberCondenser.GAS_OUTLET_INDEX);
            sc.AttachStream(liquidOutlet, ScrubberCondenser.LIQUID_OUTLET_INDEX);
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionFlowsheet", Flowsheet.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("EvaporationAndDryingSystem", this.evapAndDryingSystem, typeof(EvaporationAndDryingSystem));
         //info.AddValue("ApplicationPreferences", this.appPrefs, typeof(ApplicationPreferences));
         info.AddValue("CustomEditor", this.customEditor, typeof(ProsimoUI.CustomEditor.CustomEditor));
         //info.AddValue("ConnectionManager", this.connectionManager, typeof(ConnectionManager));
         info.AddValue("Connections", this.connectionManager.Connections, typeof(ArrayList));
         info.AddValue("FlowsheetPrefs", new FlowsheetPreferences(this), typeof(FlowsheetPreferences));

         ArrayList persistedControls = GetSolvableControls();
         info.AddValue("PersistedControls", persistedControls, typeof(ArrayList));
      }

      private ArrayList GetSolvableControls() {
         ArrayList solvableControls = new ArrayList();
         foreach (Object obj in Controls) {
            if (obj is SolvableControl) {
               solvableControls.Add(obj);
            }
         }
         return solvableControls;
      }

      private void RestoreSolvableControls(ArrayList persistedControls, ArrayList connections) {
      //private void RestoreSolvableControls(ArrayList persistedControls) {
         this.connectionManager = new ConnectionManager(this);
         foreach (SolvableControl sc in persistedControls) {
            sc.SetObjectData();
            Controls.Add(sc);
         }

         foreach (SolvableConnection sc in connections) {
            sc.UpdateConnection();
            this.connectionManager.AddConnection(sc);
         }
      }
   }
}

//switch (dr) {
//   case System.Windows.Forms.DialogResult.Yes:
//      //IEnumerator e2 = toDeleteControls.GetEnumerator();
//      //while (e2.MoveNext()) {
//      //SolvableControl ctrl = (SolvableControl)e2.Current;
//      foreach (SolvableControl ctrl in toDeleteControls) {
//         // delete from the model, the UI will be updated in the event listener
//         if (ctrl is UnitOpControl) {
//            UnitOperation unitOp = ((UnitOpControl)ctrl).UnitOperation;
//            this.evapAndDryingSystem.DeleteUnitOperation(unitOp);
//         }
//         else if (ctrl is ProcessStreamBaseControl) {
//            ProcessStreamBase processStream = ((ProcessStreamBaseControl)ctrl).ProcessStreamBase;
//            this.evapAndDryingSystem.DeleteStream(processStream);
//         }
//      }
//      break;
//   case System.Windows.Forms.DialogResult.No:
//      break;
//}
//private NewProcessSettings newProcessSettings;
//public NewProcessSettings NewProcessSettings {
//   get { return newProcessSettings; }
//   set { newProcessSettings = value; }
//}

//public Flowsheet(NewProcessSettings newProcessSettings, ApplicationPreferences appPrefs, EvaporationAndDryingSystem system) {
//   //
//   // Required for Windows Form Designer support
//   //
//   InitializeComponent();

//   //this.newProcessSettings = newProcessSettings;
//   //this.appPrefs = appPrefs;

//   if (system == null) {
//      DryingGas dg = DryingGasCatalog.GetInstance().GetDryingGas(newProcessSettings.DryingGasName);
//      if (dg == null) {
//         //               string message = "You need to set a drying gas in Materials / New Process Settings first!";
//         //               MessageBox.Show(message, "New Flowsheet Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//         dg = DryingGasCatalog.GetInstance().GetDryingGas(Substance.AIR);
//      }

//      DryingMaterial dm = DryingMaterialCatalog.GetInstance().GetDryingMaterial(newProcessSettings.DryingMaterialName);
//      if (dm == null) {
//         string message = "You need to choose a drying material for the new flowsheet to be created first! (go to Materials / New Flowsheet Settings)";
//         MessageBox.Show(message, "New Flowsheet Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//      }

//      this.evapAndDryingSystem = new EvaporationAndDryingSystem(UI.NEW_SYSTEM, dm, dg);
//   }
//   else {
//      this.evapAndDryingSystem = system;
//   }

//   this.connectionManager = new ConnectionManager(this);
//   this.customEditor = new ProsimoUI.CustomEditor.CustomEditor(this);

//   Init();
//}

//using Crownwood.Magic.Common;
//using Crownwood.Magic.Controls;
//using Crownwood.Magic.Docking;
//using Crownwood.Magic.Menus;

//public event ToolboxAliveChangedEventHandler ToolboxAliveChanged;
//public event ToolboxLocationChangedEventHandler ToolboxLocationChanged;
//public event ToolboxVisibleChangedEventHandler ToolboxVisibleChanged;

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

//private void OnToolboxAliveChanged(bool alive) {
//   if (ToolboxAliveChanged != null) {
//      ToolboxAliveChanged(alive);
//   }
//}

//private void OnToolboxVisibleChanged(bool visible) {
//   if (ToolboxVisibleChanged != null) {
//      ToolboxVisibleChanged(visible);
//   }
//}

//private void OnToolboxLocationChanged(Point location) {
//   if (ToolboxLocationChanged != null) {
//      ToolboxLocationChanged(location);
//   }
//}

//public void ShowToolbox(Point location) {
//   MainForm mainForm = (MainForm)this.Parent;
//   if (this.toolbox == null) {
//      //this.Toolbox = new Toolbox(this);
//      this.toolbox = new Toolbox(this);
//      //MainForm mainForm = (MainForm)this.Parent;
//      this.toolbox.Owner = mainForm;
//      // Create three Content objects, one of each type
//      cA = mainForm.DockingManager.Contents.Add(this.toolbox, this.toolbox.Text);
//      mainForm.DockingManager.ContentHidden += new DockingManager.ContentHandler(this.toolbox.Toolbox_FormClosed);
//      // Define the initial floating form size
//      cA.FloatingSize = new Size(78, 427);
//      //if (location != null)
//      //   cA.DisplayLocation = location;
//      //cA.FloatingSize = this.toolbox.toolBoxPanel.Size;
//      // cA.DisplaySize = this.toolbox.toolBoxPanel.Size;
//      //cA.CloseOnHide = true;
//      // Request a new Docking window be created for the first content on the bottom edge
//      WindowContent wc = mainForm.DockingManager.AddContentWithState(cA, State.Floating) as WindowContent;
//      // Make the content with title 'Notepad'
//      // visible
//      mainForm.DockingManager.ShowContent(cA);
//      this.ResetActivity(); // this call is to uncheck the misterious liquid dryer radio buton selection
//   }
//   else {
//      //if (this.toolbox.WindowState.Equals(FormWindowState.Minimized))
//      //   this.toolbox.WindowState = FormWindowState.Normal;
//      //this.toolbox.Activate();
//      mainForm.DockingManager.ShowContent(cA);
//   }
//   //this.OnToolboxVisibleChanged(true);
//   this.OnToolboxAliveChanged(true);
//}

//public void HideToolbox() {
//   MainForm mainForm = (MainForm)this.Parent;
//   if (mainForm != null) {
//      mainForm.DockingManager.HideContent(cA);
//      this.OnToolboxAliveChanged(false);
//   }
//}
//private Toolbox toolbox;
//public Toolbox Toolbox {
//   get { return toolbox; }
//   set {
//      toolbox = value;
//      this.OnToolboxAliveChanged(value != null);
//   }
//}

//public Point ToolboxLocation {
//   set {
//      this.OnToolboxLocationChanged(value);
//   }
//}