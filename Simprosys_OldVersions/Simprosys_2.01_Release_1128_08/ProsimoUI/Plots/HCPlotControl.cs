using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for HCPlotControl.
	/// </summary>
	public class HCPlotControl : System.Windows.Forms.UserControl
	{
      private IPlot iPlot;
      private ProsimoUI.Plots.HCPlotGraph plotGraph;
      public HCPlotGraph Graph
      {
         get {return plotGraph;}
      }

      public AxisVariable OxVariable
      {
         get {return (AxisVariable)this.comboBoxOxVariables.SelectedItem;}
      }

      public AxisVariable OyVariable
      {
         get {return (AxisVariable)this.comboBoxOyVariables.SelectedItem;}
      }

      private System.Windows.Forms.GroupBox groupBoxUpdate;
      private System.Windows.Forms.ComboBox comboBoxOyVariables;
      private System.Windows.Forms.ComboBox comboBoxOxVariables;
      private System.Windows.Forms.CheckedListBox checkedListBoxFamilies;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Label labelRangeXMin;
      private System.Windows.Forms.GroupBox groupBoxX;
      private System.Windows.Forms.GroupBox groupBoxRangeX;
      private System.Windows.Forms.Label labelRangeXMax;
      private System.Windows.Forms.TextBox textBoxRangeXMin;
      private System.Windows.Forms.TextBox textBoxRangeXMax;
      private System.Windows.Forms.GroupBox groupBoxY;
      private System.Windows.Forms.GroupBox groupBoxRangeY;
      private System.Windows.Forms.TextBox textBoxRangeYMax;
      private System.Windows.Forms.Label labelRangeYMin;
      private System.Windows.Forms.Label labelRangeYMax;
      private System.Windows.Forms.TextBox textBoxRangeYMin;
      private System.Windows.Forms.CheckBox checkBoxDetails;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HCPlotControl()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.iPlot != null)
            this.iPlot.PlotChanged -= new PlotChangedEventHandler(iPlot_PlotChanged);
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
         this.checkedListBoxFamilies = new System.Windows.Forms.CheckedListBox();
         this.groupBoxUpdate = new System.Windows.Forms.GroupBox();
         this.groupBoxY = new System.Windows.Forms.GroupBox();
         this.groupBoxRangeY = new System.Windows.Forms.GroupBox();
         this.textBoxRangeYMax = new System.Windows.Forms.TextBox();
         this.labelRangeYMin = new System.Windows.Forms.Label();
         this.labelRangeYMax = new System.Windows.Forms.Label();
         this.textBoxRangeYMin = new System.Windows.Forms.TextBox();
         this.comboBoxOyVariables = new System.Windows.Forms.ComboBox();
         this.groupBoxX = new System.Windows.Forms.GroupBox();
         this.groupBoxRangeX = new System.Windows.Forms.GroupBox();
         this.textBoxRangeXMax = new System.Windows.Forms.TextBox();
         this.labelRangeXMin = new System.Windows.Forms.Label();
         this.labelRangeXMax = new System.Windows.Forms.Label();
         this.textBoxRangeXMin = new System.Windows.Forms.TextBox();
         this.comboBoxOxVariables = new System.Windows.Forms.ComboBox();
         this.plotGraph = new ProsimoUI.Plots.HCPlotGraph();
         this.panel = new System.Windows.Forms.Panel();
         this.checkBoxDetails = new System.Windows.Forms.CheckBox();
         this.groupBoxUpdate.SuspendLayout();
         this.groupBoxY.SuspendLayout();
         this.groupBoxRangeY.SuspendLayout();
         this.groupBoxX.SuspendLayout();
         this.groupBoxRangeX.SuspendLayout();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // checkedListBoxFamilies
         // 
         this.checkedListBoxFamilies.CheckOnClick = true;
         this.checkedListBoxFamilies.HorizontalScrollbar = true;
         this.checkedListBoxFamilies.Location = new System.Drawing.Point(4, 324);
         this.checkedListBoxFamilies.Name = "checkedListBoxFamilies";
         this.checkedListBoxFamilies.Size = new System.Drawing.Size(180, 94);
         this.checkedListBoxFamilies.Sorted = true;
         this.checkedListBoxFamilies.TabIndex = 5;
         this.checkedListBoxFamilies.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxFamilies_ItemCheck);
         // 
         // groupBoxUpdate
         // 
         this.groupBoxUpdate.Controls.Add(this.groupBoxY);
         this.groupBoxUpdate.Controls.Add(this.groupBoxX);
         this.groupBoxUpdate.Location = new System.Drawing.Point(4, 12);
         this.groupBoxUpdate.Name = "groupBoxUpdate";
         this.groupBoxUpdate.Size = new System.Drawing.Size(180, 304);
         this.groupBoxUpdate.TabIndex = 4;
         this.groupBoxUpdate.TabStop = false;
         this.groupBoxUpdate.Text = "Variables";
         // 
         // groupBoxY
         // 
         this.groupBoxY.Controls.Add(this.groupBoxRangeY);
         this.groupBoxY.Controls.Add(this.comboBoxOyVariables);
         this.groupBoxY.Location = new System.Drawing.Point(8, 160);
         this.groupBoxY.Name = "groupBoxY";
         this.groupBoxY.Size = new System.Drawing.Size(164, 140);
         this.groupBoxY.TabIndex = 8;
         this.groupBoxY.TabStop = false;
         this.groupBoxY.Text = "Oy Variable";
         // 
         // groupBoxRangeY
         // 
         this.groupBoxRangeY.Controls.Add(this.textBoxRangeYMax);
         this.groupBoxRangeY.Controls.Add(this.labelRangeYMin);
         this.groupBoxRangeY.Controls.Add(this.labelRangeYMax);
         this.groupBoxRangeY.Controls.Add(this.textBoxRangeYMin);
         this.groupBoxRangeY.Location = new System.Drawing.Point(10, 48);
         this.groupBoxRangeY.Name = "groupBoxRangeY";
         this.groupBoxRangeY.Size = new System.Drawing.Size(144, 84);
         this.groupBoxRangeY.TabIndex = 3;
         this.groupBoxRangeY.TabStop = false;
         this.groupBoxRangeY.Text = "Range";
         // 
         // textBoxRangeYMax
         // 
         this.textBoxRangeYMax.Location = new System.Drawing.Point(44, 56);
         this.textBoxRangeYMax.Name = "textBoxRangeYMax";
         this.textBoxRangeYMax.Size = new System.Drawing.Size(92, 20);
         this.textBoxRangeYMax.TabIndex = 6;
         this.textBoxRangeYMax.Text = "";
         this.textBoxRangeYMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxRangeYMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRangeYMax_Validating);
         this.textBoxRangeYMax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // labelRangeYMin
         // 
         this.labelRangeYMin.Location = new System.Drawing.Point(8, 20);
         this.labelRangeYMin.Name = "labelRangeYMin";
         this.labelRangeYMin.Size = new System.Drawing.Size(32, 23);
         this.labelRangeYMin.TabIndex = 5;
         this.labelRangeYMin.Text = "Min:";
         this.labelRangeYMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelRangeYMax
         // 
         this.labelRangeYMax.Location = new System.Drawing.Point(8, 52);
         this.labelRangeYMax.Name = "labelRangeYMax";
         this.labelRangeYMax.Size = new System.Drawing.Size(32, 23);
         this.labelRangeYMax.TabIndex = 6;
         this.labelRangeYMax.Text = "Max:";
         this.labelRangeYMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxRangeYMin
         // 
         this.textBoxRangeYMin.Location = new System.Drawing.Point(44, 24);
         this.textBoxRangeYMin.Name = "textBoxRangeYMin";
         this.textBoxRangeYMin.Size = new System.Drawing.Size(92, 20);
         this.textBoxRangeYMin.TabIndex = 5;
         this.textBoxRangeYMin.Text = "";
         this.textBoxRangeYMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxRangeYMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRangeYMin_Validating);
         this.textBoxRangeYMin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // comboBoxOyVariables
         // 
         this.comboBoxOyVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxOyVariables.Location = new System.Drawing.Point(8, 20);
         this.comboBoxOyVariables.Name = "comboBoxOyVariables";
         this.comboBoxOyVariables.Size = new System.Drawing.Size(148, 21);
         this.comboBoxOyVariables.TabIndex = 4;
         this.comboBoxOyVariables.SelectedIndexChanged += new System.EventHandler(this.comboBoxOyVariables_SelectedIndexChanged);
         // 
         // groupBoxX
         // 
         this.groupBoxX.Controls.Add(this.groupBoxRangeX);
         this.groupBoxX.Controls.Add(this.comboBoxOxVariables);
         this.groupBoxX.Location = new System.Drawing.Point(8, 16);
         this.groupBoxX.Name = "groupBoxX";
         this.groupBoxX.Size = new System.Drawing.Size(164, 140);
         this.groupBoxX.TabIndex = 7;
         this.groupBoxX.TabStop = false;
         this.groupBoxX.Text = "Ox Variable";
         // 
         // groupBoxRangeX
         // 
         this.groupBoxRangeX.Controls.Add(this.textBoxRangeXMax);
         this.groupBoxRangeX.Controls.Add(this.labelRangeXMin);
         this.groupBoxRangeX.Controls.Add(this.labelRangeXMax);
         this.groupBoxRangeX.Controls.Add(this.textBoxRangeXMin);
         this.groupBoxRangeX.Location = new System.Drawing.Point(12, 48);
         this.groupBoxRangeX.Name = "groupBoxRangeX";
         this.groupBoxRangeX.Size = new System.Drawing.Size(144, 84);
         this.groupBoxRangeX.TabIndex = 2;
         this.groupBoxRangeX.TabStop = false;
         this.groupBoxRangeX.Text = "Range";
         // 
         // textBoxRangeXMax
         // 
         this.textBoxRangeXMax.Location = new System.Drawing.Point(44, 56);
         this.textBoxRangeXMax.Name = "textBoxRangeXMax";
         this.textBoxRangeXMax.Size = new System.Drawing.Size(92, 20);
         this.textBoxRangeXMax.TabIndex = 3;
         this.textBoxRangeXMax.Text = "";
         this.textBoxRangeXMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxRangeXMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRangeXMax_Validating);
         this.textBoxRangeXMax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // labelRangeXMin
         // 
         this.labelRangeXMin.Location = new System.Drawing.Point(8, 20);
         this.labelRangeXMin.Name = "labelRangeXMin";
         this.labelRangeXMin.Size = new System.Drawing.Size(32, 23);
         this.labelRangeXMin.TabIndex = 5;
         this.labelRangeXMin.Text = "Min:";
         this.labelRangeXMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelRangeXMax
         // 
         this.labelRangeXMax.Location = new System.Drawing.Point(8, 52);
         this.labelRangeXMax.Name = "labelRangeXMax";
         this.labelRangeXMax.Size = new System.Drawing.Size(32, 23);
         this.labelRangeXMax.TabIndex = 6;
         this.labelRangeXMax.Text = "Max:";
         this.labelRangeXMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxRangeXMin
         // 
         this.textBoxRangeXMin.Location = new System.Drawing.Point(44, 24);
         this.textBoxRangeXMin.Name = "textBoxRangeXMin";
         this.textBoxRangeXMin.Size = new System.Drawing.Size(92, 20);
         this.textBoxRangeXMin.TabIndex = 2;
         this.textBoxRangeXMin.Text = "";
         this.textBoxRangeXMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxRangeXMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRangeXMin_Validating);
         this.textBoxRangeXMin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // comboBoxOxVariables
         // 
         this.comboBoxOxVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxOxVariables.Location = new System.Drawing.Point(8, 20);
         this.comboBoxOxVariables.Name = "comboBoxOxVariables";
         this.comboBoxOxVariables.Size = new System.Drawing.Size(148, 21);
         this.comboBoxOxVariables.TabIndex = 1;
         this.comboBoxOxVariables.SelectedIndexChanged += new System.EventHandler(this.comboBoxOxVariables_SelectedIndexChanged);
         // 
         // plotGraph
         // 
         this.plotGraph.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
         this.plotGraph.EnableMouseActivity = false;
         this.plotGraph.Location = new System.Drawing.Point(188, 8);
         this.plotGraph.Name = "plotGraph";
         this.plotGraph.NumericFormatString = "F2";
         this.plotGraph.OtherData = null;
         this.plotGraph.OxVariable = null;
         this.plotGraph.OyVariable = null;
         this.plotGraph.PlotData = null;
         this.plotGraph.Points = null;
         this.plotGraph.ShowDetails = false;
         this.plotGraph.ShowOtherData = false;
         this.plotGraph.ShowPoints = false;
         this.plotGraph.Size = new System.Drawing.Size(400, 440);
         this.plotGraph.TabIndex = 0;
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.checkBoxDetails);
         this.panel.Controls.Add(this.groupBoxUpdate);
         this.panel.Controls.Add(this.plotGraph);
         this.panel.Controls.Add(this.checkedListBoxFamilies);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(596, 456);
         this.panel.TabIndex = 6;
         // 
         // checkBoxDetails
         // 
         this.checkBoxDetails.Location = new System.Drawing.Point(8, 424);
         this.checkBoxDetails.Name = "checkBoxDetails";
         this.checkBoxDetails.Size = new System.Drawing.Size(92, 24);
         this.checkBoxDetails.TabIndex = 6;
         this.checkBoxDetails.Text = "Show Details";
         this.checkBoxDetails.CheckedChanged += new System.EventHandler(this.checkBoxDetails_CheckedChanged);
         // 
         // HCPlotControl
         // 
         this.Controls.Add(this.panel);
         this.Name = "HCPlotControl";
         this.Size = new System.Drawing.Size(596, 456);
         this.groupBoxUpdate.ResumeLayout(false);
         this.groupBoxY.ResumeLayout(false);
         this.groupBoxRangeY.ResumeLayout(false);
         this.groupBoxX.ResumeLayout(false);
         this.groupBoxRangeX.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializePlotControl(IPlot iPlot)
      {
         this.iPlot = iPlot;
         ArrayList oxVars = iPlot.GetOxVariables();
         ArrayList oyVars = iPlot.GetOyVariables();

         this.comboBoxOxVariables.Items.Clear();
         this.comboBoxOyVariables.Items.Clear();

         // populate the ox variables
         IEnumerator e1 = oxVars.GetEnumerator();
         while (e1.MoveNext())
         {
            this.comboBoxOxVariables.Items.Add(e1.Current);
         }
         if (oxVars != null && oxVars.Count > 0)
            this.comboBoxOxVariables.SelectedIndex = 0;

         // populate the oy variables
         IEnumerator e2 = oyVars.GetEnumerator();
         while (e2.MoveNext())
         {
            this.comboBoxOyVariables.Items.Add(e2.Current);
         }
         if (oyVars != null && oyVars.Count > 0)
            this.comboBoxOyVariables.SelectedIndex = 0;

         PlotData plotData = iPlot.GetPlotData(this.OxVariable, this.OyVariable);

         // update the ranges
         this.UpdateRangesOnUI(this.OxVariable, this.OyVariable);

         // update the plot
         this.plotGraph.UpdatePlot(plotData, this.OxVariable, this.OyVariable);

         //
         this.UpdateFamiliesList(plotData);
         
         if (this.iPlot != null)
            this.iPlot.PlotChanged += new PlotChangedEventHandler(iPlot_PlotChanged);
      }

      private void UpdateFamiliesList(PlotData plotData)
      {
         this.checkedListBoxFamilies.Items.Clear();
         if (plotData != null)
         {
            IList list = plotData.CurveFamilies;
            IEnumerator e = list.GetEnumerator();
            while (e.MoveNext())
            {
               CurveFamily family = (CurveFamily)e.Current;
               string s = family.Name;
               if (family.Unit != null && !family.Unit.Trim().Equals(""))
                  s += " [" + family.Unit + "]";
               this.checkedListBoxFamilies.Items.Add(s);
               int i = this.checkedListBoxFamilies.Items.IndexOf(s);
               this.checkedListBoxFamilies.SetItemChecked(i, family.IsShownOnPlot);
            }
         }
      }

      private void checkedListBoxFamilies_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
      {
         bool isChecked = false;
         if (e.NewValue.Equals(CheckState.Checked))
            isChecked = true;
         int idx = e.Index;
         string s = (string)this.checkedListBoxFamilies.Items[idx];
         CurveFamily curveFamily = this.plotGraph.PlotData.GetCurveFamily(s);
         if (curveFamily != null)
            curveFamily.IsShownOnPlot = isChecked;
         this.RefreshPlot();
      }

      private void comboBoxOxVariables_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         this.iPlot.SetOxVariable(this.OxVariable);
      }

      private void comboBoxOyVariables_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         this.iPlot.SetOyVariable(this.OyVariable);
      }

      private void checkBoxDetails_CheckedChanged(object sender, System.EventArgs e)
      {
         CheckBox cb = (CheckBox)sender;
         this.plotGraph.ShowDetails = cb.Checked;
         this.RefreshPlot();
      }

      public void RefreshPlot()
      {
         this.plotGraph.Invalidate();
      }

      private void iPlot_PlotChanged(AxisVariable ox, AxisVariable oy, PlotData data)
      {
         this.Graph.UpdatePlot(data, ox, oy);
         this.UpdateFamiliesList(data);
         this.UpdateRangesOnUI(ox, oy);

         this.comboBoxOxVariables.SelectedItem = ox;
         this.comboBoxOyVariables.SelectedItem = oy;
      }

      private void UpdateRangesOnUI(AxisVariable ox, AxisVariable oy)
      {
         this.textBoxRangeXMin.Text = ox.Range.Min.ToString();
         this.textBoxRangeXMax.Text = ox.Range.Max.ToString();
         this.textBoxRangeYMin.Text = oy.Range.Min.ToString();
         this.textBoxRangeYMax.Text = oy.Range.Max.ToString();
      }

      private void textBoxRangeXMin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         TextBox tb = (TextBox)sender;
         if (UI.HasNumericValue(tb))
         {
            float xMin = UI.GetNumericValueAsFloat(tb, e);
            if (xMin != this.OxVariable.Range.Min)
            {
               this.OxVariable.Range = new Range(xMin, this.OxVariable.Range.Max);
               this.iPlot.SetOxMin(xMin);
            }
         }
         else
            e.Cancel = true;
      }

      private void textBoxRangeXMax_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         TextBox tb = (TextBox)sender;
         if (UI.HasNumericValue(tb))
         {
            float xMax = UI.GetNumericValueAsFloat(tb, e);
            if (xMax != this.OxVariable.Range.Max)
            {
               this.OxVariable.Range = new Range(this.OxVariable.Range.Min, xMax);
               this.iPlot.SetOxMax(xMax);
            }
         }
         else
            e.Cancel = true;
      }

      private void textBoxRangeYMin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         TextBox tb = (TextBox)sender;
         if (UI.HasNumericValue(tb))
         {
            float yMin = UI.GetNumericValueAsFloat(tb, e);
            if (yMin != this.OyVariable.Range.Min)
            {
               this.OyVariable.Range = new Range(yMin, this.OyVariable.Range.Max);
               this.iPlot.SetOyMin(yMin);
            }
         }
         else
            e.Cancel = true;
      }

      private void textBoxRangeYMax_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         TextBox tb = (TextBox)sender;
         if (UI.HasNumericValue(tb))
         {
            float yMax = UI.GetNumericValueAsFloat(tb, e);
            if (yMax != this.OyVariable.Range.Max)
            {
               this.OyVariable.Range = new Range(this.OyVariable.Range.Min, yMax);
               this.iPlot.SetOyMax(yMax);
            }
         }
         else
            e.Cancel = true;
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxRangeXMin);
         list.Add(this.textBoxRangeXMax);
         list.Add(this.textBoxRangeYMin);
         list.Add(this.textBoxRangeYMax);

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
