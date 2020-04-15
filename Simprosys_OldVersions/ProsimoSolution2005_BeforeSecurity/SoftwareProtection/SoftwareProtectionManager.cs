using System;
using System.IO;
using System.Windows.Forms;

namespace Prosimo.SoftwareProtection
{
	/// <summary>
	/// Summary description for SoftwareProtectionManager.
	/// </summary>
	public class SoftwareProtectionManager
	{
      private string message;
      public string Message
      {
         get {return message;}
      }

      private Lease lease;
      public Lease Lease
      {
         get {return lease;}
         set {lease = value;}
      }

      public bool ReadyToRun
      {
         get
         {
            //bool readyToRun = false;
            //DongleGenerator dg = new DongleGenerator();
            // we can pass to the dg a path where to look for
            //if (dg.DongleExists()) {
            //   this.lease = dg.GetLease();
            //   if (lease != null) {
            //      if (this.lease.ComputerName.ToUpper().Equals(Environment.MachineName.ToUpper())) {
            //         if (this.AreStartAndEndLeaseOk(this.lease.LeaseStart, this.lease.LeaseEnd)) {
            //            readyToRun = true;
            //         }
            //         else {
            //            readyToRun = false;
            //            this.message = "Something is wrong with the lease date! It might be expired.";
            //         }
            //      }
            //      else {
            //         readyToRun = false;
            //         this.message = "Has the software been reinstalled on a different computer?";
            //      }
            //   }
            //   else {
            //      readyToRun = false;
            //      this.message = "No valid information in the " + DongleGenerator.DONGLE + " file!";
            //   }
            //}
            //else {
            //   readyToRun = false;
            //   this.message = "No " + DongleGenerator.DONGLE + " file found!";
            //}
            //return readyToRun;
            return true;
         }
      }

      public SoftwareProtectionManager()
		{
         this.message = "";
         this.lease = null;
		}

      private bool AreStartAndEndLeaseOk(DateTime start, DateTime end)
      {
         bool ok = true;
         
         int ii = DateTime.Compare(start, end);
         if (ii < 0)
         {
            DateTime current = DateTime.Now;
            ii = DateTime.Compare(start, current);
            if (ii < 0)
            {
               ii = DateTime.Compare(current, end);
               if (ii < 0)
               {
                  // ok = true;
               }
               else
               {
                  ok = false;
               }
            }
            else
            {
               ok = false;
            }
         }
         else
         {
            ok = false;
         }

         return ok;
      }
	}
}
