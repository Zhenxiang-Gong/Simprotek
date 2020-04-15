using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using Prosimo.UnitOperations;
using ProsimoUI.ProcessStreamsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;
using ProsimoUI.UnitOperationsUI.HeatExchangerUI;
using Prosimo.UnitSystems;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.FluidTransport;
using ProsimoUI.UnitOperationsUI.CycloneUI;
using ProsimoUI.UnitOperationsUI.DryerUI;

namespace ProsimoUI.UnitOperationsUI
{
	/// <summary>
	/// Summary description for UnitOpManager.
	/// </summary>
	public class UnitOpManager
	{
      private Flowsheet flowsheet;

      public bool HasUnitOpControls
      {
         get
         {
            return this.HasDryerControls ||
                   this.HasHeatExchangerControls ||
                   this.HasCycloneControls ||
                   this.HasEjectorControls ||
                   this.HasWetScrubberControls ||
                   this.HasScrubberCondenserControls ||
                   this.HasMixerControls ||
                   this.HasTeeControls ||
                   this.HasFlashTankControls ||
                   this.HasFanControls ||
                   this.HasValveControls ||
                   this.HasBagFilterControls ||
                   this.HasAirFilterControls ||
                   this.HasCompressorControls ||
                   this.HasHeaterControls ||
                   this.HasCoolerControls ||
                   this.HasElectrostaticPrecipitatorControls ||
                   this.HasPumpControls ||
                   this.HasRecycleControls;
         }
      }

      public bool HasDryerControls
      {
         get
         {
            if (this.GetUnitOpControls<Dryer>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorDryerControls
      {
         get
         {
            if (this.GetShowableInEditorDryerControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasHeatExchangerControls
      {
         get
         {
            if (this.GetUnitOpControls<HeatExchanger>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorHeatExchangerControls
      {
         get
         {
            if (this.GetShowableInEditorHeatExchangerControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasCycloneControls
      {
         get
         {
            if (this.GetUnitOpControls<Cyclone>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorCycloneControls
      {
         get
         {
            if (this.GetShowableInEditorCycloneControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasEjectorControls
      {
         get
         {
            if (this.GetUnitOpControls<Ejector>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorEjectorControls
      {
         get
         {
            if (this.GetShowableInEditorEjectorControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasWetScrubberControls
      {
         get
         {
            if (this.GetUnitOpControls<WetScrubber>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorWetScrubberControls
      {
         get
         {
            if (this.GetShowableInEditorWetScrubberControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasScrubberCondenserControls
      {
         get
         {
            if (this.GetUnitOpControls<ScrubberCondenser>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorScrubberCondenserControls
      {
         get
         {
            if (this.GetShowableInEditorScrubberCondenserControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasMixerControls
      {
         get
         {
            if (this.GetUnitOpControls<Mixer>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasTeeControls
      {
         get
         {
            if (this.GetUnitOpControls<Tee>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasFlashTankControls
      {
         get
         {
            if (this.GetUnitOpControls<FlashTank>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasFanControls
      {
         get
         {
            if (this.GetUnitOpControls<Fan>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorFanControls
      {
         get
         {
            if (this.GetShowableInEditorFanControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasValveControls
      {
         get
         {
            if (this.GetUnitOpControls<Valve>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorValveControls
      {
         get
         {
            if (this.GetShowableInEditorValveControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasBagFilterControls
      {
         get
         {
            if (this.GetUnitOpControls<BagFilter>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorBagFilterControls
      {
         get
         {
            if (this.GetShowableInEditorBagFilterControls().Count > 0)
               return true;
            else
               return false;
         }
      }
      
      public bool HasAirFilterControls
      {
         get
         {
            if (this.GetUnitOpControls<AirFilter>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorAirFilterControls
      {
         get
         {
            if (this.GetShowableInEditorAirFilterControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasCompressorControls
      {
         get
         {
            if (this.GetUnitOpControls<Compressor>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorCompressorControls
      {
         get
         {
            if (this.GetShowableInEditorCompressorControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasHeaterControls
      {
         get
         {
            if (this.GetUnitOpControls<Heater>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorHeaterControls
      {
         get
         {
            if (this.GetShowableInEditorHeaterControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasCoolerControls
      {
         get
         {
            if (this.GetUnitOpControls<Cooler>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorCoolerControls
      {
         get
         {
            if (this.GetShowableInEditorCoolerControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasElectrostaticPrecipitatorControls
      {
         get
         {
            if (this.GetUnitOpControls<ElectrostaticPrecipitator>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorElectrostaticPrecipitatorControls
      {
         get
         {
            if (this.GetShowableInEditorElectrostaticPrecipitatorControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasPumpControls
      {
         get
         {
            if (this.GetUnitOpControls<Pump>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasShowableInEditorPumpControls
      {
         get
         {
            if (this.GetShowableInEditorPumpControls().Count > 0)
               return true;
            else
               return false;
         }
      }

      public bool HasRecycleControls
      {
         get
         {
            if (this.GetUnitOpControls<Recycle>().Count > 0)
               return true;
            else
               return false;
         }
      }

      public UnitOpManager(Flowsheet flowsheet)
		{
         this.flowsheet = flowsheet;
         this.flowsheet.EvaporationAndDryingSystem.UnitOpAdded += new UnitOpAddedEventHandler(EvaporationAndDryingSystem_UnitOpAdded);
         this.flowsheet.EvaporationAndDryingSystem.UnitOpDeleted += new UnitOpDeletedEventHandler(EvaporationAndDryingSystem_UnitOpDeleted);
      }

      public UnitOpControl GetUnitOpControl(UnitOperation unitOp)
      {
         return this.GetUnitOpControl(unitOp.Name);
      }

      public UnitOpControl GetUnitOpControl(string name)
      {
         UnitOpControl uoCtrl = null;
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is SolvableControl)
            {
               SolvableControl feCtrl = (SolvableControl)e.Current;
               if (feCtrl is UnitOpControl)
               {
                  if (feCtrl is DryerControl)
                  {
                     DryerControl c = (DryerControl)feCtrl;
                     if (c.Dryer.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is HeatExchangerControl)
                  {
                     HeatExchangerControl c = (HeatExchangerControl)feCtrl;
                     if (c.HeatExchanger.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is CycloneControl)
                  {
                     CycloneControl c = (CycloneControl)feCtrl;
                     if (c.Cyclone.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is EjectorControl)
                  {
                     EjectorControl c = (EjectorControl)feCtrl;
                     if (c.Ejector.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is WetScrubberControl)
                  {
                     WetScrubberControl c = (WetScrubberControl)feCtrl;
                     if (c.WetScrubber.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is ScrubberCondenserControl)
                  {
                     ScrubberCondenserControl c = (ScrubberCondenserControl)feCtrl;
                     if (c.ScrubberCondenser.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is MixerControl)
                  {
                     MixerControl c = (MixerControl)feCtrl;
                     if (c.Mixer.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is TeeControl)
                  {
                     TeeControl c = (TeeControl)feCtrl;
                     if (c.Tee.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is FlashTankControl)
                  {
                     FlashTankControl c = (FlashTankControl)feCtrl;
                     if (c.FlashTank.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is FanControl)
                  {
                     FanControl c = (FanControl)feCtrl;
                     if (c.Fan.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is ValveControl)
                  {
                     ValveControl c = (ValveControl)feCtrl;
                     if (c.Valve.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is BagFilterControl)
                  {
                     BagFilterControl c = (BagFilterControl)feCtrl;
                     if (c.BagFilter.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is AirFilterControl)
                  {
                     AirFilterControl c = (AirFilterControl)feCtrl;
                     if (c.AirFilter.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is CompressorControl)
                  {
                     CompressorControl c = (CompressorControl)feCtrl;
                     if (c.Compressor.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is HeaterControl)
                  {
                     HeaterControl c = (HeaterControl)feCtrl;
                     if (c.Heater.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is CoolerControl)
                  {
                     CoolerControl c = (CoolerControl)feCtrl;
                     if (c.Cooler.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is ElectrostaticPrecipitatorControl)
                  {
                     ElectrostaticPrecipitatorControl c = (ElectrostaticPrecipitatorControl)feCtrl;
                     if (c.ElectrostaticPrecipitator.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is PumpControl)
                  {
                     PumpControl c = (PumpControl)feCtrl;
                     if (c.Pump.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
                  if (feCtrl is RecycleControl)
                  {
                     RecycleControl c = (RecycleControl)feCtrl;
                     if (c.Recycle.Name.Equals(name))
                     {
                        uoCtrl = c;
                        break;
                     }
                  }
               }
            }
         }
         return uoCtrl;
      }

      public IList<T> GetUnitOpControls<T>()
      {
         IList<T> ctrls = new List<T>();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is T)
            {
               ctrls.Add((T)e.Current);
            }
         }
         return ctrls;
      }

      public IList GetShowableInEditorUnitOpControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is UnitOpControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public IList GetShowableInEditorDryerControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is DryerControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public DryerControl GetShowableInEditorDryerControl(string name)
      {
         DryerControl ctrl = null;
         IList ctrls = this.GetShowableInEditorDryerControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            DryerControl ctrl2 = (DryerControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorHeatExchangerControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is HeatExchangerControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public HeatExchangerControl GetShowableInEditorHeatExchangerControl(string name)
      {
         HeatExchangerControl ctrl = null;
         IList ctrls = this.GetShowableInEditorHeatExchangerControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            HeatExchangerControl ctrl2 = (HeatExchangerControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorCycloneControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is CycloneControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public CycloneControl GetShowableInEditorCycloneControl(string name)
      {
         CycloneControl ctrl = null;
         IList ctrls = this.GetShowableInEditorCycloneControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            CycloneControl ctrl2 = (CycloneControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }
      
      public IList GetShowableInEditorEjectorControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is EjectorControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public EjectorControl GetShowableInEditorEjectorControl(string name)
      {
         EjectorControl ctrl = null;
         IList ctrls = this.GetShowableInEditorEjectorControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            EjectorControl ctrl2 = (EjectorControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }
      
      public IList GetShowableInEditorWetScrubberControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is WetScrubberControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public WetScrubberControl GetShowableInEditorWetScrubberControl(string name)
      {
         WetScrubberControl ctrl = null;
         IList ctrls = this.GetShowableInEditorWetScrubberControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            WetScrubberControl ctrl2 = (WetScrubberControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorScrubberCondenserControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext())
         {
            if (e.Current is ScrubberCondenserControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public ScrubberCondenserControl GetShowableInEditorScrubberCondenserControl(string name)
      {
         ScrubberCondenserControl ctrl = null;
         IList ctrls = this.GetShowableInEditorScrubberCondenserControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            ScrubberCondenserControl ctrl2 = (ScrubberCondenserControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }
      
      public IList GetShowableInEditorFanControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is FanControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public FanControl GetShowableInEditorFanControl(string name)
      {
         FanControl ctrl = null;
         IList ctrls = this.GetShowableInEditorFanControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            FanControl ctrl2 = (FanControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorValveControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is ValveControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public ValveControl GetShowableInEditorValveControl(string name)
      {
         ValveControl ctrl = null;
         IList ctrls = this.GetShowableInEditorValveControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            ValveControl ctrl2 = (ValveControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorBagFilterControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is BagFilterControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public BagFilterControl GetShowableInEditorBagFilterControl(string name)
      {
         BagFilterControl ctrl = null;
         IList ctrls = this.GetShowableInEditorBagFilterControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            BagFilterControl ctrl2 = (BagFilterControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorAirFilterControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is AirFilterControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public AirFilterControl GetShowableInEditorAirFilterControl(string name)
      {
         AirFilterControl ctrl = null;
         IList ctrls = this.GetShowableInEditorAirFilterControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            AirFilterControl ctrl2 = (AirFilterControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorCompressorControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is CompressorControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public CompressorControl GetShowableInEditorCompressorControl(string name)
      {
         CompressorControl ctrl = null;
         IList ctrls = this.GetShowableInEditorCompressorControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            CompressorControl ctrl2 = (CompressorControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorHeaterControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is HeaterControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public HeaterControl GetShowableInEditorHeaterControl(string name)
      {
         HeaterControl ctrl = null;
         IList ctrls = this.GetShowableInEditorHeaterControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            HeaterControl ctrl2 = (HeaterControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public IList GetShowableInEditorCoolerControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is CoolerControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public CoolerControl GetShowableInEditorCoolerControl(string name)
      {
         CoolerControl ctrl = null;
         IList ctrls = this.GetShowableInEditorCoolerControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            CoolerControl ctrl2 = (CoolerControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }
      
      public IList GetShowableInEditorElectrostaticPrecipitatorControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is ElectrostaticPrecipitatorControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public ElectrostaticPrecipitatorControl GetShowableInEditorElectrostaticPrecipitatorControl(string name)
      {
         ElectrostaticPrecipitatorControl ctrl = null;
         IList ctrls = this.GetShowableInEditorElectrostaticPrecipitatorControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            ElectrostaticPrecipitatorControl ctrl2 = (ElectrostaticPrecipitatorControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }
      
      public IList GetShowableInEditorPumpControls()
      {
         IList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is PumpControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }
      
      public PumpControl GetShowableInEditorPumpControl(string name)
      {
         PumpControl ctrl = null;
         IList ctrls = this.GetShowableInEditorPumpControls();
         IEnumerator e = ctrls.GetEnumerator();
         while (e.MoveNext())
         {
            PumpControl ctrl2 = (PumpControl)e.Current;
            if (ctrl2.UnitOperation.Name.Equals(name))
            {
               ctrl = ctrl2;
               break;
            }
         }
         return ctrl;
      }

      public void DeleteSelectedUnitOpControls()
      {
         if (this.HasUnitOpControls) 
         {
            ArrayList toDeleteControls = new ArrayList();

            IEnumerator e = this.flowsheet.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is UnitOpControl)
               {
                  UnitOpControl ctrl = (UnitOpControl)e.Current;
                  if (ctrl.IsSelected)
                  {
                     toDeleteControls.Add(ctrl);
                  }
               }
            }

            if (toDeleteControls.Count > 0)
            {
               string message = "Are you sure that you want to delete the selected Unit Operations?";
               DialogResult dr = MessageBox.Show(this.flowsheet, message, "Delete: " + this.flowsheet.Text,
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

               switch (dr)
               {
                  case System.Windows.Forms.DialogResult.Yes:
                     IEnumerator e2 = toDeleteControls.GetEnumerator();
                     while (e2.MoveNext())
                     {
                        UnitOpControl ctrl = (UnitOpControl)e2.Current;
                        // delete from the model, the UI will be updated in the event listener
                        UnitOperation unitOp = ctrl.UnitOperation;
                        this.flowsheet.EvaporationAndDryingSystem.DeleteUnitOperation(unitOp);
                     }
                     break;
                  case System.Windows.Forms.DialogResult.No:
                     break;
               }
            }
         }
      }

      public void EditSelectedUnitOpControl()
      {
         if (this.HasUnitOpControls) 
         {   
            ArrayList toEditControls = new ArrayList();

            IEnumerator e = this.flowsheet.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is UnitOpControl)
               {
                  UnitOpControl ctrl = (UnitOpControl)e.Current;
                  if (ctrl.IsSelected)
                  {
                     toEditControls.Add(ctrl);
                  }
               }
            }

            if (toEditControls.Count < 1)
            { 
               string message = "Please select a Unit Operation."; 
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (toEditControls.Count > 1)
            {
               string message = "Please select only one Unit Operation."; 
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               ((IEditable)toEditControls[0]).Edit();
            }
         }
      }

      private void AddCtrlForDryerGasInlet(DryerControl uoCtrl)
      {
         if (uoCtrl.Dryer.GasInlet != null)
         {
            GasStreamControl strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, uoCtrl.Dryer.GasInlet);
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Dryer, strCtrl.ProcessStreamBase, Dryer.GAS_INLET_INDEX);
         }
      }

      private void AddCtrlForDryerGasOutlet(DryerControl uoCtrl)
      {
         if (uoCtrl.Dryer.GasOutlet != null)
         {
            GasStreamControl strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, uoCtrl.Dryer.GasOutlet);
            Point locOut = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Dryer, strCtrl.ProcessStreamBase, Dryer.GAS_OUTLET_INDEX);
         }
      }

      private void AddCtrlForDryerMaterialInlet(DryerControl uoCtrl)
      {
         if (uoCtrl.Dryer.MaterialInlet != null)
         {
            MaterialStreamControl strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, uoCtrl.Dryer.MaterialInlet);
            Point locIn = UI.CalculateUpStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width);
            strCtrl.Location = locIn; // this is a default location
            // give it a more left location 
            strCtrl.Location = new Point(uoCtrl.Location.X - strCtrl.Width, locIn.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Dryer, strCtrl.ProcessStreamBase, Dryer.MATERIAL_INLET_INDEX);
         }
      }

      private void AddCtrlForDryerMaterialOutlet(DryerControl uoCtrl)
      {
         if (uoCtrl.Dryer.MaterialOutlet != null)
         {
            MaterialStreamControl strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, uoCtrl.Dryer.MaterialOutlet);
            Point locOut = UI.CalculateDownStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut; // this is the default location
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Dryer, strCtrl.ProcessStreamBase, Dryer.MATERIAL_OUTLET_INDEX);
         }
      }

      private void AddCtrlForTwoStreamUnitOpInlet(TwoStreamUnitOpControl uoCtrl)
      {
         if (uoCtrl.TwoStreamUnitOp.Inlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.TwoStreamUnitOp.Inlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.TwoStreamUnitOp.Inlet);
            }
            else if (uoCtrl.TwoStreamUnitOp.Inlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.TwoStreamUnitOp.Inlet);
            }
            else if (uoCtrl.TwoStreamUnitOp.Inlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.TwoStreamUnitOp.Inlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            // adjust for RecycleControl
            if (uoCtrl is RecycleControl)
               locIn = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.TwoStreamUnitOp, strCtrl.ProcessStreamBase, TwoStreamUnitOperation.INLET_INDEX);
         }
      }
      
      private void AddCtrlForTwoStreamUnitOpOutlet(TwoStreamUnitOpControl uoCtrl)
      {
         if (uoCtrl.TwoStreamUnitOp.Outlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.TwoStreamUnitOp.Outlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.TwoStreamUnitOp.Outlet);
            }
            else if (uoCtrl.TwoStreamUnitOp.Outlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.TwoStreamUnitOp.Outlet);
            }
            else if (uoCtrl.TwoStreamUnitOp.Outlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.TwoStreamUnitOp.Outlet);
            }
            Point locOut = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            // adjust for RecycleControl
            if (uoCtrl is RecycleControl)
               locOut = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locOut;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.TwoStreamUnitOp, strCtrl.ProcessStreamBase, TwoStreamUnitOperation.OUTLET_INDEX);
         }      
      }

      private void AddCtrlForCycloneGasInlet(CycloneControl uoCtrl)
      {
         if (uoCtrl.Cyclone.GasInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Cyclone.GasInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.Cyclone.GasInlet);
            }
            else if (uoCtrl.Cyclone.GasInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Cyclone.GasInlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            // give it a more up location 
            strCtrl.Location = new Point(strCtrl.Location.X, strCtrl.Location.Y-10); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Cyclone, strCtrl.ProcessStreamBase, Cyclone.GAS_INLET_INDEX);
         }
      }

      private void AddCtrlForCycloneGasOutlet(CycloneControl uoCtrl)
      {
         if (uoCtrl.Cyclone.GasOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Cyclone.GasOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.Cyclone.GasOutlet);
            }
            else if (uoCtrl.Cyclone.GasOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Cyclone.GasOutlet);
            }
            Point locOut = UI.CalculateUpStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width);
            strCtrl.Location = locOut; // this is the default location
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Cyclone, strCtrl.ProcessStreamBase, Cyclone.GAS_OUTLET_INDEX);
         }      
      }

      private void AddCtrlForCycloneParticleOutlet(CycloneControl uoCtrl)
      {
         if (uoCtrl.Cyclone.ParticleOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Cyclone.ParticleOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.Cyclone.ParticleOutlet);
            }
            else if (uoCtrl.Cyclone.ParticleOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Cyclone.ParticleOutlet);
            }
            Point locOut = UI.CalculateDownStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut; // this is the default location
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Cyclone, strCtrl.ProcessStreamBase, Cyclone.PARTICLE_OUTLET_INDEX);
         }      
      }
      
      private void AddCtrlForEjectorMotiveInlet(EjectorControl uoCtrl)
      {
         if (uoCtrl.Ejector.MotiveInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Ejector.MotiveInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.Ejector.MotiveInlet);
            }
            else if (uoCtrl.Ejector.MotiveInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Ejector.MotiveInlet);
            }
            else if (uoCtrl.Ejector.MotiveInlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.Ejector.MotiveInlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Ejector, strCtrl.ProcessStreamBase, Ejector.MOTIVE_INLET_INDEX);
         }
      }

      private void AddCtrlForEjectorSuctionInlet(EjectorControl uoCtrl)
      {
         if (uoCtrl.Ejector.SuctionInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Ejector.SuctionInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.Ejector.SuctionInlet);
            }
            else if (uoCtrl.Ejector.SuctionInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Ejector.SuctionInlet);
            }
            else if (uoCtrl.Ejector.SuctionInlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.Ejector.SuctionInlet);
            }
            Point locIn = UI.CalculateDownStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locIn; // this is the default location
            // give it a more left location 
            strCtrl.Location = new Point(uoCtrl.Location.X - uoCtrl.Width, locIn.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Ejector, strCtrl.ProcessStreamBase, Ejector.SUCTION_INLET_INDEX);
         }
      }

      private void AddCtrlForEjectorDischargeOutlet(EjectorControl uoCtrl)
      {
         if (uoCtrl.Ejector.DischargeOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Ejector.DischargeOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.Ejector.DischargeOutlet);
            }
            else if (uoCtrl.Ejector.DischargeOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Ejector.DischargeOutlet);
            }
            else if (uoCtrl.Ejector.DischargeOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.Ejector.DischargeOutlet);
            }
            Point locOut = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Ejector, strCtrl.ProcessStreamBase, Ejector.DISCHARGE_OUTLET_INDEX);
         }      
      }
      
      private void AddCtrlForHeatExchangerColdInlet(HeatExchangerControl uoCtrl)
      {
         if (uoCtrl.HeatExchanger.ColdSideInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.HeatExchanger.ColdSideInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.HeatExchanger.ColdSideInlet);
            }
            else if (uoCtrl.HeatExchanger.ColdSideInlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.HeatExchanger.ColdSideInlet);
            } 
            else if (uoCtrl.HeatExchanger.ColdSideInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.HeatExchanger.ColdSideInlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.HeatExchanger, strCtrl.ProcessStreamBase, HeatExchanger.COLD_SIDE_INLET_INDEX);
         }
      }

      private void AddCtrlForHeatExchangerColdOutlet(HeatExchangerControl uoCtrl)
      {
         if (uoCtrl.HeatExchanger.ColdSideOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.HeatExchanger.ColdSideOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.HeatExchanger.ColdSideOutlet);
            }
            else if (uoCtrl.HeatExchanger.ColdSideOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.HeatExchanger.ColdSideOutlet);
            } 
            else if (uoCtrl.HeatExchanger.ColdSideOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.HeatExchanger.ColdSideOutlet);
            }
            Point locOut = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.HeatExchanger, strCtrl.ProcessStreamBase, HeatExchanger.COLD_SIDE_OUTLET_INDEX);
         }
      }

      private void AddCtrlForHeatExchangerHotInlet(HeatExchangerControl uoCtrl)
      {
         if (uoCtrl.HeatExchanger.HotSideInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.HeatExchanger.HotSideInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.HeatExchanger.HotSideInlet);
            }
            else if (uoCtrl.HeatExchanger.HotSideInlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.HeatExchanger.HotSideInlet);
            } 
            else if (uoCtrl.HeatExchanger.HotSideInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.HeatExchanger.HotSideInlet);
            }
            Point locIn = UI.CalculateUpStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width);
            strCtrl.Location = locIn;
            // give it a more left location 
            strCtrl.Location = new Point(uoCtrl.Location.X - strCtrl.Width, locIn.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.HeatExchanger, strCtrl.ProcessStreamBase, HeatExchanger.HOT_SIDE_INLET_INDEX);
         }
      }

      private void AddCtrlForHeatExchangerHotOutlet(HeatExchangerControl uoCtrl)
      {
         if (uoCtrl.HeatExchanger.HotSideOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.HeatExchanger.HotSideOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.HeatExchanger.HotSideOutlet);
            }
            else if (uoCtrl.HeatExchanger.HotSideOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.HeatExchanger.HotSideOutlet);
            } 
            else if (uoCtrl.HeatExchanger.HotSideOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.HeatExchanger.HotSideOutlet);
            }
            Point locOut = UI.CalculateDownStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut;
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.HeatExchanger, strCtrl.ProcessStreamBase, HeatExchanger.HOT_SIDE_OUTLET_INDEX);
         }
      }
      
      private void AddCtrlForWetScrubberGasInlet(WetScrubberControl uoCtrl)
      {
         if (uoCtrl.WetScrubber.GasInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.WetScrubber.GasInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.WetScrubber.GasInlet);
            }
            else if (uoCtrl.WetScrubber.GasInlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.WetScrubber.GasInlet);
            } 
            else if (uoCtrl.WetScrubber.GasInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.WetScrubber.GasInlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.WetScrubber, strCtrl.ProcessStreamBase, WetScrubber.GAS_INLET_INDEX);
         }
      }

      private void AddCtrlForWetScrubberGasOutlet(WetScrubberControl uoCtrl)
      {
         if (uoCtrl.WetScrubber.GasOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.WetScrubber.GasOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.WetScrubber.GasOutlet);
            }
            else if (uoCtrl.WetScrubber.GasOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.WetScrubber.GasOutlet);
            } 
            else if (uoCtrl.WetScrubber.GasOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.WetScrubber.GasOutlet);
            }
            Point locOut = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.WetScrubber, strCtrl.ProcessStreamBase, WetScrubber.GAS_OUTLET_INDEX);
         }
      }

      private void AddCtrlForWetScrubberLiquidInlet(WetScrubberControl uoCtrl)
      {
         if (uoCtrl.WetScrubber.LiquidInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.WetScrubber.LiquidInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.WetScrubber.LiquidInlet);
            }
            else if (uoCtrl.WetScrubber.LiquidInlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.WetScrubber.LiquidInlet);
            } 
            else if (uoCtrl.WetScrubber.LiquidInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.WetScrubber.LiquidInlet);
            }
            Point locIn = UI.CalculateUpStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width);
            strCtrl.Location = locIn;
            // give it a more left location 
            strCtrl.Location = new Point(uoCtrl.Location.X - strCtrl.Width, locIn.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.WetScrubber, strCtrl.ProcessStreamBase, WetScrubber.LIQUID_INLET_INDEX);
         }
      }

      private void AddCtrlForWetScrubberLiquidOutlet(WetScrubberControl uoCtrl)
      {
         if (uoCtrl.WetScrubber.LiquidOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.WetScrubber.LiquidOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.WetScrubber.LiquidOutlet);
            }
            else if (uoCtrl.WetScrubber.LiquidOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.WetScrubber.LiquidOutlet);
            } 
            else if (uoCtrl.WetScrubber.LiquidOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.WetScrubber.LiquidOutlet);
            }
            Point locOut = UI.CalculateDownStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut;
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.WetScrubber, strCtrl.ProcessStreamBase, WetScrubber.LIQUID_OUTLET_INDEX);
         }
      }

      private void AddCtrlForScrubberCondenserGasInlet(ScrubberCondenserControl uoCtrl)
      {
         if (uoCtrl.ScrubberCondenser.GasInlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.ScrubberCondenser.GasInlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.ScrubberCondenser.GasInlet);
            }
            else if (uoCtrl.ScrubberCondenser.GasInlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.ScrubberCondenser.GasInlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.ScrubberCondenser, strCtrl.ProcessStreamBase, ScrubberCondenser.GAS_INLET_INDEX);
         }
      }

      private void AddCtrlForScrubberCondenserGasOutlet(ScrubberCondenserControl uoCtrl)
      {
         if (uoCtrl.ScrubberCondenser.GasOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.ScrubberCondenser.GasOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.ScrubberCondenser.GasOutlet);
            }
            else if (uoCtrl.ScrubberCondenser.GasOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.ScrubberCondenser.GasOutlet);
            }
            Point locOut = UI.CalculateUpStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width);
            strCtrl.Location = locOut; // this is the default location
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y);
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.ScrubberCondenser, strCtrl.ProcessStreamBase, ScrubberCondenser.GAS_OUTLET_INDEX);
         }
      }

      private void AddCtrlForScrubberCondenserLiquidOutlet(ScrubberCondenserControl uoCtrl)
      {
         if (uoCtrl.ScrubberCondenser.LiquidOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.ScrubberCondenser.LiquidOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.ScrubberCondenser.LiquidOutlet);
            }
            else if (uoCtrl.ScrubberCondenser.LiquidOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.ScrubberCondenser.LiquidOutlet);
            }
            Point locOut = UI.CalculateDownStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut; // this is the default location
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y);
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.ScrubberCondenser, strCtrl.ProcessStreamBase, ScrubberCondenser.LIQUID_OUTLET_INDEX);
         }
      }
      
      private void AddCtrlForFlashTankInlet(FlashTankControl uoCtrl)
      {
         if (uoCtrl.FlashTank.Inlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.FlashTank.Inlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.FlashTank.Inlet);
            }
            else if (uoCtrl.FlashTank.Inlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.FlashTank.Inlet);
            }
            else if (uoCtrl.FlashTank.Inlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.FlashTank.Inlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.FlashTank, strCtrl.ProcessStreamBase, FlashTank.INLET_INDEX);
         }
      }

      private void AddCtrlForFlashTankVaporOutlet(FlashTankControl uoCtrl)
      {
         if (uoCtrl.FlashTank.VaporOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.FlashTank.VaporOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.FlashTank.VaporOutlet);
            }
            else if (uoCtrl.FlashTank.VaporOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.FlashTank.VaporOutlet);
            }
            else if (uoCtrl.FlashTank.VaporOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.FlashTank.VaporOutlet);
            }
            Point locOut = UI.CalculateUpStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width);
            strCtrl.Location = locOut; // this is the default location
            // give it a more right location 
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.FlashTank, strCtrl.ProcessStreamBase, FlashTank.VAPOR_OUTLET_INDEX);
         }      
      }

      private void AddCtrlForFlashTankLiquidOutlet(FlashTankControl uoCtrl)
      {
         if (uoCtrl.FlashTank.LiquidOutlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.FlashTank.LiquidOutlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.FlashTank.LiquidOutlet);
            }
            else if (uoCtrl.FlashTank.LiquidOutlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.FlashTank.LiquidOutlet);
            }
            else if (uoCtrl.FlashTank.LiquidOutlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.FlashTank.LiquidOutlet);
            }
            Point locOut = UI.CalculateDownStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut; // this is the default location
            // give it a more right location
            strCtrl.Location = new Point(uoCtrl.Location.X + uoCtrl.Width, locOut.Y); 
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.FlashTank, strCtrl.ProcessStreamBase, FlashTank.LIQUID_OUTLET_INDEX);
         }      
      }

      private void AddCtrlsForMixerInlets(MixerControl uoCtrl)
      {
         int k = 1;
         int n = uoCtrl.Mixer.InletStreams.Count;
         IEnumerator e = uoCtrl.Mixer.InletStreams.GetEnumerator();
         while (e.MoveNext())
         {
            ProcessStreamBaseControl strCtrl = null;
            ProcessStreamBase processStreamBase = (ProcessStreamBase)e.Current;
            if (processStreamBase is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)processStreamBase);
            }
            else if (processStreamBase is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)processStreamBase);
            }
            else if (processStreamBase is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)processStreamBase);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, k, n);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Mixer, strCtrl.ProcessStreamBase, k); // 1,2,3,...
            k++;
         }
      }

      private void AddCtrlsForTeeOutlets(TeeControl uoCtrl)
      {
         int k = 1;
         int n = uoCtrl.Tee.OutletStreams.Count;
         IEnumerator e = uoCtrl.Tee.OutletStreams.GetEnumerator();
         while (e.MoveNext())
         {
            ProcessStreamBaseControl strCtrl = null;
            ProcessStreamBase processStreamBase = (ProcessStreamBase)e.Current;
            if (processStreamBase is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)processStreamBase);
            }
            else if (processStreamBase is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)processStreamBase);
            }
            else if (processStreamBase is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)processStreamBase);
            }
            Point locOut = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, k, n, this.flowsheet.Width);
            strCtrl.Location = locOut;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Tee, strCtrl.ProcessStreamBase, k); // 1,2,3,...
            k++;
         }
      }

      private void AddCtrlForMixerOutlet(MixerControl uoCtrl)
      {
         if (uoCtrl.Mixer.Outlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Mixer.Outlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.Mixer.Outlet);
            }
            else if (uoCtrl.Mixer.Outlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.Mixer.Outlet);
            }
            else if (uoCtrl.Mixer.Outlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Mixer.Outlet);
            }
            Point locOut = UI.CalculateRightStreamLocation(uoCtrl, strCtrl, this.flowsheet.Width, this.flowsheet.Height);
            strCtrl.Location = locOut;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Mixer, strCtrl.ProcessStreamBase, Mixer.OUTLET_INDEX);
         }      
      }

      private void AddCtrlForTeeInlet(TeeControl uoCtrl)
      {
         if (uoCtrl.Tee.Inlet != null)
         {
            ProcessStreamBaseControl strCtrl = null;
            if (uoCtrl.Tee.Inlet is DryingGasStream)
            {
               strCtrl = new GasStreamControl(this.flowsheet, uoCtrl.Location, (DryingGasStream)uoCtrl.Tee.Inlet);
            }
            else if (uoCtrl.Tee.Inlet is DryingMaterialStream)
            {
               strCtrl = new MaterialStreamControl(this.flowsheet, uoCtrl.Location, (DryingMaterialStream)uoCtrl.Tee.Inlet);
            }
            else if (uoCtrl.Tee.Inlet is ProcessStream)
            {
               strCtrl = new ProcessStreamControl(this.flowsheet, uoCtrl.Location, (ProcessStream)uoCtrl.Tee.Inlet);
            }
            Point locIn = UI.CalculateLeftStreamLocation(uoCtrl, strCtrl, this.flowsheet.Height);
            strCtrl.Location = locIn;
            this.flowsheet.Controls.Add(strCtrl);
            uoCtrl.CreateConnection(uoCtrl.Tee, strCtrl.ProcessStreamBase, Tee.INLET_INDEX);
         }      
      }

      private void EvaporationAndDryingSystem_UnitOpDeleted(string unitOpName)
      {
         UnitOpControl ctrl = GetUnitOpControl(unitOpName);
         this.flowsheet.RemoveSolvableControl(ctrl);
      }

      private void EvaporationAndDryingSystem_UnitOpAdded(UnitOperation uo)
      {
         Point location = new System.Drawing.Point(this.flowsheet.X, this.flowsheet.Y);

         if (uo is Dryer)
         {
            DryerControl dryerCtrl = new DryerControl(this.flowsheet, location, uo as Dryer);
            this.flowsheet.Controls.Add(dryerCtrl);

            this.AddCtrlForDryerGasInlet(dryerCtrl);
            this.AddCtrlForDryerGasOutlet(dryerCtrl);
            this.AddCtrlForDryerMaterialInlet(dryerCtrl);
            this.AddCtrlForDryerMaterialOutlet(dryerCtrl);
         }
         else if (uo is HeatExchanger)
         {
            HeatExchangerControl heatExchangerCtrl = new HeatExchangerControl(this.flowsheet, location, uo as HeatExchanger);
            this.flowsheet.Controls.Add(heatExchangerCtrl);

            this.AddCtrlForHeatExchangerColdInlet(heatExchangerCtrl);
            this.AddCtrlForHeatExchangerColdOutlet(heatExchangerCtrl);
            this.AddCtrlForHeatExchangerHotInlet(heatExchangerCtrl);
            this.AddCtrlForHeatExchangerHotOutlet(heatExchangerCtrl);
         }
         else if (uo is Cyclone)
         {
            CycloneControl cycloneCtrl = new CycloneControl(this.flowsheet, location, uo as Cyclone);
            this.flowsheet.Controls.Add(cycloneCtrl);

            this.AddCtrlForCycloneGasInlet(cycloneCtrl);
            this.AddCtrlForCycloneGasOutlet(cycloneCtrl);         
            this.AddCtrlForCycloneParticleOutlet(cycloneCtrl);
         }
         else if (uo is Ejector)
         {
            EjectorControl ejectorCtrl = new EjectorControl(this.flowsheet, location, uo as Ejector);
            this.flowsheet.Controls.Add(ejectorCtrl);

            this.AddCtrlForEjectorMotiveInlet(ejectorCtrl);
            this.AddCtrlForEjectorSuctionInlet(ejectorCtrl);         
            this.AddCtrlForEjectorDischargeOutlet(ejectorCtrl);
         }
         else if (uo is WetScrubber)
         {
            WetScrubberControl wetScrubberCtrl = new WetScrubberControl(this.flowsheet, location, uo as WetScrubber);
            this.flowsheet.Controls.Add(wetScrubberCtrl);

            this.AddCtrlForWetScrubberGasInlet(wetScrubberCtrl);
            this.AddCtrlForWetScrubberGasOutlet(wetScrubberCtrl);         
            this.AddCtrlForWetScrubberLiquidInlet(wetScrubberCtrl);
            this.AddCtrlForWetScrubberLiquidOutlet(wetScrubberCtrl);
         }
         else if (uo is ScrubberCondenser)
         {
            ScrubberCondenserControl scrubberCondenserCtrl = new ScrubberCondenserControl(this.flowsheet, location, uo as ScrubberCondenser);
            this.flowsheet.Controls.Add(scrubberCondenserCtrl);

            this.AddCtrlForScrubberCondenserGasInlet(scrubberCondenserCtrl);
            this.AddCtrlForScrubberCondenserGasOutlet(scrubberCondenserCtrl);
            this.AddCtrlForScrubberCondenserLiquidOutlet(scrubberCondenserCtrl);
         }
         else if (uo is Mixer)
         {
            MixerControl mixerCtrl = new MixerControl(this.flowsheet, location, uo as Mixer);
            this.flowsheet.Controls.Add(mixerCtrl);

            this.AddCtrlsForMixerInlets(mixerCtrl);
            this.AddCtrlForMixerOutlet(mixerCtrl);
         }
         else if (uo is Tee)
         {
            TeeControl teeCtrl = new TeeControl(this.flowsheet, location, uo as Tee);
            this.flowsheet.Controls.Add(teeCtrl);

            this.AddCtrlsForTeeOutlets(teeCtrl);
            this.AddCtrlForTeeInlet(teeCtrl);
         }
         else if (uo is FlashTank)
         {
            FlashTankControl flashTankCtrl = new FlashTankControl(this.flowsheet, location, uo as FlashTank);
            this.flowsheet.Controls.Add(flashTankCtrl);

            this.AddCtrlForFlashTankInlet(flashTankCtrl);
            this.AddCtrlForFlashTankVaporOutlet(flashTankCtrl);         
            this.AddCtrlForFlashTankLiquidOutlet(flashTankCtrl);
         }
         else if (uo is Fan)
         {
            FanControl fanCtrl = new FanControl(this.flowsheet, location, uo as Fan);
            this.flowsheet.Controls.Add(fanCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(fanCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(fanCtrl);
         }
         else if (uo is Valve)
         {
            ValveControl valveCtrl = new ValveControl(this.flowsheet, location, uo as Valve);
            this.flowsheet.Controls.Add(valveCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(valveCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(valveCtrl);
         }
         else if (uo is BagFilter)
         {
            BagFilterControl bagFilterCtrl = new BagFilterControl(this.flowsheet, location, uo as BagFilter);
            this.flowsheet.Controls.Add(bagFilterCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(bagFilterCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(bagFilterCtrl);
         }
         else if (uo is AirFilter)
         {
            AirFilterControl airFilterCtrl = new AirFilterControl(this.flowsheet, location, uo as AirFilter);
            this.flowsheet.Controls.Add(airFilterCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(airFilterCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(airFilterCtrl);
         }
         else if (uo is Compressor)
         {
            CompressorControl compressorCtrl = new CompressorControl(this.flowsheet, location, uo as Compressor);
            this.flowsheet.Controls.Add(compressorCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(compressorCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(compressorCtrl);
         }
         else if (uo is Heater)
         {
            HeaterControl heaterCtrl = new HeaterControl(this.flowsheet, location, uo as Heater);
            this.flowsheet.Controls.Add(heaterCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(heaterCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(heaterCtrl);
         }
         else if (uo is Cooler)
         {
            CoolerControl coolerCtrl = new CoolerControl(this.flowsheet, location, uo as Cooler);
            this.flowsheet.Controls.Add(coolerCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(coolerCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(coolerCtrl);
         }
         else if (uo is ElectrostaticPrecipitator)
         {
            ElectrostaticPrecipitatorControl electrostaticPrecipitatorCtrl = new ElectrostaticPrecipitatorControl(this.flowsheet, location, uo as ElectrostaticPrecipitator);
            this.flowsheet.Controls.Add(electrostaticPrecipitatorCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(electrostaticPrecipitatorCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(electrostaticPrecipitatorCtrl);
         }
         else if (uo is Pump)
         {
            PumpControl pumpCtrl = new PumpControl(this.flowsheet, location, uo as Pump);
            this.flowsheet.Controls.Add(pumpCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(pumpCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(pumpCtrl);
         }
         else if (uo is Recycle)
         {
            RecycleControl recycleCtrl = new RecycleControl(this.flowsheet, location, uo as Recycle);
            this.flowsheet.Controls.Add(recycleCtrl);

            this.AddCtrlForTwoStreamUnitOpInlet(recycleCtrl);
            this.AddCtrlForTwoStreamUnitOpOutlet(recycleCtrl);
         }

         this.flowsheet.IsDirty = true;
      }
   }
}
