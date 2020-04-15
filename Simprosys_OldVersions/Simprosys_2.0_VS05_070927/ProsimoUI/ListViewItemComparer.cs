using System;
using System.Collections;
using System.Windows.Forms;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for ListViewItemComparer.
	/// </summary>
	public class ListViewItemComparer : IComparer
	{
      private int col;

      public ListViewItemComparer()
      {
         this.col = 0;
      }

      public ListViewItemComparer(int column)
      {
         this.col = column;
      }
      
      public int Compare(object x, object y) 
      {
         return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
      }
	}
}
