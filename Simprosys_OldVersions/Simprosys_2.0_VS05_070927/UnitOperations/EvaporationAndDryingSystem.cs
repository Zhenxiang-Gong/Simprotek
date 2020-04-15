using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;
using Prosimo.Materials;
using Prosimo.ThermalProperties;
using Prosimo.Plots;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Miscellaneous;

namespace Prosimo.UnitOperations {

   public enum UnitOpCreationType { WithInputAndOutput = 0, WithInputOnly, WithoutInputAndOutput };
   /// <summary>
   /// Summary description for EvaporationAndDryingSystem.
   /// </summary>
   [Serializable]
   public class EvaporationAndDryingSystem : UnitOperationSystem {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      //private DryingMediumType dryingMediumType;
      private EvaporationAndDryingSystemPreferences preferences;
      private DryingMaterial dryingMaterial;
      private DryingGas dryingGas;

      private PsychrometricChartModel psychrometricChartModel;

      private MoistureProperties moistureProperties;
      private HumidGasCalculator humidGasCalculator;

      private PlotCatalog plotCatalog;

      private ArrayList gasStreamList = new ArrayList();
      private ArrayList processStreamList = new ArrayList();
      private ArrayList materialStreamList = new ArrayList();
      private ArrayList dryerList = new ArrayList();
      private ArrayList fanList = new ArrayList();
      private ArrayList pumpList = new ArrayList();
      private ArrayList compressorList = new ArrayList();
      private ArrayList valveList = new ArrayList();
      private ArrayList cycloneList = new ArrayList();
      private ArrayList bagFilterList = new ArrayList();
      private ArrayList airFilterList = new ArrayList();
      private ArrayList flashTankList = new ArrayList();
      private ArrayList teeList = new ArrayList();
      private ArrayList mixerList = new ArrayList();
      private ArrayList heatExchangerList = new ArrayList();
      private ArrayList heaterList = new ArrayList();
      private ArrayList coolerList = new ArrayList();
      private ArrayList recycleList = new ArrayList();
      private ArrayList ejectorList = new ArrayList();
      private ArrayList wetScrubberList = new ArrayList();
      private ArrayList scrubberCondenserList = new ArrayList();
      private ArrayList electrostaticPrecipitatorList = new ArrayList();

      public EvaporationAndDryingSystem(string name, DryingMaterial dryingMaterial, DryingGas dryingGas)
         : base(name) {
         this.dryingMaterial = dryingMaterial;
         this.dryingGas = dryingGas;
         preferences = new EvaporationAndDryingSystemPreferences();
         InitilizeDryingSystem();
         plotCatalog = new PlotCatalog();
      }

      private void InitilizeDryingSystem() {
         moistureProperties = new MoistureProperties(dryingMaterial.Moisture);
         //humidGasCalculator = new HumidGasCalculator(dryingGas.Substance, dryingMaterial.Moisture, moistureProperties);
         humidGasCalculator = new HumidGasCalculator(dryingGas.Substance, dryingMaterial.Moisture);
         dryingMaterial.DryingMaterialChanged += new DryingMaterialChangedEventHandler(DryingMaterialChanged);
      }

      private void DryingMaterialChanged(object sender, DryingMaterialChangedEventArgs eventArgs) {
         if (this.dryingMaterial == eventArgs.DryingMaterial) {
            if (eventArgs.IsNameChangeOnly) {
               OnSystemChanged();//only need to raise the system changed event to flag the user
            }
            else {
               foreach (DryingMaterialStream materialStream in materialStreamList) {
                  if (materialStream.UpStreamOwner == null || materialStream.UpStreamOwner is Recycle) {
                     materialStream.HasBeenModified(true);
                  }
               }
            }
         }
      }

      private void RecallInitilization(DryingMaterial recalledDryingMaterial, DryingGas recalledDryingGas) {
         DryingMaterialCatalog materialCatalog = DryingMaterialCatalog.GetInstance();
         DryingMaterial dryingMaterialInCatalog = materialCatalog.GetDryingMaterial(recalledDryingMaterial.Name);
         if (dryingMaterialInCatalog == null) {
            dryingMaterial = recalledDryingMaterial;
            materialCatalog.AddDryingMaterial(recalledDryingMaterial);
         }
         else if (recalledDryingMaterial.Equals(dryingMaterialInCatalog)) {
            dryingMaterial = materialCatalog.GetDryingMaterial(recalledDryingMaterial.Name);
         }
         else {
            dryingMaterial = recalledDryingMaterial;
            string materialName = dryingMaterial.Name + " For " + this.name;
            dryingMaterial.Name = materialName;
            dryingMaterialInCatalog = materialCatalog.GetDryingMaterial(materialName);
            if (dryingMaterialInCatalog != null && !recalledDryingMaterial.Equals(dryingMaterialInCatalog)) {
               materialName = materialCatalog.GetUniqueMaterialName(materialName);
            }
            dryingMaterial.Name = materialName;
            materialCatalog.AddDryingMaterial(recalledDryingMaterial);
         }

         DryingGasCatalog gasCatalog = DryingGasCatalog.GetInstance();
         DryingGas dryingGasInCatalog = gasCatalog.GetDryingGas(recalledDryingGas.Name);
         if (dryingGasInCatalog == null) {
            dryingGas = recalledDryingGas;
            gasCatalog.AddDryingGas(recalledDryingGas);
         }
         else if (dryingGasInCatalog.Equals(recalledDryingGas)) {
            dryingGas = gasCatalog.GetDryingGas(recalledDryingGas.Name);
         }
         else {
            dryingGas = recalledDryingGas;
            string gasName = dryingGas.Name + " For " + this.name;
            dryingGas.Name = gasName;
            dryingGasInCatalog = gasCatalog.GetDryingGas(gasName);
            if (dryingGasInCatalog != null && !dryingGasInCatalog.Equals(recalledDryingGas)) {
               gasName = gasCatalog.GetUniqueGasName(gasName);
            }
            dryingGas.Name = gasName;
            gasCatalog.AddDryingGas(recalledDryingGas);
         }

         InitilizeDryingSystem();
      }


      public EvaporationAndDryingSystemPreferences Preferences {
         get { return preferences; }
      }

      public DryingMaterial DryingMaterial {
         get { return dryingMaterial; }
      }

      public DryingGas DryingGas {
         get { return dryingGas; }
      }

      public MoistureProperties MoistureProperties {
         get { return moistureProperties; }
      }

      public HumidGasCalculator HumidGasCalculator {
         get { return humidGasCalculator; }
      }

      public DryingGasStream GetGasStream(string name) {
         foreach (DryingGasStream stream in gasStreamList) {
            if (stream.Name.Equals(name)) {
               return stream;
            }
         }
         return null;
      }

      public DryingMaterialStream GetMaterialStream(string name) {
         foreach (DryingMaterialStream stream in materialStreamList) {
            if (stream.Name.Equals(name)) {
               return stream;
            }
         }
         return null;
      }

      public Dryer GetDryer(string name) {
         foreach (Dryer uo in dryerList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Heater GetHeater(string name) {
         foreach (Heater uo in heaterList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Cooler GetCooler(string name) {
         foreach (Cooler uo in coolerList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Fan GetFan(string name) {
         foreach (Fan uo in fanList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public BagFilter GetBagFilter(string name) {
         foreach (BagFilter uo in bagFilterList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public AirFilter GetAirFilter(string name) {
         foreach (AirFilter uo in airFilterList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Compressor GetCompressor(string name) {
         foreach (Compressor uo in compressorList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Cyclone GetCyclone(string name) {
         foreach (Cyclone uo in cycloneList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Pump GetPump(string name) {
         foreach (Pump uo in pumpList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Valve GetValve(string name) {
         foreach (Valve uo in valveList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public FlashTank GetFlashTank(string name) {
         foreach (FlashTank uo in flashTankList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Tee GetTee(string name) {
         foreach (Tee uo in teeList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Mixer GetMixer(string name) {
         foreach (Mixer uo in mixerList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public HeatExchanger GetHeatExchanger(string name) {
         foreach (HeatExchanger uo in heatExchangerList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Recycle GetRecycle(string name) {
         foreach (Recycle uo in recycleList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Ejector GetEjector(string name) {
         foreach (Ejector uo in ejectorList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public WetScrubber GetWetScrubber(string name) {
         foreach (WetScrubber uo in wetScrubberList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public ScrubberCondenser GetScrubberCondenser(string name) {
         foreach (ScrubberCondenser uo in scrubberCondenserList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public ElectrostaticPrecipitator GetElectrostaticPrecipitator(string name) {
         foreach (ElectrostaticPrecipitator uo in electrostaticPrecipitatorList) {
            if (uo.Name.Equals(name)) {
               return uo;
            }
         }
         return null;
      }

      public Solvable CreateSolvable(Type solvableType) {
         Solvable solvable = null;
         if (solvableType.IsSubclassOf(typeof(ProcessStreamBase))) {
            solvable = CreateStream(solvableType);
         }
         else if (solvableType.IsSubclassOf(typeof(UnitOperation))) {
            solvable = CreateUnitOperation(solvableType);
         }

         return solvable;
      }

      private ProcessStreamBase CreateStream(Type streamType) {
         ProcessStreamBase ps = MakeStream(streamType);
         OnStreamAdded(ps);
         return ps;
      }

      private UnitOperation CreateUnitOperation(Type unitOpType) {
         UnitOperation unitOp = null;
         UnitOpCreationType creationType = preferences.UnitOpCreationType;

         if (unitOpType == typeof(SolidDryer)) {
            unitOp = MakeSolidDryer(creationType);
         }
         if (unitOpType == typeof(LiquidDryer)) {
            unitOp = MakeLiquidDryer(creationType);
         }
         if (unitOpType == typeof(Heater)) {
            unitOp = MakeHeater(preferences.HeaterInletStreamType, creationType);
         }
         else if (unitOpType == typeof(Cooler)) {
            unitOp = MakeCooler(preferences.CoolerInletStreamType, creationType);
         }
         else if (unitOpType == typeof(Fan)) {
            unitOp = MakeFan(creationType);
         }
         else if (unitOpType == typeof(Pump)) {
            unitOp = MakePump(creationType);
         }
         else if (unitOpType == typeof(Compressor)) {
            unitOp = MakeCompressor(creationType);
         }
         else if (unitOpType == typeof(Cyclone)) {
            unitOp = MakeCyclone(creationType);
         }
         else if (unitOpType == typeof(BagFilter)) {
            unitOp = MakeBagFilter(creationType);
         }
         else if (unitOpType == typeof(AirFilter)) {
            unitOp = MakeAirFilter(creationType);
         }
         else if (unitOpType == typeof(Valve)) {
            unitOp = MakeValve(preferences.ValveInletStreamType, creationType);
         }
         else if (unitOpType == typeof(FlashTank)) {
            unitOp = MakeFlashTank(creationType);
         }
         else if (unitOpType == typeof(Tee)) {
            unitOp = MakeTee(preferences.TeeInletStreamType, creationType);
         }
         else if (unitOpType == typeof(Mixer)) {
            unitOp = MakeMixer(preferences.MixerInletStreamType, creationType);
         }
         else if (unitOpType == typeof(HeatExchanger)) {
            unitOp = MakeHeatExchanger(preferences.HeatExchangerHotInletType, preferences.HeatExchangerColdInletType, creationType);
         }
         else if (unitOpType == typeof(Recycle)) {
            unitOp = MakeRecycle();
         }
         else if (unitOpType == typeof(Ejector)) {
            unitOp = MakeEjector(creationType);
         }
         else if (unitOpType == typeof(WetScrubber)) {
            unitOp = MakeWetScrubber(creationType);
         }
         else if (unitOpType == typeof(ScrubberCondenser)) {
            unitOp = MakeScrubberCondenser(creationType);
         }
         else if (unitOpType == typeof(ElectrostaticPrecipitator)) {
            unitOp = MakeElectrostaticPrecipitator(creationType);
         }

         OnUnitOpAdded(unitOp);
         return unitOp;
      }

      public PsychrometricChartModel GetPsychrometricChartModel() {
         if (psychrometricChartModel == null) {
            DryingGasStream input = MakeDryingGasStream("Start State");
            DryingGasStream output = MakeDryingGasStream("End State");
            DryingGasStream current = MakeDryingGasStream("Current State");

            //must unregister the var list so that these variables are not included
            //in the formula calculator
            UnregisterProcessVars(input.VarList);
            UnregisterProcessVars(output.VarList);
            UnregisterProcessVars(current.VarList);

            psychrometricChartModel = new PsychrometricChartModel("", input, output, current, this);
         }

         return psychrometricChartModel;
      }

      public ArrayList GetStreamList(Type streamType) {
         ArrayList list = null;
         if (streamType == typeof(DryingGasStream)) {
            list = gasStreamList;
         }
         else if (streamType == typeof(DryingMaterialStream)) {
            list = materialStreamList;
         }

         if (list != null) {
            SortStreams((ArrayList)list);
         }
         return list;
      }

      public ArrayList GetUnitOpList(Type unitOpType) {
         ArrayList list = null;
         if (unitOpType == typeof(Dryer)) {
            list = dryerList;
         }
         else if (unitOpType == typeof(Heater)) {
            list = heaterList;
         }
         else if (unitOpType == typeof(Cooler)) {
            list = coolerList;
         }
         else if (unitOpType == typeof(Fan)) {
            list = fanList;
         }
         else if (unitOpType == typeof(Cyclone)) {
            list = cycloneList;
         }
         else if (unitOpType == typeof(BagFilter)) {
            list = bagFilterList;
         }
         else if (unitOpType == typeof(AirFilter)) {
            list = airFilterList;
         }
         else if (unitOpType == typeof(Pump)) {
            list = pumpList;
         }
         else if (unitOpType == typeof(Compressor)) {
            list = compressorList;
         }
         else if (unitOpType == typeof(Valve)) {
            list = valveList;
         }
         else if (unitOpType == typeof(FlashTank)) {
            list = flashTankList;
         }
         else if (unitOpType == typeof(Tee)) {
            list = teeList;
         }
         else if (unitOpType == typeof(Mixer)) {
            list = mixerList;
         }
         else if (unitOpType == typeof(HeatExchanger)) {
            list = heatExchangerList;
         }
         else if (unitOpType == typeof(Recycle)) {
            list = recycleList;
         }
         else if (unitOpType == typeof(Ejector)) {
            list = ejectorList;
         }
         else if (unitOpType == typeof(WetScrubber)) {
            list = wetScrubberList;
         }
         else if (unitOpType == typeof(ScrubberCondenser)) {
            list = scrubberCondenserList;
         }
         else if (unitOpType == typeof(ElectrostaticPrecipitator)) {
            list = electrostaticPrecipitatorList;
         }

         list.Sort();

         return list;
      }

      public void DeleteStream(ProcessStreamBase stream) {
         UnitOperation uo = stream.UpStreamOwner;
         if (uo != null) {
            uo.DetachStream(stream);
         }

         uo = stream.DownStreamOwner;
         if (uo != null) {
            uo.DetachStream(stream);
         }

         UnregisterProcessVars(stream.VarList);

         if (stream is DryingGasStream) {
            gasStreamList.Remove(stream);
         }
         else if (stream is DryingMaterialStream) {
            materialStreamList.Remove(stream);
         }

         OnStreamDeleted(stream.Name);
      }

      public void DeleteUnitOperation(UnitOperation unitOp) {
         ArrayList streams = unitOp.InOutletStreams;
         foreach (ProcessStreamBase ps in streams) {
            unitOp.DetachStream(ps);
         }

         UnregisterProcessVars(unitOp.VarList);

         if (unitOp is Dryer) {
            dryerList.Remove(unitOp);
         }
         else if (unitOp is Heater) {
            heaterList.Remove(unitOp);
         }
         else if (unitOp is Cooler) {
            coolerList.Remove(unitOp);
         }
         else if (unitOp is Fan) {
            fanList.Remove(unitOp);
         }
         else if (unitOp is Pump) {
            pumpList.Remove(unitOp);
         }
         else if (unitOp is Compressor) {
            compressorList.Remove(unitOp);
         }
         else if (unitOp is Cyclone) {
            cycloneList.Remove(unitOp);
         }
         else if (unitOp is BagFilter) {
            bagFilterList.Remove(unitOp);
         }
         else if (unitOp is AirFilter) {
            airFilterList.Remove(unitOp);
         }
         else if (unitOp is Valve) {
            valveList.Remove(unitOp);
         }
         else if (unitOp is FlashTank) {
            flashTankList.Remove(unitOp);
         }
         else if (unitOp is Tee) {
            teeList.Remove(unitOp);
         }
         else if (unitOp is Mixer) {
            mixerList.Remove(unitOp);
         }
         else if (unitOp is HeatExchanger) {
            heatExchangerList.Remove(unitOp);
         }
         else if (unitOp is Recycle) {
            recycleList.Remove(unitOp);
         }
         else if (unitOp is Ejector) {
            ejectorList.Remove(unitOp);
         }
         else if (unitOp is WetScrubber) {
            wetScrubberList.Remove(unitOp);
         }
         else if (unitOp is ScrubberCondenser) {
            scrubberCondenserList.Remove(unitOp);
         }
         else if (unitOp is ElectrostaticPrecipitator) {
            electrostaticPrecipitatorList.Remove(unitOp);
         }

         OnUnitOpDeleted(unitOp.Name);
      }

      public override ArrayList GetStreamList() {
         ArrayList streamList = new ArrayList();
         streamList.AddRange(gasStreamList);
         streamList.AddRange(materialStreamList);
         return streamList;
      }

      public override ArrayList GetUnitOpList() {
         ArrayList unitOpList = new ArrayList();
         unitOpList.AddRange(dryerList);
         unitOpList.AddRange(fanList);
         unitOpList.AddRange(pumpList);
         unitOpList.AddRange(compressorList);
         unitOpList.AddRange(valveList);
         unitOpList.AddRange(cycloneList);
         unitOpList.AddRange(bagFilterList);
         unitOpList.AddRange(airFilterList);
         unitOpList.AddRange(teeList);
         unitOpList.AddRange(mixerList);
         unitOpList.AddRange(heatExchangerList);
         unitOpList.AddRange(heaterList);
         unitOpList.AddRange(coolerList);
         unitOpList.AddRange(flashTankList);
         unitOpList.AddRange(recycleList);
         unitOpList.AddRange(ejectorList);
         unitOpList.AddRange(wetScrubberList);
         unitOpList.AddRange(scrubberCondenserList);
         unitOpList.AddRange(electrostaticPrecipitatorList);
         return unitOpList;
      }

      public PlotCatalog PlotCatalog {
         get { return plotCatalog; }
      }

      //TODo:Need to add details
      public bool IsSolved {
         get { return true; }
      }

      private ProcessStreamBase MakeStream(Type streamType) {
         ProcessStreamBase ps;
         if (streamType == typeof(DryingGasStream)) {
            ps = MakeDryingGasStream();
         }
         else if (streamType == typeof(LiquidDryingMaterialStream)) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
         }
         else if (streamType == typeof(SolidDryingMaterialStream)) {
            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
         }
         else {
            ps = MakeProcessStream();
         }

         return ps;
      }

      private ProcessStreamBase MakeStream(DryingStreamType streamType) {
         ProcessStreamBase ps;
         if (streamType == DryingStreamType.DryingGas) {
            ps = MakeDryingGasStream();
         }
         else if (streamType == DryingStreamType.LiquidDryingMaterial) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
         }
         else if (streamType == DryingStreamType.SolidDryingMaterial) {
            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
         }
         else {
            ps = MakeProcessStream();
         }

         return ps;
      }

      private DryingGasStream MakeDryingGasStream(string name) {
         ArrayList compList = new ArrayList();
         MaterialComponent mc = new MaterialComponent(dryingGas.Substance);
         compList.Add(mc);
         mc = new MaterialComponent(dryingMaterial.Moisture);
         compList.Add(mc);
         mc = new MaterialComponent(dryingMaterial.AbsoluteDryMaterial);
         compList.Add(mc);
         MaterialComponents dryingGasComponents = new DryingGasComponents(compList);

         ArrayList gasCompList = new ArrayList();
         MaterialComponent pc = new MaterialComponent(dryingGas.Substance);
         gasCompList.Add(pc);
         pc = new MaterialComponent(dryingMaterial.Moisture);
         gasCompList.Add(pc);
         GasPhase gp = new GasPhase("Drying Gas Gas Phase", gasCompList);
         dryingGasComponents.AddPhase(gp);
         return new DryingGasStream(name, dryingGasComponents, this);
      }

      private ProcessStream MakeProcessStream() {
         string name = GetUniqueStreamName(typeof(ProcessStream));

         ArrayList compList = new ArrayList();
         MaterialComponent sc = new MaterialComponent(dryingGas.Substance);
         compList.Add(sc);
         sc = new MaterialComponent(dryingMaterial.AbsoluteDryMaterial);
         compList.Add(sc);
         sc = new MaterialComponent(dryingMaterial.Moisture);
         compList.Add(sc);
         MaterialComponents mComponents = new MaterialComponents(compList);

         ProcessStream processStream = new ProcessStream(name, mComponents, this);
         processStreamList.Add(processStream);
         return processStream;
      }

      private DryingMaterialStream MakeDryingMaterialStream(MaterialStateType materialType) {
         string name = GetUniqueStreamName(typeof(DryingMaterialStream));

         ArrayList compList = new ArrayList();
         MaterialComponent mc = new MaterialComponent(dryingMaterial.AbsoluteDryMaterial);
         //MaterialComponent mc = new MaterialComponent(SubstanceCatalog.GetInstance().GetSubstance("Dry Material"));
         compList.Add(mc);
         mc = new MaterialComponent(dryingMaterial.Moisture);
         compList.Add(mc);
         MaterialComponents dryingMaterialComponents = new DryingMaterialComponents(compList);
          
         ArrayList matCompList = new ArrayList();
         MaterialComponent pc = new MaterialComponent(dryingMaterial.Moisture);
         matCompList.Add(pc);
         pc = new MaterialComponent(dryingMaterial.AbsoluteDryMaterial);
         //pc = new MaterialComponent(SubstanceCatalog.GetInstance().GetSubstance("Dry Material"));
         matCompList.Add(pc);
         LiquidPhase lp = new LiquidPhase("Material Liquid Phase", matCompList);
         dryingMaterialComponents.AddPhase(lp);

         DryingMaterialStream materialStream = new DryingMaterialStream(name, dryingMaterialComponents, materialType, this);
         materialStreamList.Add(materialStream);
         return materialStream;
      }

      private DryingGasStream MakeDryingGasStream() {
         string name = GetUniqueStreamName(typeof(DryingGasStream));

         DryingGasStream gasStream = MakeDryingGasStream(name);
         gasStreamList.Add(gasStream);
         return gasStream;
      }

      private SolidDryer MakeSolidDryer(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Dryer));
         //Dryer dryer = new Dryer(name, processType, this);
         SolidDryer dryer = new SolidDryer(name, this);
         dryerList.Add(dryer);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            dryer.DoAttach(ps, Dryer.GAS_INLET_INDEX);
            ps = MakeDryingGasStream();
            dryer.DoAttach(ps, Dryer.GAS_OUTLET_INDEX);
            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
            dryer.DoAttach(ps, Dryer.MATERIAL_INLET_INDEX);
            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
            dryer.DoAttach(ps, Dryer.MATERIAL_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            dryer.DoAttach(ps, Dryer.GAS_INLET_INDEX);
            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
            dryer.DoAttach(ps, Dryer.MATERIAL_INLET_INDEX);
         }

         return dryer;
      }

      private LiquidDryer MakeLiquidDryer(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Dryer));
         //Dryer dryer = new Dryer(name, processType, this);
         LiquidDryer dryer = new LiquidDryer(name, this);
         dryerList.Add(dryer);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            dryer.DoAttach(ps, Dryer.GAS_INLET_INDEX);
            ps = MakeDryingGasStream();
            dryer.DoAttach(ps, Dryer.GAS_OUTLET_INDEX);
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            dryer.DoAttach(ps, Dryer.MATERIAL_INLET_INDEX);
            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
            dryer.DoAttach(ps, Dryer.MATERIAL_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            dryer.DoAttach(ps, Dryer.GAS_INLET_INDEX);
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            dryer.DoAttach(ps, Dryer.MATERIAL_INLET_INDEX);
         }

         return dryer;
      }

      private Heater MakeHeater(Type streamType, UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Heater));
         Heater heater = new Heater(name, this);
         heaterList.Add(heater);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeStream(streamType);
            heater.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);

            ps = MakeStream(streamType);
            heater.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeStream(streamType);
            heater.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }

         return heater;
      }

      private Cooler MakeCooler(Type streamType, UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Cooler));
         Cooler cooler = new Cooler(name, this);
         coolerList.Add(cooler);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeStream(streamType);
            cooler.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
            ps = MakeStream(streamType);
            cooler.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeStream(streamType);
            cooler.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }

         return cooler;
      }

      private BagFilter MakeBagFilter(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(BagFilter));
         BagFilter bagFilter = new BagFilter(name, this);
         bagFilterList.Add(bagFilter);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            bagFilter.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);

            ps = MakeDryingGasStream();
            bagFilter.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            bagFilter.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }
         return bagFilter;
      }

      private AirFilter MakeAirFilter(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(AirFilter));
         AirFilter airFilter = new AirFilter(name, this);
         airFilterList.Add(airFilter);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            airFilter.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);

            ps = MakeDryingGasStream();
            airFilter.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            airFilter.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }

         return airFilter;
      }

      private ElectrostaticPrecipitator MakeElectrostaticPrecipitator(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(ElectrostaticPrecipitator));
         ElectrostaticPrecipitator electrostaticPrecipitator = new ElectrostaticPrecipitator(name, this);
         electrostaticPrecipitatorList.Add(electrostaticPrecipitator);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            electrostaticPrecipitator.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);

            ps = MakeDryingGasStream();
            electrostaticPrecipitator.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            electrostaticPrecipitator.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }
         return electrostaticPrecipitator;
      }

      private Cyclone MakeCyclone(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Cyclone));
         Cyclone cyclone = new Cyclone(name, this);
         cycloneList.Add(cyclone);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            cyclone.DoAttach(ps, Cyclone.GAS_INLET_INDEX);

            ps = MakeDryingGasStream();
            cyclone.DoAttach(ps, Cyclone.GAS_OUTLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
            cyclone.DoAttach(ps, Cyclone.PARTICLE_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            cyclone.DoAttach(ps, Cyclone.GAS_INLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Solid);
            cyclone.DoAttach(ps, Cyclone.PARTICLE_OUTLET_INDEX);
         }

         return cyclone;
      }

      private Fan MakeFan(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Fan));
         Fan fan = new Fan(name, this);
         fanList.Add(fan);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            fan.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);

            ps = MakeDryingGasStream();
            fan.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            fan.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }

         return fan;
      }

      private Compressor MakeCompressor(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Compressor));
         Compressor compressor = new Compressor(name, this);
         compressorList.Add(compressor);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            compressor.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);

            ps = MakeDryingGasStream();
            compressor.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            compressor.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }
         return compressor;
      }

      private Pump MakePump(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Pump));
         Pump pump = new Pump(name, this);
         pumpList.Add(pump);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            pump.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            pump.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            pump.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }

         return pump;
      }

      private Valve MakeValve(Type streamType, UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Valve));
         Valve valve = new Valve(name, this);
         valveList.Add(valve);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeStream(streamType);
            valve.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
            ps = MakeStream(streamType);
            valve.DoAttach(ps, TwoStreamUnitOperation.OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeStream(streamType);
            valve.DoAttach(ps, TwoStreamUnitOperation.INLET_INDEX);
         }

         return valve;
      }

      private FlashTank MakeFlashTank(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(FlashTank));
         FlashTank flashTank = new FlashTank(name, this);
         flashTankList.Add(flashTank);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            flashTank.DoAttach(ps, FlashTank.INLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            flashTank.DoAttach(ps, FlashTank.LIQUID_OUTLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            flashTank.DoAttach(ps, FlashTank.VAPOR_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            flashTank.DoAttach(ps, FlashTank.INLET_INDEX);
         }

         return flashTank;
      }

      private Tee MakeTee(DryingStreamType streamType, UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Tee));
         Tee tee = new Tee(name, this);
         teeList.Add(tee);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeStream(streamType);
            tee.DoAttach(ps, Tee.INLET_INDEX);
            ps = MakeStream(streamType);
            tee.DoAttach(ps, (Tee.INLET_INDEX + 1));
            ps = MakeStream(streamType);
            tee.DoAttach(ps, (Tee.INLET_INDEX + 2));
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeStream(streamType);
            tee.DoAttach(ps, Tee.INLET_INDEX);
         }

         return tee;
      }

      private Mixer MakeMixer(DryingStreamType streamType, UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Mixer));
         Mixer mixer = new Mixer(name, this);
         mixerList.Add(mixer);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeStream(streamType);
            mixer.DoAttach(ps, Mixer.OUTLET_INDEX);
            ps = MakeStream(streamType);
            mixer.DoAttach(ps, (Mixer.OUTLET_INDEX + 1));
            ps = MakeStream(streamType);
            mixer.DoAttach(ps, (Mixer.OUTLET_INDEX + 2));
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeStream(streamType);
            mixer.DoAttach(ps, (Mixer.OUTLET_INDEX + 1));
            ps = MakeStream(streamType);
            mixer.DoAttach(ps, (Mixer.OUTLET_INDEX + 2));
         }

         return mixer;
      }

      private HeatExchanger MakeHeatExchanger(Type hotSideStreamType, Type coldSideStreamType, UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(HeatExchanger));
         HeatExchanger exchanger = new HeatExchanger(name, this);
         heatExchangerList.Add(exchanger);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeStream(coldSideStreamType);
            exchanger.DoAttach(ps, HeatExchanger.COLD_SIDE_INLET_INDEX);
            ps = MakeStream(coldSideStreamType);
            exchanger.DoAttach(ps, HeatExchanger.COLD_SIDE_OUTLET_INDEX);

            ps = MakeStream(hotSideStreamType);
            exchanger.DoAttach(ps, HeatExchanger.HOT_SIDE_INLET_INDEX);
            ps = MakeStream(hotSideStreamType);
            exchanger.DoAttach(ps, HeatExchanger.HOT_SIDE_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeStream(coldSideStreamType);
            exchanger.DoAttach(ps, HeatExchanger.COLD_SIDE_INLET_INDEX);

            ps = MakeStream(hotSideStreamType);
            exchanger.DoAttach(ps, HeatExchanger.HOT_SIDE_INLET_INDEX);
         }

         return exchanger;
      }

      private Recycle MakeRecycle() {
         string name = GetUniqueUnitOpName(typeof(Recycle));
         Recycle recycle = new Recycle(name, this);
         recycleList.Add(recycle);

         return recycle;
      }

      private Ejector MakeEjector(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(Ejector));
         Ejector ejector = new Ejector(name, this);
         ejectorList.Add(ejector);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            ejector.DoAttach(ps, Ejector.MOTIVE_INLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            ejector.DoAttach(ps, Ejector.SUCTION_INLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            ejector.DoAttach(ps, Ejector.DISCHARGE_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            ejector.DoAttach(ps, Ejector.MOTIVE_INLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            ejector.DoAttach(ps, Ejector.SUCTION_INLET_INDEX);
         }

         return ejector;
      }

      private WetScrubber MakeWetScrubber(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(WetScrubber));
         WetScrubber wetScrubber = new WetScrubber(name, this);
         wetScrubberList.Add(wetScrubber);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            wetScrubber.DoAttach(ps, WetScrubber.LIQUID_INLET_INDEX);

            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            wetScrubber.DoAttach(ps, WetScrubber.LIQUID_OUTLET_INDEX);

            ps = MakeDryingGasStream();
            wetScrubber.DoAttach(ps, WetScrubber.GAS_INLET_INDEX);

            ps = MakeDryingGasStream();
            wetScrubber.DoAttach(ps, WetScrubber.GAS_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            wetScrubber.DoAttach(ps, WetScrubber.LIQUID_INLET_INDEX);

            ps = MakeDryingGasStream();
            wetScrubber.DoAttach(ps, WetScrubber.GAS_INLET_INDEX);
         }

         return wetScrubber;
      }

      private ScrubberCondenser MakeScrubberCondenser(UnitOpCreationType creationType) {
         string name = GetUniqueUnitOpName(typeof(ScrubberCondenser));
         ScrubberCondenser scrubberCondenser = new ScrubberCondenser(name, this);
         scrubberCondenserList.Add(scrubberCondenser);

         ProcessStreamBase ps;
         if (creationType == UnitOpCreationType.WithInputAndOutput) {
            ps = MakeDryingGasStream();
            scrubberCondenser.DoAttach(ps, ScrubberCondenser.GAS_INLET_INDEX);

            ps = MakeDryingGasStream();
            scrubberCondenser.DoAttach(ps, ScrubberCondenser.GAS_OUTLET_INDEX);
            
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            scrubberCondenser.DoAttach(ps, ScrubberCondenser.LIQUID_OUTLET_INDEX);
         }
         else if (creationType == UnitOpCreationType.WithInputOnly) {
            ps = MakeDryingGasStream();
            scrubberCondenser.DoAttach(ps, ScrubberCondenser.GAS_INLET_INDEX);
            
            ps = MakeDryingMaterialStream(MaterialStateType.Liquid);
            scrubberCondenser.DoAttach(ps, ScrubberCondenser.LIQUID_OUTLET_INDEX);
         }

         return scrubberCondenser;
      }

      private string GetUniqueUnitOpName(Type type) {
         String s;
         ArrayList list = null;
         if (type == typeof(Dryer)) {
            s = "Dryer ";
            list = dryerList;
         }
         else if (type == typeof(Heater)) {
            s = "Heater ";
            list = heaterList;
         }
         else if (type == typeof(Cooler)) {
            s = "Cooler ";
            list = coolerList;
         }
         else if (type == typeof(Fan)) {
            s = "Fan ";
            list = fanList;
         }
         else if (type == typeof(Cyclone)) {
            s = "Cyclone ";
            list = cycloneList;
         }
         else if (type == typeof(BagFilter)) {
            s = "BagFilter ";
            list = bagFilterList;
         }
         else if (type == typeof(AirFilter)) {
            s = "AirFilter ";
            list = airFilterList;
         }
         else if (type == typeof(Pump)) {
            s = "Pump ";
            list = pumpList;
         }
         else if (type == typeof(Compressor)) {
            s = "Compressor ";
            list = compressorList;
         }
         else if (type == typeof(Valve)) {
            s = "Valve ";
            list = valveList;
         }
         else if (type == typeof(FlashTank)) {
            s = "Separator ";
            list = flashTankList;
         }
         else if (type == typeof(Tee)) {
            s = "Tee ";
            list = teeList;
         }
         else if (type == typeof(Mixer)) {
            s = "Mixer ";
            list = mixerList;
         }
         else if (type == typeof(HeatExchanger)) {
            s = "Exch ";
            list = heatExchangerList;
         }
         else if (type == typeof(Recycle)) {
            s = "Recycle ";
            list = recycleList;
         }
         else if (type == typeof(Ejector)) {
            s = "Ejector ";
            list = ejectorList;
         }
         else if (type == typeof(WetScrubber)) {
            s = "Scrubber ";
            list = wetScrubberList;
         }
         else if (type == typeof(ScrubberCondenser)) {
            s = "ScrbCond ";
            list = scrubberCondenserList;
         }
         else if (type == typeof(ElectrostaticPrecipitator)) {
            s = "Esp ";
            list = electrostaticPrecipitatorList;
         }
         else {
            s = "";
         }

         return GetUniqueName(list, s);
      }

      private string GetUniqueStreamName(Type type) {
         String s;
         ArrayList list = null;
         if (type == typeof(DryingGasStream)) {
            s = "Gas ";
            list = gasStreamList;
         }
         else if (type == typeof(DryingMaterialStream)) {
            s = "Mat ";
            list = materialStreamList;
         }
         else if (type == typeof(ProcessStream)) {
            s = "Stream ";
            list = processStreamList;
         }
         else {
            s = "";
         }

         return GetUniqueName(list, s);
      }

      private string GetUniqueName(ArrayList list, string prefix) {
         int newIndex = 0;
         Solvable solvable;
         string solvableName;
         int index;
         string suffix;
         for (int i = 0; i < list.Count; i++) {
            solvable = (Solvable)list[i];
            solvableName = solvable.Name;

            if (!solvableName.StartsWith(prefix)) {
               continue;
            }

            index = prefix.Length;
            suffix = solvableName.Substring(index);
            char[] chars = null;
            if (suffix != null) {
               chars = suffix.ToCharArray();
            }
            bool suffixIsANumber = false;
            if (chars != null && chars.Length > 0) {
               suffixIsANumber = true;
               foreach (char c in chars) {
                  if (!char.IsDigit(c)) {
                     suffixIsANumber = false;
                     break;
                  }
               }
            }

            if (suffixIsANumber == true) {
               try {
                  index = Int32.Parse(suffix);
                  if (index > newIndex) {
                     newIndex = index;
                  }
               }
               catch (FormatException e) {
                  Console.WriteLine(e.Message);
               }
            }
         }

         return prefix + (newIndex + 1);
      }

      #region Persistence
      protected EvaporationAndDryingSystem(SerializationInfo info, StreamingContext context)
         : base(info, context) {
         //preferences = new EvaporationAndDryingSystemPreferences();
      }

      public void SetSystemFileName(string name) {
         this.name = name;
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionEvaporationAndDryingSystem", typeof(int));
         DryingMaterial recalledDryingMaterial = null;
         DryingGas recalledDryingGas = null;
         if (persistedClassVersion == 1) {
            this.preferences = RecallStorableObject("Preferences", typeof(EvaporationAndDryingSystemPreferences)) as EvaporationAndDryingSystemPreferences;
            recalledDryingMaterial = RecallStorableObject("DryingMaterial", typeof(DryingMaterial)) as DryingMaterial;
            recalledDryingGas = RecallStorableObject("DryingGas", typeof(DryingGas)) as DryingGas;

            this.plotCatalog = RecallStorableObject("PlotCatalog", typeof(PlotCatalog)) as PlotCatalog;

            this.gasStreamList = (ArrayList)RecallArrayListObject("GasStreamList");
            this.materialStreamList = (ArrayList)RecallArrayListObject("MaterialStreamList");
            this.processStreamList = (ArrayList)RecallArrayListObject("ProcessStreamList");
            this.dryerList = (ArrayList)RecallArrayListObject("DryerList");
            this.fanList = (ArrayList)RecallArrayListObject("FanList");
            this.pumpList = (ArrayList)RecallArrayListObject("PumpList");
            this.compressorList = (ArrayList)RecallArrayListObject("CompressorList");
            this.valveList = (ArrayList)RecallArrayListObject("ValveList");
            this.cycloneList = (ArrayList)RecallArrayListObject("CycloneList");
            this.bagFilterList = (ArrayList)RecallArrayListObject("BagFilterList");
            this.airFilterList = (ArrayList)RecallArrayListObject("AirFilterList");
            this.teeList = (ArrayList)RecallArrayListObject("TeeList");
            this.mixerList = (ArrayList)RecallArrayListObject("MixerList");
            this.heatExchangerList = (ArrayList)RecallArrayListObject("HeatExchangerList");
            this.heaterList = (ArrayList)RecallArrayListObject("HeaterList");
            this.coolerList = (ArrayList)RecallArrayListObject("CoolerList");
            this.flashTankList = (ArrayList)RecallArrayListObject("FlashTankList");
            this.recycleList = (ArrayList)RecallArrayListObject("RecycleList");
            this.ejectorList = (ArrayList)RecallArrayListObject("EjectorList");
            this.wetScrubberList = (ArrayList)RecallArrayListObject("WetScrubberList");
            this.scrubberCondenserList = (ArrayList)RecallArrayListObject("ScrubberCondenserList");
            this.electrostaticPrecipitatorList = (ArrayList)RecallArrayListObject("ElectrostaticPrecipitatorList");
         }
         RecallInitilization(recalledDryingMaterial, recalledDryingGas);
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionEvaporationAndDryingSystem", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Preferences", this.preferences, typeof(EvaporationAndDryingSystemPreferences));
         info.AddValue("DryingMaterial", this.dryingMaterial, typeof(DryingMaterial));
         info.AddValue("DryingGas", this.dryingGas, typeof(DryingGas));

         info.AddValue("PlotCatalog", this.plotCatalog, typeof(PlotCatalog));

         info.AddValue("GasStreamList", this.gasStreamList, typeof(ArrayList));
         info.AddValue("MaterialStreamList", this.materialStreamList, typeof(ArrayList));
         info.AddValue("ProcessStreamList", this.processStreamList, typeof(ArrayList));
         info.AddValue("DryerList", this.dryerList, typeof(ArrayList));
         info.AddValue("FanList", this.fanList, typeof(ArrayList));
         info.AddValue("PumpList", this.pumpList, typeof(ArrayList));
         info.AddValue("CompressorList", this.compressorList, typeof(ArrayList));
         info.AddValue("ValveList", this.valveList, typeof(ArrayList));
         info.AddValue("CycloneList", this.cycloneList, typeof(ArrayList));
         info.AddValue("BagFilterList", this.bagFilterList, typeof(ArrayList));
         info.AddValue("AirFilterList", this.airFilterList, typeof(ArrayList));
         info.AddValue("TeeList", this.teeList, typeof(ArrayList));
         info.AddValue("MixerList", this.mixerList, typeof(ArrayList));
         info.AddValue("HeatExchangerList", this.heatExchangerList, typeof(ArrayList));
         info.AddValue("HeaterList", this.heaterList, typeof(ArrayList));
         info.AddValue("CoolerList", this.coolerList, typeof(ArrayList));
         info.AddValue("FlashTankList", this.flashTankList, typeof(ArrayList));
         info.AddValue("RecycleList", this.recycleList, typeof(ArrayList));
         info.AddValue("EjectorList", this.ejectorList, typeof(ArrayList));
         info.AddValue("WetScrubberList", this.wetScrubberList, typeof(ArrayList));
         info.AddValue("ScrubberCondenserList", this.scrubberCondenserList, typeof(ArrayList));
         info.AddValue("ElectrostaticPrecipitatorList", this.electrostaticPrecipitatorList, typeof(ArrayList));
      }
      #endregion Persistence
   }
}
      /*public Dryer CreateDryer(UnitOpCreationType creationType) {
         Dryer dryer = MakeDryer(ProcessType.Continuous, materialType, creationType);
         OnUnitOpAdded(dryer);
         return dryer;
      }
      
      public Dryer CreateDryer(MaterialType matType, UnitOpCreationType creationType) {
         Dryer dryer = MakeDryer(ProcessType.Continuous, matType, creationType);
         OnUnitOpAdded(dryer);
         return dryer;
      }*/
      
      /*public Valve CreateValve(Type streamType, UnitOpCreationType creationType) {
         Valve valve = MakeValve(streamType, creationType);
         OnUnitOpAdded(valve);
         return valve;
      }

      public Tee CreateTee(Type streamType, UnitOpCreationType creationType) {
         Tee tee = MakeTee(streamType, creationType);
         OnUnitOpAdded(tee);
         return tee;
      }

      public Mixer CreateMixer(Type streamType, UnitOpCreationType creationType) {
         Mixer mixer = MakeMixer(streamType, creationType);
         OnUnitOpAdded(mixer);
         return mixer;
      }

      public HeatExchanger CreateHeatExchanger(Type hotSideStreamType, Type coldSideStreamType, UnitOpCreationType creationType) {
         HeatExchanger exchanger = MakeHeatExchanger(hotSideStreamType, coldSideStreamType, creationType);
         OnUnitOpAdded(exchanger);
         return exchanger;
      }*/


     /*public ArrayList GetGasStreamList() {
         SortStreams((ArrayList) gasStreamList);
         return gasStreamList;
      }
      
      public ArrayList GetMaterialStreamList() {
         SortStreams((ArrayList) materialStreamList);
         return materialStreamList;
      }
      
      public ArrayList GetDryerList() {
         return dryerList;
      }

      public ArrayList GetHeaterList() {
         return heaterList;
      }

      public ArrayList GetCoolerList() {
         return coolerList;
      }

      public ArrayList GetFanList() {
         return fanList;
      }
      
      public ArrayList GetPumpList() {
         return pumpList;
      }

      public ArrayList GetCompressorList() {
         return compressorList;
      }

      public ArrayList GetBagFilterList() {
         return bagFilterList;
      }
      
      public ArrayList GetAirFilterList() {
         return airFilterList;
      }
      
      public ArrayList GetCycloneList() {
         return cycloneList;
      }
      
      public ArrayList GetValveList() {
         return valveList;
      }
      
      public ArrayList GetFlashTankList() {
         return flashTankList;
      }

      public ArrayList GetTeeList() {
         return teeList;
      }

      public ArrayList GetMixerList() {
         return mixerList;
      }

      public ArrayList GetHeatExchangerList() {
         return heatExchangerList;
      }

      public ArrayList GetRecycleList() {
         return recycleList;
      }

      public ArrayList GetEjectorList() {
         return ejectorList;
      }

      public ArrayList GetWetScrubberList() {
         return wetScrubberList;
      }

      public ArrayList GetElectrostaticPrecipitatorList() {
         return electrostaticPrecipitatorList;
      }*/
      
/*public DryingGasStream CreateDryingGasStream() {
         DryingGasStream dgs = MakeDryingGasStream();
         OnStreamAdded(dgs);
         return dgs; 
      }
      
      public DryingMaterialStream CreateDryingMaterialStream() {
         return CreateDryingMaterialStream(materialType);
      }
      
      public ProcessStream CreateProcessStream() {
         ProcessStream ps = MakeProcessStream();
         OnStreamAdded(ps);
         return ps; 
      }*/
      

      /*public bool CanRename(string aName) {
         bool canRename = true;
         ArrayList solvableList = GetSolvableList();
         foreach (Solvable solvable in solvableList) {
            if (solvable.Name.Equals(aName)) {
               canRename = false;
               break;
            }
         }
         return canRename;
      }

      private void SortStreams(ArrayList list) {
         ArrayList tempList = new ArrayList();
         foreach (ProcessStreamBase ps in list) {
            if (ps.UpStreamOwner == null && ps.DownStreamOwner == null) {
               tempList.Add(ps);
            }
         }

         for (int i = 0; i < tempList.Count; i++) {
            list.Remove(tempList[i]);
         }
         
         list.AddRange(tempList);
      }*/

      /*public override ArrayList GetSolvableList() {
         ArrayList solvableList = new ArrayList();
         solvableList.AddRange(GetStreamList());
         solvableList.AddRange(GetUnitOpList());
         return solvableList;
      }

      public UnitOperation GetUnitOperation(string name) {
         UnitOperation retValue = null;
         ArrayList unitOps = GetUnitOpList();
         foreach (UnitOperation uo in unitOps) {
            if (uo.Name.Equals(name)) {
               retValue = uo;
               break;
            }
         }
         return retValue;
      }

      public ProcessStreamBase GetStream(string name) {
         foreach (ProcessStreamBase psb in GetStreamList()) {
            if (psb.Name.Equals(name)) {
               return psb;
            }
         }
         return null;
      }

      public Hashtable FormulaTable {
         get {return  formulaTable;}
      }

      public ProcessVar GetProcessVar(int uniqueID) {
         return  procVarTable[uniqueID] as ProcessVar;
      }

      internal void RegisterProcessVars(ArrayList varList) {
         int id;
         foreach (ProcessVar var in varList) {
            id = processVarCounter;
            var.ID = id;
            procVarTable.Add(id, var);
            processVarCounter++;
         }
      }

      internal void DeregisterProcessVars(ArrayList varList) {
         foreach (ProcessVar var in varList) {
            procVarTable.Remove(var.ID);
         }
      }*/

     
 //private void preferences_CurrentUnitSystemChanged(UnitSystem unitSystem) {
      //   UnitSystemService.GetInstance().SetCurrentUnitSystem(this.preferences.CurrentUnitSystem);
      //}
/*   public DryingMaterialStream CreateDryingMaterialStream(DryingMaterialStream infoMaterialStream) {
         DryingMaterialStream materialStream = new DryingMaterialStream(this);
         materialStream.SetObjectData(infoMaterialStream.SerializationInfo, infoMaterialStream.StreamingContext);
         materialStreamList.Add(materialStream);
         return materialStream;
      }
      

public ProcessStream CreateProcessStream(ProcessStream infoProcessStream) {
         ProcessStream processStream = new ProcessStream(this);
         processStream.SetObjectData(infoProcessStream.SerializationInfo, infoProcessStream.StreamingContext);
         processStreamList.Add(processStream);
         return processStream;
      }
      
      public DryingGasStream CreateDryingGasStream(DryingGasStream infoGasStream) {
         //DryingGasStream gasStream = new DryingGasStream(this);
         //gasStream.SetObjectData(infoGasStream.SerializationInfo, infoGasStream.StreamingContext);
         //gasStreamList.Add(gasStream);
         //return gasStream;
         infoGasStream.SetObjectData(infoGasStream.SerializationInfo, infoGasStream.StreamingContext);
         infoGasStream.dryingSystem = this;
         infoGasStream.solveController = solveController;
         gasStreamList.Add(infoGasStream);
         return infoGasStream;
      }
      
      public Dryer CreateDryer(Dryer infoDryer) {
         Dryer dryer = new Dryer(this);
         dryer.SetObjectData(infoDryer.SerializationInfo, infoDryer.StreamingContext);
         dryerList.Add(dryer);
         return dryer;
      }*/
           /*public Heater CreateHeater(Heater infoHeater) {
         Heater heater = new Heater(this);
         heater.SetObjectData(infoHeater.SerializationInfo, infoHeater.StreamingContext);
         heaterList.Add(heater);
         return heater;
      }*/

      /*public Cooler CreateCooler(Cooler infoCooler) {
         Cooler cooler = new Cooler(this);
         cooler.SetObjectData(infoCooler.SerializationInfo, infoCooler.StreamingContext);
         coolerList.Add(cooler);
         return cooler;
      }*/
      /*public BagFilter CreateBagFilter(BagFilter infoBagFilter) {
         BagFilter bagFilter = new BagFilter(this);
         bagFilter.SetObjectData(infoBagFilter.SerializationInfo, infoBagFilter.StreamingContext);
         bagFilterList.Add(bagFilter);
         return bagFilter;
      }*/
      
      /*public Cyclone CreateCyclone(Cyclone infoCyclone) {
         Cyclone cyclone = new Cyclone(this);
         cyclone.SetObjectData(infoCyclone.SerializationInfo, infoCyclone.StreamingContext);
         cycloneList.Add(cyclone);
         return cyclone;
      }*/

      /*public Fan CreateFan(Fan infoFan) {
         Fan fan = new Fan(this);
         fan.SetObjectData(infoFan.SerializationInfo, infoFan.StreamingContext);
         fanList.Add(fan);
         return fan;
      } */

      /*public Compressor CreateCompressor(Compressor infoCompressor) {
         Compressor compressor = new Compressor(this);
         compressor.SetObjectData(infoCompressor.SerializationInfo, infoCompressor.StreamingContext);
         compressorList.Add(compressor);
         return compressor;
      }*/

      /*public Pump CreatePump(Pump infoPump) {
         Pump pump = new Pump(this);
         pump.SetObjectData(infoPump.SerializationInfo, infoPump.StreamingContext);
         pumpList.Add(pump);
         return pump;
      } */
      
       /*public void CreateSolvable(Solvable infoSolvable) {
         infoSolvable.UnitOpSystem = this;
         infoSolvable.SolveController = solveController;
         infoSolvable.SetObjectData();

         if (infoSolvable is DryingGasStream) {
            gasStreamList.Add(infoSolvable);
         }
         else if (infoSolvable is DryingMaterialStream) {
            materialStreamList.Add(infoSolvable);
         }
         else if (infoSolvable is Dryer) {
            dryerList.Add(infoSolvable);
         }
         else if (infoSolvable is Fan) {
            fanList.Add(infoSolvable);
         }
         else if (infoSolvable is Heater) {
            heaterList.Add(infoSolvable);
         }
         else if (infoSolvable is Cooler) {
            coolerList.Add(infoSolvable);
         }
         else if (infoSolvable is Cyclone) {
            cycloneList.Add(infoSolvable);
         }
         else if (infoSolvable is BagFilter) {
            bagFilterList.Add(infoSolvable);
         }
         else if (infoSolvable is AirFilter) {
            airFilterList.Add(infoSolvable);
         }
         else if (infoSolvable is Compressor) {
            compressorList.Add(infoSolvable);
         }
         else if (infoSolvable is Pump) {
            pumpList.Add(infoSolvable);
         }
         else if (infoSolvable is Valve) {
            valveList.Add(infoSolvable);
         }
         else if (infoSolvable is FlashTank) {
            flashTankList.Add(infoSolvable);
         }
         else if (infoSolvable is Tee) {
            teeList.Add(infoSolvable);
         }
         else if (infoSolvable is Mixer) {
            mixerList.Add(infoSolvable);
         }
         else if (infoSolvable is HeatExchanger) {
            heatExchangerList.Add(infoSolvable);
         }
      }*/
//public ProcessStreamBase CreateStream(Type streamType) {
//   ProcessStreamBase ps = MakeStream(streamType);
//   OnStreamAdded(ps);
//   return ps;
//}

//public DryingGasStream CreateDryingGasStream() {
//   DryingGasStream dg = MakeDryingGasStream();
//   OnStreamAdded(dg);
//   return dg;
//}

//public DryingMaterialStream CreateSolidMaterialStream() {
//   DryingMaterialStream dms = MakeDryingMaterialStream(MaterialStateType.Solid);
//   OnStreamAdded(dms);
//   return dms;
//}

//public DryingMaterialStream CreateLiquidMaterialStream() {
//   DryingMaterialStream dms = MakeDryingMaterialStream(MaterialStateType.Liquid);
//   OnStreamAdded(dms);
//   return dms;
//}