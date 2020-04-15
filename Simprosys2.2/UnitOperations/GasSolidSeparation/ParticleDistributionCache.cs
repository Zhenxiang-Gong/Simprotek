using System;
using System.Collections;

using Prosimo;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.HeatTransfer;

namespace Prosimo.UnitOperations.GasSolidSeparation {
   public delegate void ParticleDistributionChangedEventHandler(ParticleDistributionCache particleDistributionCache);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class ParticleDistributionCache {
      private GasSolidSeparatorRatingModel owner;
      private ArrayList sizeFractionAndEfficiencyList = new ArrayList();
      
      public event ParticleDistributionChangedEventHandler ParticleDistributionChanged;

      public ProcessVarDouble TotalEfficiency {
         get {return owner.CollectionEfficiency;}
      }

      public ParticleDistributionCache(GasSolidSeparatorRatingModel owner) {
         this.owner = owner;
         if (owner.ParticleSizeFractionAndEfficiencyList.Count > 0) {
            ParticleSizeFractionAndEfficiency aCopy;
            foreach (ParticleSizeFractionAndEfficiency psf in owner.ParticleSizeFractionAndEfficiencyList) {
               aCopy = (ParticleSizeFractionAndEfficiency) psf.Clone();
               sizeFractionAndEfficiencyList.Add(aCopy);
            }
         }
         else {
            ParticleSizeFractionAndEfficiency psf;
            psf = new ParticleSizeFractionAndEfficiency();
            sizeFractionAndEfficiencyList.Add(psf);
            psf = new ParticleSizeFractionAndEfficiency();
            sizeFractionAndEfficiencyList.Add(psf);
            DoNormalization();
         }
      }

      public ArrayList SizeFractionAndEfficiencyList {
         get {return sizeFractionAndEfficiencyList;}
      }

      public void AddParticleSizeFractionAndEfficiency() {
         ParticleSizeFractionAndEfficiency psf = new ParticleSizeFractionAndEfficiency();
         sizeFractionAndEfficiencyList.Add(psf);
         OnParticleDistributionChanged();
      }

      public void RemoveParticleSizeFractionAndEfficiency(ParticleSizeFractionAndEfficiency psf) {
         if (sizeFractionAndEfficiencyList.Count > 1) {
            sizeFractionAndEfficiencyList.Remove(psf);
            OnParticleDistributionChanged();
         }
      }

      public void RemoveParticleSizeFractionAndEfficiencys(int startIndex, int numOfRows) {
         if (startIndex > 0 && (startIndex + numOfRows) < (sizeFractionAndEfficiencyList.Count -1)) {
            sizeFractionAndEfficiencyList.RemoveRange(startIndex, numOfRows);
         }
      }

      public void RemoveParticleSizeFractionAndEfficiencyAt(int index) {
         if (sizeFractionAndEfficiencyList.Count > 1) {
            if (index > 0 && index < sizeFractionAndEfficiencyList.Count) {
               ParticleSizeFractionAndEfficiency psf = (ParticleSizeFractionAndEfficiency) sizeFractionAndEfficiencyList[index];
               sizeFractionAndEfficiencyList.RemoveAt(index);
               OnParticleDistributionChanged();
            }
         }
      }

      public void Normalize() {
         DoNormalization();
         double totalEff = owner.CalculateCollectionEfficiencies(sizeFractionAndEfficiencyList);
         TotalEfficiency.Value = totalEff;
         OnParticleDistributionChanged();
      }

      public ErrorMessage FinishSpecifications() {
         DoNormalization();
         ErrorMessage retMsg = null;
         string msg = CheckSpecifiedDistribution();
         if (msg == null) {
            owner.CommittParticleDistributions(sizeFractionAndEfficiencyList);
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", msg);
         }
         return retMsg;
      }
      
      private void DoNormalization() {
         double total = 0.0;
         foreach(ParticleSizeFractionAndEfficiency psf in sizeFractionAndEfficiencyList) {
            total += psf.WeightFraction.Value;
         }
         
         if (total > 1.0e-8) {
            foreach(ParticleSizeFractionAndEfficiency psf in sizeFractionAndEfficiencyList) {
               psf.WeightFraction.Value /= total;
            }
         }
      }

      private string CheckSpecifiedDistribution() {
         string retMsg = null;
         ArrayList aList = new ArrayList();
         double aValue;
         foreach(ParticleSizeFractionAndEfficiency psf in sizeFractionAndEfficiencyList) {
            aValue = psf.Diameter.Value;
            if (!aList.Contains(aValue)) {
               aList.Add(aValue);
            }
            else {
               retMsg = "The same particle size cannot appear in the same distribution twice or more."; 
               break;
            }
         }

         return retMsg;
      }

      private void OnParticleDistributionChanged() {
         if (ParticleDistributionChanged != null) {
            ParticleDistributionChanged(this);
         }
      }
   }
}
