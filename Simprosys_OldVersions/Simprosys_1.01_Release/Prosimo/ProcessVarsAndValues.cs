using System;
using System.Collections;

namespace Prosimo {
   public class ProcessVarsAndValues {
      private ArrayList varList = new ArrayList();
      private Hashtable varValueTable = new Hashtable();

      public ProcessVarsAndValues() {
      }

      public ArrayList ProcessVarList {
         get { return varList; }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="pv"></param>
      /// <returns>either a ProcessVarDoubleValues or ProcessVarIntValues object</returns>
      public object GetRecommendedValue(ProcessVar pv) {
         return varValueTable[pv];
      }

      public object GetVarRange(ProcessVar pv) {
         return varValueTable[pv];
      }

      internal void AddVarAndItsRecommendedValue(ProcessVarDouble pvd, double recommendedValue) {
         varList.Add(pvd);
         varValueTable.Add(pvd, recommendedValue);
      }

      internal void AddVarAndItsRecommendedValue(ProcessVarInt pvi, int recommendedValue) {
         varList.Add(pvi);
         varValueTable.Add(pvi, recommendedValue);
      }

      internal void AddVarAndItsRange(ProcessVarDouble pvd, DoubleRange doubleRange) {
         varList.Add(pvd);
         varValueTable.Add(pvd, doubleRange);
      }

      internal void AddVarAndItsRange(ProcessVarInt pvi, IntRange intRange) {
         varList.Add(pvi);
         varValueTable.Add(pvi, intRange);
      }

      //      public bool HasMinValues()
      //      {
      //         bool hasValues = true;
      //         object o;
      //         foreach (ProcessVar pv in varList) 
      //         {
      //            o = GetProcessVarValues(pv);
      //            if (o is ProcessVarDoubleValues) 
      //            {
      //               if (((ProcessVarDoubleValues) o).MinValue == Constants.NO_VALUE) 
      //               {
      //                  hasValues = false;
      //                  break;
      //               }
      //            }
      //            else if (o is ProcessVarDoubleValues) 
      //            {
      //               if (((ProcessVarIntValues) o).MinValue == Constants.NO_VALUE_INT) 
      //               {
      //                  hasValues = false;
      //                  break;
      //               }
      //            }
      //         }
      //         return hasValues;
      //      }
      //
      //      public bool HasMaxValues()
      //      {
      //         bool hasValues = true;
      //         
      //         object o;
      //         foreach (ProcessVar pv in varList) 
      //         {
      //            o = GetProcessVarValues(pv);
      //            if (o is ProcessVarDoubleValues) 
      //            {
      //               if (((ProcessVarDoubleValues) o).MaxValue == Constants.NO_VALUE) 
      //               {
      //                  hasValues = false;
      //                  break;
      //               }
      //            }
      //            else if (o is ProcessVarDoubleValues) 
      //            {
      //               if (((ProcessVarIntValues) o).MaxValue == Constants.NO_VALUE_INT) 
      //               {
      //                  hasValues = false;
      //                  break;
      //               }
      //            }
      //         }
      //         return hasValues;
      //      }
      //
      //      public bool HasRecommendedValues()
      //      {
      //         bool hasValues = true;
      //         
      //         object o;
      //         foreach (ProcessVar pv in varList) 
      //         {
      //            o = GetProcessVarValues(pv);
      //            if (o is ProcessVarDoubleValues) 
      //            {
      //               if (((ProcessVarDoubleValues) o).RecommendedValue == Constants.NO_VALUE) 
      //               {
      //                  hasValues = false;
      //                  break;
      //               }
      //            }
      //            else if (o is ProcessVarDoubleValues) 
      //            {
      //               if (((ProcessVarIntValues) o).RecommendedValue == Constants.NO_VALUE_INT) 
      //               {
      //                  hasValues = false;
      //                  break;
      //               }
      //            }
      //         }
      //         return hasValues;
      //      }
      //
      //      internal void AddVarAndItsRecommendedValue(ProcessVarDouble pvd, double recommendedValue) 
      //      {
      //         varList.Add(pvd);
      //         varValueTable.Add(pvd, new ProcessVarDoubleValues(recommendedValue));
      //      }
      //
      //      internal void AddVarAndItsRecommendedValue(ProcessVarInt pvi, int recommendedValue) 
      //      {
      //         varList.Add(pvi);
      //         varValueTable.Add(pvi, new ProcessVarIntValues(recommendedValue));
      //      }
      //
      //      internal void AddVarAndItsMinAndMaxValues(ProcessVarDouble pvd, double minValue, double maxValue) 
      //      {
      //         varList.Add(pvd);
      //         varValueTable.Add(pvd, new ProcessVarDoubleValues(minValue, maxValue, maxValue));
      //      }
      //
      //      internal void AddVarAndItsMinAndMaxValues(ProcessVarInt pvi, int minValue, int maxValue) 
      //      {
      //         varList.Add(pvi);
      //         varValueTable.Add(pvi, new ProcessVarIntValues(minValue, maxValue, maxValue));
      //      }
      //
      //      internal void AddVarAndItsMinMaxAndRecommendedValues(ProcessVarDouble pvd, double minValue, double maxValue, double recommendedValue) 
      //      {
      //         varList.Add(pvd);
      //         
      //         varValueTable.Add(pvd, new ProcessVarDoubleValues(minValue, maxValue, recommendedValue));
      //      }
      //
      //      internal void AddVarAndItsMinMaxAndRecommendedValues(ProcessVarInt pvi, int minValue, int maxValue, int recommendedValue) 
      //      {
      //         varList.Add(pvi);
      //         varValueTable.Add(pvi, new ProcessVarIntValues(minValue, maxValue, recommendedValue));
      //      }
   }
}
