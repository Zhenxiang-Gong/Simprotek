using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;

namespace ProsimoUI.SubstancesUI
{
	/// <summary>
	/// Summary description for SubstanceDetailsControl.
	/// </summary>
	public class SubstanceDetailsControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.Label labelFormula;
      private System.Windows.Forms.Label labelMolarWeight;
      private System.Windows.Forms.Label labelType;
      private System.Windows.Forms.Label labelCASRegistryNo;
      private System.Windows.Forms.Label labelUserDefined;
      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.TextBox textBoxCASRegistryNo;
      private System.Windows.Forms.TextBox textBoxMolarWeight;
      private System.Windows.Forms.TextBox textBoxFormula;
      private System.Windows.Forms.TextBox textBoxUserDefined;
      private System.Windows.Forms.TextBox textBoxType;
      private System.Windows.Forms.GroupBox groupBoxCriticalProperties;
      private System.Windows.Forms.TextBox textBoxCriticalCompressibility;
      private System.Windows.Forms.TextBox textBoxCriticalPressure;
      private System.Windows.Forms.TextBox textBoxCriticalTemperature;
      private System.Windows.Forms.TextBox textBoxCriticalVolume;
      private System.Windows.Forms.TextBox textBoxAccentricFactor;
      private System.Windows.Forms.Label labelCriticalPressure;
      private System.Windows.Forms.Label labelCriticalTemperature;
      private System.Windows.Forms.Label labelCriticalCompressibility;
      private System.Windows.Forms.Label labelCriticalVolume;
      private System.Windows.Forms.Label labelAccentricFactor;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SubstanceDetailsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
         this.labelName = new System.Windows.Forms.Label();
         this.labelMolarWeight = new System.Windows.Forms.Label();
         this.labelCASRegistryNo = new System.Windows.Forms.Label();
         this.labelType = new System.Windows.Forms.Label();
         this.labelFormula = new System.Windows.Forms.Label();
         this.labelUserDefined = new System.Windows.Forms.Label();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.textBoxCASRegistryNo = new System.Windows.Forms.TextBox();
         this.textBoxMolarWeight = new System.Windows.Forms.TextBox();
         this.textBoxFormula = new System.Windows.Forms.TextBox();
         this.textBoxUserDefined = new System.Windows.Forms.TextBox();
         this.textBoxType = new System.Windows.Forms.TextBox();
         this.groupBoxCriticalProperties = new System.Windows.Forms.GroupBox();
         this.textBoxCriticalCompressibility = new System.Windows.Forms.TextBox();
         this.textBoxCriticalPressure = new System.Windows.Forms.TextBox();
         this.textBoxCriticalTemperature = new System.Windows.Forms.TextBox();
         this.textBoxCriticalVolume = new System.Windows.Forms.TextBox();
         this.textBoxAccentricFactor = new System.Windows.Forms.TextBox();
         this.labelCriticalPressure = new System.Windows.Forms.Label();
         this.labelCriticalTemperature = new System.Windows.Forms.Label();
         this.labelCriticalCompressibility = new System.Windows.Forms.Label();
         this.labelCriticalVolume = new System.Windows.Forms.Label();
         this.labelAccentricFactor = new System.Windows.Forms.Label();
         this.groupBoxCriticalProperties.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelName
         // 
         this.labelName.Location = new System.Drawing.Point(4, 12);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(100, 16);
         this.labelName.TabIndex = 0;
         this.labelName.Text = "Name:";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelMolarWeight
         // 
         this.labelMolarWeight.Location = new System.Drawing.Point(4, 92);
         this.labelMolarWeight.Name = "labelMolarWeight";
         this.labelMolarWeight.Size = new System.Drawing.Size(100, 16);
         this.labelMolarWeight.TabIndex = 1;
         this.labelMolarWeight.Text = "Molar Weight:";
         this.labelMolarWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelCASRegistryNo
         // 
         this.labelCASRegistryNo.Location = new System.Drawing.Point(4, 112);
         this.labelCASRegistryNo.Name = "labelCASRegistryNo";
         this.labelCASRegistryNo.Size = new System.Drawing.Size(100, 16);
         this.labelCASRegistryNo.TabIndex = 2;
         this.labelCASRegistryNo.Text = "CAS Registry No:";
         this.labelCASRegistryNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelType
         // 
         this.labelType.Location = new System.Drawing.Point(4, 52);
         this.labelType.Name = "labelType";
         this.labelType.Size = new System.Drawing.Size(100, 16);
         this.labelType.TabIndex = 3;
         this.labelType.Text = "Type:";
         this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelFormula
         // 
         this.labelFormula.Location = new System.Drawing.Point(4, 72);
         this.labelFormula.Name = "labelFormula";
         this.labelFormula.Size = new System.Drawing.Size(100, 16);
         this.labelFormula.TabIndex = 4;
         this.labelFormula.Text = "Formula:";
         this.labelFormula.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelUserDefined
         // 
         this.labelUserDefined.Location = new System.Drawing.Point(4, 32);
         this.labelUserDefined.Name = "labelUserDefined";
         this.labelUserDefined.Size = new System.Drawing.Size(100, 16);
         this.labelUserDefined.TabIndex = 5;
         this.labelUserDefined.Text = "User Defined:";
         this.labelUserDefined.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxName
         // 
         this.textBoxName.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxName.Location = new System.Drawing.Point(104, 8);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.ReadOnly = true;
         this.textBoxName.Size = new System.Drawing.Size(116, 20);
         this.textBoxName.TabIndex = 6;
         this.textBoxName.WordWrap = false;
         // 
         // textBoxCASRegistryNo
         // 
         this.textBoxCASRegistryNo.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxCASRegistryNo.Location = new System.Drawing.Point(104, 108);
         this.textBoxCASRegistryNo.Name = "textBoxCASRegistryNo";
         this.textBoxCASRegistryNo.ReadOnly = true;
         this.textBoxCASRegistryNo.Size = new System.Drawing.Size(116, 20);
         this.textBoxCASRegistryNo.TabIndex = 7;
         this.textBoxCASRegistryNo.WordWrap = false;
         // 
         // textBoxMolarWeight
         // 
         this.textBoxMolarWeight.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMolarWeight.Location = new System.Drawing.Point(104, 88);
         this.textBoxMolarWeight.Name = "textBoxMolarWeight";
         this.textBoxMolarWeight.ReadOnly = true;
         this.textBoxMolarWeight.Size = new System.Drawing.Size(116, 20);
         this.textBoxMolarWeight.TabIndex = 8;
         this.textBoxMolarWeight.WordWrap = false;
         // 
         // textBoxFormula
         // 
         this.textBoxFormula.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxFormula.Location = new System.Drawing.Point(104, 68);
         this.textBoxFormula.Name = "textBoxFormula";
         this.textBoxFormula.ReadOnly = true;
         this.textBoxFormula.Size = new System.Drawing.Size(116, 20);
         this.textBoxFormula.TabIndex = 9;
         this.textBoxFormula.WordWrap = false;
         // 
         // textBoxUserDefined
         // 
         this.textBoxUserDefined.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxUserDefined.Location = new System.Drawing.Point(104, 28);
         this.textBoxUserDefined.Name = "textBoxUserDefined";
         this.textBoxUserDefined.ReadOnly = true;
         this.textBoxUserDefined.Size = new System.Drawing.Size(116, 20);
         this.textBoxUserDefined.TabIndex = 10;
         this.textBoxUserDefined.WordWrap = false;
         // 
         // textBoxType
         // 
         this.textBoxType.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxType.Location = new System.Drawing.Point(104, 48);
         this.textBoxType.Name = "textBoxType";
         this.textBoxType.ReadOnly = true;
         this.textBoxType.Size = new System.Drawing.Size(116, 20);
         this.textBoxType.TabIndex = 11;
         this.textBoxType.WordWrap = false;
         // 
         // groupBoxCriticalProperties
         // 
         this.groupBoxCriticalProperties.Controls.Add(this.textBoxCriticalCompressibility);
         this.groupBoxCriticalProperties.Controls.Add(this.textBoxCriticalPressure);
         this.groupBoxCriticalProperties.Controls.Add(this.textBoxCriticalTemperature);
         this.groupBoxCriticalProperties.Controls.Add(this.textBoxCriticalVolume);
         this.groupBoxCriticalProperties.Controls.Add(this.textBoxAccentricFactor);
         this.groupBoxCriticalProperties.Controls.Add(this.labelCriticalPressure);
         this.groupBoxCriticalProperties.Controls.Add(this.labelCriticalTemperature);
         this.groupBoxCriticalProperties.Controls.Add(this.labelCriticalCompressibility);
         this.groupBoxCriticalProperties.Controls.Add(this.labelCriticalVolume);
         this.groupBoxCriticalProperties.Controls.Add(this.labelAccentricFactor);
         this.groupBoxCriticalProperties.Location = new System.Drawing.Point(224, 4);
         this.groupBoxCriticalProperties.Name = "groupBoxCriticalProperties";
         this.groupBoxCriticalProperties.Size = new System.Drawing.Size(256, 124);
         this.groupBoxCriticalProperties.TabIndex = 12;
         this.groupBoxCriticalProperties.TabStop = false;
         this.groupBoxCriticalProperties.Text = "Critical Properties";
         // 
         // textBoxCriticalCompressibility
         // 
         this.textBoxCriticalCompressibility.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxCriticalCompressibility.Location = new System.Drawing.Point(132, 36);
         this.textBoxCriticalCompressibility.Name = "textBoxCriticalCompressibility";
         this.textBoxCriticalCompressibility.ReadOnly = true;
         this.textBoxCriticalCompressibility.Size = new System.Drawing.Size(116, 20);
         this.textBoxCriticalCompressibility.TabIndex = 21;
         this.textBoxCriticalCompressibility.WordWrap = false;
         // 
         // textBoxCriticalPressure
         // 
         this.textBoxCriticalPressure.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxCriticalPressure.Location = new System.Drawing.Point(132, 56);
         this.textBoxCriticalPressure.Name = "textBoxCriticalPressure";
         this.textBoxCriticalPressure.ReadOnly = true;
         this.textBoxCriticalPressure.Size = new System.Drawing.Size(116, 20);
         this.textBoxCriticalPressure.TabIndex = 20;
         this.textBoxCriticalPressure.WordWrap = false;
         // 
         // textBoxCriticalTemperature
         // 
         this.textBoxCriticalTemperature.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxCriticalTemperature.Location = new System.Drawing.Point(132, 76);
         this.textBoxCriticalTemperature.Name = "textBoxCriticalTemperature";
         this.textBoxCriticalTemperature.ReadOnly = true;
         this.textBoxCriticalTemperature.Size = new System.Drawing.Size(116, 20);
         this.textBoxCriticalTemperature.TabIndex = 19;
         this.textBoxCriticalTemperature.WordWrap = false;
         // 
         // textBoxCriticalVolume
         // 
         this.textBoxCriticalVolume.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxCriticalVolume.Location = new System.Drawing.Point(132, 96);
         this.textBoxCriticalVolume.Name = "textBoxCriticalVolume";
         this.textBoxCriticalVolume.ReadOnly = true;
         this.textBoxCriticalVolume.Size = new System.Drawing.Size(116, 20);
         this.textBoxCriticalVolume.TabIndex = 18;
         this.textBoxCriticalVolume.WordWrap = false;
         // 
         // textBoxAccentricFactor
         // 
         this.textBoxAccentricFactor.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxAccentricFactor.Location = new System.Drawing.Point(132, 16);
         this.textBoxAccentricFactor.Name = "textBoxAccentricFactor";
         this.textBoxAccentricFactor.ReadOnly = true;
         this.textBoxAccentricFactor.Size = new System.Drawing.Size(116, 20);
         this.textBoxAccentricFactor.TabIndex = 17;
         this.textBoxAccentricFactor.WordWrap = false;
         // 
         // labelCriticalPressure
         // 
         this.labelCriticalPressure.Location = new System.Drawing.Point(4, 60);
         this.labelCriticalPressure.Name = "labelCriticalPressure";
         this.labelCriticalPressure.Size = new System.Drawing.Size(128, 16);
         this.labelCriticalPressure.TabIndex = 16;
         this.labelCriticalPressure.Text = "Critical Pressure:";
         this.labelCriticalPressure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelCriticalTemperature
         // 
         this.labelCriticalTemperature.Location = new System.Drawing.Point(4, 80);
         this.labelCriticalTemperature.Name = "labelCriticalTemperature";
         this.labelCriticalTemperature.Size = new System.Drawing.Size(128, 16);
         this.labelCriticalTemperature.TabIndex = 15;
         this.labelCriticalTemperature.Text = "Critical Temperature:";
         this.labelCriticalTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelCriticalCompressibility
         // 
         this.labelCriticalCompressibility.Location = new System.Drawing.Point(4, 40);
         this.labelCriticalCompressibility.Name = "labelCriticalCompressibility";
         this.labelCriticalCompressibility.Size = new System.Drawing.Size(128, 16);
         this.labelCriticalCompressibility.TabIndex = 14;
         this.labelCriticalCompressibility.Text = "Critical Compressibility:";
         this.labelCriticalCompressibility.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelCriticalVolume
         // 
         this.labelCriticalVolume.Location = new System.Drawing.Point(4, 100);
         this.labelCriticalVolume.Name = "labelCriticalVolume";
         this.labelCriticalVolume.Size = new System.Drawing.Size(128, 16);
         this.labelCriticalVolume.TabIndex = 13;
         this.labelCriticalVolume.Text = "Critical Volume:";
         this.labelCriticalVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelAccentricFactor
         // 
         this.labelAccentricFactor.Location = new System.Drawing.Point(4, 20);
         this.labelAccentricFactor.Name = "labelAccentricFactor";
         this.labelAccentricFactor.Size = new System.Drawing.Size(128, 16);
         this.labelAccentricFactor.TabIndex = 12;
         this.labelAccentricFactor.Text = "Accentric Factor:";
         this.labelAccentricFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // SubstanceDetailsControl
         // 
         this.Controls.Add(this.groupBoxCriticalProperties);
         this.Controls.Add(this.textBoxType);
         this.Controls.Add(this.textBoxUserDefined);
         this.Controls.Add(this.textBoxFormula);
         this.Controls.Add(this.textBoxMolarWeight);
         this.Controls.Add(this.textBoxCASRegistryNo);
         this.Controls.Add(this.textBoxName);
         this.Controls.Add(this.labelUserDefined);
         this.Controls.Add(this.labelFormula);
         this.Controls.Add(this.labelType);
         this.Controls.Add(this.labelCASRegistryNo);
         this.Controls.Add(this.labelMolarWeight);
         this.Controls.Add(this.labelName);
         this.Name = "SubstanceDetailsControl";
         this.Size = new System.Drawing.Size(484, 132);
         this.groupBoxCriticalProperties.ResumeLayout(false);
         this.groupBoxCriticalProperties.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }
		#endregion

      public void SetSubstance(Substance substance)
      {
         this.textBoxName.Text = substance.Name;
         // TODO the type
         this.textBoxFormula.Text = substance.Formula;
         this.textBoxMolarWeight.Text = substance.MolarWeight.ToString();
         this.textBoxCASRegistryNo.Text = substance.CASRegistryNo.ToString();
         if (substance.IsUserDefined)
            this.textBoxUserDefined.Text = "Yes";
         else
            this.textBoxUserDefined.Text = "No";
         // TODO...
      }
	}
}
