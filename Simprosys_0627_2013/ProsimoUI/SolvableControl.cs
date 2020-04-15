using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitSystems;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for SolvableControl.
   /// </summary>
   [Serializable]
   public class SolvableControl : System.Windows.Forms.UserControl, IEditable, IPrintable, ISerializable {

      private const int CLASS_PERSISTENCE_VERSION = 2;

      protected StreamingContext context;
      protected SerializationInfo info;
      
      private int X;
      private int Y;

      private int xBeforeMove;
      private int yBeforeMove;
      protected ToolTip toolTip;
      protected Graphics g;
      protected static Pen penBlack20 = new Pen(Color.Black, 20);

      protected Solvable solvable;
      public Solvable Solvable {
         get { return solvable; }
         set { solvable = value; }
      }

      internal protected virtual string SolvableTypeName
      { 
         get { return ""; } 
      }

      internal string SolvableDispalyTitle
      {
         get { return SolvableTypeName + ": " + solvable.Name; }
      }

      protected Flowsheet flowsheet;
      public Flowsheet Flowsheet {
         get { return flowsheet; }
         set { flowsheet = value; }
      }

      private bool isShownInEditor = true;
      public bool IsShownInEditor {
         get { return isShownInEditor; }
         set { isShownInEditor = value; }
      }

      protected TextBox nameCtrl;
      public TextBox NameControl {
         get { return nameCtrl; }
      }

      private bool isSelected;
      public bool IsSelected {
         get { return isSelected; }
         set {
            isSelected = value;
            this.DoThePaint();
         }
      }

      private bool isDirty;
      public bool IsDirty {
         get { return isDirty; }
         set {
            isDirty = value;
            this.flowsheet.IsDirty = value;
         }
      }

      protected Form editor;
      public Form Editor {
         get { return editor; }
         set { editor = value; }
      }

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public SolvableControl() {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public SolvableControl(Flowsheet flowsheet, Point location, Solvable solvable) {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.solvable = solvable;
         this.flowsheet = flowsheet;
         this.Location = location;
         //this.isShownInEditor = true;

         InitControl();
      }

      private void InitControl() {
         this.isDirty = false;
         this.isSelected = false;

         this.nameCtrl = new TextBox();
         this.nameCtrl.BackColor = UI.NAME_CTRL_COLOR;
         this.nameCtrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.nameCtrl.ReadOnly = true;
         this.nameCtrl.WordWrap = false;
         this.nameCtrl.AutoSize = false;
         this.nameCtrl.Enabled = false;
         FontFamily ff = this.nameCtrl.Font.FontFamily;
         Font f = new Font(ff, 7);
         this.nameCtrl.Font = f;
         // the size is overriden in the extended class
         this.nameCtrl.Height = f.Height + 1;
         this.nameCtrl.Width = 60;
         this.nameCtrl.Text = this.solvable.Name;

         SetCtrlSize(this.nameCtrl);
         this.flowsheet.Controls.Add(this.nameCtrl);
         this.toolTip = new ToolTip();
         this.UpdateToolTipText();

         this.solvable.NameChanged += new NameChangedEventHandler(Solvable_NameChanged);
         this.solvable.SolveComplete += new SolveCompleteEventHandler(Solvable_SolveComplete);
         this.LocationChanged += new EventHandler(SolvableControl_LocationChanged);
         this.flowsheet.EvaporationAndDryingSystem.SystemChanged += new SystemChangedEventHandler(EvaporationAndDryingSystem_SystemChanged);
         this.DrawSelection();

         UI.SetStatusColor(this, this.solvable.SolveState);
         this.UpdateBackImage();
         g = this.CreateGraphics();
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.editor != null)
            this.editor.Dispose();

         if (this.solvable != null) {
            this.solvable.NameChanged -= new NameChangedEventHandler(Solvable_NameChanged);
            this.solvable.SolveComplete -= new SolveCompleteEventHandler(Solvable_SolveComplete);
            this.flowsheet.EvaporationAndDryingSystem.SystemChanged -= new SystemChangedEventHandler(EvaporationAndDryingSystem_SystemChanged);
         }

         if (disposing) {
            this.NameControl.Dispose();
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.SuspendLayout();
         // 
         // SolvableControl
         // 
         this.BackColor = System.Drawing.SystemColors.Control;
         this.Name = "SolvableControl";
         this.Size = new System.Drawing.Size(112, 88);
         this.DoubleClick += new System.EventHandler(this.SolvableControl_DoubleClick);
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SolvableControl_MouseDown);
         this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SolvableControl_MouseMove);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SolvableControl_KeyUp);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.SolvableControl_Paint);
         this.MouseHover += new System.EventHandler(this.SolvableControl_MouseHover);
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SolvableControl_MouseUp);
         this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SolvableControl_KeyDown);
         this.ResumeLayout(false);

      }
      #endregion

      public static void SetCtrlSize(Control ctrl) {
         Graphics g = ctrl.CreateGraphics();
         SizeF textSize = g.MeasureString(ctrl.Text, ctrl.Font);
         ctrl.Height = (int)textSize.Height + 1;
         ctrl.Width = (int)textSize.Width;

         g.Dispose();
      }

      public static string GetVariableName(ProcessVar var, UnitSystem unitSystem) {
         string name = null;
         string unit = UnitSystemService.GetInstance().GetUnitAsString(var.Type);
         if (unit != null && !unit.Trim().Equals(""))
            name = var.VarTypeName + " (" + unit + ")";
         else
            name = var.VarTypeName;
         return name;
      }

      public static string GetVariableValue(ProcessVar var, UnitSystem unitSystem, string numericFormatStr) {
         string valStr = null;
         if (var is ProcessVarDouble) {
            ProcessVarDouble varDouble = (ProcessVarDouble)var;
            double val = UnitSystemService.GetInstance().ConvertFromSIValue(var.Type, varDouble.Value);
            if (varDouble.Value == Constants.NO_VALUE)
               valStr = "";
            else
               valStr = val.ToString(numericFormatStr);
         }
         else if (var is ProcessVarInt) {
            ProcessVarInt varInt = (ProcessVarInt)var;
            if (varInt.Value == Constants.NO_VALUE_INT)
               valStr = "";
            else
               valStr = varInt.Value.ToString(UI.DECIMAL);
         }
         return valStr;
      }
      
      protected virtual void UpdateNameControlLocation() {
         if (this.NameControl != null) {
            this.NameControl.Left = this.Left + (2 * this.Width) / 3;
            this.NameControl.Top = this.Bottom;
         }
      }

      public void PerformSelection() {
         if (!this.IsSelected) {
            if (!this.flowsheet.MultipleSelection) {
               // unselect all previous selected controls
               this.flowsheet.SetSolvableControlsSelection(false);
            }
            this.IsSelected = true;
         }
         else {
            if (this.flowsheet.MultipleSelection) {
               this.IsSelected = false;
            }
         }
         // NOTE: the selection does not make the control dirty.
      }

      public void PrepareForTheMove(int x, int y) {
         this.X = x;
         this.Y = y;
         this.flowsheet.PrepareForTheMoveWithoutSelection(this, X, Y);
      }

      public void PrepareForTheMoveWithoutSelection(int x, int y) {
         this.X = x;
         this.Y = y;
      }

      public void ChangeLocation(Point p) {
         if (X != -1 || Y != -1) {
            //            Cursor = System.Windows.Forms.Cursors.Hand;
            this.Left = this.Left + p.X - X;
            this.Top = this.Top + p.Y - Y;
         }
         // check the this.Left if it's less than top left of flow sheet, set it to left of flowsheet.
         if (this.Left < this.flowsheet.Left)
            this.Left = this.flowsheet.Left;
         if (this.Top < this.flowsheet.Top)
            this.Top = this.flowsheet.Top;
      }

      protected virtual void DoThePaint() {
         this.DrawSelection();
      }

      protected void DrawSelection() {
         if (this.isSelected)
            this.BorderStyle = BorderStyle.FixedSingle;
         else
            this.BorderStyle = BorderStyle.None;

         //Graphics g = this.CreateGraphics();
         //int d = UI.SELECTION_SIZE;
         //UI ui = new UI();

         //Rectangle rNW = new Rectangle(0,0,d,d);
         //Rectangle rNE = new Rectangle(this.Width-d,0,d,d);
         //Rectangle rSW = new Rectangle(0,this.Height-d,d,d);
         //Rectangle rSE = new Rectangle(this.Width-d,this.Height-d,d,d);

         //SolidBrush brush = null;
         //Pen pen = null;
         //if (this.IsSelected)
         //{
         //   brush = new SolidBrush(ui.SELECTION_COLOR);
         //   pen = new Pen(ui.SELECTION_COLOR,0.5f);
         //}
         //else
         //{
         //   brush = new SolidBrush(this.flowsheet.BackColor);
         //   pen = new Pen(this.flowsheet.BackColor, 0.5f);
         //}

         //g.FillRectangle(brush, rNW);
         //g.FillRectangle(brush, rNE);
         //g.FillRectangle(brush, rSW);
         //g.FillRectangle(brush, rSE);

         //g.DrawLine(pen,0,0,this.Width-1,0);
         //g.DrawLine(pen,0,0,0,this.Height-1);
         //g.DrawLine(pen,this.Width-1,0,this.Width-1,this.Height-1);
         //g.DrawLine(pen,0,this.Height-1,this.Width-1,this.Height-1);
      }

      public override string ToString() {
         return this.solvable.Name;
      }

      public virtual void Edit() {
         if (this.editor == null) {
            this.editor = new SolvableEditor(this);
            this.editor.Owner = (Form)this.flowsheet.Parent;
            this.editor.Show();
         }
         else {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            this.editor.Activate();
         }
      }

      public virtual string ToPrint() {
         return ToPrintVarList();
      }

      public virtual string ToPrintToolTip() {
         return ToPrint();
      }

      protected string ToPrintVarList()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();
         sb.Append(SolvableDispalyTitle);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         foreach (ProcessVar var in solvable.VarList)
         {
            if (var.Enabled)
            {
               sb.Append(GetVariableName(var, us));
               sb.Append(" = ");
               sb.Append(GetVariableValue(var, us, nfs));
               if (var.IsSpecified)
               {
                  sb.Append(" * ");
               }
               sb.Append(UI.NEW_LINE);
            }
         }
         return sb.ToString();
      }

      protected virtual void UpdateToolTipText() {
         this.toolTip.SetToolTip(this, this.ToPrintToolTip());
      }

      private void SolvableControl_KeyDown(object sender, KeyEventArgs e) {
         if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            this.flowsheet.MultipleSelection = true;
      }

      private void SolvableControl_KeyUp(object sender, KeyEventArgs e) {
         if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            this.flowsheet.MultipleSelection = false;
         else if (e.KeyCode == Keys.Delete) {
            this.flowsheet.DeleteSelectedSolvables();
         }
      }

      protected virtual void SolvableControl_LocationChanged(object sender, EventArgs e) {
         this.UpdateNameControlLocation();
      }

      private void Solvable_NameChanged(object sender, string newName, string oldName) {
         this.flowsheet.ConnectionManager.SetOwner(this.NameControl.Text, newName);
         this.NameControl.Text = newName;
         SetCtrlSize(this.NameControl);
      }

      protected virtual void Solvable_SolveComplete(object sender, SolveState solveState) {
         this.UpdateBackImage();
      }

      protected virtual void UpdateBackImage() {
      }

      protected virtual void MouseDownHandler(Point p) {
      }

      protected virtual void ShowConnectionPoints(Point p) {
      }

      private void SolvableControl_DoubleClick(object sender, System.EventArgs e) {
         this.Edit();
      }

      private void SolvableControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
         this.X = e.X;
         this.Y = e.Y;
         this.xBeforeMove = this.Location.X;
         this.yBeforeMove = this.Location.Y;
         if (e.Button == System.Windows.Forms.MouseButtons.Left) {
            Point p = new Point(e.X, e.Y);
            this.MouseDownHandler(p);
            //PerformSelection();
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right) {
            this.flowsheet.SetSolvableControlsSelection(false);
            this.flowsheet.ResetActivity();
            this.IsSelected = true;
         }
      }

      private void SolvableControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
         Point p = new Point(e.X, e.Y);
         if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right) {
            this.ChangeLocation(p);
            this.flowsheet.ChangeLocation(this, p);
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.None) {
            //            if (this.flowsheet.Activity == FlowsheetActivity.Default)
            //               this.Cursor = Cursors.Default;
            //            else
            this.ShowConnectionPoints(p);
         }
      }

      private void SolvableControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
         if (e.Button == System.Windows.Forms.MouseButtons.Left) {
            if (this.Left != this.xBeforeMove || this.Top != this.yBeforeMove) {
               this.IsDirty = true;
               //SolvableControl_LocationChanged(sender, e);
            }
            //            Cursor = System.Windows.Forms.Cursors.Default;
         }
      }

      private void SolvableControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
         this.DoThePaint();
      }

      private void EvaporationAndDryingSystem_SystemChanged(object sender) {
         this.UpdateToolTipText();
      }

      private void SolvableControl_MouseHover(object sender, EventArgs e) {
         if ((this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) ||
              (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo))
            PerformSelection();
      }

      protected SolvableControl(SerializationInfo info, StreamingContext context) {
         this.info = info;
         this.context = context;

         InitializeComponent();
      }

      //public virtual void SetObjectData(SerializationInfo info, StreamingContext context) {
      public virtual void SetObjectData() {
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionSolvableControl");
         this.Location = (Point)info.GetValue("Location", typeof(Point));
         if (persistedClassVersion == 1) {
            string solvableName = info.GetString("SolvableName");
            solvable = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName);
         }
         else if (persistedClassVersion > 1) {
            //this.solvable = (Solvable)Storable.RecallStorableObject(info, context, "Solvable", typeof(Solvable));
            this.solvable = (Solvable)info.GetValue("Solvable", typeof(Solvable));
            this.flowsheet = (Flowsheet)info.GetValue("Flowsheet", typeof(Flowsheet));
            this.isShownInEditor = info.GetBoolean("IsShownInEditor");
         }

         InitControl();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionSolvableControl", SolvableControl.CLASS_PERSISTENCE_VERSION, typeof(int));
         //version 1
         info.AddValue("Location", this.Location, typeof(Point));
         //info.AddValue("SolvableName", this.NameControl.Text, typeof(string));
         //version 2
         info.AddValue("Solvable", this.solvable, typeof(Solvable));
         info.AddValue("Flowsheet", this.flowsheet, typeof(Flowsheet));
         info.AddValue("IsShownInEditor", this.isShownInEditor, typeof(bool));
      }
   }
}

//public StreamingContext StreamingContext {
//   get { return context; }
//   set { context = value; }
//}

//public SerializationInfo SerializationInfo {
//   get { return info; }
//   set { info = value; }
//}

//private StreamingContext streamingContext;
//public StreamingContext StreamingContext
//{
//   get {return streamingContext;}
//   set {streamingContext = value;}
//}

//private SerializationInfo serializationInfo;
//public SerializationInfo SerializationInfo
//{
//   get {return serializationInfo;}
//   set {serializationInfo = value;}
//}


