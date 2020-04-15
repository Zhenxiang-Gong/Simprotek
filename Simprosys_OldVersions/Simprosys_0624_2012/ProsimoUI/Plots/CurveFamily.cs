using System;
using System.Drawing;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for CurveFamily.
	/// </summary>
   public class CurveFamily
   {
      private Pen pen = new Pen(Color.DarkKhaki, 1.0f);
      public Pen Pen
      {
         get {return pen;}
         set {pen = value;}
      }

      private bool isShownOnPlot;
      public bool IsShownOnPlot
      {
         get {return isShownOnPlot;}
         set {isShownOnPlot = value;}
      }

      private string name;
      public string Name
      {
         get {return name;}
         set {name = value;}
      }

      private string unit;
      public string Unit
      {
         get {return unit;}
         set {unit = value;}
      }
      
      private Curve[] curves;
      public Curve[] Curves
      {
         get {return curves;}
         set {curves = value;}
      }

      public CurveFamily()
      {
         this.name = null;
         this.unit = null;
         this.curves = null;
         this.isShownOnPlot = true;
         this.pen = new Pen(Color.DarkKhaki, 1.0f);
      }

      public CurveFamily(string name, string unit, Curve[] curves)
      {
         this.name = name;
         this.unit = unit;
         this.curves = curves;
         this.isShownOnPlot = true;
         this.pen = new Pen(Color.Red, 1.0f);
      }
   }
}
