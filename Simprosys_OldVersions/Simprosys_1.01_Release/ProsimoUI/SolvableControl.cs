using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SolvableControl.
	/// </summary>
   [Serializable]
   public class SolvableControl : System.Windows.Forms.UserControl,
   IEditable, IPrintable, ISerializable
	{
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private int X;
      private int Y;

      private int xBeforeMove;
      private int yBeforeMove;
      protected ToolTip toolTip;

      private StreamingContext streamingContext;
      public StreamingContext StreamingContext
      {
         get {return streamingContext;}
         set {streamingContext = value;}
      }

      private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo
      {
         get {return serializationInfo;}
         set {serializationInfo = value;}
      }

      private Solvable solvable;
      public Solvable Solvable
      {
         get {return solvable;}
         set {solvable = value;}
      }

      protected Flowsheet flowsheet;
      public Flowsheet Flowsheet
      {
         get {return flowsheet;}
         set {flowsheet = value;}
      }

      private bool isSelected;
      public bool IsSelected
      {
         get {return isSelected;}
         set
         {
            isSelected = value;
            this.DoThePaint();
         }
      }

      private bool isDirty;
      public bool IsDirty
      {
         get {return isDirty;}
         set
         {
            isDirty = value;
            this.flowsheet.IsDirty = value;
         }
      }

      private bool isShownInEditor;
      public bool IsShownInEditor
      {
         get {return isShownInEditor;}
         set {isShownInEditor = value;}
      }

      protected TextBox nameCtrl;
      public TextBox NameControl
      {
         get {return nameCtrl;}
      }

      private Form editor;
      public Form Editor
      {
         get {return editor;}
         set {editor = value;}
      }

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public SolvableControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public SolvableControl(Flowsheet flowsheet, Point location, Solvable solvable)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.Solvable = solvable;
         this.flowsheet = flowsheet;
         this.Location = location;
         this.isDirty = false;
         this.IsSelected = false;
         this.IsShownInEditor = true;

         this.nameCtrl = new TextBox(); 
         this.nameCtrl.BackColor = new UI().NAME_CTRL_COLOR;
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
         
         this.NameControl.Text = this.Solvable.Name;
         UI.SetCtrlSize(this.NameControl);
         this.flowsheet.Controls.Add(this.NameControl);
         this.toolTip = new ToolTip();
         this.UpdateToolTipText();

         this.Solvable.NameChanged += new NameChangedEventHandler(Solvable_NameChanged);
         this.Solvable.SolveComplete += new SolveCompleteEventHandler(Solvable_SolveComplete);
         this.LocationChanged += new EventHandler(SolvableControl_LocationChanged);
         this.flowsheet.EvaporationAndDryingSystem.SystemChanged += new SystemChangedEventHandler(EvaporationAndDryingSystem_SystemChanged);
         this.DrawSelection();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.editor != null)
            this.editor.Dispose();

         if (this.Solvable != null)
         {
            this.Solvable.NameChanged -= new NameChangedEventHandler(Solvable_NameChanged);
            this.Solvable.SolveComplete -= new SolveCompleteEventHandler(Solvable_SolveComplete);
            this.flowsheet.EvaporationAndDryingSystem.SystemChanged -= new SystemChangedEventHandler(EvaporationAndDryingSystem_SystemChanged);
         }

         if( disposing )
			{
            this.NameControl.Dispose();
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.SuspendLayout();
         // 
         // SolvableControl
         // 
         this.AutoScroll = true;
         this.BackColor = System.Drawing.SystemColors.Control;
         this.Name = "SolvableControl";
         this.Size = new System.Drawing.Size(112, 88);
         this.DoubleClick += new System.EventHandler(this.SolvableControl_DoubleClick);
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SolvableControl_MouseDown);
         this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SolvableControl_MouseMove);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SolvableControl_KeyUp);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.SolvableControl_Paint);
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SolvableControl_MouseUp);
         this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SolvableControl_KeyDown);
         this.ResumeLayout(false);

      }
		#endregion

      protected virtual void UpdateNameControlLocation()
      {
         if (this.NameControl != null)
         {
            this.NameControl.Left = this.Left + (2*this.Width)/3;
            this.NameControl.Top = this.Bottom;
         }
      }

      public void PerformSelection()
      {
         if (!this.IsSelected)
         {
            if (!this.flowsheet.MultipleSelection)
            {
               // unselect all previous selected controls
               this.flowsheet.SetSolvableControlsSelection(false);
            }
            this.IsSelected = true;
         }
         else
         {
            if (this.flowsheet.MultipleSelection)
            {
               this.IsSelected = false;
            }
         }
         // NOTE: the selection does not make the control dirty.
      }

      public void PrepareForTheMove(int x, int y)
      {
         this.X = x;
         this.Y = y;
         this.flowsheet.PrepareForTheMoveWithoutSelection(this, X, Y);
      }

      public void PrepareForTheMoveWithoutSelection(int x, int y)
      {
         this.X = x;
         this.Y = y;
      }

      public void ChangeLocation(Point p)
      {
         if (X != -1 || Y != -1)
         {
//            Cursor = System.Windows.Forms.Cursors.Hand;
            this.Left = this.Left + p.X - X;
            this.Top = this.Top + p.Y - Y;
         }
      }

      protected virtual void DoThePaint()
      {
         this.DrawSelection();
      }

      protected void DrawSelection()
      {
         Graphics g = this.CreateGraphics();
         int d = UI.SELECTION_SIZE;
         UI ui = new UI();

         Rectangle rNW = new Rectangle(0,0,d,d);
         Rectangle rNE = new Rectangle(this.Width-d,0,d,d);
         Rectangle rSW = new Rectangle(0,this.Height-d,d,d);
         Rectangle rSE = new Rectangle(this.Width-d,this.Height-d,d,d);

         SolidBrush brush = null;
         Pen pen = null;
         if (this.IsSelected)
         {
            brush = new SolidBrush(ui.SELECTION_COLOR);
            pen = new Pen(ui.SELECTION_COLOR,0.5f);
         }
         else
         {
            brush = new SolidBrush(this.flowsheet.BackColor);
            pen = new Pen(this.flowsheet.BackColor, 0.5f);
         }

         g.FillRectangle(brush, rNW);
         g.FillRectangle(brush, rNE);
         g.FillRectangle(brush, rSW);
         g.FillRectangle(brush, rSE);

         g.DrawLine(pen,0,0,this.Width-1,0);
         g.DrawLine(pen,0,0,0,this.Height-1);
         g.DrawLine(pen,this.Width-1,0,this.Width-1,this.Height-1);
         g.DrawLine(pen,0,this.Height-1,this.Width-1,this.Height-1);
      }

      public override string ToString()
      {
         return this.Solvable.Name;
      }

      public virtual void Edit()
      {
      }

      public virtual string ToPrint()
      {
         return "";
      }

      public virtual string ToPrintToolTip()
      {
         return "";
      }

      protected virtual void UpdateToolTipText()
      {
         this.toolTip.SetToolTip(this, this.ToPrintToolTip());         
      }

      private void SolvableControl_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            this.flowsheet.MultipleSelection = true;
      }

      private void SolvableControl_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            this.flowsheet.MultipleSelection = false;
         else if (e.KeyCode == Keys.Delete)
         {
            this.flowsheet.DeleteSelectedSolvables();
         }
      }

      protected virtual void SolvableControl_LocationChanged(object sender, EventArgs e)
      {
         this.UpdateNameControlLocation();
      }

      private void Solvable_NameChanged(object sender, string newName, string oldName)
      {
         this.flowsheet.ConnectionManager.SetOwner(this.NameControl.Text, newName);
         this.NameControl.Text = newName;
         UI.SetCtrlSize(this.NameControl);
      }

      protected virtual void Solvable_SolveComplete(object sender, SolveState solveState)
      {
         this.UpdateBackImage();
      }
     
      protected virtual void UpdateBackImage()
      {
      }

      protected virtual void MouseDownHandler(Point p)
      {
      }

      protected virtual void ShowConnectionPoints(Point p)
      {
      }

      private void SolvableControl_DoubleClick(object sender, System.EventArgs e)
      {
         this.Edit();
      }

      private void SolvableControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.X = e.X;
         this.Y = e.Y;
         this.xBeforeMove = this.Location.X;
         this.yBeforeMove = this.Location.Y;
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            Point p = new Point(e.X, e.Y);
            this.MouseDownHandler(p);
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right)
         {
//            this.X = e.X;
//            this.Y = e.Y;
            this.flowsheet.SetSolvableControlsSelection(false);
            this.flowsheet.ResetActivity();
            this.IsSelected = true;
         }
      }

      private void SolvableControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         Point p = new Point(e.X, e.Y);
         if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            this.ChangeLocation(p);
            this.flowsheet.ChangeLocation(this, p);
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.None)
         {
//            if (this.flowsheet.Activity == FlowsheetActivity.Default)
//               this.Cursor = Cursors.Default;
//            else
               this.ShowConnectionPoints(p);
         }
      }

      private void SolvableControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if(e.Button==System.Windows.Forms.MouseButtons.Left)
         {
            if (this.Left != this.xBeforeMove || this.Top != this.yBeforeMove)
            {
               this.IsDirty = true;
            }
            //            Cursor = System.Windows.Forms.Cursors.Default;
         }
      }

      private void SolvableControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
      {
         this.DoThePaint();
      }

      private void EvaporationAndDryingSystem_SystemChanged(object sender)
      {
         this.UpdateToolTipText();
      }

      protected SolvableControl(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionSolvableControl", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.Location = (Point)info.GetValue("Location", typeof(Point));
               this.NameControl.Text = (string)info.GetValue("SolvableName", typeof(string));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionSolvableControl", SolvableControl.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Location", this.Location, typeof(Point));
         info.AddValue("SolvableName", this.NameControl.Text, typeof(string));
      }
   }
}
