using System;
using System.IO;
using System.Windows.Forms;

namespace Prosimo.SoftwareProtection {
   /// <summary>
   /// Summary description for SoftwareProtectionManager.
   /// </summary>
   public class SoftwareProtectionManager {
      private string message;
      public string Message {
         get { return message; }
      }

      private Lease lease;
      public Lease Lease {
         get { return lease; }
         set { lease = value; }
      }

      public bool ReadyToRun {
         get {
            bool readyToRun = false;
            DongleGenerator dg = new DongleGenerator();
            // we can pass to the dg a path where to look for
            if (dg.DongleExists()) {
               this.lease = dg.GetLease();
               if (lease != null) {
                  if (this.lease.ComputerName.ToUpper().Equals(Environment.MachineName.ToUpper())) {
                     if (this.AreStartAndEndLeaseOk(this.lease.LeaseStart, this.lease.LeaseEnd)) {
                        readyToRun = true;
                     }
                     else {
                        readyToRun = false;
                        this.message = "You don't have a valid lease for this software. Please contact Simprotek Corporation (at support@simprotek.com) to extend your lease.";
                     }
                  }
                  else {
                     readyToRun = false;
                     this.message = "Your current computer name doesn't match the one you registered in the dongle. Please contact Simprotek Corporation (at support@simprotek.com) for valid dongle file.";
                  }
               }
               else {
                  readyToRun = false;
                  this.message = "You don't have a valid dongle for this software. Please contact Simprotek Corporation (at support@simprotek.com) for a valid dongle file.";
               }
            }
            else {
               readyToRun = false;
               this.message = "No " + DongleGenerator.DONGLE + " file is found. Please contact Simprotek Corporation (at support@simprotek.com) for the required dongle file.";
            }
            //return readyToRun;
            return true;
         }
      }

      public SoftwareProtectionManager() {
         this.message = "";
         this.lease = null;
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
