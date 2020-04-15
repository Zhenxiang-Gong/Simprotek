using System;
using System.Collections;

namespace Prosimo {
   public struct DoubleRange {
      private double minValue;
      private double maxValue;

      public DoubleRange(double minValue, double maxValue) {
         this.minValue = minValue;
         this.maxValue = maxValue;
      }

      public double MinValue {
         get { return minValue; }
      }

      public double MaxValue {
         get { return maxValue; }
      }
   }

   public struct IntRange {
      private int minValue;
      private int maxValue;

      public IntRange(int minValue, int maxValue) {
         this.minValue = minValue;
         this.maxValue = maxValue;
      }

      public int MinValue {
         get { return minValue; }
      }

      public int MaxValue {
         get { return maxValue; }
      }
   }

   //   public class ProcessVarDoubleValues 
   //   {
   //      double minValue = Constants.NO_VALUE;
   //      double maxValue = Constants.NO_VALUE;
   //      double recommendedValue = Constants.NO_VALUE;
   //
   //      public ProcessVarDoubleValues (double recommendedValue) 
   //      {
   //         this.recommendedValue = recommendedValue;
   //      }
   //
   //      public ProcessVarDoubleValues (double minValue, double maxValue) 
   //      {
   //         this.minValue = minValue;
   //         this.maxValue = maxValue;
   //      }
   //
   //      public ProcessVarDoubleValues (double minValue, double maxValue, double recommendedValue) 
   //      {
   //         this.minValue = minValue;
   //         this.maxValue = maxValue;
   //         this.recommendedValue = recommendedValue;
   //      }
   //
   //      public double MinValue 
   //      {
   //         get { return minValue;}
   //      }
   //
   //      public double MaxValue 
   //      {
   //         get { return maxValue;}
   //      }
   //
   //      public double RecommendedValue 
   //      {
   //         get { return recommendedValue;}
   //      }
   //   }
   //
   //   public class ProcessVarIntValues 
   //   {
   //      int minValue = Constants.NO_VALUE_INT;
   //      int maxValue = Constants.NO_VALUE_INT;
   //      int recommendedValue = Constants.NO_VALUE_INT;
   //
   //      public ProcessVarIntValues (int recommendedValue) 
   //      {
   //         this.recommendedValue = recommendedValue;
   //      }
   //
   //      public ProcessVarIntValues (int minValue, int maxValue) 
   //      {
   //         this.minValue = minValue;
   //         this.maxValue = maxValue;
   //      }
   //
   //      public ProcessVarIntValues (int minValue, int maxValue, int recommendedValue) 
   //      {
   //         this.minValue = minValue;
   //         this.maxValue = maxValue;
   //         this.recommendedValue = recommendedValue;
   //      }
   //
   //      public int MinValue 
   //      {
   //         get { return minValue;}
   //      }
   //
   //      public int MaxValue 
   //      {
   //         get { return maxValue;}
   //      }
   //
   //      public int RecommendedValue 
   //      {
   //         get { return recommendedValue;}
   //      }
   //   }
}
