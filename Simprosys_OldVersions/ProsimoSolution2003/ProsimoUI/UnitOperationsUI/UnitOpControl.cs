using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;
using ProsimoUI;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.UnitOperationsUI
{
	/// <summary>
	/// Summary description for UnitOpControl.
	/// </summary>
   [Serializable]
   public class UnitOpControl : SolvableControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private System.Windows.Forms.ContextMenu contextMenu;
      public System.Windows.Forms.MenuItem menuItemEdit;
      private System.Windows.Forms.MenuItem menuItemDelete; 

      public UnitOperation UnitOperation
      {
         get {return (UnitOperation)this.Solvable;}
         set {this.Solvable = value;}
      }

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public UnitOpControl()
      {
      }

		public UnitOpControl(Flowsheet flowsheet, Point location, UnitOperation unitOp) :
         base(flowsheet, location, unitOp)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.UnitOperation.StreamAttached += new StreamAttachedEventHandler(UnitOperation_StreamAttached); 
         this.UnitOperation.StreamDetached += new StreamDetachedEventHandler(UnitOperation_StreamDetached);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
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
         this.contextMenu = new System.Windows.Forms.ContextMenu();
         this.menuItemEdit = new System.Windows.Forms.MenuItem();
         this.menuItemDelete = new System.Windows.Forms.MenuItem();
         // 
         // contextMenu
         // 
         this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.menuItemEdit,
                                                                                    this.menuItemDelete});
         // 
         // menuItemEdit
         // 
         this.menuItemEdit.Index = 0;
         this.menuItemEdit.Text = "Edit...";
         this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
         // 
         // menuItemDelete
         // 
         this.menuItemDelete.Index = 1;
         this.menuItemDelete.Text = "Delete";
         this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
         // 
         // UnitOpControl
         // 
         this.ContextMenu = this.contextMenu;
         this.Name = "UnitOpControl";
         this.Size = new System.Drawing.Size(150, 131);

      }
		#endregion

      protected void DrawBorder()
      {
//         int d = UI.SELECTION_SIZE;
         int d = UI.SELECTION_SIZE*2; // 6 pixels is the white border
         Graphics g = this.CreateGraphics();

         Rectangle sideN = new Rectangle(0,0,this.Width,d);
         Rectangle sideS = new Rectangle(0,this.Height-d,this.Width,d);
         Rectangle sideE = new Rectangle(this.Width-d,0,d,this.Height);
         Rectangle sideW = new Rectangle(0,0,d,this.Height);
         
         SolidBrush brush = null;
         UI ui = new UI();
         brush = new SolidBrush(ui.FLOWSHEET_COLOR);

         g.FillRectangle(brush, sideN);
         g.FillRectangle(brush, sideS);
         g.FillRectangle(brush, sideE);
         g.FillRectangle(brush, sideW);
      }

      protected virtual void DrawSlot(Control ctrl, Point p, SlotPosition position, SlotDirection direction)
      {
         Graphics g = ctrl.CreateGraphics();
         UI ui = new UI();
         SolidBrush brush = new SolidBrush(ui.SLOT_COLOR);
         Pen pen = new Pen(brush, 0.5f);

         Point pN = new Point(0,0);         
         Point pS = new Point(0,0);         
         Point pW = new Point(0,0);         
         Point pE = new Point(0,0);         
         Point[] points = new Point[3];
        
         if (position.Equals(SlotPosition.Up))
         {
            if (direction == SlotDirection.In)
            {
               pN = p;
               pS = new Point(p.X, p.Y + 5);
               pW = new Point(p.X - 3, p.Y + 2);
               pE = new Point(p.X + 3, p.Y + 2);
            
               points[0] = pW;
               points[1] = pE;
               points[2] = pS;
               g.DrawLine(pen, pN, pS);
            }
            else if (direction == SlotDirection.Out)
            {
               pN = new Point(p.X, p.Y+1);
               pS = new Point(p.X, p.Y + 5);
               pW = new Point(p.X - 4, p.Y + 4);
               pE = new Point(p.X + 4, p.Y + 4);
            
               points[0] = pW;
               points[1] = pE;
               points[2] = pN;
               g.DrawLine(pen, pN, pS);
            }
         }
         else if (position.Equals(SlotPosition.Left))
         {
            if (direction == SlotDirection.In)
            {
               pW = p;
               pS = new Point(p.X+2, p.Y + 3);
               pN = new Point(p.X +2, p.Y -3);
               pE = new Point(p.X + 5, p.Y);
            
               points[0] = pN;
               points[1] = pE;
               points[2] = pS;
               g.DrawLine(pen, pW, pE);
            }
            else if (direction == SlotDirection.Out)
            {
               pW = new Point(p.X+1, p.Y);
               pS = new Point(p.X+4, p.Y + 3);
               pN = new Point(p.X +4, p.Y -3);
               pE = new Point(p.X + 5, p.Y);
            
               points[0] = pN;
               points[1] = pS;
               points[2] = pW;
               g.DrawLine(pen, pW, pE);
            }
         }
         else if (position.Equals(SlotPosition.Right))
         {
            if (direction == SlotDirection.Out)
            {
               pE = new Point(p.X-1, p.Y);
               pS = new Point(pE.X -3, pE.Y + 3);
               pW = new Point(pE.X - 5, pE.Y);
               pN = new Point(pE.X - 3, pE.Y - 3);
            
               points[0] = pN;
               points[1] = pE;
               points[2] = pS;
               g.DrawLine(pen, pW, pE);
            }
            else if (direction == SlotDirection.In)
            {
               pE = new Point(p.X-1, p.Y);
               pS = new Point(pE.X -3, pE.Y + 3);
               pW = new Point(pE.X - 6, pE.Y);
               pN = new Point(pE.X - 3, pE.Y - 3);
            
               points[0] = pN;
               points[1] = pS;
               points[2] = pW;
               g.DrawLine(pen, pW, pE);
            }
         }
         else if (position.Equals(SlotPosition.Down))
         {
            if (direction == SlotDirection.Out)
            {
               pS = new Point(p.X, p.Y-1);
               pN = new Point(pS.X, pS.Y - 5);
               pW = new Point(pS.X - 3, pS.Y - 3);
               pE = new Point(pS.X + 3, pS.Y - 3);
            
               points[0] = pW;
               points[1] = pE;
               points[2] = pS;
               g.DrawLine(pen, pN, pS);
            }
            else if (direction == SlotDirection.In)
            {
               pS = new Point(p.X, p.Y-1);
               pN = new Point(pS.X, pS.Y - 6);
               pW = new Point(pS.X - 3, pS.Y - 2);
               pE = new Point(pS.X + 3, pS.Y - 2);
            
               points[0] = pW;
               points[1] = pE;
               points[2] = pN;
               g.DrawLine(pen, pN, pS);
            }
         }
         g.FillPolygon(brush, points);
      }

      private void menuItemEdit_Click(object sender, System.EventArgs e)
      {
         this.Edit();
      }

      private void menuItemDelete_Click(object sender, System.EventArgs e)
      {
         this.flowsheet.DeleteSolvable(this);
      }

      public virtual void CreateConnection(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
      }

      protected virtual void DeleteConnection(UnitOperation uo, ProcessStreamBase ps)
      {
         this.flowsheet.ConnectionManager.RemoveConnections(ps.Name, uo.Name);
      }

      private void UnitOperation_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.CreateConnection(uo, ps, desc);
      }

      private void UnitOperation_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.DeleteConnection(uo, ps);
      }

      protected UnitOpControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionUnitOpControl", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionUnitOpControl", UnitOpControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
