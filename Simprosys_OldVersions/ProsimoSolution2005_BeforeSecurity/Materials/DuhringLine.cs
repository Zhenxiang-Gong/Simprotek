using System;
using System.Drawing;
using System.Collections;

using Prosimo.SubstanceLibrary;
using Prosimo.Plots;

namespace Prosimo.Materials {

   public class DuhringLine {
      private ProcessVarDouble concentration;
      private ProcessVarDouble startSolventBoilingPoint;
      private ProcessVarDouble startSolutionBoilingPoint;
      private ProcessVarDouble endSolventBoilingPoint;
      private ProcessVarDouble endSolutionBoilingPoint;

      public ProcessVarDouble Concentration {
         get { return concentration; }
      }

      public ProcessVarDouble StartSolventBoilingPoint {
         get { return startSolventBoilingPoint; }
      }

      public ProcessVarDouble StartSolutionBoilingPoint {
         get { return startSolutionBoilingPoint; }
      }

      public ProcessVarDouble EndSolventBoilingPoint {
         get { return endSolventBoilingPoint; }
      }

      public ProcessVarDouble EndSolutionBoilingPoint {
         get { return endSolutionBoilingPoint; }
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
         startSolventBoilingPoint = new ProcessVarDouble(StringConstants.SOLVENT_BOILING_POINT, PhysicalQuantity.Temperature, (double)startPoint.X, VarState.Specified, owner);
         startSolutionBoilingPoint = new ProcessVarDouble(StringConstants.SOLUTION_BOILING_POINT, PhysicalQuantity.Temperature, (double)startPoint.Y, VarState.Specified, owner);
         endSolventBoilingPoint = new ProcessVarDouble(StringConstants.SOLVENT_BOILING_POINT, PhysicalQuantity.Temperature, (double)endPoint.X, VarState.Specified, owner);
         endSolutionBoilingPoint = new ProcessVarDouble(StringConstants.SOLUTION_BOILING_POINT, PhysicalQuantity.Temperature, (double)endPoint.Y, VarState.Specified, owner);
      }
   }
}
