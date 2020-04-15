using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.Plots;
using Prosimo;
using Prosimo.UnitSystems;

namespace ProsimoUI.Plots
{
   /// <summary>
   /// Summary description for ViewPlot2DForm.
   /// </summary>
   public class ViewPlot2DForm : System.Windows.Forms.Form
   {
      private Hashtable hashtableGraphPlot;
      private Plot2D selectedP2D;
      private Plot2DGraph selectedP2DGraph;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Panel panelPlots;
      private System.Windows.Forms.Panel panelLeft;
      private ProsimoUI.Plots.Plot2DControl plot2DControl;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ViewPlot2DForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public ViewPlot2DForm(ArrayList list, PlotGraphSize size)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         int d = 4;
         int x = d;
         int y = 4;
         int i = 0;
         this.hashtableGraphPlot = new Hashtable();
         IEnumerator en = list.GetEnumerator();
         while (en.MoveNext())
         {
            i++;
            Plot2D p2d = (Plot2D)en.Current;
            Plot2DGraph p2dGraph = new Plot2DGraph();
            this.AdjustGraphSize(p2dGraph, size);

            AxisVariable avX = UI.ConvertToAxisVariable(p2d.PlotVariableX);
            AxisVariable avY = UI.ConvertToAxisVariable(p2d.PlotVariableY);
            PlotData pd = this.ConvertToPlotData(p2d);
            p2dGraph.UpdatePlot(pd, avX, avY);
            
            x = i*d + (i-1)* p2dGraph.Width;
            p2dGraph.Location = new Point(x, y);
            
            this.panelPlots.Controls.Add(p2dGraph);
            this.hashtableGraphPlot.Add(p2dGraph, p2d);

            p2dGraph.PlotSelected += new PlotSelectedEventHandler(p2dGraph_PlotSelected);
            p2d.Plot2DChanged += new Plot2DChangedEventHandler(p2d_Plot2DChanged);
         }

         if (size == PlotGraphSize.Medium)
         {
            this.panelPlots.Height -= 135;
            this.Height -= 150;
         }
         else if (size == PlotGraphSize.Small)
         {
            this.panelPlots.Height -= 208;
            this.Height -= 222;
         }

         this.UnselectAllPlots();
         this.plot2DControl.CheckBoxDetails.CheckedChanged += new EventHandler(CheckBoxDetails_CheckedChanged);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         IDictionaryEnumerator en = this.hashtableGraphPlot.GetEnumerator();
         while (en.MoveNext())
         {
            Plot2DGraph p2dGraph = (Plot2DGraph)en.Key;
            Plot2D p2d = (Plot2D)en.Value;
            p2dGraph.PlotSelected -= new PlotSelectedEventHandler(p2dGraph_PlotSelected);
            p2d.Plot2DChanged -= new Plot2DChangedEventHandler(p2d_Plot2DChanged);
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ViewPlot2DForm));
         this.panel = new System.Windows.Forms.Panel();
         this.panelLeft = new System.Windows.Forms.Panel();
         this.plot2DControl = new ProsimoUI.Plots.Plot2DControl();
         this.panelPlots = new System.Windows.Forms.Panel();
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel.SuspendLayout();
         this.panelLeft.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.panelLeft);
         this.panel.Controls.Add(this.panelPlots);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(562, 461);
         this.panel.TabIndex = 0;
         // 
         // panelLeft
         // 
         this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelLeft.Controls.Add(this.plot2DControl);
         this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.panelLeft.Location = new System.Drawing.Point(0, 0);
         this.panelLeft.Name = "panelLeft";
         this.panelLeft.Size = new System.Drawing.Size(136, 457);
         this.panelLeft.TabIndex = 2;
         this.panelLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLeft_MouseDown);
         // 
         // plot2DControl
         // 
         this.plot2DControl.Location = new System.Drawing.Point(0, 0);
         this.plot2DControl.Name = "plot2DControl";
         this.plot2DControl.Size = new System.Drawing.Size(132, 244);
         this.plot2DControl.TabIndex = 0;
         // 
         // panelPlots
         // 
         this.panelPlots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.panelPlots.AutoScroll = true;
         this.panelPlots.BackColor = System.Drawing.SystemColors.Control;
         this.panelPlots.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelPlots.Location = new System.Drawing.Point(140, 4);
         this.panelPlots.Name = "panelPlots";
         this.panelPlots.Size = new System.Drawing.Size(410, 448);
         this.panelPlots.TabIndex = 1;
         this.panelPlots.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPlots_MouseDown);
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // ViewPlot2DForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(562, 461);
         this.Controls.Add(this.panel);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "ViewPlot2DForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "View Plots";
         this.panel.ResumeLayout(false);
         this.panelLeft.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private void AdjustGraphSize(Plot2DGraph p2dGraph, PlotGraphSize size)
      {
         if (size == PlotGraphSize.Medium)
         {
            p2dGraph.Width = (int)((p2dGraph.Width * 2)/3);
            p2dGraph.Height = (int)((p2dGraph.Height * 2)/3);
         }
         else if (size == PlotGraphSize.Small)
         {
            p2dGraph.Width = (int)(p2dGraph.Width / 2);
            p2dGraph.Height = (int)(p2dGraph.Height / 2);
         }
      }

      private PlotData ConvertToPlotData(Plot2D p2d)
      {
         PhysicalQuantity xPhysicalQuantity = p2d.PlotVariableX.Variable.Type;
         PhysicalQuantity yPhysicalQuantity = p2d.PlotVariableY.Variable.Type;

         if (p2d.CurveFamily.Name == null)
            p2d.CurveFamily.Name = p2d.Name;
         
         PlotData plotData = new PlotData();
         plotData.Name = p2d.Name;
         CurveFamilyF[] oldFamilies = new CurveFamilyF[1];
         oldFamilies[0] = p2d.CurveFamily;
         CurveFamily[] newFamilies = UI.ConvertCurveFamilies(oldFamilies, xPhysicalQuantity, yPhysicalQuantity);
         plotData.CurveFamilies = newFamilies;
         return plotData;
      }

      private void panelLeft_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.UnselectAllPlots();
      }

      private void p2dGraph_PlotSelected(Plot2DGraph plotGraph)
      {
         IDictionaryEnumerator en = this.hashtableGraphPlot.GetEnumerator();
         while (en.MoveNext())
         {
            Plot2DGraph p2dGraph = (Plot2DGraph)en.Key;
            if (p2dGraph != plotGraph)
               p2dGraph.IsSelected = false;
         }

         this.selectedP2DGraph = plotGraph;
         this.selectedP2D = (Plot2D)this.hashtableGraphPlot[plotGraph];
         this.plot2DControl.SetPlot2D(this.selectedP2D);
         this.selectedP2DGraph.ShowDetails = this.plot2DControl.CheckBoxDetails.Checked;
         this.selectedP2DGraph.Invalidate();
      }

      private void panelPlots_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.UnselectAllPlots();      
      }

      private void UnselectAllPlots()
      {
         IDictionaryEnumerator en = this.hashtableGraphPlot.GetEnumerator();
         while (en.MoveNext())
         {
            Plot2DGraph p2dGraph = (Plot2DGraph)en.Key;
            p2dGraph.IsSelected = false;
         }
         this.plot2DControl.SetPlot2D(null);
         this.selectedP2D = null;
         this.selectedP2DGraph = null;
      }

      private void p2d_Plot2DChanged(Plot2D plot)
      {
         AxisVariable avX = UI.ConvertToAxisVariable(plot.PlotVariableX);
         AxisVariable avY = UI.ConvertToAxisVariable(plot.PlotVariableY);
         PlotData pd = this.ConvertToPlotData(plot);
         this.selectedP2DGraph.UpdatePlot(pd, avX, avY);
      }

      private void CheckBoxDetails_CheckedChanged(object sender, EventArgs e)
      {
         CheckBox cb = (CheckBox)sender;
         this.selectedP2DGraph.ShowDetails = cb.Checked;
         this.selectedP2DGraph.Invalidate();
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
   }
}
