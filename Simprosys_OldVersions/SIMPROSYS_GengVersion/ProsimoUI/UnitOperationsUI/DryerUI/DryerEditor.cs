using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Drying;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerEditor.
	/// </summary>
	public class DryerEditor : UnitOpEditor
	{
      public const int INDEX_BALANCE = 0;
      public const int INDEX_SCOPING = 1;

      private DryerScopingEditor scopingEditor;
      private bool inConstruction;
        
      public DryerControl DryerCtrl
      {
         get {return (DryerControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

      private MenuItem menuItemScoping;
      private System.Windows.Forms.Label labelCalculationType;
      private System.Windows.Forms.ComboBox comboBoxCalculationType;

      GasStreamLabelsControl gasLabelsCtrl;
      GasStreamValuesControl gasValuesInCtrl;
      GasStreamValuesControl gasValuesOutCtrl;
      MaterialStreamLabelsControl materialLabelsCtrl;
      MaterialStreamValuesControl materialValuesInCtrl;
      MaterialStreamValuesControl materialValuesOutCtrl;

      private System.Windows.Forms.GroupBox groupBoxDryingMedium;
      private System.Windows.Forms.GroupBox groupBoxMaterial;
      private ProsimoUI.SolvableNameTextBox textBoxGasInName;
      private ProsimoUI.SolvableNameTextBox textBoxGasOutName;
      private ProsimoUI.SolvableNameTextBox textBoxMaterialInName;
      private ProsimoUI.SolvableNameTextBox textBoxMaterialOutName;
      private System.Windows.Forms.GroupBox groupBoxDryer;
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerEditor(DryerControl dryerCtrl) : base(dryerCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
         this.inConstruction = true;
         this.DryerCtrl = dryerCtrl;
         Dryer dryer = dryerCtrl.Dryer;
         this.Text = "Dryer: " + dryer.Name;
         
         this.UpdateStreamsUI();

         initializeGrid(dryerCtrl, columnIndex, false, "Dryer");

         
         dryerCtrl.Dryer.StreamAttached += new StreamAttachedEventHandler(Dryer_StreamAttached);
         dryerCtrl.Dryer.StreamDetached += new StreamDetachedEventHandler(Dryer_StreamDetached);

         this.menuItemScoping = new MenuItem();
         this.menuItemScoping.Index = this.menuItemReport.Index + 1;
         this.menuItemScoping.Text = "Scoping";
         this.menuItemScoping.Click += new EventHandler(menuItemScoping_Click);
         this.mainMenu.MenuItems.Add(this.menuItemScoping);

         this.labelCalculationType = new System.Windows.Forms.Label();
         this.comboBoxCalculationType = new System.Windows.Forms.ComboBox();

         // labelCalculationType
         this.labelCalculationType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelCalculationType.Location = new System.Drawing.Point(310, 8);
         this.labelCalculationType.Name = "labelCalculationType";
         this.labelCalculationType.BackColor = Color.DarkGray;
         this.labelCalculationType.Size = new System.Drawing.Size(192, 20);
         this.labelCalculationType.TabIndex = 5;
         this.labelCalculationType.Text = "Calculation Type:";
         this.labelCalculationType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

         // comboBoxCalculationType
         this.comboBoxCalculationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxCalculationType.Items.AddRange(new object[] {
                                                                     "Balance",
                                                                     "Scoping"});
         this.comboBoxCalculationType.Location = new System.Drawing.Point(502, 8);
         this.comboBoxCalculationType.Name = "comboBoxCalculationType";
         this.comboBoxCalculationType.Size = new System.Drawing.Size(80, 21);
         this.comboBoxCalculationType.TabIndex = 7;
         this.comboBoxCalculationType.SelectedIndexChanged += new EventHandler(comboBoxCalculationType_SelectedIndexChanged);

         this.namePanel.Controls.Add(this.labelCalculationType);
         this.namePanel.Controls.Add(this.comboBoxCalculationType);

         this.comboBoxCalculationType.SelectedIndex = -1;
         this.inConstruction = false;
         this.SetCalculationType(this.DryerCtrl.Dryer.DryerCalculationType);
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.DryerCtrl.Dryer != null)
         {
            this.DryerCtrl.Dryer.StreamAttached -= new StreamAttachedEventHandler(Dryer_StreamAttached);
            this.DryerCtrl.Dryer.StreamDetached -= new StreamDetachedEventHandler(Dryer_StreamDetached);
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
      //  private void InitializeComponent()
      //  {
      //   this.groupBoxDryingMedium = new System.Windows.Forms.GroupBox();
      //   this.textBoxGasOutName = new ProsimoUI.SolvableNameTextBox();
      //   this.textBoxGasInName = new ProsimoUI.SolvableNameTextBox();
      //   this.groupBoxMaterial = new System.Windows.Forms.GroupBox();
      //   this.textBoxMaterialOutName = new ProsimoUI.SolvableNameTextBox();
      //   this.textBoxMaterialInName = new ProsimoUI.SolvableNameTextBox();
      //   this.groupBoxDryingMedium.SuspendLayout();
      //   this.groupBoxMaterial.SuspendLayout();
      //   ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
      //   this.panel.SuspendLayout();
      //   this.SuspendLayout();
      //   // 
      //   // groupBoxDryingMedium
      //   // 
      //   this.groupBoxDryingMedium.Controls.Add(this.textBoxGasOutName);
      //   this.groupBoxDryingMedium.Controls.Add(this.textBoxGasInName);
      //   this.groupBoxDryingMedium.Location = new System.Drawing.Point(364, 24);
      //   this.groupBoxDryingMedium.Name = "groupBoxDryingMedium";
      //   this.groupBoxDryingMedium.Size = new System.Drawing.Size(360, 280);
      //   this.groupBoxDryingMedium.TabIndex = 118;
      //   this.groupBoxDryingMedium.TabStop = false;
      //   this.groupBoxDryingMedium.Text = "Drying Medium Inlet/Outlet";
      //   // 
      //   // textBoxGasOutName
      //   // 
      //   this.textBoxGasOutName.Location = new System.Drawing.Point(276, 12);
      //   this.textBoxGasOutName.Name = "textBoxGasOutName";
      //   this.textBoxGasOutName.Size = new System.Drawing.Size(80, 20);
      //   this.textBoxGasOutName.TabIndex = 13;
      //   this.textBoxGasOutName.Text = "";
      //   // 
      //   // textBoxGasInName
      //   // 
      //   this.textBoxGasInName.Location = new System.Drawing.Point(196, 12);
      //   this.textBoxGasInName.Name = "textBoxGasInName";
      //   this.textBoxGasInName.Size = new System.Drawing.Size(80, 20);
      //   this.textBoxGasInName.TabIndex = 12;
      //   this.textBoxGasInName.Text = "";
      //   // 
      //   // groupBoxMaterial
      //   // 
      //   this.groupBoxMaterial.Controls.Add(this.textBoxMaterialOutName);
      //   this.groupBoxMaterial.Controls.Add(this.textBoxMaterialInName);
      //   this.groupBoxMaterial.Location = new System.Drawing.Point(4, 24);
      //   this.groupBoxMaterial.Name = "groupBoxMaterial";
      //   this.groupBoxMaterial.Size = new System.Drawing.Size(360, 300);
      //   this.groupBoxMaterial.TabIndex = 119;
      //   this.groupBoxMaterial.TabStop = false;
      //   this.groupBoxMaterial.Text = "Material Inlet/Outlet";
      //   // 
      //   // textBoxMaterialOutName
      //   // 
      //   this.textBoxMaterialOutName.Location = new System.Drawing.Point(276, 12);
      //   this.textBoxMaterialOutName.Name = "textBoxMaterialOutName";
      //   this.textBoxMaterialOutName.Size = new System.Drawing.Size(80, 20);
      //   this.textBoxMaterialOutName.TabIndex = 11;
      //   this.textBoxMaterialOutName.Text = "";
      //   // 
      //   // textBoxMaterialInName
      //   // 
      //   this.textBoxMaterialInName.Location = new System.Drawing.Point(196, 12);
      //   this.textBoxMaterialInName.Name = "textBoxMaterialInName";
      //   this.textBoxMaterialInName.Size = new System.Drawing.Size(80, 20);
      //   this.textBoxMaterialInName.TabIndex = 10;
      //   this.textBoxMaterialInName.Text = "";
      //   // 
      //   // panel
      //   // 
      //   this.panel.Controls.Add(this.groupBoxMaterial);
      //   this.panel.Controls.Add(this.groupBoxDryingMedium);
      //   this.panel.Size = new System.Drawing.Size(1010, 329);
      //   // 
      //   // DryerEditor
      //   // 
      //   this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
      //   this.ClientSize = new System.Drawing.Size(1010, 351);
      //   this.Name = "DryerEditor";
      //   this.Text = "Dryer";
      //   this.groupBoxDryingMedium.ResumeLayout(false);
      //   this.groupBoxMaterial.ResumeLayout(false);
      //   ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
      //   this.panel.ResumeLayout(false);
      //   this.ResumeLayout(false);
      //}
		#endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         Dryer dryer = this.DryerCtrl.Dryer;
         TextBox tb = (TextBox)sender;
         if (tb.Text != null)
         {
            if (tb.Text.Trim().Equals(""))
            {
               if (sender == this.textBoxName)
               {
                  e.Cancel = true;
                  string message3 = "Please specify a name!";
                  MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
            else
            {
               if (sender == this.textBoxName)
               {
                  ErrorMessage error = dryer.SpecifyName(this.textBoxName.Text);
                  if (error != null)
                     UI.ShowError(error);
               }
            }
         }
      }

      protected override void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxName);
         list.Add(this.textBoxMaterialInName);
         list.Add(this.textBoxMaterialOutName);
         list.Add(this.textBoxGasInName);
         list.Add(this.textBoxGasOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void Dryer_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamsUI();
      }

      private void Dryer_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI()
      {
         // clear the stream group-boxes and start again
          //this.groupBoxDryingMedium.Controls.Clear();
          //this.groupBoxMaterial.Controls.Clear();
          
         Dryer dryer = this.DryerCtrl.Dryer;

         bool hasGasIn = false;
         bool hasGasOut = false;
         bool hasMaterialIn = false;
         bool hasMaterialOut = false;
         
         DryingGasStream gasIn = dryer.GasInlet;
         if (gasIn != null)
            hasGasIn = true;

         DryingGasStream gasOut = dryer.GasOutlet;
         if (gasOut != null)
            hasGasOut = true;

         DryingMaterialStream materialIn = dryer.MaterialInlet;
         if (materialIn != null)
            hasMaterialIn = true;

         DryingMaterialStream materialOut = dryer.MaterialOutlet;
         if (materialOut != null)
            hasMaterialOut = true;

        GasStreamControl gasInCtrl, gasOutCtrl;
        MaterialStreamControl materialInCtrl, materialOutCtrl;

         if (hasGasIn) 
         {
            gasInCtrl = (GasStreamControl)this.DryerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.DryerCtrl.Dryer.GasInlet.Name);
            initializeGrid(gasInCtrl, columnIndex, false, "Drying Medium Inlet/Outlet");
            columnIndex += 2;
            if (hasGasOut)
            {                
                gasOutCtrl = (GasStreamControl)this.DryerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.DryerCtrl.Dryer.GasOutlet.Name);
                initializeGrid(gasOutCtrl, columnIndex, true, "Drying Medium Inlet/Outlet");
                columnIndex ++;
            }
            UI.SetStatusColor(this.statusBar, dryer.GasInlet.SolveState);
         }
         else
         if (hasGasOut)
         {
            gasOutCtrl = (GasStreamControl)this.DryerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.DryerCtrl.Dryer.GasOutlet.Name);
            initializeGrid(gasOutCtrl, columnIndex, false, "Drying Medium Inlet/Outlet");
            columnIndex += 2;
            UI.SetStatusColor(this.statusBar, dryer.GasOutlet.SolveState);
         }

         if (hasMaterialIn)
         {
            materialInCtrl = (MaterialStreamControl)this.DryerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.DryerCtrl.Dryer.MaterialInlet.Name);
            initializeGrid(materialInCtrl, columnIndex, false, "Material Inlet/Outlet");
            columnIndex += 2;
            if (hasMaterialOut)
            {
                materialOutCtrl = (MaterialStreamControl)this.DryerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.DryerCtrl.Dryer.MaterialOutlet.Name);
                initializeGrid(materialOutCtrl, columnIndex, true, "Material Inlet/Outlet");
                columnIndex++;
            }
            UI.SetStatusColor(this.statusBar, dryer.MaterialInlet.SolveState);
         }
         else
         if (hasMaterialOut)
         {
            materialOutCtrl = (MaterialStreamControl)this.DryerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.DryerCtrl.Dryer.MaterialOutlet.Name);
            initializeGrid(materialOutCtrl, columnIndex, true, "Material Inlet/Outlet");
            columnIndex += 2;
            UI.SetStatusColor(this.statusBar, dryer.MaterialOutlet.SolveState);
         }
      }

      private void menuItemScoping_Click(object sender, EventArgs e)
      {
         if (this.DryerCtrl.Dryer.ScopingModel != null)
         {
            this.comboBoxCalculationType.Enabled = false;

            if (this.scopingEditor == null)
            {
               this.scopingEditor = new DryerScopingEditor(this.DryerCtrl);
               this.scopingEditor.Owner = this;
               this.scopingEditor.Closed += new EventHandler(scopingEditor_Closed);
               this.scopingEditor.Show();
            }
            else
            {
               if (this.scopingEditor.WindowState.Equals(FormWindowState.Minimized))
                  this.scopingEditor.WindowState = FormWindowState.Normal;
               this.scopingEditor.Activate();
            }
         }
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == DryerEditor.INDEX_BALANCE)
            {
               error = this.DryerCtrl.Dryer.SpecifyDryerCalculationType(DryerCalculationType.Balance);
               if (error == null)
               {
                  this.menuItemScoping.Enabled = false;
               }
            }
            else if (idx == DryerEditor.INDEX_SCOPING)
            {
               error = this.DryerCtrl.Dryer.SpecifyDryerCalculationType(DryerCalculationType.Scoping);
               if (error == null)
               {
                  this.menuItemScoping.Enabled = true;
               }
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetCalculationType(this.DryerCtrl.Dryer.DryerCalculationType);
            }
         }
      }

      public void SetCalculationType(DryerCalculationType type)
      {
         if (type == DryerCalculationType.Balance)
            this.comboBoxCalculationType.SelectedIndex = INDEX_BALANCE;
         else if (type == DryerCalculationType.Scoping)
            this.comboBoxCalculationType.SelectedIndex = INDEX_SCOPING;
      }

      private void scopingEditor_Closed(object sender, EventArgs e)
      {
         this.scopingEditor = null;
         this.comboBoxCalculationType.Enabled = true;
      }
   }
}