using System;
using System.IO;

namespace Prosimo.SoftwareProtection {
   /// <summary>
   /// Summary description for SoftwareProtectionManager.
   /// </summary>
   public class SoftwareProtectionManager {
      private bool hasValidLease;
      private string message;
      public string Message {
         get { return message; }
      }

      private Lease lease;
      public Lease Lease {
         get { return lease; }
         set { lease = value; }
      }

      public bool HasValidLease {
         get { return hasValidLease; }
      }

      public SoftwareProtectionManager() {
         this.message = "";
         this.lease = null;
         hasValidLease = IsLeaseValid();
      }

      private bool IsLeaseValid() {
         bool isLeaseValid = false;
         DongleGenerator dg = new DongleGenerator();
         // we can pass to the dg a path where to look for
         if (dg.DongleExists()) {
            this.lease = dg.GetLease();
            if (lease != null) {
               if (this.lease.ComputerName.ToUpper().Equals(Environment.MachineName.ToUpper())) {
                  if (this.AreStartAndEndLeaseOk(this.lease.LeaseStart, this.lease.LeaseEnd)) {
                     isLeaseValid = true;
                  }
                  else {
                     isLeaseValid = false;
                     this.message = "The license for this software has expired. Please contact Simprotek Corporation by email at support@simprotek.com to extend it.";
                  }
               }
               else {
                  isLeaseValid = false;
                  this.message = "Your current computer name doesn't match the one you registered in the license. Please contact Simprotek Corporation by email at support@simprotek.com for a valid dongle file.";
               }
            }
            else {
               isLeaseValid = false;
               this.message = "You don't have a valid license for this software. Please contact Simprotek Corporation by email at support@simprotek.com for one.";
            }
         }
         else {
            isLeaseValid = false;
            this.message = "You don't have a license for this software. Please contact Simprotek Corporation by email at support@simprotek.com for one.";
         }

         return isLeaseValid;
      }
      
      private bool AreStartAndEndLeaseOk(DateTime start, DateTime end) {
         bool ok = true;

         int ii = DateTime.Compare(start, end);
         if (ii < 0) {
            DateTime current = DateTime.Now;
            ii = DateTime.Compare(start, current);
            if (ii < 0) {
               ii = DateTime.Compare(current, end);
               if (ii < 0) {
                  // ok = true;
               }
               else {
                  ok = false;
               }
            }
            else {
               ok = false;
            }
         }
         else {
            ok = false;
         }

         return ok;
      }
   }
}
