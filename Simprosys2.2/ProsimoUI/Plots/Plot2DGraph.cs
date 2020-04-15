using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for Plot2DGraph.
	/// </summary>
	public class Plot2DGraph : System.Windows.Forms.UserControl
	{
      public event PlotSelectedEventHandler PlotSelected;

      private float oxLength;
      private float oyLength;
      private PointF origin;

      private bool isSelected;
      public bool IsSelected
      {
         get {return isSelected;}
         set
         {
            isSelected = value;
            if (value)
               this.OnPlotSelected(this);
            this.Invalidate();
         }
      }

      private PlotData plotData;
      public PlotData PlotData
      {
         get {return plotData;}
         set {plotData = value;}
      }

      private PlotData otherData;
      public PlotData OtherData
      {
         get {return otherData;}
         set {otherData = value;}
      }

      private PointF[] points;
      public PointF[] Points
      {
         get {return points;}
         set {points = value;}
      }

      private AxisVariable oxVar;
      public AxisVariable OxVariable
      {
         get {return oxVar;}
         set {oxVar = value;}
      }
      
      private AxisVariable oyVar;
      public AxisVariable OyVariable
      {
         get {return oyVar;}
         set {oyVar = value;}
      }

      public bool ValidAxes
      {
         get {return (this.oxVar != null && this.oyVar != null) ? true : false;}
      }

      private bool showDetails;
      public bool ShowDetails
      {
         get {return showDetails;}
         set {showDetails = value;}
      }

      private bool showOtherData;
      public bool ShowOtherData
      {
         get {return showOtherData;}
         set {showOtherData = value;}
      }

      private bool showPoints;
      public bool ShowPoints
      {
         get {return showPoints;}
         set {showPoints = value;}
      }

      // This gives the number of pixels for one Ox variable unit.
      // The values of the points on curve.Data are on Ox & Oy variable units.
      public float OxScaleDensity
      {
         get {return CalculateOxScaleDensity();}
      }

      // This gives the number of pixels for one Oy variable unit.
      // The values of the points on curve.Data are on Ox & Oy variable units.
      public float OyScaleDensity
      {
         get {return CalculateOyScaleDensity();}
      }

      private string numericFormatString;
      public string NumericFormatString
      {
         get {return numericFormatString;}
         set {numericFormatString = value;}
      }

      private System.Windows.Forms.Label labelTitle;
      private System.Windows.Forms.Panel panelHeader;

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public Plot2DGraph()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.numericFormatString = "F2"; // fixed point with 2 decimals

         this.showDetails = false;
         this.showOtherData = false;
         this.showPoints = false;
         this.IsSelected = false;
         this.labelTitle.Paint += new PaintEventHandler(labelTitle_Paint);
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
         this.labelTitle = new System.Windows.Forms.Label();
         this.panelHeader = new System.Windows.Forms.Panel();
         this.panelHeader.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelTitle
         // 
         this.labelTitle.BackColor = System.Drawing.Color.White;
         this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
         this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.labelTitle.ForeColor = System.Drawing.SystemColors.ControlDark;
         this.labelTitle.Location = new System.Drawing.Point(0, 0);
         this.labelTitle.Name = "labelTitle";
         this.labelTitle.Size = new System.Drawing.Size(400, 20);
         this.labelTitle.TabIndex = 0;
         this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
         // 
         // panelHeader
         // 
         this.panelHeader.Controls.Add(this.labelTitle);
         this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelHeader.Location = new System.Drawing.Point(0, 0);
         this.panelHeader.Name = "panelHeader";
         this.panelHeader.Size = new System.Drawing.Size(400, 20);
         this.panelHeader.TabIndex = 2;
         // 
         // Plot2DGraph
         // 
         this.BackColor = System.Drawing.Color.White;
         this.Controls.Add(this.panelHeader);
         this.Name = "Plot2DGraph";
         this.Size = new System.Drawing.Size(400, 420);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.Plot2DGraph_Paint);
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Plot2DGraph_MouseDown);
         this.panelHeader.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private float CalculateOxScaleDensity()
      {
         return this.CalculateScaleDensity(this.OxVariable, this.oxLength);
      }

      private float CalculateOyScaleDensity()
      {
         return this.CalculateScaleDensity(this.OyVariable, this.oyLength);
      }

      private float CalculateScaleDensity(AxisVariable av, float axisLength)
      {
         float density = axisLength/(av.Range.Max-av.Range.Min);
         return density;
      }

      public void UpdatePlot(PlotData plotData, AxisVariable oxVariable, AxisVariable oyVariable)
      {
         this.oxVar = oxVariable;
         this.oyVar = oyVariable;
         this.plotData = plotData;

         this.oxLength = this.Width - PlotsConst.OX_LEFT_MARGIN - PlotsConst.OX_RIGHT_MARGIN;
         this.oyLength = this.Height - PlotsConst.OY_DOWN_MARGIN - PlotsConst.OY_UP_MARGIN - this.labelTitle.Height;
         this.origin = new PointF(PlotsConst.OX_LEFT_MARGIN, oyLength + PlotsConst.OY_UP_MARGIN + this.labelTitle.Height);

         this.labelTitle.Text = "";
         if (this.PlotData != null)
         {
            this.labelTitle.Text = this.plotData.Name;
         }

         this.Invalidate();
      }

      private void Plot2DGraph_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
      {
         if (this.oxVar != null && this.oyVar != null)
         {
            this.DrawCoordinateSystem();
            this.DrawOxVariable();
            this.DrawOyVariable();
            this.DrawPlotData();
            if (this.showDetails && this.PlotData != null)
               this.DrawDetails();
            if (this.showOtherData && this.otherData != null)
               this.DrawOtherData();
            if (this.showPoints && this.points != null)
               this.DrawPoints();
            this.DrawSelection();
         }
      }

      private void DrawSelection()
      {
         // Note: this will not draw the selection on the upper side because there
         //is a label there! We need to draw the selection on the label too.

         Graphics g = this.CreateGraphics();
         int d = 4;

         Rectangle rNW = new Rectangle(0,0,d,d); // no effect
         Rectangle rNE = new Rectangle(this.Width-d,0,d,d); // no effect
         Rectangle rSW = new Rectangle(0,this.Height-d,d,d);
         Rectangle rSE = new Rectangle(this.Width-d,this.Height-d,d,d);

         SolidBrush brush = null;
         Pen pen = null;
         if (this.IsSelected)
         {
            brush = new SolidBrush(Color.Blue);
            pen = new Pen(Color.Blue, 1f);
         }
         else
         {
            brush = new SolidBrush(Color.White);
            pen = new Pen(Color.DarkGray, 0.5f);
         }

         g.FillRectangle(brush, rNW); // no effect
         g.FillRectangle(brush, rNE); // no effect
         g.FillRectangle(brush, rSW); 
         g.FillRectangle(brush, rSE);

         g.DrawLine(pen,0,0,this.Width-1,0); // no effect
         g.DrawLine(pen,0,0,0,this.Height-1);
         g.DrawLine(pen,this.Width-1,0,this.Width-1,this.Height-1);
         g.DrawLine(pen,0,this.Height-1,this.Width-1,this.Height-1);

         this.DrawSelectionOnTitleLabel();
         
         g.Dispose();
         if (pen != null) {
            pen.Dispose();
         }
         if (brush != null) {
            brush.Dispose();
         }
      }

      private void DrawSelectionOnTitleLabel()
      {
         // Note: we draw on W, N, E sides

         Graphics g = this.labelTitle.CreateGraphics();
         int d = 4;

         Rectangle rNW = new Rectangle(0,0,d,d);
         Rectangle rNE = new Rectangle(this.labelTitle.Width-d,0,d,d);

         SolidBrush brush = null;
         Pen pen = null;
         if (this.IsSelected)
         {
            brush = new SolidBrush(Color.Blue);
            pen = new Pen(Color.Blue, 1f);
         }
         else
         {
            brush = new SolidBrush(Color.White);
            pen = new Pen(Color.DarkGray, 0.5f);
         }

         g.FillRectangle(brush, rNW);
         g.FillRectangle(brush, rNE);

         g.DrawLine(pen,0,0,this.labelTitle.Width-1,0);
         g.DrawLine(pen,0,0,0,this.labelTitle.Height-1);
         g.DrawLine(pen,this.labelTitle.Width-1,0,this.labelTitle.Width-1,this.labelTitle.Height-1);
         
         g.Dispose();
         if (pen != null) {
            pen.Dispose();
         }
         if (brush != null) {
            brush.Dispose();
         }

      }

      private void DrawCoordinateSystem()
      {
         Graphics g = this.GetCoordinateSystemGraphics();
         Pen pen = new Pen(Color.Black, 0.5f);
         g.DrawLine(pen, new PointF(0f,0f), new PointF(this.oxLength, 0f));
         g.DrawLine(pen, new PointF(0f,0f), new PointF(0f, this.oyLength));
         
         g.Dispose();
         pen.Dispose();
      }

      private Graphics GetCoordinateSystemGraphics()
      {
         Graphics g = this.CreateGraphics();
         Matrix oyReflectMatrix = new Matrix(1, 0, 0, -1, 0, 0);
         g.Transform = oyReflectMatrix;
         g.TranslateTransform(PlotsConst.OX_LEFT_MARGIN, this.Height - PlotsConst.OY_DOWN_MARGIN, MatrixOrder.Append);

         return g;
      }

      private void DrawOxVariable()
      {
         Graphics g = this.CreateGraphics();

         // draw variable
         string xStr = this.oxVar.Name + " (" + this.oxVar.Unit + ")";
         Brush brush = new SolidBrush(Color.Black);
         FontFamily fontFamily = new FontFamily("Times New Roman");
         Font font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel); 
         SizeF sizeF = g.MeasureString(xStr, font);
         PointF startPoint = new PointF(this.origin.X + this.oxLength - sizeF.Width, this.origin.Y + PlotsConst.OY_CONST_1 + 1f);
         g.DrawString(xStr, font, brush, startPoint);
         
         g.Dispose();
         brush.Dispose();
         font.Dispose();

         this.DrawMinorOxTicks();
         this.DrawMinorOxGrid();
         this.DrawMajorOxTicks();
         this.DrawMajorOxGrid();
         this.DrawMajorOxTickValues();
         // draw minor tick values ?
      }

      private void DrawMajorOxTicks()
      {
         this.DrawOxTicks(this.origin.X, PlotsConst.MAJOR_TICKS_OX, PlotsConst.MAJOR_TICK_LENGTH, this.oxLength);
      }

      private void DrawMinorOxTicks()
      {
         float tick_all_length = this.oxLength/PlotsConst.MAJOR_TICKS_OX;
         for (int i=0; i<PlotsConst.MAJOR_TICKS_OX; i++)
         {
            float tick_min = this.origin.X + i*tick_all_length;
            this.DrawOxTicks(tick_min, PlotsConst.MINOR_TICKS_OX, PlotsConst.MINOR_TICK_LENGTH, tick_all_length);
         }
      }

      private void DrawOxTicks(float min, int ticks_ox, float tick_length, float all_length)
      {
         Graphics g = this.CreateGraphics();
         Pen pen = new Pen(Color.Black, 0.5f);   
         float tickDelta = all_length/ticks_ox;
         float tickY = this.origin.Y + 2f;
         int tickXLinesNumber = ticks_ox + 1;
         float[] tickXs = new float[tickXLinesNumber];
         for (int i=0; i<tickXLinesNumber; i++)
         {
            tickXs[i] = min + i*tickDelta;
            PointF p1 = new PointF(tickXs[i], tickY);
            PointF p2 = new PointF(tickXs[i], tickY + tick_length);
            g.DrawLine(pen, p1, p2);
         }

         g.Dispose();
         pen.Dispose();
      }

      private void DrawMajorOxTickValues()
      {
         this.DrawOxTickValues(this.origin.X, this.oxVar.Range, PlotsConst.MAJOR_TICKS_OX, this.oxLength);
      }

      private void DrawOxTickValues(float min, Range range, int ticks_ox, float all_length)
      {
         Graphics g = this.CreateGraphics();
         Brush brush = new SolidBrush(Color.Black);
         FontFamily fontFamily = new FontFamily("Times New Roman");
         Font font = new Font(fontFamily, 9, FontStyle.Regular, GraphicsUnit.Pixel);
         
         float tickDeltaValue = range.Span/ticks_ox;
         float tickDelta = all_length/ticks_ox;
         float tickY = this.origin.Y + PlotsConst.OY_CONST_2;
         int tickXLinesNumber = ticks_ox + 1;
         float[] tickXs = new float[tickXLinesNumber];
         for (int i=0; i<tickXLinesNumber; i++)
         {
            tickXs[i] = min + i*tickDelta;
            float val = range.Min + i*tickDeltaValue;
            SizeF sizeF = g.MeasureString(val.ToString(), font);
            PointF p = new PointF(tickXs[i] - sizeF.Width/2, tickY);
            g.DrawString(val.ToString(), font, brush, p);
         }

         g.Dispose();
         brush.Dispose();
         font.Dispose();
      }

      private void DrawMajorOxGrid()
      {
         Pen pen = new Pen(Color.DarkGray, 0.5f);         
         this.DrawOxGrid(this.origin.X, PlotsConst.MAJOR_TICKS_OX, this.oxLength, pen);
         
         pen.Dispose();
      }

      private void DrawMinorOxGrid()
      {
         Pen pen = new Pen(Color.LightGray, 0.5f);
         float tick_all_length = this.oxLength/PlotsConst.MAJOR_TICKS_OX;
         for (int i=0; i<PlotsConst.MAJOR_TICKS_OX; i++)
         {
            float tick_min = this.origin.X + i*tick_all_length;
            this.DrawOxGrid(tick_min, PlotsConst.MINOR_TICKS_OX, tick_all_length, pen);
         }
         
         pen.Dispose();
      }

      private void DrawOxGrid(float min, int ticks_ox, float all_length, Pen pen)
      {
         // the lines perpendicular on ox
         Graphics g = this.CreateGraphics();
         float tickDelta = all_length/ticks_ox;
         float tickY = this.origin.Y;
         int tickXLinesNumber = ticks_ox;
         float[] tickXs = new float[tickXLinesNumber];
         for (int i=0; i<tickXLinesNumber; i++)
         {
            tickXs[i] = min + (i+1)*tickDelta;
            PointF p1 = new PointF(tickXs[i], tickY - 1f);
            PointF p2 = new PointF(tickXs[i], tickY - this.oyLength);
            g.DrawLine(pen, p1, p2);
         }

         g.Dispose();
      }

      private void DrawOyVariable()
      {
         Graphics g = this.CreateGraphics();

         string yStr = this.oyVar.Name + " (" + this.oyVar.Unit + ")";
         Brush brush = new SolidBrush(Color.Black);
         FontFamily fontFamily = new FontFamily("Times New Roman");
         Font font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel);
         SizeF sizeF = g.MeasureString(yStr, font);
         PointF startPoint = new PointF(0, this.labelTitle.Height + 1);
         g.DrawString(yStr, font, brush, startPoint);
         
         g.Dispose();
         brush.Dispose();
         font.Dispose();

         this.DrawMinorOyTicks();
         this.DrawMinorOyGrid();
         this.DrawMajorOyTicks();
         this.DrawMajorOyGrid();
         this.DrawMajorOyTickValues();
         // draw minor tick values ?
      }

      private void DrawMajorOyTicks()
      {
         this.DrawOyTicks(this.origin.Y, PlotsConst.MAJOR_TICKS_OY, PlotsConst.MAJOR_TICK_LENGTH, this.oyLength);
      }

      private void DrawMinorOyTicks()
      {
         float tick_all_length = this.oyLength/PlotsConst.MAJOR_TICKS_OY;
         for (int i=0; i<PlotsConst.MAJOR_TICKS_OY; i++)
         {
            float tick_min = this.origin.Y - i*tick_all_length;
            this.DrawOyTicks(tick_min, PlotsConst.MINOR_TICKS_OY, PlotsConst.MINOR_TICK_LENGTH, tick_all_length);
         }
      }

      private void DrawOyTicks(float min, int ticks_oy, float tick_length, float all_length)
      {
         Graphics g = this.CreateGraphics();
         Pen pen = new Pen(Color.Black, 0.5f);
         float tickDelta = all_length/ticks_oy;
         float tickX = this.origin.X - 2f;
         int tickYLinesNumber = ticks_oy + 1;
         float[] tickYs = new float[tickYLinesNumber];
         for (int i=0; i<tickYLinesNumber; i++)
         {
            tickYs[i] = min - i*tickDelta;
            PointF p1 = new PointF(tickX, tickYs[i]);
            PointF p2 = new PointF(tickX - tick_length, tickYs[i]);
            g.DrawLine(pen, p1, p2);
         }

         g.Dispose();
         pen.Dispose();
      }

      private void DrawMajorOyTickValues()
      {
         this.DrawOyTickValues(this.origin.Y, this.oyVar.Range, PlotsConst.MAJOR_TICKS_OY, this.oyLength);
      }

      private void DrawOyTickValues(float min, Range range, int ticks_oy, float all_length)
      {
         Graphics g = this.CreateGraphics();
         Brush brush = new SolidBrush(Color.Black);
         FontFamily fontFamily = new FontFamily("Times New Roman");
         Font font = new Font(fontFamily, 9, FontStyle.Regular, GraphicsUnit.Pixel);
         
         float tickDeltaValue = range.Span/ticks_oy;
         float tickDelta = all_length/ticks_oy;
         float tickX = this.origin.X - PlotsConst.MAJOR_TICK_LENGTH - 4f;
         int tickYLinesNumber = ticks_oy + 1;
         float[] tickYs = new float[tickYLinesNumber];
         for (int i=0; i<tickYLinesNumber; i++)
         {
            tickYs[i] = min - i*tickDelta;
            float val = range.Min + i*tickDeltaValue;
            SizeF sizeF = g.MeasureString(val.ToString(), font);
            PointF p = new PointF(tickX - sizeF.Width, tickYs[i] - sizeF.Height/2);
            g.DrawString(val.ToString(), font, brush, p);
         }

         g.Dispose();
         brush.Dispose();
         font.Dispose();
      }

      private void DrawMajorOyGrid()
      {
         Pen pen = new Pen(Color.DarkGray, 0.5f);
         this.DrawOyGrid(this.origin.Y, PlotsConst.MAJOR_TICKS_OY, this.oyLength, pen);

         pen.Dispose();
      }

      private void DrawMinorOyGrid()
      {
         Pen pen = new Pen(Color.LightGray, 0.5f);
         float tick_all_length = this.oyLength/PlotsConst.MAJOR_TICKS_OY;
         for (int i=0; i<PlotsConst.MAJOR_TICKS_OY; i++)
         {
            float tick_min = this.origin.Y - i*tick_all_length;
            this.DrawOyGrid(tick_min, PlotsConst.MINOR_TICKS_OY, tick_all_length, pen);
         }

         pen.Dispose();
      }

      private void DrawOyGrid(float min, int ticks_oy, float all_length, Pen pen)
      {
         // the lines perpendicular on ox
         Graphics g = this.CreateGraphics();
         float tickDelta = all_length/ticks_oy;
         float tickX = this.origin.X;
         int tickYLinesNumber = ticks_oy;
         float[] tickYs = new float[tickYLinesNumber];
         for (int i=0; i<tickYLinesNumber; i++)
         {
            tickYs[i] = min - (i+1)*tickDelta;
            PointF p1 = new PointF(tickX + 1f, tickYs[i]);
            PointF p2 = new PointF(tickX + this.oxLength, tickYs[i]);
            g.DrawLine(pen, p1, p2);
         }

         g.Dispose();
      }

      private void DrawPlotData()
      {
         if (this.PlotData != null)
         {
            foreach (CurveFamily curveFamily in plotData.CurveFamilies)
            {
               if (curveFamily.IsShownOnPlot)
               {
                  this.DrawCurveFamily(curveFamily);
               }
            }
         }
      }

      private void DrawCurveFamily(CurveFamily curveFamily)
      {
         if (curveFamily != null)
         {
            Pen pen = new Pen(Color.Red, 1.0f);
            curveFamily.Pen = pen; // change the color of curves
            foreach (Curve curve in curveFamily.Curves)
            {
               this.DrawCurve(curveFamily.Pen, curve);
            }

            pen.Dispose();
         }
      }

      private void DrawCurve(Pen p, Curve curve)
      {
         if (curve != null)
         {
            Graphics g = this.GetCoordinateSystemGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            PointF[] worldData = this.ConvertFromDomainToWorld(curve.Data);
            g.DrawCurve(p, worldData, 1.0f);
            
            g.Dispose();
         }
      }

      private PointF[] ConvertFromDomainToWorld(PointF[] domainData)
      {
         PointF[] worldData = new PointF[domainData.Length];
         for (int i=0; i<domainData.Length; i++)
         {
            float worldX = (domainData[i].X - this.OxVariable.Range.Min) * this.OxScaleDensity;
            float worldY = (domainData[i].Y - this.OyVariable.Range.Min) * this.OyScaleDensity;
            worldData[i] = new PointF(worldX, worldY);
         }
         return worldData;
      }

      public PointF ConvertFromDomainToWorld(PointF domainPoint)
      {
         float worldX = (domainPoint.X - this.OxVariable.Range.Min) * this.OxScaleDensity;
         float worldY = (domainPoint.Y - this.OyVariable.Range.Min) * this.OyScaleDensity;
         return new PointF(worldX, worldY);
      }

      public PointF ConvertFromWorldToDomain(PointF worldPoint)
      {
         float domainX = (worldPoint.X/this.OxScaleDensity) + this.OxVariable.Range.Min;
         float domainY = (worldPoint.Y/this.OyScaleDensity) + this.OyVariable.Range.Min;
         return new PointF(domainX, domainY);
      }

      private PointF[] ConvertFromWorldToDomain(PointF[] worldData)
      {
         PointF[] domainData = new PointF[worldData.Length];
         for (int i=0; i<worldData.Length; i++)
         {
            float domainX = (worldData[i].X/this.OxScaleDensity) + this.OxVariable.Range.Min;
            float domainY = (worldData[i].Y/this.OyScaleDensity) + this.OyVariable.Range.Min;
            domainData[i] = new PointF(domainX, domainY);
         }
         return domainData;
      }

      public PointF ConvertFromWorldToPage(PointF worldPoint)
      {
         PointF[] points = {worldPoint};
         Matrix m = this.GetCoordinateSystemGraphics().Transform;
         m.TransformPoints(points);
         return points[0];         
      }

      private PointF[] ConvertFromWorldToPage(PointF[] worldData)
      {
         Matrix m = this.GetCoordinateSystemGraphics().Transform;
         m.TransformPoints(worldData);
         return worldData; // now it is pageData         
      }

      public PointF ConvertFromPageToWorld(PointF pagePoint)
      {
         PointF[] points = {pagePoint};
         Matrix m = this.GetCoordinateSystemGraphics().Transform.Clone();
         m.Invert();
         m.TransformPoints(points);
         return points[0];         
      }

      private PointF[] ConvertFromPageToWorld(PointF[] pageData)
      {
         Matrix m = this.GetCoordinateSystemGraphics().Transform.Clone();
         m.Invert();
         m.TransformPoints(pageData);
         return pageData; // now it is worldData         
      }

      public PointF ConvertFromPageToDomain(PointF pagePoint)
      {
         PointF[] points = {pagePoint};
         Matrix m = this.GetCoordinateSystemGraphics().Transform.Clone();
         m.Invert();
         m.TransformPoints(points);
         return this.ConvertFromWorldToDomain(points[0]);         
      }

      public PointF ConvertFromDomainToPage(PointF domainPoint)
      {
         PointF worldPoint = this.ConvertFromDomainToWorld(domainPoint);
         PointF[] points = {worldPoint};
         Matrix m = this.GetCoordinateSystemGraphics().Transform;
         m.TransformPoints(points);
         return points[0];         
      }

      private PointF[] ConvertFromPageToDomain(PointF[] pageData)
      {
         Matrix m = this.GetCoordinateSystemGraphics().Transform.Clone();
         m.Invert();
         m.TransformPoints(pageData);
         return this.ConvertFromWorldToDomain(pageData); // now it is worldData         
      }

      private PointF[] ConvertFromDomainToPage(PointF[] domainData)
      {
         PointF[] worldData = this.ConvertFromDomainToWorld(domainData);
         Matrix m = this.GetCoordinateSystemGraphics().Transform;
         m.TransformPoints(worldData);
         return worldData; // now it is pageData         
      }

      private void DrawDetails()
      {
         foreach (CurveFamily curveFamily in plotData.CurveFamilies)
         {
            if (curveFamily.IsShownOnPlot)
            {
               if (curveFamily != null)
               {
                  foreach (Curve curve in curveFamily.Curves)
                  {
                     if (curve != null)
                     {
                        Graphics g = this.CreateGraphics();
                        string str = curve.Value.ToString(this.numericFormatString);
                        FontFamily fontFamily = new FontFamily("Times New Roman");
                        Font font = new Font(fontFamily, 10, FontStyle.Regular, GraphicsUnit.Pixel);
                        Brush brush = new SolidBrush(Color.Black);
                        int idx = (int)curve.Data.Length/2;
                        PointF pagePoint = this.ConvertFromDomainToPage(curve.Data[idx]);
                        PointF pointPoint = new PointF(pagePoint.X-1, pagePoint.Y-1);
                        g.FillEllipse(brush, pagePoint.X, pointPoint.Y, 5.0f, 5.0f);
                        PointF textPoint = new PointF(pagePoint.X+2, pagePoint.Y+2);
                        g.DrawString(str, font, brush, textPoint);

                        g.Dispose();
                        brush.Dispose();
                        font.Dispose();
                     }
                  }
               }
            }
         }
      }

      private void DrawOtherData()
      {
         if (this.OtherData != null)
         {
            foreach (CurveFamily curveFamily in this.OtherData.CurveFamilies)
            {
               this.DrawOtherCurveFamily(curveFamily);
            }
         }
      }

      private void DrawOtherCurveFamily(CurveFamily curveFamily)
      {
         if (curveFamily != null)
         {
            curveFamily.Pen = new Pen(Color.Black, 1.0f);
            foreach (Curve curve in curveFamily.Curves)
            {
               this.DrawOtherCurve(curveFamily.Pen, curve);
            }
         }
      }

      private void DrawOtherCurve(Pen p, Curve curve)
      {
         if (curve != null)
         {
            Graphics g = this.GetCoordinateSystemGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            PointF[] worldData = this.ConvertFromDomainToWorld(curve.Data);
            g.DrawCurve(p, worldData, 1.0f);

            g.Dispose();
         }
      }

      private void DrawPoints()
      {
         if (this.points != null)
         {
            Graphics g = this.GetCoordinateSystemGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            PointF[] worldPoints = this.ConvertFromDomainToWorld(points);
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(b, 0.5f);
            for (int i=0; i<worldPoints.Length; i++)
            {
               if (worldPoints[i].X >= 0 && worldPoints[i].Y >= 0)
               {
                  g.FillEllipse(b, worldPoints[i].X-1, worldPoints[i].Y-1, 2.0f, 2.0f);
                  g.DrawEllipse(p, worldPoints[i].X-5, worldPoints[i].Y-5, 10.0f, 10.0f);
               }
            }
            b.Dispose();
            g.Dispose();
            p.Dispose();
         }
      }

      private void OnPlotSelected(Plot2DGraph plotGraph) 
      {
         if (PlotSelected != null) 
         {
            PlotSelected(plotGraph);
         }
      }
      
      private void Plot2DGraph_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.IsSelected = true;
      }

      private void labelTitle_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.IsSelected = true;
      }

      private void labelTitle_Paint(object sender, PaintEventArgs e)
      {
         this.Invalidate();
      }
   }
}
