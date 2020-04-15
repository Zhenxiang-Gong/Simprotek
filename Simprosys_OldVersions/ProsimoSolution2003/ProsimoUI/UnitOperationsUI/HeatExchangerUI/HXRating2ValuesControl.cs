using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRating2ValuesControl.
	/// </summary>
	public class HXRating2ValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 40;

      private ProsimoUI.ProcessVarTextBox textBoxWallThermalConductivity;
      private ProsimoUI.ProcessVarTextBox textBoxWallThickness;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRating2ValuesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public HXRating2ValuesControl(Flowsheet flowsheet, HeatExchanger heatExchanger)
      {
         // NOTE: this constructor is not used
         InitializeComponent();

         HXRatingModel ratingModel = heatExchanger.CurrentRatingModel as HXRatingModel;
         this.InitializeVariableTextBoxes(flowsheet, ratingModel);
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
         this.textBoxWallThickness = new ProsimoUI.ProcessVarTextBox();
         this.textBoxWallThermalConductivity = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxWallThickness
         // 
         this.textBoxWallThickness.Location = new System.Drawing.Point(0, 20);
         this.textBoxWallThickness.Name = "textBoxWallThickness";
         this.textBoxWallThickness.Size = new System.Drawing.Size(80, 20);
         this.textBoxWallThickness.TabIndex = 5;
         this.textBoxWallThickness.Text = "";
         this.textBoxWallThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxWallThickness.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxWallThermalConductivity
         // 
         this.textBoxWallThermalConductivity.Location = new System.Drawing.Point(0, 0);
         this.textBoxWallThermalConductivity.Name = "textBoxWallThermalConductivity";
         this.textBoxWallThermalConductivity.Size = new System.Drawing.Size(80, 20);
         this.textBoxWallThermalConductivity.TabIndex = 4;
         this.textBoxWallThermalConductivity.Text = "";
         this.textBoxWallThermalConductivity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxWallThermalConductivity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HXRating2ValuesControl
         // 
         this.Controls.Add(this.textBoxWallThickness);
         this.Controls.Add(this.textBoxWallThermalConductivity);
         this.Name = "HXRating2ValuesControl";
         this.Size = new System.Drawing.Size(80, 40);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, HXRatingModel rating)
      {
         this.textBoxWallThermalConductivity.InitializeVariable(flowsheet, rating.WallThermalConductivity);
         this.textBoxWallThickness.InitializeVariable(flowsheet, rating.WallThickness);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxWallThermalConductivity);
         list.Add(this.textBoxWallThickness);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }
	}
}
