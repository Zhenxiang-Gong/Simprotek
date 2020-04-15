using System;
using System.Drawing;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for Curve.
	/// </summary>
   public class Curve
   {
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

      private float val;
      public float Value
      {
         get {return val;}
         set {val = value;}
      }

      private PointF[] data;
      public PointF[] Data
      {
         get {return data;}
         set {data = value;}
      }

      public Curve()
      {
         this.name = null;
         this.unit = null;
         this.val = 0f;
         this.data = null;
      }

      public Curve(string name, string unit, float val, PointF[] data)
      {
         this.name = name;
         this.unit = unit;
         this.val = val;
         this.data = data;
      }
   }
}
