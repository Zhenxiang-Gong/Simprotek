using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.LiquidSolidSeparation {
   /// <summary>
   /// Summary description for Filter.
   /// </summary>
   [Serializable] 
   public class Filter : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int SLURRY_INLET_INDEX = 0;
      public static int CAKE_OUTLET_INDEX = 1;
      public static int FILTRATE_OUTLET_INDEX = 2;
      
      private ProcessStreamBase slurryInlet;
      private ProcessStreamBase cakeOutlet;
      private ProcessStreamBase filtrateOutlet;
      
      #region public properties
      public ProcessStreamBase SlurryInlet {
         get { return slurryInlet; }
      }
      
      public ProcessStreamBase CakeOutlet {
         get { return cakeOutlet; }
      }

      public ProcessStreamBase FiltrateOutlet {
         get { return filtrateOutlet; }
      }
      
      #endregion

      public Filter(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanConnect(int streamIndex) {
         bool retValue = false;
         if (streamIndex == SLURRY_INLET_INDEX && slurryInlet == null) {
            retValue = true;
         }
         else if (streamIndex == CAKE_OUTLET_INDEX && cakeOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == FILTRATE_OUTLET_INDEX && filtrateOutlet == null) {
            retValue = true;
         }
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         bool canAttach = false;
         bool isLiquidMaterial = false;
         if (ps is DryingMaterialStream) {
            DryingMaterialStream dms = ps as DryingMaterialStream;
            isLiquidMaterial = (dms.MaterialStateType == MaterialStateType.Liquid);
         }
         
         if (streamIndex == SLURRY_INLET_INDEX && slurryInlet == null && ps.DownStreamOwner == null) {
            if (isLiquidMaterial) {
               canAttach = true;
            }
         }
         else if (streamIndex == CAKE_OUTLET_INDEX && cakeOutlet == null && ps.UpStreamOwner == null) {
            if (ps is DryingMaterialStream && !isLiquidMaterial) {
               canAttach = true;
            }
         }
         else if (streamIndex == FILTRATE_OUTLET_INDEX && filtrateOutlet == null && ps.UpStreamOwner == null) {
            if (isLiquidMaterial) {
               canAttach = true;
            }
         }
         
         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == SLURRY_INLET_INDEX) {
            slurryInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == CAKE_OUTLET_INDEX) {
            cakeOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == FILTRATE_OUTLET_INDEX) {
            filtrateOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else {
            attached = false;
         }
         return attached;
      }
      
      internal override bool DoDetach(ProcessStreamBase ps) {
         bool detached = true;
         if (ps == slurryInlet) {
            slurryInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == cakeOutlet) {
            cakeOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == filtrateOutlet) {
            filtrateOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else {
            detached = false;
         }

         if (detached) {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }
      
      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (slurryInlet == null || cakeOutlet == null || filtrateOutlet == null) {
            isReady = false;
         }
         return isReady;
      }

      protected override bool IsSolveReady() {
         bool isReady = false;
         if (slurryInlet.HasSolvedAlready && filtrateOutlet.Pressure.HasValue) {
            isReady = true;
         }
         return isReady;
      }

      public override void Execute(bool propagate) {
         PreSolve();
         if (IsSolveReady()) {
            Solve();
         }
            
         PostSolve();
      }
               
      private void Solve() {
         //flow balance
         if (slurryInlet.MassFlowRate.HasValue) {
            if (cakeOutlet.MassFlowRate.HasValue) {
               Calculate(filtrateOutlet.MassFlowRate, slurryInlet.MassFlowRate.Value - cakeOutlet.MassFlowRate.Value);
            }
            else if (filtrateOutlet.MassFlowRate.HasValue) {
               Calculate(cakeOutlet.MassFlowRate, slurryInlet.MassFlowRate.Value - filtrateOutlet.MassFlowRate.Value);
            }
         }
         else if (cakeOutlet.MassFlowRate.HasValue) {
            if (filtrateOutlet.MassFlowRate.HasValue) {
               Calculate(slurryInlet.MassFlowRate, cakeOutlet.MassFlowRate.Value + filtrateOutlet.MassFlowRate.Value);
            }
         }

         if (slurryInlet.Temperature.HasValue) {
            Calculate(filtrateOutlet.Temperature, slurryInlet.Temperature.Value);
            Calculate(cakeOutlet.Temperature, slurryInlet.Temperature.Value);
         }
         else if (filtrateOutlet.Temperature.HasValue) {
            Calculate(slurryInlet.Temperature, filtrateOutlet.Temperature.Value);
            Calculate(cakeOutlet.Temperature, filtrateOutlet.Temperature.Value);
         }
         else if (cakeOutlet.Temperature.HasValue) {
            Calculate(slurryInlet.Temperature, cakeOutlet.Temperature.Value);
            Calculate(filtrateOutlet.Temperature, cakeOutlet.Temperature.Value);
         }

         if (slurryInlet is DryingMaterialStream) {
            DryingMaterialStream dmsInlet = slurryInlet as DryingMaterialStream;
            DryingMaterialStream dmsFiltrateOutlet = filtrateOutlet as DryingMaterialStream;
            DryingMaterialStream dmsCakeOutlet = cakeOutlet as DryingMaterialStream;
            if (dmsInlet.MassFlowRate.HasValue && dmsInlet.MoistureContentWetBase.HasValue) {
               double totalSolid = dmsInlet.MassFlowRate.Value * dmsInlet.MoistureContentWetBase.Value;
               if (dmsCakeOutlet.MassFlowRate.HasValue && dmsCakeOutlet.MoistureContentWetBase.HasValue) {
                  double cakeSolid = dmsCakeOutlet.MassFlowRate.Value * dmsCakeOutlet.MoistureContentWetBase.Value;
                  if (dmsFiltrateOutlet.MassFlowRate.HasValue) {
                     double filtrateMassConcentration = (totalSolid - cakeSolid) / dmsFiltrateOutlet.MassFlowRate.Value;
                     Calculate(dmsFiltrateOutlet.MassConcentration, filtrateMassConcentration);
                  }
                  else if (dmsFiltrateOutlet.MassConcentration.HasValue) {
                     double filtrateFlowRate = (totalSolid - cakeSolid) / dmsFiltrateOutlet.MassConcentration.Value;
                     Calculate(dmsFiltrateOutlet.MassFlowRate, filtrateFlowRate);
                  }
               }
               else if (dmsFiltrateOutlet.MassFlowRate.HasValue && dmsFiltrateOutlet.MoistureContentWetBase.HasValue) {
                  double filtrateSolid = dmsFiltrateOutlet.MassFlowRate.Value * dmsFiltrateOutlet.MoistureContentWetBase.Value;
                  if (dmsCakeOutlet.MassFlowRate.HasValue) {
                     double cakeMassConcentration = (totalSolid - filtrateSolid) / dmsCakeOutlet.MassFlowRate.Value;
                     Calculate(dmsCakeOutlet.MassConcentration, cakeMassConcentration);
                  }
                  else if (dmsCakeOutlet.MassConcentration.HasValue) {
                     double cakeFlowRate = (totalSolid - filtrateSolid) / dmsCakeOutlet.MassConcentration.Value;
                     Calculate(dmsCakeOutlet.MassFlowRate, cakeFlowRate);
                  }
               }
            }
            else if (dmsCakeOutlet.MassFlowRate.HasValue && dmsCakeOutlet.MoistureContentWetBase.HasValue) {
               double cakeSolid = dmsCakeOutlet.MassFlowRate.Value * dmsCakeOutlet.MoistureContentWetBase.Value;
               if (dmsFiltrateOutlet.MassFlowRate.HasValue && dmsFiltrateOutlet.MoistureContentWetBase.HasValue) {
                  double filtrateSolid = dmsFiltrateOutlet.MassFlowRate.Value * dmsFiltrateOutlet.MoistureContentWetBase.Value;
                  if (dmsInlet.MassFlowRate.HasValue) {
                     double slurryMassConcentration = (cakeSolid + filtrateSolid) / dmsInlet.MassFlowRate.Value;
                     Calculate(dmsInlet.MassConcentration, slurryMassConcentration);
                  }
                  else if (dmsInlet.MassConcentration.HasValue) {
                     double slurryFlowRate = (cakeSolid + filtrateSolid) / dmsInlet.MassConcentration.Value;
                     Calculate(dmsInlet.MassFlowRate, slurryFlowRate);
                  }
               }
            }
         }
      }

      protected Filter(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFilter", typeof(int));
         if (persistedClassVersion == 1) {
            this.slurryInlet = info.GetValue("SlurryInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.cakeOutlet = info.GetValue("CakeOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.filtrateOutlet = info.GetValue("FiltrateOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionFilter", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("SlurryInlet", this.slurryInlet, typeof(ProcessStreamBase));
         info.AddValue("CakeOutlet", this.cakeOutlet, typeof(ProcessStreamBase));
         info.AddValue("FiltrateOutlet", this.filtrateOutlet, typeof(ProcessStreamBase));
      }
   }
}

