using System;

namespace ProsimoUI.Plots
{
   /// <summary>
   /// Summary description for AxisVariable.
   /// </summary>
   public class AxisVariable
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

      private Range range;
      public Range Range
      {
         get {return range;}
         set {range = value;}
      }

      public AxisVariable(string name, string unit, float min, float max) :
         this(name, unit, new Range(min, max))
      {
      }

      public AxisVariable(string name, string unit, Range range)
      {
         this.name = name;
         this.unit = unit;
         this.range = range;
      }

      public bool Contains(float val)
      {
         bool contains = false;
         if (val >= this.Range.Min && val <= this.Range.Max)
            contains = true;
         return contains;
      }

      public override string ToString()
      {
         return this.Name;
      }
   }

   public struct Range
   {
      private float min;
      public float Min
      {
         get {return min;}
      }
      
      private float max;
      public float Max
      {
         get {return max;}
      }

      public float Span
      {
         get {return this.max - this.min;}
      }

      public Range(float min, float max)
      {
         this.min = min;
         this.max = max;
      }

      public bool Equals(Range range)
      {
         if (range.Min == this.min && range.Max == this.max)
            return true;
         else
            return false;
      }
   }
}
