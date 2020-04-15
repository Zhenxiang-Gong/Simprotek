using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations;
using ProsimoUI.UnitOperationsUI.TwoStream;
using ProsimoUI.UnitOperationsUI;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo;
using ProsimoUI.UnitOperationsUI.HeatExchangerUI;
using ProsimoUI.UnitOperationsUI.CycloneUI;
using ProsimoUI.UnitOperationsUI.DryerUI;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for ConnectionManager.
	/// </summary>
	public class ConnectionManager
	{
      private const int STREAM = 1;
      private const int UNIT_OP = 2;
      private const int ALL = 3;

      private Flowsheet flowsheet;

      private ArrayList connections;
      public ArrayList Connections
      {
         get {return connections;}
         set {connections = value;}
      }

		public ConnectionManager(Flowsheet flowsheet)
		{
         this.flowsheet = flowsheet;
         this.Connections = new ArrayList();
		}

      public void DrawConnections()
      {
         Graphics g = this.flowsheet.CreateGraphics();
         g.Clear(this.flowsheet.BackColor);
         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext())
         {
            SolvableConnection conn = (SolvableConnection)e.Current;
            conn.DrawConnection();
         }
      }

      public void SetOwner(string oldName, string newName)
      {
         if (this.Connections.Count > 0) 
         {
            IEnumerator e = this.Connections.GetEnumerator();
            while (e.MoveNext()) 
            {
               SolvableConnection dc = (SolvableConnection)e.Current;

               if (dc.StreamPoint.Name.Equals(oldName))
               {
                  dc.StreamPoint.Name = newName;
               }
               if (dc.UnitOpPoint.Name.Equals(oldName))
               {
                  dc.UnitOpPoint.Name = newName;
               }
            }
         }
      }

      public void RemoveConnections(string name)
      {
         this.RemoveConnections(name, ALL);
      }

      public void RemoveStreamConnections(string name)
      {
         this.RemoveConnections(name, STREAM);
      }

      public void RemoveUnitOpConnections(string name)
      {
         this.RemoveConnections(name, UNIT_OP);
      }

      public void RemoveConnections(string name, int whom)
      {
         if (this.Connections.Count > 0) 
         {
            ArrayList toDeleteConnections = new ArrayList();
            IEnumerator e = this.Connections.GetEnumerator();
            while (e.MoveNext()) 
            {
               SolvableConnection dc = (SolvableConnection)e.Current;
               switch (whom)
               {
                  case STREAM:
                     if (dc.StreamPoint.Name.Equals(name))
                     {
                        toDeleteConnections.Add(dc);
                     }
                     break;
                  case UNIT_OP:
                     if (dc.UnitOpPoint.Name.Equals(name))
                     {
                        toDeleteConnections.Add(dc);
                     }
                     break;
                  case ALL:
                     if (dc.StreamPoint.Name.Equals(name) || dc.UnitOpPoint.Name.Equals(name))
                     {
                        toDeleteConnections.Add(dc);
                     }
                     break;
                  default:
                     break;
               }
            }

            if (toDeleteConnections.Count > 0)
            {
               IEnumerator e2 = toDeleteConnections.GetEnumerator();
               while (e2.MoveNext()) 
               {
                  SolvableConnection conn = (SolvableConnection)e2.Current;
                  this.Connections.Remove(conn);
               }
               this.DrawConnections();
            }
         }
      }

      public void RemoveConnections(string strName, string uoName)
      {
         if (this.Connections.Count > 0) 
         {
            ArrayList toDeleteConnections = new ArrayList();
            IEnumerator e = this.Connections.GetEnumerator();
            while (e.MoveNext()) 
            {
               SolvableConnection dc = (SolvableConnection)e.Current;
               if (dc.StreamPoint.Name.Equals(strName) && dc.UnitOpPoint.Name.Equals(uoName))
               {
                  toDeleteConnections.Add(dc);
               }
            }

            if (toDeleteConnections.Count > 0)
            {
               IEnumerator e2 = toDeleteConnections.GetEnumerator();
               while (e2.MoveNext()) 
               {
                  SolvableConnection conn = (SolvableConnection)e2.Current;
                  this.Connections.Remove(conn);
               }
               this.DrawConnections();
            }
         }
      }

      public void UpdateConnections(ProcessStreamBaseControl streamCtrl)
      {
         string name = streamCtrl.ProcessStreamBase.Name;

         PointOrientation inOrientation = streamCtrl.InOrientation;
         int inIdx = ProcessStreamBaseControl.IN_INDEX;
         Point inPoint = streamCtrl.GetInConnectionPoint();
         ConnectionPoint inCp = new ConnectionPoint(inIdx, name, inPoint, inOrientation);  
         
         PointOrientation outOrientation = streamCtrl.OutOrientation;
         int outIdx = ProcessStreamBaseControl.OUT_INDEX;
         Point outPoint = streamCtrl.GetOutConnectionPoint();
         ConnectionPoint outCp = new ConnectionPoint(outIdx, name, outPoint, outOrientation);
         
         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.StreamPoint.Equals(inCp))
            {
               dc.StreamPoint.Point = inPoint;
               dc.StreamPoint.Orientation = inOrientation;
            }
            if (dc.StreamPoint.Equals(outCp))
            {
               dc.StreamPoint.Point = outPoint;
               dc.StreamPoint.Orientation = outOrientation;
            }
         }
         this.DrawConnections();
      }
      
      public void UpdateConnections(TwoStreamUnitOpControl twoStrUnitOpCtrl)
      {
         string name = twoStrUnitOpCtrl.TwoStreamUnitOp.Name;

         PointOrientation inOrientation = TwoStreamUnitOpControl.INLET_ORIENTATION;
         // do an adjustment if ti is RecycleControl
         if (twoStrUnitOpCtrl is RecycleControl)
            inOrientation = RecycleControl.INLET_ORIENTATION;
         int inIdx = TwoStreamUnitOperation.INLET_INDEX;
         Point inPoint = twoStrUnitOpCtrl.GetStreamInConnectionPoint();
         ConnectionPoint inCp = new ConnectionPoint(inIdx, name, inPoint, inOrientation);  

         PointOrientation outOrientation = TwoStreamUnitOpControl.OUTLET_ORIENTATION;
         // do an adjustment if ti is RecycleControl
         if (twoStrUnitOpCtrl is RecycleControl)
            outOrientation = RecycleControl.OUTLET_ORIENTATION;
         int outIdx = TwoStreamUnitOperation.OUTLET_INDEX;
         Point outPoint = twoStrUnitOpCtrl.GetStreamOutConnectionPoint();
         ConnectionPoint outCp = new ConnectionPoint(outIdx, name, outPoint, outOrientation);  

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.UnitOpPoint.Equals(inCp))
            {
               dc.UnitOpPoint.Point = inPoint;
            }
            if (dc.UnitOpPoint.Equals(outCp))
            {
               dc.UnitOpPoint.Point = outPoint;
            }
         }
         this.DrawConnections();
      }

      public void UpdateConnections(CycloneControl cycloneCtrl)
      {
         string name = cycloneCtrl.Cyclone.Name;

         PointOrientation mixtureInOrientation = CycloneControl.MIXTURE_INLET_ORIENTATION;
         int mixtureInIdx = Cyclone.GAS_INLET_INDEX;
         Point mixtureInPoint = cycloneCtrl.GetGasInConnectionPoint();
         ConnectionPoint mixtureInCp = new ConnectionPoint(mixtureInIdx, name, mixtureInPoint, mixtureInOrientation);  

         PointOrientation fluidOutOrientation = CycloneControl.FLUID_OUTLET_ORIENTATION;
         int fluidOutIdx = Cyclone.GAS_OUTLET_INDEX;
         Point fluidOutPoint = cycloneCtrl.GetGasOutConnectionPoint();
         ConnectionPoint fluidOutCp = new ConnectionPoint(fluidOutIdx, name, fluidOutPoint, fluidOutOrientation);  

         PointOrientation particleOutOrientation = CycloneControl.PARTICLE_OUTLET_ORIENTATION;
         int particleOutIdx = Cyclone.PARTICLE_OUTLET_INDEX;
         Point particleOutPoint = cycloneCtrl.GetParticleOutConnectionPoint();
         ConnectionPoint particleOutCp = new ConnectionPoint(particleOutIdx, name, particleOutPoint, particleOutOrientation);  

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.UnitOpPoint.Equals(mixtureInCp))
            {
               dc.UnitOpPoint.Point = mixtureInPoint;
            }
            if (dc.UnitOpPoint.Equals(fluidOutCp))
            {
               dc.UnitOpPoint.Point = fluidOutPoint;
            }
            if (dc.UnitOpPoint.Equals(particleOutCp))
            {
               dc.UnitOpPoint.Point = particleOutPoint;
            }
         }
         this.DrawConnections();
      }
      
      public void UpdateConnections(EjectorControl ejectorCtrl)
      {
         string name = ejectorCtrl.Ejector.Name;

         PointOrientation motiveInOrientation = EjectorControl.MOTIVE_INLET_ORIENTATION;
         int motiveInIdx = Ejector.MOTIVE_INLET_INDEX;
         Point motiveInPoint = ejectorCtrl.GetMotiveInConnectionPoint();
         ConnectionPoint motiveInCp = new ConnectionPoint(motiveInIdx, name, motiveInPoint, motiveInOrientation);  

         PointOrientation suctionInOrientation = EjectorControl.SUCTION_INLET_ORIENTATION;
         int suctionInIdx = Ejector.SUCTION_INLET_INDEX;
         Point suctionInPoint = ejectorCtrl.GetSuctionInConnectionPoint();
         ConnectionPoint suctionInCp = new ConnectionPoint(suctionInIdx, name, suctionInPoint, suctionInOrientation);  

         PointOrientation dischargeOutOrientation = EjectorControl.DISCHARGE_OUTLET_ORIENTATION;
         int dischargeOutIdx = Ejector.DISCHARGE_OUTLET_INDEX;
         Point dischargeOutPoint = ejectorCtrl.GetDischargeOutConnectionPoint();
         ConnectionPoint dischargeOutCp = new ConnectionPoint(dischargeOutIdx, name, dischargeOutPoint, dischargeOutOrientation);  

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.UnitOpPoint.Equals(motiveInCp))
            {
               dc.UnitOpPoint.Point = motiveInPoint;
            }
            if (dc.UnitOpPoint.Equals(suctionInCp))
            {
               dc.UnitOpPoint.Point = suctionInPoint;
            }
            if (dc.UnitOpPoint.Equals(dischargeOutCp))
            {
               dc.UnitOpPoint.Point = dischargeOutPoint;
            }
         }
         this.DrawConnections();
      }
      
      public void UpdateConnections(WetScrubberControl wetScrubberCtrl)
      {
         string name = wetScrubberCtrl.WetScrubber.Name;

         PointOrientation gasInOrientation = WetScrubberControl.GAS_INLET_ORIENTATION;
         int gasInIdx = WetScrubber.GAS_INLET_INDEX;
         Point gasInPoint = wetScrubberCtrl.GetGasInConnectionPoint();
         ConnectionPoint gasInCp = new ConnectionPoint(gasInIdx, name, gasInPoint, gasInOrientation);  

         PointOrientation gasOutOrientation = WetScrubberControl.GAS_OUTLET_ORIENTATION;
         int gasOutIdx = WetScrubber.GAS_OUTLET_INDEX;
         Point gasOutPoint = wetScrubberCtrl.GetGasOutConnectionPoint();
         ConnectionPoint gasOutCp = new ConnectionPoint(gasOutIdx, name, gasOutPoint, gasOutOrientation);  

         PointOrientation liquidInOrientation = WetScrubberControl.LIQUID_INLET_ORIENTATION;
         int liquidInIdx = WetScrubber.LIQUID_INLET_INDEX;
         Point liquidInPoint = wetScrubberCtrl.GetLiquidInConnectionPoint();
         ConnectionPoint liquidInCp = new ConnectionPoint(liquidInIdx, name, liquidInPoint, liquidInOrientation);  

         PointOrientation liquidOutOrientation = WetScrubberControl.LIQUID_OUTLET_ORIENTATION;
         int liquidOutIdx = WetScrubber.LIQUID_OUTLET_INDEX;
         Point liquidOutPoint = wetScrubberCtrl.GetLiquidOutConnectionPoint();
         ConnectionPoint liquidOutCp = new ConnectionPoint(liquidOutIdx, name, liquidOutPoint, liquidOutOrientation);  

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.UnitOpPoint.Equals(gasInCp))
            {
               dc.UnitOpPoint.Point = gasInPoint;
            }
            if (dc.UnitOpPoint.Equals(gasOutCp))
            {
               dc.UnitOpPoint.Point = gasOutPoint;
            }
            if (dc.UnitOpPoint.Equals(liquidInCp))
            {
               dc.UnitOpPoint.Point = liquidInPoint;
            }
            if (dc.UnitOpPoint.Equals(liquidOutCp))
            {
               dc.UnitOpPoint.Point = liquidOutPoint;
            }
         }
         this.DrawConnections();
      }

      public void UpdateConnections(HeatExchangerControl heatExchangerCtrl)
      {
         string name = heatExchangerCtrl.HeatExchanger.Name;

         PointOrientation coldInOrientation = HeatExchangerControl.COLD_INLET_ORIENTATION;
         int coldInIdx = HeatExchanger.COLD_SIDE_INLET_INDEX;
         Point coldInPoint = heatExchangerCtrl.GetColdInConnectionPoint();
         ConnectionPoint coldInCp = new ConnectionPoint(coldInIdx, name, coldInPoint, coldInOrientation);  

         PointOrientation coldOutOrientation = HeatExchangerControl.COLD_OUTLET_ORIENTATION;
         int coldOutIdx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
         Point coldOutPoint = heatExchangerCtrl.GetColdOutConnectionPoint();
         ConnectionPoint coldOutCp = new ConnectionPoint(coldOutIdx, name, coldOutPoint, coldOutOrientation);  

         PointOrientation hotInOrientation = HeatExchangerControl.HOT_INLET_ORIENTATION;
         int hotInIdx = HeatExchanger.HOT_SIDE_INLET_INDEX;
         Point hotInPoint = heatExchangerCtrl.GetHotInConnectionPoint();
         ConnectionPoint hotInCp = new ConnectionPoint(hotInIdx, name, hotInPoint, hotInOrientation);  

         PointOrientation hotOutOrientation = HeatExchangerControl.HOT_OUTLET_ORIENTATION;
         int hotOutIdx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;
         Point hotOutPoint = heatExchangerCtrl.GetHotOutConnectionPoint();
         ConnectionPoint hotOutCp = new ConnectionPoint(hotOutIdx, name, hotOutPoint, hotOutOrientation);  

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.UnitOpPoint.Equals(coldInCp))
            {
               dc.UnitOpPoint.Point = coldInPoint;
            }
            if (dc.UnitOpPoint.Equals(coldOutCp))
            {
               dc.UnitOpPoint.Point = coldOutPoint;
            }
            if (dc.UnitOpPoint.Equals(hotInCp))
            {
               dc.UnitOpPoint.Point = hotInPoint;
            }
            if (dc.UnitOpPoint.Equals(hotOutCp))
            {
               dc.UnitOpPoint.Point = hotOutPoint;
            }
         }
         this.DrawConnections();
      }
      
      public void UpdateConnections(MixerControl mixerCtrl)
      {
         string name = mixerCtrl.Mixer.Name;

         ArrayList streamInList = new ArrayList();
         int count = mixerCtrl.Mixer.InletStreams.Count;
         for (int i=0; i<count; i++)
         {
            PointOrientation streamInOrientation = MixerControl.INLET_ORIENTATION;
            int streamInIdx = i+1;
            Point streamInPoint = mixerCtrl.GetStreamInConnectionPoint(i+1, count);
            ConnectionPoint streamInCp = new ConnectionPoint(streamInIdx, name, streamInPoint, streamInOrientation);
            streamInList.Add(streamInCp);
         }

         PointOrientation streamOutOrientation = MixerControl.OUTLET_ORIENTATION;
         int streamOutIdx = Mixer.OUTLET_INDEX;
         Point streamOutPoint = mixerCtrl.GetStreamOutConnectionPoint();
         ConnectionPoint streamOutCp = new ConnectionPoint(streamOutIdx, name, streamOutPoint, streamOutOrientation);  

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            IEnumerator en = streamInList.GetEnumerator();
            while (en.MoveNext())
            {
               ConnectionPoint streamInCp = (ConnectionPoint)en.Current;
               if (dc.UnitOpPoint.Equals(streamInCp))
               {
                  dc.UnitOpPoint.Point = streamInCp.Point;
               }
            }

            if (dc.UnitOpPoint.Equals(streamOutCp))
            {
               dc.UnitOpPoint.Point = streamOutPoint;
            }
         }
         this.DrawConnections();
      }
      
      public void UpdateConnections(TeeControl teeCtrl)
      {
         string name = teeCtrl.Tee.Name;

         ArrayList streamOutList = new ArrayList();
         int count = teeCtrl.Tee.OutletStreams.Count;
         for (int i=0; i<count; i++)
         {
            PointOrientation streamOutOrientation = TeeControl.OUTLET_ORIENTATION;
            int streamOutIdx = i+1;
            Point streamOutPoint = teeCtrl.GetStreamOutConnectionPoint(i+1, count);
            ConnectionPoint streamOutCp = new ConnectionPoint(streamOutIdx, name, streamOutPoint, streamOutOrientation);
            streamOutList.Add(streamOutCp);
         }

         PointOrientation streamInOrientation = TeeControl.INLET_ORIENTATION;
         int streamInIdx = Tee.INLET_INDEX;
         Point streamInPoint = teeCtrl.GetStreamInConnectionPoint();
         ConnectionPoint streamInCp = new ConnectionPoint(streamInIdx, name, streamInPoint, streamInOrientation);

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            IEnumerator en = streamOutList.GetEnumerator();
            while (en.MoveNext())
            {
               ConnectionPoint streamOutCp = (ConnectionPoint)en.Current;
               if (dc.UnitOpPoint.Equals(streamOutCp))
               {
                  dc.UnitOpPoint.Point = streamOutCp.Point;
               }
            }

            if (dc.UnitOpPoint.Equals(streamInCp))
            {
               dc.UnitOpPoint.Point = streamInPoint;
            }
         }
         this.DrawConnections();
      }

      public void UpdateConnections(FlashTankControl flashTankCtrl)
      {
         string name = flashTankCtrl.FlashTank.Name;

         PointOrientation inOrientation = FlashTankControl.INLET_ORIENTATION;
         int inIdx = FlashTank.INLET_INDEX;
         Point inPoint = flashTankCtrl.GetInConnectionPoint();
         ConnectionPoint inCp = new ConnectionPoint(inIdx, name, inPoint, inOrientation);  

         PointOrientation vaporOutOrientation = FlashTankControl.VAPOR_OUTLET_ORIENTATION;
         int vaporOutIdx = FlashTank.VAPOR_OUTLET_INDEX;
         Point vaporOutPoint = flashTankCtrl.GetVaporOutConnectionPoint();
         ConnectionPoint vaporOutCp = new ConnectionPoint(vaporOutIdx, name, vaporOutPoint, vaporOutOrientation);  

         PointOrientation liquidOutOrientation = FlashTankControl.LIQUID_OUTLET_ORIENTATION;
         int liquidOutIdx = FlashTank.LIQUID_OUTLET_INDEX;
         Point liquidOutPoint = flashTankCtrl.GetLiquidOutConnectionPoint();
         ConnectionPoint liquidOutCp = new ConnectionPoint(liquidOutIdx, name, liquidOutPoint, liquidOutOrientation);

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.UnitOpPoint.Equals(inCp))
            {
               dc.UnitOpPoint.Point = inPoint;
            }
            if (dc.UnitOpPoint.Equals(vaporOutCp))
            {
               dc.UnitOpPoint.Point = vaporOutPoint;
            }
            if (dc.UnitOpPoint.Equals(liquidOutCp))
            {
               dc.UnitOpPoint.Point = liquidOutPoint;
            }
         }
         this.DrawConnections();
      }

      public void UpdateConnections(DryerControl dryerCtrl)
      {
         string name = dryerCtrl.Dryer.Name;

         PointOrientation gasInOrientation = DryerControl.GAS_INLET_ORIENTATION;
         int gasInIdx = Dryer.GAS_INLET_INDEX;
         Point gasInPoint = dryerCtrl.GetGasInConnectionPoint();
         ConnectionPoint gasInCp = new ConnectionPoint(gasInIdx, name, gasInPoint, gasInOrientation);

         PointOrientation gasOutOrientation = DryerControl.GAS_OUTLET_ORIENTATION;
         int gasOutIdx = Dryer.GAS_OUTLET_INDEX;
         Point gasOutPoint = dryerCtrl.GetGasOutConnectionPoint();
         ConnectionPoint gasOutCp = new ConnectionPoint(gasOutIdx, name, gasOutPoint, gasOutOrientation);

         PointOrientation matInOrientation = DryerControl.MATERIAL_INLET_ORIENTATION;
         int matInIdx = Dryer.MATERIAL_INLET_INDEX;
         Point matInPoint = dryerCtrl.GetMaterialInConnectionPoint();
         ConnectionPoint matInCp = new ConnectionPoint(matInIdx, name, matInPoint, matInOrientation);

         PointOrientation matOutOrientation = DryerControl.MATERIAL_OUTLET_ORIENTATION;
         int matOutIdx = Dryer.MATERIAL_OUTLET_INDEX;
         Point matOutPoint = dryerCtrl.GetMaterialOutConnectionPoint();
         ConnectionPoint matOutCp = new ConnectionPoint(matOutIdx, name, matOutPoint, matOutOrientation);

         IEnumerator e = this.Connections.GetEnumerator();
         while (e.MoveNext()) 
         {
            SolvableConnection dc = (SolvableConnection)e.Current;

            if (dc.UnitOpPoint.Equals(gasInCp))
            {
               dc.UnitOpPoint.Point = gasInPoint;
            }
            if (dc.UnitOpPoint.Equals(gasOutCp))
            {
               dc.UnitOpPoint.Point = gasOutPoint;
            }
            if (dc.UnitOpPoint.Equals(matInCp))
            {
               dc.UnitOpPoint.Point = matInPoint;
            }
            if (dc.UnitOpPoint.Equals(matOutCp))
            {
               dc.UnitOpPoint.Point = matOutPoint;
            }
         }
         this.DrawConnections();
      }

      public void DeleteConnection(Point p)
      {
         if (this.Connections.Count > 0) 
         {
            IEnumerator e4 = this.Connections.GetEnumerator();
            while (e4.MoveNext())
            {
               SolvableConnection conn = (SolvableConnection)e4.Current;
               if (conn.HitTest(p))
               {
                  string streamName = conn.StreamPoint.Name;
                  string uoName = conn.UnitOpPoint.Name;

                  UnitOpControl unitOpCtrl = this.flowsheet.UnitOpManager.GetUnitOpControl(uoName);
                  ProcessStreamBaseControl psCtrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(streamName);
                  this.DetachStreamFromUnitOp(unitOpCtrl.UnitOperation, psCtrl.ProcessStreamBase);

                  break;
               }
            }
         }
      }

      private void DetachStreamFromUnitOp(UnitOperation unitOp, ProcessStreamBase ps)
      {
         if (unitOp != null && ps != null)
         {
            ErrorMessage error = unitOp.DetachStream(ps);
            if (error != null)
               UI.ShowError(error);
         }
      }
	}
}
