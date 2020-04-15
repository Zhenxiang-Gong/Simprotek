using System;
using System.Drawing;
using System.Collections;

namespace Prosimo.UnitOperations
{
	/// <summary>
	/// Summary description for HeatCurve.
	/// </summary>
   public class HeatCurve {
      private string name;
      private ArrayList dataPoints;
     
      public string Name {
         get {return name;}
         set {name = value;}
      }

      public ArrayList DataPoints {
         get {return dataPoints;}
         set {dataPoints = value;}
      }

      public HeatCurve(string name, ArrayList dataPoints) {
         this.name = name;
         this.dataPoints = dataPoints;
      }

      public HeatCurve(string name) {
         this.name = name;
         this.dataPoints = new ArrayList();
      }

      public void AddDataPoint(PointF point) {
         dataPoints.Add(point);
      }
      
      public void RemoveDataPoint(PointF point) {
         dataPoints.Remove(point);
      }
   }
}
