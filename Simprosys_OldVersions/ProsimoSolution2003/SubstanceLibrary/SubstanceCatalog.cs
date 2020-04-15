using System;
using System.Collections;

namespace Prosimo.SubstanceLibrary
{
   public delegate void SubstanceAddedEventHandler(Substance substance);
   public delegate void SubstanceDeletedEventHandler(string name);
   public delegate void SubstanceChangedEventHandler(Substance substance);
   
   /// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class SubstanceCatalog  {
      public event SubstanceAddedEventHandler SubstanceAdded;
      public event SubstanceDeletedEventHandler SubstanceDeleted;
      public event SubstanceChangedEventHandler SubstanceChanged;
      
      private static SubstanceCatalog self;
      private IList allSubstanceList;
      private IList gasSubstanceList;
      private IList materialSubstanceList;
      private IList moistureSubstanceList;

      private SubstanceCatalog() {
         allSubstanceList = new ArrayList();
         gasSubstanceList = new ArrayList();
         materialSubstanceList = new ArrayList();
         moistureSubstanceList = new ArrayList();
         InitializeCatalog();
      }
      
      public static SubstanceCatalog GetInstance() {
         if (self == null) {
            self = new SubstanceCatalog();
         }
         return self;
      }

//      public SubstanceCatalog(IList list) {
//         allSubstanceList = list;
//      }
//
      private void InitializeCatalog() {
         double[] airGasCpCoeffs = {0.2896e5, 0.0939e5, 3.012e3, 0.0758e5, 1484.0};
         double[] airLiqCpCoeffs = {-2.1446e5, 9.1851e3, -1.0612e2, 4.1616e-1, 0};
         double[] airEvapHeatCoeffs = null;
         double[] airVapPressureCoeffs = null;
         double[] airLiqKCoeffs = null;
         double[] airGasKCoeffs = null;
         double[] airLiqViscCoeffs = null;
         double[] airGasViscCoeffs = null;
         double[] airLiqDensityCoeffs = null;
         ThermalPropsAndCoeffs thermalPropsAndCoeffs = new ThermalPropsAndCoeffs (airLiqCpCoeffs, airGasCpCoeffs,
            airEvapHeatCoeffs, airVapPressureCoeffs, airLiqKCoeffs, airGasKCoeffs,
            airLiqViscCoeffs, airGasViscCoeffs, airLiqDensityCoeffs);
         CriticalProperties criticalProps = new CriticalProperties(132.45, 3.79e6, 92.0, 0.318, 0.0);
         Substance substance = new Substance("Air", SubstanceType.Inorganic, 132259100, "", 28.951, thermalPropsAndCoeffs, null);
         gasSubstanceList.Add(substance);
         allSubstanceList.Add(substance);

         //from Perry's
         double[] waterLiqCpCoeffs = {2.7637e5, -2.0901e3, 8.125, -1.4116e-2, 9.3701e-6};
         double[] waterGasCpCoeffs = {0.3336e5, 0.2679e5, 2.6105e3, 0.089e5, 1169};
         double[] waterEvapHeatCoeffs = {5.2053e7, 0.3199, -0.212, 0.25795};
         double[] waterVapPressureCoeffs = {73.649, -7258.2, -7.3037, 4.1653e-6, 2.0};
         double[] waterLiqDensityCoeffs = {5.459, 0.30542, 647.13, 0.081};
         //from Yaws's
         double[] waterLiqKCoeffs = {-0.2758, 4.6120e-3, -5.5391e-6};
         double[] waterGasKCoeffs = {0.00053, 4.7093e-5, 4.9551e-8};
         double[] waterLiqViscCoeffs = {-10.2158, 1.7925e3, 1.773e-2, -1.2631e-5};
         double[] waterGasViscCoeffs = {-36.826, 0.429, 1.62e-5};
         thermalPropsAndCoeffs = new ThermalPropsAndCoeffs (waterLiqCpCoeffs, waterGasCpCoeffs,
            waterEvapHeatCoeffs, waterVapPressureCoeffs, waterLiqKCoeffs, waterGasKCoeffs,
            waterLiqViscCoeffs, waterGasViscCoeffs, waterLiqDensityCoeffs);

         //from Perry's
         criticalProps = new CriticalProperties(647.13, 21.94e6, 56.0, 0.228, 0.343);
         substance = new Substance("Water", SubstanceType.Inorganic, 7732185, "", 18.015, thermalPropsAndCoeffs, criticalProps);
         moistureSubstanceList.Add(substance);
         allSubstanceList.Add(substance);

         CreateMaterialSubstance("Dry Material", SubstanceType.Inorganic);
         CreateMaterialSubstance("Carbohydrate", SubstanceType.Organic);
         CreateMaterialSubstance("Ash", SubstanceType.Organic);
         CreateMaterialSubstance("Fiber", SubstanceType.Organic);
         CreateMaterialSubstance("Fat", SubstanceType.Organic);
         CreateMaterialSubstance("Protein", SubstanceType.Organic);
      }

      private void CreateMaterialSubstance(string name, SubstanceType type)  {
         Substance substance = new Substance(name, type, false);
         materialSubstanceList.Add(substance);
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
            if (substance.Name.Equals(name) && substance.IsUserDefined) {
               allSubstanceList.Remove(substance);
               OnSubstanceDeleted(name);
            }
         }
      }
      
      public bool IsInCatalog(Substance substance) {
         bool isInCatalog = false;
         foreach (Substance sbc in allSubstanceList) {
            if (sbc.Name.Equals(substance.Name)) {
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
            Substance substance = (Substance) allSubstanceList[index];
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
            if (substance.Name.Equals(name)) {
               ret = substance;
               break;
            }
         }
         return ret;
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
