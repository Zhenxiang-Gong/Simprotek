using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for BagFilterLabelsControl.
	/// </summary>
	public class BagFilterLabelsControl : FabricFilterLabelsControl
	{
      
      // these need to be in sync with the dimensions of this control
      public new const int WIDTH = 192;
      public new const int HEIGHT = 220;

      private ProcessVarLabel labelBagDiameter;
      private ProcessVarLabel labelBagLength;
      private ProcessVarLabel labelNumberOfBags;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public BagFilterLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public BagFilterLabelsControl(BagFilter uo) : base(uo)
		{
         InitializeComponent();
         this.InitializeVariableLabels(uo);
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
         this.labelBagDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelBagLength = new ProsimoUI.ProcessVarLabel();
         this.labelNumberOfBags = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelBagDiameter
         // 
         this.labelBagDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBagDiameter.Location = new System.Drawing.Point(0, 160);
         this.labelBagDiameter.Name = "labelBagDiameter";
         this.labelBagDiameter.Size = new System.Drawing.Size(192, 20);
         this.labelBagDiameter.TabIndex = 96;
         this.labelBagDiameter.Text = "BagDiameter";
         this.labelBagDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelBagLength
         // 
         this.labelBagLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBagLength.Location = new System.Drawing.Point(0, 180);
         this.labelBagLength.Name = "labelBagLength";
         this.labelBagLength.Size = new System.Drawing.Size(192, 20);
         this.labelBagLength.TabIndex = 96;
         this.labelBagLength.Text = "BagLength";
         this.labelBagLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelNumberOfBags
         // 
         this.labelNumberOfBags.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelNumberOfBags.Location = new System.Drawing.Point(0, 200);
         this.labelNumberOfBags.Name = "labelNumberOfBags";
         this.labelNumberOfBags.Size = new System.Drawing.Size(192, 20);
         this.labelNumberOfBags.TabIndex = 96;
         this.labelNumberOfBags.Text = "NumberOfBags";
         this.labelNumberOfBags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // BagFilterLabelsControl
         // 
         this.Controls.Add(this.labelBagDiameter);
         this.Controls.Add(this.labelBagLength);
         this.Controls.Add(this.labelNumberOfBags);
         this.Name = "BagFilterLabelsControl";
         this.Size = new System.Drawing.Size(192, 220);
         this.Controls.SetChildIndex(this.labelNumberOfBags, 0);
         this.Controls.SetChildIndex(this.labelBagLength, 0);
         this.Controls.SetChildIndex(this.labelBagDiameter, 0);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(BagFilter uo)
      {
         this.labelBagDiameter.InitializeVariable(uo.BagDiameter);
         this.labelBagLength.InitializeVariable(uo.BagLength);
         this.labelNumberOfBags.InitializeVariable(uo.NumberOfBags);
      }
   }
}
