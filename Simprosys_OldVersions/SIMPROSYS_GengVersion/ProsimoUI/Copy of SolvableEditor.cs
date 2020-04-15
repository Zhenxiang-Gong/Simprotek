using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SolvableEditor.
	/// </summary>
	public class SolvableEditor : System.Windows.Forms.Form
	{
        protected   SolvableControl solvableCtrl;
        protected   System.Windows.Forms.StatusBar      statusBar;
        protected   System.Windows.Forms.StatusBarPanel statusBarPanel;
        protected   System.Windows.Forms.MainMenu       mainMenu;
        protected   Panel           panel;
        protected   TextBox         textBoxName;
        private     PropertyGrid    propertyGrid;
        private     Button          CloseBtn;
        private     IContainer      components;
        private     ArrayList       processVarList;
        private     Label           nameLabel;
        private     PropertyTable   propertyTable;  
  
        public SolvableEditor()
        {
        }

        public SolvableEditor(SolvableControl solvableCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.solvableCtrl = solvableCtrl;
            this.processVarList = solvableCtrl.Solvable.VarList;
            this.propertyTable = new PropertyTable();

            // set the property dialog name
            if (solvableCtrl.Solvable != null)
                this.Text = solvableCtrl.Solvable.Name;
            else
                this.Text = "";

            textBoxName.Text = this.Text;

            ProcessVar var;
            ProcessVarDouble varDouble;
            ProcessVarInt varInt;
            // set the property names and values
            for (int counter = 0; counter < processVarList.Count; counter++)
            {
                var = processVarList[counter] as ProcessVar;
                if (var is ProcessVarDouble)
                {
                    varDouble = var as ProcessVarDouble;
                    propertyTable.Properties.Add(new PropertySpec(var.ToString(), "System.Double", this.Text, null));
                    
                }
                else
                    if (var is ProcessVarInt)
                    {
                        varInt = var as ProcessVarInt;
                        propertyTable.Properties.Add(new PropertySpec(var.ToString(), "System.Int64", this.Text, null));
                        
                    }
                propertyTable[var.ToString()] = new ProcessVarObj(solvableCtrl.Flowsheet.ApplicationPrefs, var);
            }

           // this.Text = var + ": " + solvableCtrl.Solvable.Name;
            // add the table to the grid.
            propertyGrid.SelectedObject = propertyTable;

            //register for complete event to redisplay the value
            this.Solvable_SolveComplete(this.solvableCtrl.Solvable, this.solvableCtrl.Solvable.SolveState);

            this.solvableCtrl.Solvable.SolveComplete += new SolveCompleteEventHandler(Solvable_SolveComplete);
            this.solvableCtrl.Solvable.NameChanged += new NameChangedEventHandler(Solvable_NameChanged);
            this.ResizeEnd += new EventHandler(SolvableEditor_ResizeEnd);
		}

      void SolvableEditor_ResizeEnd(object sender, EventArgs e)
      {
         if (this.solvableCtrl.Flowsheet != null)
         {
            this.solvableCtrl.Flowsheet.ConnectionManager.DrawConnections();
         }
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.solvableCtrl != null && this.solvableCtrl.Solvable != null)
         {
            this.solvableCtrl.Solvable.SolveComplete -= new SolveCompleteEventHandler(Solvable_SolveComplete);
            this.solvableCtrl.Solvable.NameChanged -= new NameChangedEventHandler(Solvable_NameChanged);
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
            this.components = new System.ComponentModel.Container();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 544);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(345, 25);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 82;
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Width = 345;
            // 
            // propertyGrid
            // 
            this.propertyGrid.BackColor = System.Drawing.Color.LightSteelBlue;
            this.propertyGrid.CategoryForeColor = System.Drawing.Color.Black;
            this.propertyGrid.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.propertyGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertyGrid.HelpBackColor = System.Drawing.Color.LightSteelBlue;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.LineColor = System.Drawing.Color.LightSteelBlue;
            this.propertyGrid.Location = new System.Drawing.Point(12, 38);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(321, 444);
            this.propertyGrid.TabIndex = 83;
            this.propertyGrid.Validating += new System.ComponentModel.CancelEventHandler(this.propertyGrid_Validating);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(135, 488);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(75, 23);
            this.CloseBtn.TabIndex = 84;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(75, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(258, 20);
            this.textBoxName.TabIndex = 85;
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.ValidatingHandler);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 15);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 86;
            this.nameLabel.Text = "Name";
            // 
            // SolvableEditor
            // 
            this.ClientSize = new System.Drawing.Size(345, 569);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.statusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "SolvableEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solvable";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingHandler);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

      }
		#endregion

      private void ClosingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         // assume only one dependent
         this.solvableCtrl.Editor = null;
      }

      protected virtual void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         Solvable solvable = this.solvableCtrl.Solvable;
         TextBox tb = (TextBox)sender;
         if (tb.Text.Trim().Equals(""))
         {
            e.Cancel = true;
            string message3 = "Please specify a name!";
            MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         else
         {
            ErrorMessage error = solvable.SpecifyName(this.textBoxName.Text);
            // ErrorMessage error = solvable.SpecifyName(this.Text);
            if (error != null)
               UI.ShowError(error);
         }
      }

      protected virtual void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ActiveControl = this.statusBar;
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();      
      }

      private void Solvable_SolveComplete(object sender, SolveState solveState)
      {
         UI.SetStatusTextAndIcon(this.statusBarPanel, solveState);
      }

      private void Solvable_NameChanged(object sender, string name, string oldName)
      {
        // this.textBoxName.Text = name;
          this.Text = name;
      }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void propertyGrid_Validating(object sender, CancelEventArgs e)
        {
            //ProcessVarObj   var;
            
            //if (var = e as ProcessVarObj)
            //    var.ProcessVarObj_Validating(sender, e);
        }

   }
}
