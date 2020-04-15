using System;
using System.Drawing;
using System.Collections;

using Prosimo.SubstanceLibrary;
using Prosimo.Plots;

namespace Prosimo.Materials {
   //public enum SolutionType {Raoult, Aqueous, InorganicSalts, Ideal, Sucrose, ReducingSugars, Juices, Unknown};
   
   public delegate void DuhringLinesChangedEventHandler(DuhringLinesCache duhringLinesCache);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class DuhringLinesCache : NonSolvableProcessVarOwner {
      private DryingMaterialCache owner;
      private ArrayList duhringLines = new ArrayList();
      
      public event DuhringLinesChangedEventHandler DuhringLinesChanged;

      private void OnDuhringLinesChanged() {
         if (DuhringLinesChanged != null) {
            DuhringLinesChanged(this);
         }
      }

      public DuhringLinesCache(DryingMaterialCache owner) {
         this.owner = owner;
         if (owner.DuhringLines != null) {
            foreach (CurveF c in owner.DuhringLines) {
               DuhringLine line = new DuhringLine(this, c.Value, c.Data[0], c.Data[1]);
               duhringLines.Add(line);
            }
         }
         else {
            DuhringLine line = new DuhringLine(this);
            duhringLines.Add(line);
            line = new DuhringLine(this);
            duhringLines.Add(line);
         }
      }

      public ArrayList DuhringLines {
         get {return duhringLines;}
      }

      public void AddDuhringLine() {
         DuhringLine line = new DuhringLine(this);
         duhringLines.Add(line);
         OnDuhringLinesChanged();
      }

      public void RemoveDuhringLine(DuhringLine line) {
         if (duhringLines.Count > 2) {
            duhringLines.Remove(line);
            OnDuhringLinesChanged();
         }
      }

//      public void RemoveDuhringLines(int startIndex, int numOfRows) {
//         if (startIndex > 0 && (startIndex + numOfRows) < (duhringLines.Count -1)) {
//            duhringLines.RemoveRange(startIndex, numOfRows);
//         }
//      }
//
      public void RemoveDuhringLineAt(int index) {
         if (duhringLines.Count > 2) {
            if (index >= 0 && index < duhringLines.Count) {
               duhringLines.RemoveAt(index);
               OnDuhringLinesChanged();
            }
         }
      }

      public ErrorMessage FinishSpecifications() {
         ErrorMessage errorMsg = null;
         CurveF[] lines = new CurveF[duhringLines.Count];
         DuhringLine line;
         double startSolventBoilingPoint;
         double startSolutionBoilingPoint;
         double endSolventBoilingPoint;
         double endSolutionBoilingPoint;
         line = (DuhringLine) duhringLines[0];
         double previousConcentration = line.Concentration.Value;

         for(int i = 0; i < duhringLines.Count; i++) {
            line = (DuhringLine) duhringLines[i];

            if (i > 0 && line.Concentration.Value <= previousConcentration) { 
               string msg = "Line " + (i+1) + "'s mass concentration vlaue must be greater than Line " + i + "'s.\n Mass concentration values of the duhring lines must go from low to high.";
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Input Value", msg); 
               break;
            }                                                     
            
            previousConcentration = line.Concentration.Value;
            
            startSolventBoilingPoint = line.StartSolventBoilingPoint.Value;
            startSolutionBoilingPoint = line.StartSolutionBoilingPoint.Value;
            endSolventBoilingPoint = line.EndSolventBoilingPoint.Value;
            endSolutionBoilingPoint = line.EndSolutionBoilingPoint.Value;
            if (startSolutionBoilingPoint < startSolventBoilingPoint) {
               string msg = "Line " + (i+1) + "--the boiling point of the solution must be greater than the boiling point of the solvent.";
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Input Value", msg); 
               break;
            }

            if (endSolutionBoilingPoint < endSolventBoilingPoint) {
               string msg = "Line " + (i+1) + "--the boiling point of the solution must be greater than the boiling point of the solvent.";
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Input Value", msg); 
               break;
            }

            if (endSolventBoilingPoint <= startSolventBoilingPoint) {
               string msg = "Line " + (i+1) + "--the boiling point of the solvent at the end must be greater than boiling point of the solvent at the start.";
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Input Value", msg); 
               break;
            }

            if (endSolutionBoilingPoint <= startSolutionBoilingPoint) {
               string msg = "Line " + (i+1) + "--the boiling point of the solution at the end must be greater than boiling point of the solution at the start.";
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Input Value", msg); 
               break;
            }

            PointF[] points = {new PointF((float)startSolventBoilingPoint, (float)startSolutionBoilingPoint),
                                 new PointF((float)endSolventBoilingPoint, (float)endSolutionBoilingPoint)};
            lines[i] = new CurveF((float)line.Concentration.Value, points); 
         }

         if (errorMsg == null) {
            owner.DuhringLines = lines;
         }

         return errorMsg;
      }
   }
   
   public class DuhringLine {
      private ProcessVarDouble concentration;
      private ProcessVarDouble startSolventBoilingPoint;
      private ProcessVarDouble startSolutionBoilingPoint;
      private ProcessVarDouble endSolventBoilingPoint;
      private ProcessVarDouble endSolutionBoilingPoint;

      public ProcessVarDouble Concentration {
         get {return concentration;}
      }

      public ProcessVarDouble StartSolventBoilingPoint {
         get {return startSolventBoilingPoint;}
      }
      
      public ProcessVarDouble StartSolutionBoilingPoint {
         get { return startSolutionBoilingPoint;}
      }

      public ProcessVarDouble EndSolventBoilingPoint {
         get {return endSolventBoilingPoint;}
      }

      public ProcessVarDouble EndSolutionBoilingPoint {
         get {return endSolutionBoilingPoint;}
      }

      public DuhringLine(IProcessVarOwner owner) {
         concentration = new ProcessVarDouble("DuhringLineConcentration", PhysicalQuantity.Unknown, VarState.Specified, owner);
         startSolventBoilingPoint = new ProcessVarDouble(StringConstants.SOLVENT_BOILING_POINT, PhysicalQuantity.Temperature, VarState.Specified, owner);
         startSolutionBoilingPoint = new ProcessVarDouble(StringConstants.SOLUTION_BOILING_POINT, PhysicalQuantity.Temperature, VarState.Specified, owner);
         endSolventBoilingPoint = new ProcessVarDouble(StringConstants.SOLVENT_BOILING_POINT, PhysicalQuantity.Temperature, VarState.Specified, owner);
         endSolutionBoilingPoint = new ProcessVarDouble(StringConstants.SOLUTION_BOILING_POINT, PhysicalQuantity.Temperature, VarState.Specified, owner);
      }                                    

      public DuhringLine(IProcessVarOwner owner, double concentratn, PointF startPoint, PointF endPoint) {
         concentration = new ProcessVarDouble(StringConstants.MASS_CONCENTRATION, PhysicalQuantity.Unknown, concentratn, VarState.Specified, owner);
         startSolventBoilingPoint = new ProcessVarDouble(StringConstants.SOLVENT_BOILING_POINT, PhysicalQuantity.Temperature, startPoint.X, VarState.Specified, owner);
         startSolutionBoilingPoint = new ProcessVarDouble(StringConstants.SOLUTION_BOILING_POINT, PhysicalQuantity.Temperature,  startPoint.Y, VarState.Specified, owner);
         endSolventBoilingPoint = new ProcessVarDouble(StringConstants.SOLVENT_BOILING_POINT, PhysicalQuantity.Temperature,  endPoint.X, VarState.Specified, owner);
         endSolutionBoilingPoint = new ProcessVarDouble(StringConstants.SOLUTION_BOILING_POINT, PhysicalQuantity.Temperature, endPoint.Y, VarState.Specified, owner);
      }
   }
}
