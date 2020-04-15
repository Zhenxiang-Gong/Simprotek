using System;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for ListViewVariableItem.
	/// </summary>
	public class ListViewVariableItem : ListViewItem
	{
      private ProcessVar var;
      public ProcessVar Variable
      {
         get {return var;}
         set {var = value;}
      }

		public ListViewVariableItem(ProcessVar var) : base(var.ToString())
		{
         this.var = var;
		}
	}
}
