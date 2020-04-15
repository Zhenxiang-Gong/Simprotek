using System;
using System.Collections;

namespace Prosimo
{
   public delegate void ProcessVarValueCommittedEventHandler(ProcessVar var);
   /// <summary>
   /// Summary description for IProcessVarOwner.
   /// </summary>
   public interface IProcessVarOwner 
   {
      event ProcessVarValueCommittedEventHandler ProcessVarValueCommitted;

      ErrorMessage Specify(ProcessVarDouble pv, double aValue);
      ErrorMessage Specify(ProcessVarInt pv, int aValue);
      ErrorMessage Specify(Hashtable procVarAndValueTable); 
      string Name 
      {
         get;
      }
   }
}
