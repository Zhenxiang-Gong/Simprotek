using System;
using System.Windows.Forms;
using System.Drawing;
using Prosimo.UnitOperations;
using Prosimo.UnitSystems;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SolvableLabel.
	/// </summary>
   public class SolvableLabel : Label
   {
      private Solvable solvable;

      public SolvableLabel()
      {
         this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.Size = new System.Drawing.Size(80, 20);
         this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      }

      public void InitializeSolvable(Solvable solvable)
      {
         this.solvable = solvable;
         this.Text = solvable.Name;
         UI.SetStatusColor(this, solvable.SolveState);
         solvable.SolveComplete += new SolveCompleteEventHandler(solvable_SolveComplete);
         solvable.NameChanged += new Prosimo.NameChangedEventHandler(solvable_NameChanged);
      }

      private void solvable_SolveComplete(object sender, SolveState solveState)
      {
         UI.SetStatusColor(this, solveState);
      }

      protected override void Dispose( bool disposing )
      {
         if (this.solvable != null)
         {
            this.solvable.SolveComplete -=new SolveCompleteEventHandler(solvable_SolveComplete);
            this.solvable.NameChanged -= new Prosimo.NameChangedEventHandler(solvable_NameChanged);
         }
         base.Dispose( disposing );
      }

      private void solvable_NameChanged(object sender, string name, string oldName)
      {
         this.Text = this.solvable.Name;
      }
   }
}
