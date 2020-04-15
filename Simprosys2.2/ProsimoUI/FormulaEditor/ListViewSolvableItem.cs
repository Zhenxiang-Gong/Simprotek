using System;
using System.Windows.Forms;
using Prosimo.UnitOperations;

namespace ProsimoUI.FormulaEditor
{
	/// <summary>
	/// Summary description for ListViewSolvableItem.
	/// </summary>
	public class ListViewSolvableItem : ListViewItem
	{
      private Solvable solvable;
      public Solvable Solvable
      {
         get {return solvable;}
         set {solvable = value;}
      }

		public ListViewSolvableItem(Solvable solvable) : base(solvable.ToString())
		{
         this.solvable = solvable;
		}
	}
}
