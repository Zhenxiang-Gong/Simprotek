using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.Plots;
using Prosimo;
using Prosimo.UnitSystems;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for Plot2DControl.
	/// </summary>
	public class Plot2DControl : System.Windows.Forms.UserControl
	{
      private bool inConstruction;
      private Plot2D p2d;

      public CheckBox CheckBoxDetails
      {
         get {return this.checkBoxDetails;}
      }

      private System.Windows.Forms.CheckBox checkBoxDetails;
      private System.Windows.Forms.GroupBox groupBoxX;
      private System.Windows.Forms.TextBox textBoxXMax;
      private System.Windows.Forms.Label labelXMin;
      private System.Windows.Forms.Label labelXMax;
      private System.Windows.Forms.TextBox textBoxXMin;
      private System.Windows.Forms.GroupBox groupBoxY;
      private System.Windows.Forms.TextBox textBoxYMax;
      private System.Windows.Forms.Label labelYMin;
      private System.Windows.Forms.Label labelYMax;
      private System.Windows.Forms.TextBox textBoxYMin;
      private System.Windows.Forms.GroupBox groupBoxParam;
      private System.Windows.Forms.TextBox textBoxParamMax;
      private System.Windows.Forms.Label labelParamMin;
      private System.Windows.Forms.Label labelParamMax;
      private System.Windows.Forms.TextBox textBoxParamMin;
      private System.Windows.Forms.Label labelParameterValues;
      private System.Windows.Forms.DomainUpDown domainUpDownParamValues;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Plot2DControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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
         this.groupBoxX = new System.Windows.Forms.GroupBox();
         this.textBoxXMax = new System.Windows.Forms.TextBox();
         this.labelXMin = new System.Windows.Forms.Label();
         this.labelXMax = new System.Windows.Forms.Label();
         this.textBoxXMin = new System.Windows.Forms.TextBox();
         this.groupBoxY = new System.Windows.Forms.GroupBox();
         this.textBoxYMax = new System.Windows.Forms.TextBox();
         this.labelYMin = new System.Windows.Forms.Label();
         this.labelYMax = new System.Windows.Forms.Label();
         this.textBoxYMin = new System.Windows.Forms.TextBox();
         this.checkBoxDetails = new System.Windows.Forms.CheckBox();
         this.groupBoxParam = new System.Windows.Forms.GroupBox();
         this.domainUpDownParamValues = new System.Windows.Forms.DomainUpDown();
         this.labelParameterValues = new System.Windows.Forms.Label();
         this.textBoxParamMax = new System.Windows.Forms.TextBox();
         this.labelParamMin = new System.Windows.Forms.Label();
         this.labelParamMax = new System.Windows.Forms.Label();
         this.textBoxParamMin = new System.Windows.Forms.TextBox();
         this.groupBoxX.SuspendLayout();
         this.groupBoxY.SuspendLayout();
         this.groupBoxParam.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxX
         // 
         this.groupBoxX.Controls.Add(this.textBoxXMax);
         this.groupBoxX.Controls.Add(this.labelXMin);
         this.groupBoxX.Controls.Add(this.labelXMax);
         this.groupBoxX.Controls.Add(this.textBoxXMin);
         this.groupBoxX.Location = new System.Drawing.Point(4, 4);
         this.groupBoxX.Name = "groupBoxX";
         this.groupBoxX.Size = new System.Drawing.Size(128, 60);
         this.groupBoxX.TabIndex = 3;
         this.groupBoxX.TabStop = false;
         this.groupBoxX.Text = "X Variable";
         // 
         // textBoxXMax
         // 
         this.textBoxXMax.Location = new System.Drawing.Point(44, 36);
         this.textBoxXMax.Name = "textBoxXMax";
         this.textBoxXMax.Size = new System.Drawing.Size(80, 20);
         this.textBoxXMax.TabIndex = 3;
         this.textBoxXMax.Text = "";
         this.textBoxXMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxXMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxXMax_Validating);
         this.textBoxXMax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // labelXMin
         // 
         this.labelXMin.Location = new System.Drawing.Point(8, 16);
         this.labelXMin.Name = "labelXMin";
         this.labelXMin.Size = new System.Drawing.Size(32, 20);
         this.labelXMin.TabIndex = 5;
         this.labelXMin.Text = "Min:";
         this.labelXMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelXMax
         // 
         this.labelXMax.Location = new System.Drawing.Point(8, 36);
         this.labelXMax.Name = "labelXMax";
         this.labelXMax.Size = new System.Drawing.Size(32, 20);
         this.labelXMax.TabIndex = 6;
         this.labelXMax.Text = "Max:";
         this.labelXMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxXMin
         // 
         this.textBoxXMin.Location = new System.Drawing.Point(44, 16);
         this.textBoxXMin.Name = "textBoxXMin";
         this.textBoxXMin.Size = new System.Drawing.Size(80, 20);
         this.textBoxXMin.TabIndex = 2;
         this.textBoxXMin.Text = "";
         this.textBoxXMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxXMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxXMin_Validating);
         this.textBoxXMin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // groupBoxY
         // 
         this.groupBoxY.Controls.Add(this.textBoxYMax);
         this.groupBoxY.Controls.Add(this.labelYMin);
         this.groupBoxY.Controls.Add(this.labelYMax);
         this.groupBoxY.Controls.Add(this.textBoxYMin);
         this.groupBoxY.Location = new System.Drawing.Point(4, 68);
         this.groupBoxY.Name = "groupBoxY";
         this.groupBoxY.Size = new System.Drawing.Size(128, 60);
         this.groupBoxY.TabIndex = 4;
         this.groupBoxY.TabStop = false;
         this.groupBoxY.Text = "Y Variable";
         // 
         // textBoxYMax
         // 
         this.textBoxYMax.Location = new System.Drawing.Point(44, 36);
         this.textBoxYMax.Name = "textBoxYMax";
         this.textBoxYMax.Size = new System.Drawing.Size(80, 20);
         this.textBoxYMax.TabIndex = 6;
         this.textBoxYMax.Text = "";
         this.textBoxYMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxYMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxYMax_Validating);
         this.textBoxYMax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // labelYMin
         // 
         this.labelYMin.Location = new System.Drawing.Point(8, 16);
         this.labelYMin.Name = "labelYMin";
         this.labelYMin.Size = new System.Drawing.Size(32, 20);
         this.labelYMin.TabIndex = 5;
         this.labelYMin.Text = "Min:";
         this.labelYMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelYMax
         // 
         this.labelYMax.Location = new System.Drawing.Point(8, 36);
         this.labelYMax.Name = "labelYMax";
         this.labelYMax.Size = new System.Drawing.Size(32, 20);
         this.labelYMax.TabIndex = 6;
         this.labelYMax.Text = "Max:";
         this.labelYMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxYMin
         // 
         this.textBoxYMin.Location = new System.Drawing.Point(44, 16);
         this.textBoxYMin.Name = "textBoxYMin";
         this.textBoxYMin.Size = new System.Drawing.Size(80, 20);
         this.textBoxYMin.TabIndex = 5;
         this.textBoxYMin.Text = "";
         this.textBoxYMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxYMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxYMin_Validating);
         this.textBoxYMin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // checkBoxDetails
         // 
         this.checkBoxDetails.Location = new System.Drawing.Point(4, 224);
         this.checkBoxDetails.Name = "checkBoxDetails";
         this.checkBoxDetails.Size = new System.Drawing.Size(92, 20);
         this.checkBoxDetails.TabIndex = 7;
         this.checkBoxDetails.Text = "Show Details";
         // 
         // groupBoxParam
         // 
         this.groupBoxParam.Controls.Add(this.domainUpDownParamValues);
         this.groupBoxParam.Controls.Add(this.labelParameterValues);
         this.groupBoxParam.Controls.Add(this.textBoxParamMax);
         this.groupBoxParam.Controls.Add(this.labelParamMin);
         this.groupBoxParam.Controls.Add(this.labelParamMax);
         this.groupBoxParam.Controls.Add(this.textBoxParamMin);
         this.groupBoxParam.Location = new System.Drawing.Point(4, 132);
         this.groupBoxParam.Name = "groupBoxParam";
         this.groupBoxParam.Size = new System.Drawing.Size(128, 84);
         this.groupBoxParam.TabIndex = 8;
         this.groupBoxParam.TabStop = false;
         this.groupBoxParam.Text = "Parameter";
         // 
         // domainUpDownParamValues
         // 
         this.domainUpDownParamValues.BackColor = System.Drawing.Color.White;
         this.domainUpDownParamValues.Items.Add("20");
         this.domainUpDownParamValues.Items.Add("19");
         this.domainUpDownParamValues.Items.Add("18");
         this.domainUpDownParamValues.Items.Add("17");
         this.domainUpDownParamValues.Items.Add("16");
         this.domainUpDownParamValues.Items.Add("15");
         this.domainUpDownParamValues.Items.Add("14");
         this.domainUpDownParamValues.Items.Add("13");
         this.domainUpDownParamValues.Items.Add("12");
         this.domainUpDownParamValues.Items.Add("11");
         this.domainUpDownParamValues.Items.Add("10");
         this.domainUpDownParamValues.Items.Add("9");
         this.domainUpDownParamValues.Items.Add("8");
         this.domainUpDownParamValues.Items.Add("7");
         this.domainUpDownParamValues.Items.Add("6");
         this.domainUpDownParamValues.Items.Add("5");
         this.domainUpDownParamValues.Items.Add("4");
         this.domainUpDownParamValues.Items.Add("3");
         this.domainUpDownParamValues.Items.Add("2");
         this.domainUpDownParamValues.Items.Add("1");
         this.domainUpDownParamValues.Location = new System.Drawing.Point(86, 60);
         this.domainUpDownParamValues.Name = "domainUpDownParamValues";
         this.domainUpDownParamValues.ReadOnly = true;
         this.domainUpDownParamValues.Size = new System.Drawing.Size(40, 20);
         this.domainUpDownParamValues.TabIndex = 9;
         this.domainUpDownParamValues.SelectedItemChanged += new System.EventHandler(this.domainUpDownParamValues_SelectedItemChanged);
         // 
         // labelParameterValues
         // 
         this.labelParameterValues.Location = new System.Drawing.Point(8, 62);
         this.labelParameterValues.Name = "labelParameterValues";
         this.labelParameterValues.Size = new System.Drawing.Size(76, 16);
         this.labelParameterValues.TabIndex = 7;
         this.labelParameterValues.Text = "No.of Values:";
         this.labelParameterValues.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // textBoxParamMax
         // 
         this.textBoxParamMax.Location = new System.Drawing.Point(44, 36);
         this.textBoxParamMax.Name = "textBoxParamMax";
         this.textBoxParamMax.Size = new System.Drawing.Size(80, 20);
         this.textBoxParamMax.TabIndex = 3;
         this.textBoxParamMax.Text = "";
         this.textBoxParamMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxParamMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxParamMax_Validating);
         this.textBoxParamMax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // labelParamMin
         // 
         this.labelParamMin.Location = new System.Drawing.Point(8, 16);
         this.labelParamMin.Name = "labelParamMin";
         this.labelParamMin.Size = new System.Drawing.Size(32, 20);
         this.labelParamMin.TabIndex = 5;
         this.labelParamMin.Text = "Min:";
         this.labelParamMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelParamMax
         // 
         this.labelParamMax.Location = new System.Drawing.Point(8, 36);
         this.labelParamMax.Name = "labelParamMax";
         this.labelParamMax.Size = new System.Drawing.Size(32, 20);
         this.labelParamMax.TabIndex = 6;
         this.labelParamMax.Text = "Max:";
         this.labelParamMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxParamMin
         // 
         this.textBoxParamMin.Location = new System.Drawing.Point(44, 16);
         this.textBoxParamMin.Name = "textBoxParamMin";
         this.textBoxParamMin.Size = new System.Drawing.Size(80, 20);
         this.textBoxParamMin.TabIndex = 2;
         this.textBoxParamMin.Text = "";
         this.textBoxParamMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxParamMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxParamMin_Validating);
         this.textBoxParamMin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // Plot2DControl
         // 
         this.Controls.Add(this.groupBoxParam);
         this.Controls.Add(this.checkBoxDetails);
         this.Controls.Add(this.groupBoxY);
         this.Controls.Add(this.groupBoxX);
         this.Name = "Plot2DControl";
         this.Size = new System.Drawing.Size(132, 244);
         this.groupBoxX.ResumeLayout(false);
         this.groupBoxY.ResumeLayout(false);
         this.groupBoxParam.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void SetPlot2D(Plot2D p2d)
      {
         this.p2d = p2d;
         if (p2d != null)
         {
            this.SetPlotInfo(p2d);
         }
         else
            this.ClearPlotInfo();
      }

      private void SetPlotInfo(Plot2D p2d)
      {
         this.inConstruction = true;

         this.domainUpDownParamValues.Enabled = true;
         this.textBoxParamMax.Enabled = true;
         this.textBoxParamMin.Enabled = true;
         this.textBoxXMax.Enabled = true;
         this.textBoxXMin.Enabled = true;
         this.textBoxYMax.Enabled = true;
         this.textBoxYMin.Enabled = true;
         this.checkBoxDetails.Enabled = true;

         // TODO convert from SI
         PhysicalQuantity pq = p2d.PlotVariableX.Variable.Type;
         double newVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, p2d.PlotVariableX.Max);
         this.textBoxXMax.Text = newVal.ToString();
         newVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, p2d.PlotVariableX.Min);
         this.textBoxXMin.Text = newVal.ToString();

         pq = p2d.PlotVariableY.Variable.Type;
         newVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, p2d.PlotVariableY.Max);
         this.textBoxYMax.Text = newVal.ToString();
         newVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, p2d.PlotVariableY.Min);
         this.textBoxYMin.Text = newVal.ToString();

         if (p2d.IncludePlotParameter)
         {
            this.groupBoxParam.Visible = true;
            this.domainUpDownParamValues.SelectedItem = p2d.PlotParameter.NumberOfValues.ToString();

            // TODO convert from SI
            pq = p2d.PlotParameter.Variable.Type;
            newVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, p2d.PlotParameter.Max);
            this.textBoxParamMax.Text = newVal.ToString();
            newVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, p2d.PlotParameter.Min);
            this.textBoxParamMin.Text = newVal.ToString();
         }
         else
         {
            this.groupBoxParam.Visible = false;
         }
         
         this.inConstruction = false;
      }

      private void ClearPlotInfo()
      {
         this.inConstruction = true;

         this.domainUpDownParamValues.SelectedItem = "1";
         this.textBoxParamMax.Text = "";
         this.textBoxParamMin.Text = "";
         this.textBoxXMax.Text = "";
         this.textBoxXMin.Text = "";
         this.textBoxYMax.Text = "";
         this.textBoxYMin.Text = "";

         this.groupBoxParam.Visible = true;

         this.domainUpDownParamValues.Enabled = false;
         this.textBoxParamMax.Enabled = false;
         this.textBoxParamMin.Enabled = false;
         this.textBoxXMax.Enabled = false;
         this.textBoxXMin.Enabled = false;
         this.textBoxYMax.Enabled = false;
         this.textBoxYMin.Enabled = false;
         this.checkBoxDetails.Enabled = false;
         
         this.inConstruction = false;
      }
      
      private void textBoxXMin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            TextBox tb = (TextBox)sender;
            if (UI.HasNumericValue(tb))
            {
               double newVal = UI.GetNumericValueAsDouble(tb, e);
               if (newVal != this.p2d.PlotVariableX.Min)
               {
                  PhysicalQuantity pq = this.p2d.PlotVariableX.Variable.Type;
                  double convertedVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)newVal);
                  this.p2d.SetOxMin((double)convertedVal);
               }
            }
            else
               e.Cancel = true;
         }
      }

      private void textBoxXMax_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            TextBox tb = (TextBox)sender;
            if (UI.HasNumericValue(tb))
            {
               double newVal = UI.GetNumericValueAsDouble(tb, e);
               if (newVal != this.p2d.PlotVariableX.Max)
               {
                  PhysicalQuantity pq = this.p2d.PlotVariableX.Variable.Type;
                  double convertedVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)newVal);
                  this.p2d.SetOxMax((double)convertedVal);
               }
            }
            else
               e.Cancel = true;
         }
      }

      private void textBoxYMin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            TextBox tb = (TextBox)sender;
            if (UI.HasNumericValue(tb))
            {
               double newVal = UI.GetNumericValueAsDouble(tb, e);
               if (newVal != this.p2d.PlotVariableY.Min)
               {
                  PhysicalQuantity pq = this.p2d.PlotVariableY.Variable.Type;
                  double convertedVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)newVal);
                  this.p2d.SetOyMin((double)convertedVal);
               }
            }
            else
               e.Cancel = true;
         }
      }

      private void textBoxYMax_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            TextBox tb = (TextBox)sender;
            if (UI.HasNumericValue(tb))
            {
               double newVal = UI.GetNumericValueAsDouble(tb, e);
               if (newVal != this.p2d.PlotVariableY.Max)
               {
                  PhysicalQuantity pq = this.p2d.PlotVariableY.Variable.Type;
                  double convertedVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)newVal);
                  this.p2d.SetOyMax((double)convertedVal);
               }
            }
            else
               e.Cancel = true;
         }
      }

      private void textBoxParamMin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            TextBox tb = (TextBox)sender;
            if (UI.HasNumericValue(tb))
            {
               double newVal = UI.GetNumericValueAsDouble(tb, e);
               if (newVal != this.p2d.PlotParameter.Min)
               {
                  PhysicalQuantity pq = this.p2d.PlotParameter.Variable.Type;
                  double convertedVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)newVal);
                  this.p2d.SetParameterMin((double)convertedVal);
               }
            }
            else
               e.Cancel = true;
         }
      }

      private void textBoxParamMax_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            TextBox tb = (TextBox)sender;
            if (UI.HasNumericValue(tb))
            {
               double newVal = UI.GetNumericValueAsDouble(tb, e);
               if (newVal != this.p2d.PlotParameter.Max)
               {
                  PhysicalQuantity pq = this.p2d.PlotParameter.Variable.Type;
                  double convertedVal = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)newVal);
                  this.p2d.SetParameterMax((double)convertedVal);
               }
            }
            else
               e.Cancel = true;
         }
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (!this.inConstruction)
         {
            ArrayList list = new ArrayList();
            list.Add(this.textBoxXMin);
            list.Add(this.textBoxXMax);
            list.Add(this.textBoxYMin);
            list.Add(this.textBoxYMax);
            list.Add(this.textBoxParamMin);
            list.Add(this.textBoxParamMax);

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

      private void domainUpDownParamValues_SelectedItemChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            int noOfVals = 1;
            try
            {
               noOfVals = Int32.Parse((string)this.domainUpDownParamValues.SelectedItem);
            }
            catch (FormatException)
            {
            }
            if (this.p2d != null)
            {
               ErrorMessage error = this.p2d.SetNumberOfParameterValues(noOfVals);
               if (error != null)
               {
                  UI.ShowError(error);
                  this.domainUpDownParamValues.SelectedItem = p2d.PlotParameter.NumberOfValues.ToString();
               }
            }
         }
      }
	}
}
