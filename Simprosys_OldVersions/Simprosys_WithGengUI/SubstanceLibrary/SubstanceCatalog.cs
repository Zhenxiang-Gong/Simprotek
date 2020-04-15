using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters;

//Air Composition--Detailed
//Nitrogen        N2       78.084 % 
//Oxygen          O2       20.9476 % 
//Argon           Ar       0.934 % 
//Carbon Dioxide  CO2      0.0314 % 
//Neon            Ne       0.001818 % 
//Methane         CH4      0.0002 % 
//Helium          He       0.000524 % 
//Krypton         Kr       0.000114 % 
//Hydrogen        H2       0.00005 % 
//Xenon           Xe       0.0000087 % 

//Air Composition--Simple
//Nitrogen        N2       78% 
//Oxygen          O2       21% 
//Argon           Ar       1 % 

//Natural gas compostioin
//Methane               CH4             70-90% 
//Ethane                C2H6            0-20% 
//Propane               C3H8 
//Butane                C4H10 
//Carbon Dioxide        CO2             0-8% 
//Oxygen                O2              0-0.2% 
//Nitrogen              N2              0-5% 
//Hydrogen sulphide     H2S             0-5% 
//Rare gases            A, He, Ne, Xe   trace 

//Component Typical Analysis      (mole %) Range(mole %)
//Methane                          94.9     87.0 - 96.0
//Ethane                           2.5      1.8 - 5.1
//Propane                          0.2      0.1 - 1.5
//iso-Butane                       0.03     0.01 - 0.3
//normal-Butane                    0.03     0.01 - 0.3
//iso-Pentane                      0.01     trace - 0.14
//normal-Pentane                   0.01     trace - 0.04
//Hexanes plus                     0.01     trace - 0.06
//Nitrogen                         1.6      1.3 - 5.6
//Carbon Dioxide                   0.7      0.1 - 1.0
//Oxygen                           0.02     0.01 - 0.1
//Hydrogen                         trace    trace - 0.02
 


namespace Prosimo.SubstanceLibrary {
   public delegate void SubstanceAddedEventHandler(Substance substance);
   public delegate void SubstanceDeletedEventHandler(string name);
   public delegate void SubstanceChangedEventHandler(Substance substance);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class SubstanceCatalog {
      public event SubstanceAddedEventHandler SubstanceAdded;
      public event SubstanceDeletedEventHandler SubstanceDeleted;
      public event SubstanceChangedEventHandler SubstanceChanged;

      private static SubstanceCatalog self;
      private IList allSubstanceList;
      private IList gasSubstanceList;
      private IList materialSubstanceList;
      private IList moistureSubstanceList;
      
      private IList genericFoodSubstanceList;
      private Substance genericSubstance;

      private IList genericFuelSubstanceList;
      private IList naturalGasSubstanceList;

      private IList recalledSubstanceList;

      private SubstanceCatalog() {
         allSubstanceList = new ArrayList();
         gasSubstanceList = new ArrayList();
         materialSubstanceList = new ArrayList();
         moistureSubstanceList = new ArrayList();
         genericFoodSubstanceList = new ArrayList();
         genericFuelSubstanceList = new ArrayList();
         naturalGasSubstanceList = new ArrayList();

         LoadSubstanceList();
         InitializeCatalog();
      }

      public static SubstanceCatalog GetInstance() {
         if (self == null) {
            self = new SubstanceCatalog();
         }
         return self;
      }

      private void InitializeCatalog() {
         SubstanceFormula formula = new SubstanceFormula();
         formula.AddElement("Air", 1);
         Substance substance = new Substance("Air", SubstanceType.Inorganic, "132259100", formula, 28.951, null);
         gasSubstanceList.Add(substance);
         allSubstanceList.Add(substance);

         foreach (Substance s in recalledSubstanceList) {
            allSubstanceList.Add(s);
         }

         moistureSubstanceList.Add(GetSubstance("water"));
         moistureSubstanceList.Add(GetSubstance("carbon tetrachloride"));
         moistureSubstanceList.Add(GetSubstance("benzene"));
         moistureSubstanceList.Add(GetSubstance("toluene"));

         //CreateGenericFoodSubstance("Dry Material", SubstanceType.Unknown);
         genericSubstance = new Substance("Generic Substance", SubstanceType.Unknown, false);
         allSubstanceList.Add(genericSubstance);

         CreateGenericFoodSubstance("Carbohydrate", SubstanceType.Organic);
         CreateGenericFoodSubstance("Ash", SubstanceType.Organic);
         CreateGenericFoodSubstance("Fiber", SubstanceType.Organic);
         CreateGenericFoodSubstance("Fat", SubstanceType.Organic);
         CreateGenericFoodSubstance("Protein", SubstanceType.Organic);

         genericFuelSubstanceList.Add(GetSubstance("carbon"));
         genericFuelSubstanceList.Add(GetSubstance("hydrogen"));
         genericFuelSubstanceList.Add(GetSubstance("oxygen"));
         genericFuelSubstanceList.Add(GetSubstance("sulfur"));

         naturalGasSubstanceList.Add(GetSubstance("methane"));
         naturalGasSubstanceList.Add(GetSubstance("ethane"));
         naturalGasSubstanceList.Add(GetSubstance("propane"));
         naturalGasSubstanceList.Add(GetSubstance("n-butane"));
         naturalGasSubstanceList.Add(GetSubstance("carbon dioxide"));
         naturalGasSubstanceList.Add(GetSubstance("oxygen"));
         naturalGasSubstanceList.Add(GetSubstance("nitrogen"));
         naturalGasSubstanceList.Add(GetSubstance("hydrogen sulfide"));
         naturalGasSubstanceList.Add(GetSubstance("n-pentane"));
         naturalGasSubstanceList.Add(GetSubstance("n-hexane"));
      }

      public void LoadSubstanceList() {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\SubstanceDatabase";
         Stream stream = null;
         try {
            stream = new FileStream(baseDirectory + "\\Substances.dat", FileMode.Open);
            SoapFormatter serializer = new SoapFormatter();
            recalledSubstanceList = (ArrayList)serializer.Deserialize(stream);
            foreach (Storable s in recalledSubstanceList) {
               s.SetObjectData();
            }
         }
         catch (Exception) {
            throw;
         }
         finally {
            stream.Close();
         }
      }

      private void CreateGenericFoodSubstance(string name, SubstanceType type) {
         Substance substance = new Substance(name, type, false);
         genericFoodSubstanceList.Add(substance);
         allSubstanceList.Add(substance);
      }

      public void AddSubstance(Substance substance) {
         if (!IsInCatalog(substance)) {
            allSubstanceList.Add(substance);
            OnSubstanceAdded(substance);
         }
      }

      public void RemoveSubstance(string name) {
         foreach (Substance substance in allSubstanceList) {
            if (substance.Name.ToLower().Equals(name.ToLower()) && substance.IsUserDefined) {
               allSubstanceList.Remove(substance);
               OnSubstanceDeleted(name);
            }
         }
      }

      public bool IsInCatalog(Substance substance) {
         bool isInCatalog = false;
         foreach (Substance sbc in allSubstanceList) {
            if (sbc.Name.ToLower().Equals(substance.Name.ToLower())) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      public void RemoveSubstance(Substance substance) {
         if (substance.IsUserDefined) {
            string name = substance.Name;
            allSubstanceList.Remove(substance);
            OnSubstanceDeleted(name);
         }
      }

      public void RemoveSubstance(int index) {
         if (index < allSubstanceList.Count && index >= 0) {
            Substance substance = (Substance)allSubstanceList[index];
            if (substance.IsUserDefined) {
               string name = substance.Name;
               allSubstanceList.RemoveAt(index);
               OnSubstanceDeleted(name);
            }
         }
      }

      public Substance GetSubstance(string name) {
         Substance ret = null;
         foreach (Substance substance in allSubstanceList) {
            if (substance.Name.ToLower().Equals(name.ToLower())) {
               ret = substance;
               break;
            }
         }
         return ret;
      }

      public Substance GetGenericSubstance() {
         return genericSubstance;
      }

      public IList GetSubstanceList() {
         return allSubstanceList;
      }

      public IList GetSubstanceList(bool isUserDefined) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in allSubstanceList) {
            if (s.IsUserDefined == isUserDefined) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetSubstanceList(SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in allSubstanceList) {
            if (s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetSubstanceList(bool isUserDefined, SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in allSubstanceList) {
            if (s.IsUserDefined == isUserDefined && s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetGasSubstanceList() {
         return gasSubstanceList;
      }

      public IList GetGasSubstanceList(bool isUserDefined) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in gasSubstanceList) {
            if (s.IsUserDefined == isUserDefined) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetGasSubstanceList(SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in gasSubstanceList) {
            if (s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetGasSubstanceList(bool isUserDefined, SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in gasSubstanceList) {
            if (s.IsUserDefined == isUserDefined && s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetMaterialSubstanceList() {
         return materialSubstanceList;
      }

      public IList GetGenericFoodSubstanceList() {
         return genericFoodSubstanceList;
      }

      public IList GetMaterialSubstanceList(bool isUserDefined) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in materialSubstanceList) {
            if (s.IsUserDefined == isUserDefined) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetMaterialSubstanceList(SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in materialSubstanceList) {
            if (s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetMaterialSubstanceList(bool isUserDefined, SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in materialSubstanceList) {
            if (s.IsUserDefined == isUserDefined && s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetMoistureSubstanceList() {
         return moistureSubstanceList;
      }

      public IList GetMoistureSubstanceList(bool isUserDefined) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in moistureSubstanceList) {
            if (s.IsUserDefined == isUserDefined) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetMoistureSubstanceList(SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in moistureSubstanceList) {
            if (s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetMoistureSubstanceList(bool isUserDefined, SubstanceType type) {
         ArrayList retList = new ArrayList();
         foreach (Substance s in moistureSubstanceList) {
            if (s.IsUserDefined == isUserDefined && s.SubstanceType == type) {
               retList.Add(s);
            }
         }

         return retList;
      }

      public IList GetGenericFuelSubstanceList() {
         return genericFuelSubstanceList;
      }

      public IList GetNaturalGasSubstanceList() {
         return naturalGasSubstanceList;
      }
      
      private void OnSubstanceAdded(Substance substance) {
         if (SubstanceAdded != null)
            SubstanceAdded(substance);
      }

      private void OnSubstanceDeleted(string name) {
         if (SubstanceDeleted != null)
            SubstanceDeleted(name);
      }

      private void OnSubstanceChanged(Substance substance) {
         if (SubstanceChanged != null)
            SubstanceChanged(substance);
      }
   }
}

//private void InitializeCatalog() {
//   //from Perry's
//   double[] airGasCpCoeffs = { 0.2896e5, 0.0939e5, 3.012e3, 0.0758e5, 1484.0, 50, 1500};
//   double[] airLiqCpCoeffs = { -2.1446e5, 9.1851e3, -1.0612e2, 4.1616e-1, 0, 75, 115  };
//   double[] airEvapHeatCoeffs = { 0.8474e7, 0.3822, 0.0, 0.0, 59.15, 132.45}; ;
//   double[] airVapPressureCoeffs = { 21.662, -692.39, -0.39208, 4.7574e-3, 1.0, 59.15, 132.45 };
//   double[] airLiqDensityCoeffs = { 2.8963, 0.26733, 132.45, 0.27341, 59.15, 132.45 }; ;
//   CriticalProperties criticalProps = new CriticalProperties(132.45, 3.79e6, 0.092, 0.318, 0.0);

//   double[] airLiqKCoeffs = null;
//   double[] airGasKCoeffs = null;
//   double[] airLiqViscCoeffs = null;
//   double[] airGasViscCoeffs = null;
//   ThermalPropsAndCoeffs thermalPropsAndCoeffs = new ThermalPropsAndCoeffs(airLiqCpCoeffs, airGasCpCoeffs,
//      airEvapHeatCoeffs, airVapPressureCoeffs, airLiqKCoeffs, airGasKCoeffs,
//      airLiqViscCoeffs, airGasViscCoeffs, airLiqDensityCoeffs);
//   Substance substance = new Substance("Air", SubstanceType.Inorganic, "132259100", "Air", 28.951, null);
//   gasSubstanceList.Add(substance);
//   allSubstanceList.Add(substance);

//   //from Perry's
//   double[] waterGasCpCoeffs = { 0.3336e5, 0.2679e5, 2.6105e3, 0.089e5, 1169, 100, 2273.15 };
//   double[] waterLiqCpCoeffs = { 2.7637e5, -2.0901e3, 8.125, -1.4116e-2, 9.3701e-6, 273.16, 533.15 };
//   double[] waterEvapHeatCoeffs = { 5.2053e7, 0.3199, -0.212, 0.25795, 273.16, 647.13};
//   double[] waterVapPressureCoeffs = { 73.649, -7258.2, -7.3037, 4.1653e-6, 2.0, 273.16, 647.13 };
//   double[] waterLiqDensityCoeffs = { 5.459, 0.30542, 647.13, 0.081, 273.16, 333.15 };
//   criticalProps = new CriticalProperties(647.13, 21.94e6, 0.056, 0.228, 0.343);
//   //from Yaws's
//   double[] waterLiqKCoeffs = { -0.2758, 4.6120e-3, -5.5391e-6 };
//   double[] waterGasKCoeffs = { 0.00053, 4.7093e-5, 4.9551e-8 };
//   double[] waterLiqViscCoeffs = { -10.2158, 1.7925e3, 1.773e-2, -1.2631e-5 };
//   double[] waterGasViscCoeffs = { -36.826, 0.429, 1.62e-5 };
//   thermalPropsAndCoeffs = new ThermalPropsAndCoeffs(waterLiqCpCoeffs, waterGasCpCoeffs,
//      waterEvapHeatCoeffs, waterVapPressureCoeffs, waterLiqKCoeffs, waterGasKCoeffs,
//      waterLiqViscCoeffs, waterGasViscCoeffs, waterLiqDensityCoeffs);

//   substance = new Substance("Water", SubstanceType.Inorganic, "7732185", "H2O", 18.015, criticalProps);
//   moistureSubstanceList.Add(substance);
//   allSubstanceList.Add(substance);

//   //from Perry's
//   double[] carbonTetrachlorideGasCpCoeffs = { 0.3758e5, 0.7054e5, 0.5121e3, 0.4850e5, 236.1, 100, 1500 };
//   double[] carbonTetrachlorideLiqCpCoeffs = { -7.5270e5, 8.9661e3, -3.0394e1, 3.4455e-2, 0.0, 250.33, 388.71 };
//   double[] carbonTetrachlorideEvapHeatCoeffs = { 4.3252e7, 0.37688, 0.0, 0.0, 250.33, 556.35 };
//   double[] carbonTetrachlorideVapPressureCoeffs = { 78.441, -6128.1, -8.5766, 6.8465e-6, 2.0, 250.33, 556.35 };
//   double[] carbonTetrachlorideLiqDensityCoeffs = { 0.99835, 0.274, 556.35, 0.287, 250.33, 556.35 };
//   criticalProps = new CriticalProperties(556.35, 4.54e6, 0.274, 0.27, 0.191);
//   //from Yaws's
//   double[] carbonTetrachlorideLiqKCoeffs = { -1.8791, 1.0875, 556.35 };
//   double[] carbonTetrachlorideGasKCoeffs = { -0.00070, 2.2065E-05, 6.7913E-09 };
//   double[] carbonTetrachlorideLiqViscCoeffs = { -6.4564, 1.0379E+03, 1.4021E-02, -1.4107E-05 };
//   double[] carbonTetrachlorideGasViscCoeffs = { -7.745, 3.9481E-01, -1.1150E-04 };
//   thermalPropsAndCoeffs = new ThermalPropsAndCoeffs(carbonTetrachlorideLiqCpCoeffs, carbonTetrachlorideGasCpCoeffs,
//                     carbonTetrachlorideEvapHeatCoeffs, carbonTetrachlorideVapPressureCoeffs, carbonTetrachlorideLiqKCoeffs,
//                     carbonTetrachlorideGasKCoeffs, carbonTetrachlorideLiqViscCoeffs,
//                     carbonTetrachlorideGasViscCoeffs, carbonTetrachlorideLiqDensityCoeffs);
//   substance = new Substance("Carbon Tetrachloride", SubstanceType.Organic, "56235", "CCl4", 153.822, criticalProps);
//   moistureSubstanceList.Add(substance);
//   allSubstanceList.Add(substance);

//   double[] benzeneGasCpCoeffs = { 0.4442e5, 2.3205e5, 1.4946e3, 1.7213e5, -678.15, 200, 1500 };
//   double[] benzeneLiqCpCoeffs = { 1.2944e5, -1.6950e2, 6.4781e-1, 0.0, 0.0, 278.68, 353.24 };
//   double[] benzeneEvapHeatCoeffs = { 4.7500e7, 0.45238, 0.0534, -0.1181, 278.68, 562.16 };
//   double[] benzeneVapPressureCoeffs = { 83.918, -6517.7, -9.3453, 7.1182e-6, 2.0, 278.68, 562.16 };
//   double[] benzeneLiqDensityCoeffs = { 0.99835, 0.274, 556.35, 0.287, 278.68, 562.16 };
//   criticalProps = new CriticalProperties(562.16, 4.88e6, 0.261, 0.273, 0.209);
//   //from Yaws's
//   double[] benzeneLiqKCoeffs = { -1.6846, 1.0520, 562.16 };
//   double[] benzeneGasKCoeffs = { -0.00565, 3.4493E-05, 6.9298E-08 };
//   double[] benzeneLiqViscCoeffs = { -7.4005, 1.1815E+03, 1.4888E-02, -1.3713E-05 };
//   double[] benzeneGasViscCoeffs = { -0.151, 2.5706E-01, -8.9797E-06 };
//   thermalPropsAndCoeffs = new ThermalPropsAndCoeffs(benzeneLiqCpCoeffs, benzeneGasCpCoeffs,
//                      benzeneEvapHeatCoeffs, benzeneVapPressureCoeffs, benzeneLiqKCoeffs, benzeneGasKCoeffs,
//                      benzeneLiqViscCoeffs, benzeneGasViscCoeffs, benzeneLiqDensityCoeffs);
//   substance = new Substance("Benzene", SubstanceType.Organic, "71432", "C6H6", 78.114, criticalProps);
//   moistureSubstanceList.Add(substance);
//   allSubstanceList.Add(substance);

//   double[] tolueneGasCpCoeffs = { 0.5814e5, 2.8630e5, 1.4406e3, 1.8980e5, -650.43, 200,1500 };
//   double[] tolueneLiqCpCoeffs = { 1.4014e5, -1.5230e2, 6.9500e-1, 0.0, 0.0, 178.18, 500 };
//   double[] tolueneEvapHeatCoeffs = { 5.0144e7, 0.3859, 0.0, 0.0, 178.18, 591.8 };
//   double[] tolueneVapPressureCoeffs = { 80.877, -6902.4, -8.7761, 5.8034e-6, 2.0, 178.18, 591.8 };
//   double[] tolueneLiqDensityCoeffs = { 0.99835, 0.274, 556.35, 0.287, 178.18, 591.8 };
//   criticalProps = new CriticalProperties(591.8, 4.10e6, 0.314, 0.262, 0.162);
//   //from Yaws's
//   double[] tolueneLiqKCoeffs = { -1.6735, 0.9773, 591.79 };
//   double[] tolueneGasKCoeffs = { -0.00776, 4.4905E-05, 6.4514E-08 };
//   double[] tolueneLiqViscCoeffs = { -5.1649, 8.1068E+02, 1.0454E-02, -1.0488E-05 };
//   double[] tolueneGasViscCoeffs ={ 1.787, 2.3566E-01, -9.3508E-06 };
//   thermalPropsAndCoeffs = new ThermalPropsAndCoeffs(tolueneLiqCpCoeffs, tolueneGasCpCoeffs,
//                      tolueneEvapHeatCoeffs, tolueneVapPressureCoeffs, tolueneLiqKCoeffs, tolueneGasKCoeffs,
//                      tolueneLiqViscCoeffs, tolueneGasViscCoeffs, tolueneLiqDensityCoeffs);
//   substance = new Substance("Toluene", SubstanceType.Organic, "108883", "C7H8", 92.141, criticalProps);
//   moistureSubstanceList.Add(substance);
//   allSubstanceList.Add(substance);

//   CreateMaterialSubstance("Dry Material", SubstanceType.Unknown);
//   CreateMaterialSubstance("Carbohydrate", SubstanceType.Organic);
//   CreateMaterialSubstance("Ash", SubstanceType.Organic);
//   CreateMaterialSubstance("Fiber", SubstanceType.Organic);
//   CreateMaterialSubstance("Fat", SubstanceType.Organic);
//   CreateMaterialSubstance("Protein", SubstanceType.Organic);
//}

