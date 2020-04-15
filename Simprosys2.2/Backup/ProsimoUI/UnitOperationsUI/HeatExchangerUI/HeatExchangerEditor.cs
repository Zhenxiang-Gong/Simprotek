using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HeatExchangerEditor.
	/// </summary>
	public class HeatExchangerEditor : UnitOpEditor
	{
      public const int INDEX_SIMPLE_GENERIC = 0;
      public const int INDEX_SHELL_AND_TUBE = 1;
      public const int INDEX_PLATE_AND_FRAME = 2;

      private const string COLD_INLET_OUTLET = "Cold Inlet/Outlet";
      private const string HOT_INLET_OUTLET = "Hot Inlet/Outlet";
      private const string COLD_SHELL_INLET_OUTLET = "Cold-Shell Inlet/Outlet";
      private const string HOT_SHELL_INLET_OUTLET = "Hot-Shell Inlet/Outlet";
      private const string COLD_TUBE_INLET_OUTLET = "Cold-Tube Inlet/Outlet";
      private const string HOT_TUBE_INLET_OUTLET = "Hot-Tube Inlet/Outlet";

      private HeatExchangerRatingEditor ratingEditor;
      private bool inConstruction;

      public HeatExchangerControl HeatExchangerCtrl
      {
         get {return (HeatExchangerControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

      private MenuItem menuItemRating;

      private System.Windows.Forms.GroupBox groupBoxHotSide;
      private System.Windows.Forms.RadioButton radioButtonHotSideShell;
      private System.Windows.Forms.RadioButton radioButtonHotSideTube;

      private System.Windows.Forms.ComboBox comboBoxExchangerType;
      private System.Windows.Forms.Label labelExchangerType;
      private System.Windows.Forms.ComboBox comboBoxCalculationType;
      private System.Windows.Forms.Label labelCalculationType;

      private System.Windows.Forms.GroupBox groupBoxColdStream;
      private System.Windows.Forms.GroupBox groupBoxHotStream;
      private ProsimoUI.SolvableNameTextBox textBoxColdInName;
      private ProsimoUI.SolvableNameTextBox textBoxColdOutName;
      private ProsimoUI.SolvableNameTextBox textBoxHotInName;
      private ProsimoUI.SolvableNameTextBox textBoxHotOutName;
      private System.Windows.Forms.GroupBox groupBoxHeatExchanger;
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HeatExchangerEditor(HeatExchangerControl heatExchangerCtrl) : base(heatExchangerCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.inConstruction = true;
         HeatExchanger heatExchanger = this.HeatExchangerCtrl.HeatExchanger;
         this.Text = "Heat Exchanger: " + heatExchanger.Name;
         this.UpdateStreamsUI();

         this.groupBoxHeatExchanger = new System.Windows.Forms.GroupBox();
         this.groupBoxHeatExchanger.Location = new System.Drawing.Point(724, 24);
         this.groupBoxHeatExchanger.Name = "groupBoxHeatExchanger";
         this.groupBoxHeatExchanger.Text = "Heat Exchanger";
         this.groupBoxHeatExchanger.Size = new System.Drawing.Size(280, 300);
         this.groupBoxHeatExchanger.TabIndex = 128;
         this.groupBoxHeatExchanger.TabStop = false;
         this.panel.Controls.Add(this.groupBoxHeatExchanger);

         // TO DO: to customize the height? or not

//         if (heatExchanger.ColdSideInlet is DryingGasStream)
//         {
//            this.groupBoxColdStream.Size = new System.Drawing.Size(360, 280);
//            this.panel.Size = new System.Drawing.Size(1010, 309);
//            this.ClientSize = new System.Drawing.Size(1010, 331);
//         }
//         else if (heatExchanger.ColdSideInlet is DryingMaterialStream)
//         {
            this.groupBoxColdStream.Size = new System.Drawing.Size(360, 300);
            this.panel.Size = new System.Drawing.Size(1010, 329);
            this.ClientSize = new System.Drawing.Size(1010, 351);
//         }
         this.groupBoxHotStream.Size = new System.Drawing.Size(360, 300);

         HeatExchangerLabelsControl heatExchangerLabelsCtrl = new HeatExchangerLabelsControl(this.HeatExchangerCtrl.HeatExchanger);
         //ProcessVarLabelsControl heatExchangerLabelsCtrl = new ProcessVarLabelsControl(this.HeatExchangerCtrl.HeatExchanger.VarList);
         this.groupBoxHeatExchanger.Controls.Add(heatExchangerLabelsCtrl);
         heatExchangerLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         HeatExchangerValuesControl heatExchangerValuesCtrl = new HeatExchangerValuesControl(this.HeatExchangerCtrl);
         //ProcessVarValuesControl heatExchangerValuesCtrl = new ProcessVarValuesControl(this.HeatExchangerCtrl);
         this.groupBoxHeatExchanger.Controls.Add(heatExchangerValuesCtrl);
         heatExchangerValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         // shell and tube hot side
         this.groupBoxHotSide = new System.Windows.Forms.GroupBox();
         this.radioButtonHotSideTube = new System.Windows.Forms.RadioButton();
         this.radioButtonHotSideShell = new System.Windows.Forms.RadioButton();
         // 
         // groupBoxHotSide
         // 
         this.groupBoxHotSide.Controls.Add(this.radioButtonHotSideTube);
         this.groupBoxHotSide.Controls.Add(this.radioButtonHotSideShell);
         this.groupBoxHotSide.Location = new System.Drawing.Point(0, 0);
         this.groupBoxHotSide.Name = "groupBoxHotSide";
         this.groupBoxHotSide.Size = new System.Drawing.Size(104, 68);
         this.groupBoxHotSide.TabIndex = 0;
         this.groupBoxHotSide.TabStop = false;
         this.groupBoxHotSide.Text = "Hot Side";
         // 
         // radioButtonHotSideTube
         // 
         this.radioButtonHotSideTube.Location = new System.Drawing.Point(8, 44);
         this.radioButtonHotSideTube.Name = "radioButtonHotSideTube";
         this.radioButtonHotSideTube.Size = new System.Drawing.Size(72, 20);
         this.radioButtonHotSideTube.TabIndex = 1;
         this.radioButtonHotSideTube.Text = "Tube";
         this.radioButtonHotSideTube.CheckedChanged += new System.EventHandler(this.HotSideHandler);
         // 
         // radioButtonHotSideShell
         // 
         this.radioButtonHotSideShell.Location = new System.Drawing.Point(8, 20);
         this.radioButtonHotSideShell.Name = "radioButtonHotSideShell";
         this.radioButtonHotSideShell.Size = new System.Drawing.Size(72, 20);
         this.radioButtonHotSideShell.TabIndex = 0;
         this.radioButtonHotSideShell.Text = "Shell";
         this.radioButtonHotSideShell.CheckedChanged += new System.EventHandler(this.HotSideHandler);

         this.groupBoxHeatExchanger.Controls.Add(this.groupBoxHotSide);
         this.groupBoxHotSide.Location = new Point(20, 112);

         heatExchangerCtrl.HeatExchanger.StreamAttached += new StreamAttachedEventHandler(HeatExchanger_StreamAttached);
         heatExchangerCtrl.HeatExchanger.StreamDetached += new StreamDetachedEventHandler(HeatExchanger_StreamDetached);

         this.menuItemRating = new MenuItem();
         this.menuItemRating.Index = this.menuItemReport.Index + 1;
         this.menuItemRating.Text = "Rating";
         this.menuItemRating.Click += new EventHandler(menuItemRating_Click);
         this.mainMenu.MenuItems.Add(this.menuItemRating);

         this.comboBoxCalculationType = new System.Windows.Forms.ComboBox();
         this.comboBoxExchangerType = new System.Windows.Forms.ComboBox();
         this.labelCalculationType = new System.Windows.Forms.Label();
         this.labelCalculationType.BackColor = Color.DarkGray;
         this.labelExchangerType = new System.Windows.Forms.Label();
         this.labelExchangerType.BackColor = Color.DarkGray;

         // comboBoxCalculationType
         this.comboBoxCalculationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxCalculationType.Items.AddRange(new object[] {
                                                                     "Balance",
                                                                     "Rating"});
         this.comboBoxCalculationType.Location = new System.Drawing.Point(492, 0);
         this.comboBoxCalculationType.Name = "comboBoxCalculationType";
         this.comboBoxCalculationType.Size = new System.Drawing.Size(80, 21);
         this.comboBoxCalculationType.TabIndex = 7;
         this.comboBoxCalculationType.SelectedIndexChanged += new EventHandler(comboBoxCalculationType_SelectedIndexChanged);

         // comboBoxExchangerType
         this.comboBoxExchangerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxExchangerType.Items.AddRange(new object[] {
                                                                   "Simple Generic",
                                                                   "Shell And Tube",
                                                                   "Plate And Frame"});
         this.comboBoxExchangerType.Location = new System.Drawing.Point(792, 0);
         this.comboBoxExchangerType.Name = "comboBoxExchangerType";
         this.comboBoxExchangerType.Size = new System.Drawing.Size(108, 21);
         this.comboBoxExchangerType.TabIndex = 6;
         this.comboBoxExchangerType.SelectedIndexChanged += new EventHandler(comboBoxExchangerType_SelectedIndexChanged);

         // labelCalculationType
         this.labelCalculationType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelCalculationType.Location = new System.Drawing.Point(300, 0);
         this.labelCalculationType.Name = "labelCalculationType";
         this.labelCalculationType.Size = new System.Drawing.Size(192, 20);
         this.labelCalculationType.TabIndex = 5;
         this.labelCalculationType.Text = "Calculation Type:";
         this.labelCalculationType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

         // labelExchangerType
         this.labelExchangerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelExchangerType.Location = new System.Drawing.Point(600, 0);
         this.labelExchangerType.Name = "labelExchangerType";
         this.labelExchangerType.Size = new System.Drawing.Size(192, 20);
         this.labelExchangerType.TabIndex = 4;
         this.labelExchangerType.Text = "Exchanger Type:";
         this.labelExchangerType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

         this.panel.Controls.Add(this.labelExchangerType);
         this.panel.Controls.Add(this.comboBoxExchangerType);
         this.panel.Controls.Add(this.labelCalculationType);
         this.panel.Controls.Add(this.comboBoxCalculationType);

         this.HeatExchangerCtrl.HeatExchanger.HXHotSideChanged += new HXHotSideChangedEventHandler(HeatExchanger_HXHotSideChanged);

         this.comboBoxCalculationType.SelectedIndex = -1;
         this.comboBoxExchangerType.SelectedIndex = -1;
         this.inConstruction = false;

         this.SetExchangerType(this.HeatExchangerCtrl.HeatExchanger.ExchangerType);
         this.SetCalculationType(this.HeatExchangerCtrl.HeatExchanger.CalculationType);
         this.comboBoxCalculationType.Enabled = true;
         if (this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel != null)
         {
            if (this.HeatExchangerCtrl.HeatExchanger.ExchangerType == ExchangerType.ShellAndTube)
            {
               HXRatingModelShellAndTube ratingModel = this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
               this.SetHotSideShell(ratingModel.IsShellSideHot);
            }
         }
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.HeatExchangerCtrl != null)
         {
            this.HeatExchangerCtrl.HeatExchanger.StreamAttached -= new StreamAttachedEventHandler(HeatExchanger_StreamAttached);
            this.HeatExchangerCtrl.HeatExchanger.StreamDetached -= new StreamDetachedEventHandler(HeatExchanger_StreamDetached);
            this.HeatExchangerCtrl.HeatExchanger.HXHotSideChanged -= new HXHotSideChangedEventHandler(HeatExchanger_HXHotSideChanged);
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
         this.groupBoxColdStream = new System.Windows.Forms.GroupBox();
         this.textBoxColdOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxColdInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxHotStream = new System.Windows.Forms.GroupBox();
         this.textBoxHotOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxHotInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxColdStream.SuspendLayout();
         this.groupBoxHotStream.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxColdStream
         // 
         this.groupBoxColdStream.Controls.Add(this.textBoxColdOutName);
         this.groupBoxColdStream.Controls.Add(this.textBoxColdInName);
         this.groupBoxColdStream.Location = new System.Drawing.Point(4, 24);
         this.groupBoxColdStream.Name = "groupBoxColdStream";
         this.groupBoxColdStream.Size = new System.Drawing.Size(360, 300);
         this.groupBoxColdStream.TabIndex = 118;
         this.groupBoxColdStream.TabStop = false;
         this.groupBoxColdStream.Text = "Cold Inlet/Outlet";
         // 
         // textBoxColdOutName
         // 
         this.textBoxColdOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxColdOutName.Name = "textBoxColdOutName";
         this.textBoxColdOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdOutName.TabIndex = 13;
         this.textBoxColdOutName.Text = "";
         // 
         // textBoxColdInName
         // 
         this.textBoxColdInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxColdInName.Name = "textBoxColdInName";
         this.textBoxColdInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdInName.TabIndex = 12;
         this.textBoxColdInName.Text = "";
         // 
         // groupBoxHotStream
         // 
         this.groupBoxHotStream.Controls.Add(this.textBoxHotOutName);
         this.groupBoxHotStream.Controls.Add(this.textBoxHotInName);
         this.groupBoxHotStream.Location = new System.Drawing.Point(364, 24);
         this.groupBoxHotStream.Name = "groupBoxHotStream";
         this.groupBoxHotStream.Size = new System.Drawing.Size(360, 280);
         this.groupBoxHotStream.TabIndex = 119;
         this.groupBoxHotStream.TabStop = false;
         this.groupBoxHotStream.Text = "Hot Inlet/Outlet";
         // 
         // textBoxHotOutName
         // 
         this.textBoxHotOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxHotOutName.Name = "textBoxHotOutName";
         this.textBoxHotOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotOutName.TabIndex = 11;
         this.textBoxHotOutName.Text = "";
         // 
         // textBoxHotInName
         // 
         this.textBoxHotInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxHotInName.Name = "textBoxHotInName";
         this.textBoxHotInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotInName.TabIndex = 10;
         this.textBoxHotInName.Text = "";
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxHotStream);
         this.panel.Controls.Add(this.groupBoxColdStream);
         this.panel.Size = new System.Drawing.Size(1010, 329);
         // 
         // HeatExchangerEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(1010, 351);
         this.Name = "HeatExchangerEditor";
         this.Text = "Heat Exchanger";
         this.groupBoxColdStream.ResumeLayout(false);
         this.groupBoxHotStream.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);
      }
		#endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         HeatExchanger heatExchanger = this.HeatExchangerCtrl.HeatExchanger;
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
                  ErrorMessage error = heatExchanger.SpecifyName(this.textBoxName.Text);
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
         list.Add(this.textBoxHotInName);
         list.Add(this.textBoxHotOutName);
         list.Add(this.textBoxColdInName);
         list.Add(this.textBoxColdOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void HeatExchanger_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamsUI();
      }

      private void HeatExchanger_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI()
      {
         // clear the stream group-boxes and start again
         this.groupBoxColdStream.Controls.Clear();
         this.groupBoxHotStream.Controls.Clear();

         HeatExchanger heatExchanger = this.HeatExchangerCtrl.HeatExchanger;
         bool hasColdIn = false;
         bool hasColdOut = false;
         bool hasHotIn = false;
         bool hasHotOut = false;

         ProcessStreamBase coldIn = heatExchanger.ColdSideInlet;
         hasColdIn = coldIn != null;

         ProcessStreamBase coldOut = heatExchanger.ColdSideOutlet;
         hasColdOut = coldOut != null;

         ProcessStreamBase hotIn = heatExchanger.HotSideInlet;
         hasHotIn = hotIn != null;

         ProcessStreamBase hotOut = heatExchanger.HotSideOutlet;
         hasHotOut = hotOut != null;

         if (hasColdIn || hasColdOut)
         {
            ProcessStreamBase labelsStream = hasColdIn ? coldIn : coldOut;

            UserControl ctrl = null;
            if (labelsStream is WaterStream) {
               ctrl = new ProcessVarLabelsControl(labelsStream.VarList);
            }
            else if (labelsStream is DryingGasStream) {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }
            else if (labelsStream is DryingMaterialStream) {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
            }
            this.groupBoxColdStream.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if (hasColdIn)
         {
            UserControl ctrl = null;
            ProcessStreamBaseControl baseCtrl = (ProcessStreamBaseControl)this.HeatExchangerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.HeatExchangerCtrl.HeatExchanger.ColdSideInlet.Name);
            if (coldIn is WaterStream) {
               WaterStreamControl processInCtrl = baseCtrl as WaterStreamControl;
               ctrl = new ProcessVarValuesControl(processInCtrl);
            }
            else if (coldIn is DryingGasStream) {
               GasStreamControl gasInCtrl = baseCtrl as GasStreamControl;
               ctrl = new GasStreamValuesControl(gasInCtrl);
            }
            else if (coldIn is DryingMaterialStream) {
               MaterialStreamControl matInCtrl = baseCtrl as MaterialStreamControl;
               ctrl = new MaterialStreamValuesControl(matInCtrl);
            }

            this.groupBoxColdStream.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxColdInName.SetSolvable(heatExchanger.ColdSideInlet);
            this.groupBoxColdStream.Controls.Add(this.textBoxColdInName);
            this.textBoxColdInName.Text = heatExchanger.ColdSideInlet.Name;
            UI.SetStatusColor(this.textBoxColdInName, heatExchanger.ColdSideInlet.SolveState);
         }

         if (hasColdOut) {
            ProcessStreamBaseControl baseCtrl = (ProcessStreamBaseControl)this.HeatExchangerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.HeatExchangerCtrl.HeatExchanger.ColdSideOutlet.Name);
            UserControl ctrl = null;
            if (coldOut is WaterStream) {
               WaterStreamControl processOutCtrl = baseCtrl as WaterStreamControl;
               ctrl = new ProcessVarValuesControl(processOutCtrl);
            }
            else if (coldOut is DryingGasStream) {
               GasStreamControl gasOutCtrl = baseCtrl as GasStreamControl;
               ctrl = new GasStreamValuesControl(gasOutCtrl);
            }
            else if (coldOut is DryingMaterialStream) {
               MaterialStreamControl matOutCtrl = baseCtrl as MaterialStreamControl;
               ctrl = new MaterialStreamValuesControl(matOutCtrl);
            }
            this.groupBoxColdStream.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxColdOutName.SetSolvable(heatExchanger.ColdSideOutlet);
            this.groupBoxColdStream.Controls.Add(this.textBoxColdOutName);
            this.textBoxColdOutName.Text = heatExchanger.ColdSideOutlet.Name;
            UI.SetStatusColor(this.textBoxColdOutName, heatExchanger.ColdSideOutlet.SolveState);
         }

         if (hasHotIn || hasHotOut)
         {
            ProcessStreamBase labelsStream = hasHotIn ? hotIn : hotOut;

            UserControl ctrl = null;
            if (labelsStream is WaterStream) {
               ctrl = new ProcessVarLabelsControl(labelsStream.VarList);
            }
            else if (labelsStream is DryingGasStream) {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }
            else if (labelsStream is DryingMaterialStream) {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
            }
            this.groupBoxHotStream.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if (hasHotIn)
         {
            UserControl ctrl = null;
            ProcessStreamBaseControl baseCtrl = (ProcessStreamBaseControl)this.HeatExchangerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.HeatExchangerCtrl.HeatExchanger.HotSideInlet.Name);
            if (hotIn is WaterStream) {
               WaterStreamControl processInCtrl = baseCtrl as WaterStreamControl;
               ctrl = new ProcessVarValuesControl(processInCtrl);
            }
            else if (hotIn is DryingGasStream) {
               GasStreamControl gasInCtrl = baseCtrl as GasStreamControl;
               ctrl = new GasStreamValuesControl(gasInCtrl);
            }
            else if (hotIn is DryingMaterialStream) {
               MaterialStreamControl matInCtrl = baseCtrl as MaterialStreamControl;
               ctrl = new MaterialStreamValuesControl(matInCtrl);
            }

            this.groupBoxHotStream.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxHotInName.SetSolvable(heatExchanger.HotSideInlet);
            this.groupBoxHotStream.Controls.Add(this.textBoxHotInName);
            this.textBoxHotInName.Text = heatExchanger.HotSideInlet.Name;
            UI.SetStatusColor(this.textBoxHotInName, heatExchanger.HotSideInlet.SolveState);
         }

         if (hasHotOut) {
            ProcessStreamBaseControl baseCtrl = (ProcessStreamBaseControl)this.HeatExchangerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.HeatExchangerCtrl.HeatExchanger.HotSideOutlet.Name);
            UserControl ctrl = null;
            if (hotOut is WaterStream) {
               WaterStreamControl processOutCtrl = baseCtrl as WaterStreamControl;
               ctrl = new ProcessVarValuesControl(processOutCtrl);
            }
            else if (hotOut is DryingGasStream) {
               GasStreamControl gasOutCtrl = baseCtrl as GasStreamControl;
               ctrl = new GasStreamValuesControl(gasOutCtrl);
            }
            else if (hotOut is DryingMaterialStream) {
               MaterialStreamControl matOutCtrl = baseCtrl as MaterialStreamControl;
               ctrl = new MaterialStreamValuesControl(matOutCtrl);
            }
            this.groupBoxHotStream.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxHotOutName.SetSolvable(heatExchanger.HotSideOutlet);
            this.groupBoxHotStream.Controls.Add(this.textBoxHotOutName);
            this.textBoxHotOutName.Text = heatExchanger.HotSideOutlet.Name;
            UI.SetStatusColor(this.textBoxHotOutName, heatExchanger.HotSideOutlet.SolveState);
         }
      }

      private void comboBoxExchangerType_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;

            int idx = this.comboBoxExchangerType.SelectedIndex;
            if (idx == HeatExchangerEditor.INDEX_SIMPLE_GENERIC)
            {
               error = this.HeatExchangerCtrl.HeatExchanger.SpecifyExchangerType(ExchangerType.SimpleGeneric);
               if (error == null)
               {
                  this.groupBoxHotSide.Visible = false;
                  this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_INLET_OUTLET;
                  this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_INLET_OUTLET;
               }
            }
            else if (idx == HeatExchangerEditor.INDEX_SHELL_AND_TUBE)
            {
               error = this.HeatExchangerCtrl.HeatExchanger.SpecifyExchangerType(ExchangerType.ShellAndTube);
               if (error == null)
               {
                  this.groupBoxHotSide.Visible = true;
                  this.SetHotSideShell((this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube).IsShellSideHot);
                  if ((this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube).IsShellSideHot)
                  {
                     this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_TUBE_INLET_OUTLET;
                     this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_SHELL_INLET_OUTLET;
                  }
                  else
                  {
                     this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_SHELL_INLET_OUTLET;
                     this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_TUBE_INLET_OUTLET;
                  }
               }
            }
            else if (idx == HeatExchangerEditor.INDEX_PLATE_AND_FRAME)
            {
               error = this.HeatExchangerCtrl.HeatExchanger.SpecifyExchangerType(ExchangerType.PlateAndFrame);
               if (error == null)
               {
                  this.groupBoxHotSide.Visible = false;
                  this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_INLET_OUTLET;
                  this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_INLET_OUTLET;
               }
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetExchangerType(this.HeatExchangerCtrl.HeatExchanger.ExchangerType);
            }
         }
      }

      public void SetExchangerType(ExchangerType type)
      {
         if (type == ExchangerType.ShellAndTube)
            this.comboBoxExchangerType.SelectedIndex = INDEX_SHELL_AND_TUBE;
         else if (type == ExchangerType.SimpleGeneric)
            this.comboBoxExchangerType.SelectedIndex = INDEX_SIMPLE_GENERIC;
         else if (type == ExchangerType.PlateAndFrame)
            this.comboBoxExchangerType.SelectedIndex = INDEX_PLATE_AND_FRAME;
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == INDEX_BALANCE)
            {
               error = this.HeatExchangerCtrl.HeatExchanger.SpecifyCalculationType(UnitOpCalculationType.Balance);
               if (error == null)
               {
                  this.menuItemRating.Enabled = false;
                  this.comboBoxExchangerType.Enabled = false;
                  this.groupBoxHotSide.Visible = false;
                  this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_INLET_OUTLET;
                  this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_INLET_OUTLET;
               }
            }
            else if (idx == INDEX_RATING)
            {
               error = this.HeatExchangerCtrl.HeatExchanger.SpecifyCalculationType(UnitOpCalculationType.Rating);
               if (error == null)
               {
                  this.menuItemRating.Enabled = true;
                  this.comboBoxExchangerType.Enabled = true;
                  if (this.HeatExchangerCtrl.HeatExchanger.ExchangerType == ExchangerType.ShellAndTube)
                  {
                     this.groupBoxHotSide.Visible = true;
                     this.SetHotSideShell((this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube).IsShellSideHot);
                     if ((this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube).IsShellSideHot)
                     {
                        this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_TUBE_INLET_OUTLET;
                        this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_SHELL_INLET_OUTLET;
                     }
                     else
                     {
                        this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_SHELL_INLET_OUTLET;
                        this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_TUBE_INLET_OUTLET;
                     }
                  }
                  else
                  {
                     this.groupBoxHotSide.Visible = false;
                     this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_INLET_OUTLET;
                     this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_INLET_OUTLET;
                  }
               }
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetCalculationType(this.HeatExchangerCtrl.HeatExchanger.CalculationType);
            }
         }
      }
      
      public void SetCalculationType(UnitOpCalculationType type)
      {
         if (type == UnitOpCalculationType.Balance)
            this.comboBoxCalculationType.SelectedIndex = INDEX_BALANCE;
         else if (type == UnitOpCalculationType.Rating)
            this.comboBoxCalculationType.SelectedIndex = INDEX_RATING;
      }

      private void menuItemRating_Click(object sender, EventArgs e)
      {
         if (this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel != null)
         {
            this.comboBoxCalculationType.Enabled = false;
            this.comboBoxExchangerType.Enabled = false;

            if (this.ratingEditor == null)
            {
               if (this.HeatExchangerCtrl.HeatExchanger.ExchangerType == ExchangerType.SimpleGeneric)
                  this.ratingEditor = new HXRatingSimpleGenericEditor(this.HeatExchangerCtrl);
               else if (this.HeatExchangerCtrl.HeatExchanger.ExchangerType == ExchangerType.PlateAndFrame)
                  this.ratingEditor = new HXRatingPlateAndFrameEditor(this.HeatExchangerCtrl);
               else if (this.HeatExchangerCtrl.HeatExchanger.ExchangerType == ExchangerType.ShellAndTube)
                  this.ratingEditor = new HXRatingShellAndTubeEditor(this.HeatExchangerCtrl);
               else
                  this.ratingEditor = new HeatExchangerRatingEditor(this.HeatExchangerCtrl);
               this.ratingEditor.Owner = this;
               this.ratingEditor.Closed += new EventHandler(ratingEditor_Closed);
               this.ratingEditor.Show();
            }
            else
            {
               if (this.ratingEditor.WindowState.Equals(FormWindowState.Minimized))
                  this.ratingEditor.WindowState = FormWindowState.Normal;
               this.ratingEditor.Activate();
            }
         }
      }

      private void ratingEditor_Closed(object sender, EventArgs e)
      {
         this.ratingEditor = null;
         this.comboBoxCalculationType.Enabled = true;
         this.comboBoxExchangerType.Enabled = true;
      }

      private void HotSideHandler(object sender, System.EventArgs e)
      {
         if (this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel != null)
         {
            if (this.HeatExchangerCtrl.HeatExchanger.ExchangerType == ExchangerType.ShellAndTube)
            {
               ErrorMessage error = null;
               HXRatingModelShellAndTube ratingModel = this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
               RadioButton rb = (RadioButton)sender;
               if (rb == this.radioButtonHotSideShell)
               {
                  error = ratingModel.SpecifyIsShellSideHot(rb.Checked);
               }
               else if (rb == this.radioButtonHotSideTube)
               {
                  error = ratingModel.SpecifyIsShellSideHot(!rb.Checked);
               }
               if (error != null)
               {
                  UI.ShowError(error);
                  this.SetHotSideShell(ratingModel.IsShellSideHot);
               }
            }
         }
      }

      public void SetHotSideShell(bool val)
      {
         if (val)
            this.radioButtonHotSideShell.Checked = true;
         else
            this.radioButtonHotSideTube.Checked = true;
      }

      private void HeatExchanger_HXHotSideChanged(HeatExchanger hx)
      {
         HXRatingModelShellAndTube ratingModel = this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
         this.SetHotSideShell(ratingModel.IsShellSideHot);
         if ((this.HeatExchangerCtrl.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube).IsShellSideHot)
         {
            this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_TUBE_INLET_OUTLET;
            this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_SHELL_INLET_OUTLET;
         }
         else
         {
            this.groupBoxColdStream.Text = HeatExchangerEditor.COLD_SHELL_INLET_OUTLET;
            this.groupBoxHotStream.Text = HeatExchangerEditor.HOT_TUBE_INLET_OUTLET;
         }
      }
   }
}

//if (labelsStream is WaterStream)
//{
//   ctrl = new WaterStreamLabelsControl((WaterStream)labelsStream);
//}
//else if (labelsStream is DryingGasStream)
//{
//   ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
//}
//else if (labelsStream is DryingMaterialStream)
//{
//   ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
//}
//if (hotIn is WaterStream)
//{
//   WaterStreamControl processInCtrl = baseCtrl as WaterStreamControl;
//   ctrl = new WaterStreamValuesControl(processInCtrl);
//}
//else if (hotIn is DryingGasStream)
//{
//   GasStreamControl gasInCtrl = baseCtrl as GasStreamControl;
//   ctrl = new GasStreamValuesControl(gasInCtrl);
//}
//else if (hotIn is DryingMaterialStream)
//{
//   MaterialStreamControl matInCtrl = baseCtrl as MaterialStreamControl;
//   ctrl = new MaterialStreamValuesControl(matInCtrl);
//}

//if (hotOut is WaterStream)
//{
//   WaterStreamControl processOutCtrl = baseCtrl as WaterStreamControl;
//   ctrl = new WaterStreamValuesControl(processOutCtrl);
//}
//else if (hotOut is DryingGasStream)
//{
//   GasStreamControl gasOutCtrl = baseCtrl as GasStreamControl;
//   ctrl = new GasStreamValuesControl(gasOutCtrl);
//}
//else if (hotOut is DryingMaterialStream)
//{
//   MaterialStreamControl matOutCtrl = baseCtrl as MaterialStreamControl;
//   ctrl = new MaterialStreamValuesControl(matOutCtrl);
//}
//if (coldOut is WaterStream)
//{
//   WaterStreamControl processOutCtrl = baseCtrl as WaterStreamControl;
//   ctrl = new WaterStreamValuesControl(processOutCtrl);
//}
//else if (coldOut is DryingGasStream)
//{
//   GasStreamControl gasOutCtrl = baseCtrl as GasStreamControl;
//   ctrl = new GasStreamValuesControl(gasOutCtrl);
//}
//else if (coldOut is DryingMaterialStream)
//{
//   MaterialStreamControl matOutCtrl = baseCtrl as MaterialStreamControl;
//   ctrl = new MaterialStreamValuesControl(matOutCtrl);
//}
//if (coldIn is WaterStream) {
//   WaterStreamControl processInCtrl = baseCtrl as WaterStreamControl;
//   ctrl = new WaterStreamValuesControl(processInCtrl);
//}
//else if (coldIn is DryingGasStream)
//{
//   GasStreamControl gasInCtrl = baseCtrl as GasStreamControl;
//   ctrl = new GasStreamValuesControl(gasInCtrl);
//}
//else if (coldIn is DryingMaterialStream)
//{
//   MaterialStreamControl matInCtrl = baseCtrl as MaterialStreamControl;
//   ctrl = new MaterialStreamValuesControl(matInCtrl);
//}

//if (labelsStream is WaterStream) {
//   ctrl = new WaterStreamLabelsControl((WaterStream)labelsStream);
//}
//if (labelsStream is DryingGasStream)
//{
//   ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
//}
//else if (labelsStream is DryingMaterialStream)
//{
//   ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
//}



