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
        protected   Panel               panel;
        protected TextBox textBoxName;
        private     IContainer          components;
        private     ArrayList           processVarList;
        private Button CloseBtn;
        protected TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel flowLayoutPanel;
        protected Panel namePanel;
        private Panel closeBtnPanel;
        private Label nameLabel;
        protected  int columnIndex;
  
        public SolvableEditor()
        {
        }

        public SolvableEditor(SolvableControl solvableCtrl)
		{
            UnitOperation           unitOp;
            TwoStreamUnitOperation  twoUnitOp;
            int rowCount;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            columnIndex = 0;

            this.solvableCtrl = solvableCtrl;
            this.processVarList = solvableCtrl.Solvable.VarList;
           // this.propertyTable = new PropertyTable();

           // // set the property dialog name
            if (solvableCtrl.Solvable != null)
                this.Text = solvableCtrl.Solvable.Name;
            else
                this.Text = "";

            textBoxName.Text = this.Text;

            //register for complete event to redisplay the value
            this.Solvable_SolveComplete(this.solvableCtrl.Solvable, this.solvableCtrl.Solvable.SolveState);

            this.solvableCtrl.Solvable.SolveComplete += new SolveCompleteEventHandler(Solvable_SolveComplete);
            this.solvableCtrl.Solvable.NameChanged += new NameChangedEventHandler(Solvable_NameChanged);
            this.ResizeEnd += new EventHandler(SolvableEditor_ResizeEnd);
		}

        protected int initializeGrid(SolvableControl ctrl, int columnIndex, Boolean bValueOnly, string ctrlType)
        {
            ProcessVar var;
            ProcessVarLabel label;
            ProcessVarTextBox valueTextBox;
            ArrayList varList;
            Label typeLabel, nameLabel;
            int counter, extraRowCount;

            varList = ctrl.Solvable.VarList;
            if (ctrlType.Length != 0)
            {
                typeLabel = new Label();
                typeLabel.Size = new System.Drawing.Size(192, 20);
                typeLabel.Dock = DockStyle.Fill;
                typeLabel.Anchor = AnchorStyles.Left;
                typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                typeLabel.Text = ctrlType;
                typeLabel.BackColor = Color.DarkGray;

                nameLabel = new Label();
                nameLabel.Size = new System.Drawing.Size(80, 20);
                nameLabel.Dock = DockStyle.Fill;
                nameLabel.Anchor = AnchorStyles.Left;
                nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                nameLabel.Text = ctrl.Solvable.ToString();
                nameLabel.BackColor = Color.DarkGray;
                if (bValueOnly)
                {
                    this.tableLayoutPanel.ColumnCount = columnIndex + 1;
                    this.tableLayoutPanel.Controls.Add(nameLabel, columnIndex, 0);
                }
                else
                {
                    this.tableLayoutPanel.ColumnCount = columnIndex + 2;
                    this.tableLayoutPanel.Controls.Add(typeLabel, columnIndex, 0);
                    this.tableLayoutPanel.Controls.Add(nameLabel, columnIndex + 1, 0);
                }
                extraRowCount = 1;
            }
            else
                extraRowCount = 0;
            // set the property names and values
            for (counter = 0; counter < varList.Count; counter++)
            {   
                var = varList[counter] as ProcessVar;
                if (var == null)
                    continue;

                valueTextBox = new ProcessVarTextBox();
                valueTextBox.Size = new System.Drawing.Size(80, 20);
                
                valueTextBox.Dock = DockStyle.Fill;
                valueTextBox.Anchor = AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Top & AnchorStyles.Bottom;
                valueTextBox.InitializeVariable(solvableCtrl.Flowsheet.ApplicationPrefs, var);

                if (bValueOnly)
                {
                    this.tableLayoutPanel.Controls.Add(valueTextBox, columnIndex, counter + extraRowCount);
                }
                else
                {
                    label = new ProcessVarLabel();
                    label.Anchor = System.Windows.Forms.AnchorStyles.Left;
                    label.Size = new System.Drawing.Size(192, 20);
                    label.AutoSize = false;
                    //label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    label.Dock = DockStyle.Fill;
                    label.Text = var.VarTypeName;
                    label.InitializeVariable(var);

                    this.tableLayoutPanel.Controls.Add(label, columnIndex, counter + extraRowCount);
                    this.tableLayoutPanel.Controls.Add(valueTextBox, columnIndex + 1, counter + extraRowCount);
                }
                this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            }
            if (this.tableLayoutPanel.RowCount < counter + extraRowCount)
                this.tableLayoutPanel.RowCount = counter + extraRowCount;
            return counter + extraRowCount;
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.namePanel = new System.Windows.Forms.Panel();
            this.closeBtnPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
            this.flowLayoutPanel.SuspendLayout();
            this.namePanel.SuspendLayout();
            this.closeBtnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 504);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(314, 25);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 82;
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Width = 314;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(49, 8);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(252, 20);
            this.textBoxName.TabIndex = 85;
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.ValidatingHandler);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.DarkGray;
            this.nameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameLabel.Location = new System.Drawing.Point(3, 8);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(40, 15);
            this.nameLabel.TabIndex = 86;
            this.nameLabel.Text = "Name:";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CloseBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CloseBtn.Location = new System.Drawing.Point(115, 13);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(85, 31);
            this.CloseBtn.TabIndex = 84;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.AutoScrollMinSize = new System.Drawing.Size(10, 10);
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.BackColor = System.Drawing.Color.AliceBlue;
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 37);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 15;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(285, 408);
            this.tableLayoutPanel.TabIndex = 87;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.Controls.Add(this.namePanel);
            this.flowLayoutPanel.Controls.Add(this.tableLayoutPanel);
            this.flowLayoutPanel.Controls.Add(this.closeBtnPanel);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(314, 504);
            this.flowLayoutPanel.TabIndex = 89;
            // 
            // namePanel
            // 
            this.namePanel.AutoSize = true;
            this.namePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.namePanel.Controls.Add(this.textBoxName);
            this.namePanel.Controls.Add(this.nameLabel);
            this.namePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.namePanel.Location = new System.Drawing.Point(3, 3);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(304, 31);
            this.namePanel.TabIndex = 88;
            // 
            // closeBtnPanel
            // 
            this.closeBtnPanel.AutoSize = true;
            this.closeBtnPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeBtnPanel.Controls.Add(this.CloseBtn);
            this.closeBtnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.closeBtnPanel.Location = new System.Drawing.Point(3, 448);
            this.closeBtnPanel.Name = "closeBtnPanel";
            this.closeBtnPanel.Size = new System.Drawing.Size(304, 47);
            this.closeBtnPanel.TabIndex = 89;
            // 
            // SolvableEditor
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(314, 529);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.statusBar);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "SolvableEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " SolvableEditor";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingHandler);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.closeBtnPanel.ResumeLayout(false);
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
            this.Close();
        }

        private void propertyGrid_Validating(object sender, CancelEventArgs e)
        {
            //ProcessVarObj   var;
            
            //if (var = e as ProcessVarObj)
            //    var.ProcessVarObj_Validating(sender, e);
        }

   }
}
