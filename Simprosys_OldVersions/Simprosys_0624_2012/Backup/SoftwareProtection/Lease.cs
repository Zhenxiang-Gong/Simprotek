using System;
using System.Text;

namespace Prosimo.SoftwareProtection
{
	/// <summary>
	/// Summary description for Lease.
	/// </summary>
	public class Lease
	{
      private const string SEPARATOR = ";";

      private DateTime leaseStart;
      public DateTime LeaseStart
      {
         get {return leaseStart;}
         set {leaseStart = value;}
      }

      private DateTime leaseEnd;
      public DateTime LeaseEnd
      {
         get {return leaseEnd;}
         set {leaseEnd = value;}
      }

      public string SerialNumber
      {
         get {return this.GetStringFromDateTime(this.leaseStart);}
      }

      private string computerName;
      public string ComputerName
      {
         get {return computerName;}
         set {computerName = value;}
      }

		public Lease()
		{
         this.leaseStart = DateTime.Now;
         this.leaseEnd = DateTime.Now;
         this.leaseEnd.AddYears(1);
         this.computerName = "";
      }

      public Lease(DateTime leaseStart, DateTime leaseEnd)
      {
         this.leaseStart = leaseStart;
         this.leaseEnd = leaseEnd;
         this.computerName = "";
      }

      public Lease(DateTime leaseStart, DateTime leaseEnd, string computerName)
      {
         this.leaseStart = leaseStart;
         this.leaseEnd = leaseEnd;
         this.computerName = computerName;
      }

      public Lease(string dongle)
      {
         string semiColumn = ";";
         string[] elements = dongle.Split(semiColumn.ToCharArray(), 3);
         this.leaseStart = this.GetDateTimeFromString(elements[0]);
         this.leaseEnd = this.GetDateTimeFromString(elements[1]);
         this.computerName = elements[2];
      }

      public override string ToString()
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(this.GetStringFromDateTime(this.leaseStart));
         sb.Append(Lease.SEPARATOR);
         sb.Append(this.GetStringFromDateTime(this.leaseEnd));
         sb.Append(Lease.SEPARATOR);
         sb.Append(this.computerName);
         return sb.ToString();
      }

      public string GetStringFromDateTime(DateTime dateTime)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(dateTime.Year.ToString());

         // make sure the rest of the elements have 2 digits
         if (dateTime.Month < 10)
            sb.Append("0");
         sb.Append(dateTime.Month.ToString());
         
         if (dateTime.Day < 10)
            sb.Append("0");
         sb.Append(dateTime.Day.ToString());
         
         if (dateTime.Hour < 10)
            sb.Append("0");
         sb.Append(dateTime.Hour.ToString());
         
         if (dateTime.Minute < 10)
            sb.Append("0");
         sb.Append(dateTime.Minute.ToString());
         
         if (dateTime.Second < 10)
            sb.Append("0");
         sb.Append(dateTime.Second.ToString());

         return sb.ToString();
      }

      public DateTime GetDateTimeFromString(string date)
      {
         // into the string, the info is organized like this: YYYYMMDDhhmmss
         int year = System.Convert.ToInt32(date.Substring(0, 4));
         int month = System.Convert.ToInt32(date.Substring(4, 2));
         int day = System.Convert.ToInt32(date.Substring(6, 2));
         int hour = System.Convert.ToInt32(date.Substring(8, 2));
         int minute = System.Convert.ToInt32(date.Substring(10, 2));
         int second = System.Convert.ToInt32(date.Substring(12, 2));
         return new DateTime(year,month,day,hour,minute,second);
      }
	}
}
