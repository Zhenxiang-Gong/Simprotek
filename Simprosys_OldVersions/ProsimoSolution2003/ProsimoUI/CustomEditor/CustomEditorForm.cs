using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI.CustomEditor
{
	/// <summary>
	/// Summary description for CustomEditorForm.
	/// </summary>
	public class CustomEditorForm : System.Windows.Forms.Form
	{
      private const int ELEMENT_HEIGHT = 20;
      private const int ELEMENT_WIDTH = 356;

      private CustomEditor editor;
      public CustomEditor Editor
      {
         get {return editor;}
      }

      private ArrayList elements;
      private int baseIndex;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.MenuItem menuItemAdd;
      private System.Windows.Forms.MenuItem menuItemDelete;
      private System.Windows.Forms.Panel panelHeader;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CustomEditorForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public CustomEditorForm(CustomEditor editor)
		{
			InitializeComponent();

         this.editor = editor;
         this.elements = new ArrayList();
         this.baseIndex = 0;

         // compose the header, assuming max 10 columns
         for (int column=0; column<10; column++)
         {
            CustomEditorHeaderControl header = new CustomEditorHeaderControl();
            header.Location = new System.Drawing.Point(ELEMENT_WIDTH*column, 0);
            this.panelHeader.Controls.Add(header);
         }

         IEnumerator en = this.editor.Variables.GetEnumerator();
         while (en.MoveNext())
         {
            ProcessVar var = (ProcessVar)en.Current;
            this.AddUIElement(var);
         }
         this.DisplayElements();

         this.editor.ProcessVarAdded += new ProcessVarAddedEventHandler(editor_ProcessVarAdded);
         this.editor.ProcessVarDeleted += new ProcessVarDeletedEventHandler(editor_ProcessVarDeleted);
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.editor != null)
         {
            this.editor.ProcessVarAdded -= new ProcessVarAddedEventHandler(editor_ProcessVarAdded);
            this.editor.ProcessVarDeleted -= new ProcessVarDeletedEventHandler(editor_ProcessVarDeleted);
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CustomEditorForm));
         this.panel = new System.Windows.Forms.Panel();
         this.panelHeader = new System.Windows.Forms.Panel();
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.menuItemAdd = new System.Windows.Forms.MenuItem();
         this.menuItemDelete = new System.Windows.Forms.MenuItem();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.AutoScroll = true;
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.panelHeader);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(360, 471);
         this.panel.TabIndex = 0;
         this.panel.Click += new System.EventHandler(this.panel_Click);
         this.panel.SizeChanged += new System.EventHandler(this.panel_SizeChanged);
         // 
         // panelHeader
         // 
         this.panelHeader.Location = new System.Drawing.Point(0, 0);
         this.panelHeader.Name = "panelHeader";
         this.panelHeader.Size = new System.Drawing.Size(356, 20);
         this.panelHeader.TabIndex = 0;
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose,
                                                                                 this.menuItemAdd,
                                                                                 this.menuItemDelete});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // menuItemAdd
         // 
         this.menuItemAdd.Index = 1;
         this.menuItemAdd.Text = "Add...";
         this.menuItemAdd.Click += new System.EventHandler(this.menuItemAdd_Click);
         // 
         // menuItemDelete
         // 
         this.menuItemDelete.Index = 2;
         this.menuItemDelete.Text = "Delete";
         this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
         // 
         // CustomEditorForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(360, 471);
         this.Controls.Add(this.panel);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.MinimumSize = new System.Drawing.Size(368, 120);
         this.Name = "CustomEditorForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Custom Editor";
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void editor_ProcessVarAdded(ProcessVar var)
      {
         this.AddUIElement(var);
         this.DisplayElements();
         this.Editor.flowsheet.IsDirty = true;
      }

      private void AddUIElement(ProcessVar var)
      {
         CustomEditorElementControl element = new CustomEditorElementControl(this, this.editor.flowsheet, var);
         this.panel.Controls.Add(element);
         elements.Add(element);
      }

      private void editor_ProcessVarDeleted(ArrayList vars, ArrayList idxs)
      {
         IEnumerator e = idxs.GetEnumerator();
         while (e.MoveNext())
         {
            int idx = (int)e.Current;
            CustomEditorElementControl element = (CustomEditorElementControl)this.elements[idx];
            this.elements.RemoveAt(idx);
            this.panel.Controls.Remove(element);
         }
         this.DisplayElements();
         this.Editor.flowsheet.IsDirty = true;
      }

      private void panel_Click(object sender, System.EventArgs e)
      {
         this.UnselectElements();
      }

      private ArrayList GetSelectedIndexes()
      {
         ArrayList list = new ArrayList();
         for (int i=this.elements.Count-1; i>=0; i--)
         {
            if ((this.elements[i] as CustomEditorElementControl).IsSelected)
            {
               list.Add(i);
            }
         }
         // the list should be sorted in descending order
         return list;
      }

      private void menuItemAdd_Click(object sender, System.EventArgs e)
      {
         VariableSelectorForm form = new VariableSelectorForm(this.editor);
         form.ShowDialog();
      }

      private void menuItemDelete_Click(object sender, System.EventArgs e)
      {
         if (this.GetSelectedIndexes().Count > 0)
         {
            this.editor.DeleteProcessVars(this.GetSelectedIndexes());
         }
      }

      private void DisplayElements()
      {
         this.panel.Visible = false;

         // calculate the number of elements that fit in a column
         // (we substract the header's height)
         double e1 = System.Convert.ToDouble(this.panel.Height-ELEMENT_HEIGHT)/System.Convert.ToDouble(ELEMENT_HEIGHT);
         double e2 = System.Math.Floor(e1);
         int countColumnElements = System.Convert.ToInt32(e2);

         // calculate the number of columns
         double d1 = System.Convert.ToDouble(this.elements.Count)/System.Convert.ToDouble(countColumnElements);
         double d2 = System.Math.Ceiling(d1);
         int countColumns = System.Convert.ToInt32(d2);

         // calculate the reminder of elements in the last column
         int reminderElements = 0;
         System.Math.DivRem(this.elements.Count, countColumnElements, out reminderElements);

         if (reminderElements == 0)
         {
            // treat all the columns the same
            for (int column=0; column<countColumns; column++)
            {
               for (int row=0; row<countColumnElements; row++)
               {
                  int idx = column*countColumnElements + row;
                  CustomEditorElementControl element = (CustomEditorElementControl)elements[idx];
                  element.Location = new Point(ELEMENT_WIDTH*column, 20*(row+1));
               }
            }
         }
         else
         {
            // treat the all the columns, except the last one, in the same way
            for (int column=0; column<countColumns-1; column++)
            {
               for (int row=0; row<countColumnElements; row++)
               {
                  int idx = column*countColumnElements + row;
                  CustomEditorElementControl element = (CustomEditorElementControl)elements[idx];
                  element.Location = new Point(ELEMENT_WIDTH*column, 20*(row+1));
               }
            }
            // treat the last column in a special way
            for (int row=0; row<reminderElements; row++)
            {
               int idx = (countColumns-1)*countColumnElements + row;
               CustomEditorElementControl element = (CustomEditorElementControl)elements[idx];
               element.Location = new Point(ELEMENT_WIDTH*(countColumns-1), 20*(row+1));
            }
         }

         // adjust the width of the header
         this.panelHeader.Width = ELEMENT_WIDTH*countColumns;

         this.panel.Visible = true;
      }

      private void panel_SizeChanged(object sender, System.EventArgs e)
      {
         this.DisplayElements();
      }

      public void UnselectElements()
      {
         IEnumerator e = this.panel.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is CustomEditorElementControl)
            {
               ((CustomEditorElementControl)e.Current).IsSelected = false;
            }
         }
      }

      public void SetElementsSelection(CustomEditorElementControl elementCtrl, ModifierKey modKey)
      {
         if (modKey == ModifierKey.Ctrl)
         {
            elementCtrl.IsSelected = !elementCtrl.IsSelected;
            this.baseIndex = this.GetIndexOfElement(elementCtrl);
         }
         else if (modKey == ModifierKey.Shift)
         {
            int currentIdx = this.GetIndexOfElement(elementCtrl);
            if (currentIdx >= 0 && this.baseIndex >= 0)
            {
               this.UnselectElements();
               int minIdx = System.Math.Min(currentIdx, this.baseIndex);
               int maxIdx = System.Math.Max(currentIdx, this.baseIndex);
               this.SetElementsSelection(minIdx, maxIdx);
            }
         }
         else
         {
            this.UnselectElements();
            elementCtrl.IsSelected = true;
            this.baseIndex = this.GetIndexOfElement(elementCtrl);
         }
      }

      private int GetIndexOfElement(CustomEditorElementControl elementCtrl)
      {
         int index = -1;
         for (int i=0; i<this.elements.Count; i++)
         {
            if ((this.elements[i] as CustomEditorElementControl).Equals(elementCtrl))
            {
               index = i;
               break;
            }
         }
         return index;
      }

      private void SetElementsSelection(int minIdx, int maxIdx)
      {
         CustomEditorElementControl element = null;
         for (int i=0; i<this.elements.Count; i++)
         {
            if (i >= minIdx && i <= maxIdx)
            {
               element = this.elements[i] as CustomEditorElementControl;
               element.IsSelected = true;
            }
         }
      }
   }
}
