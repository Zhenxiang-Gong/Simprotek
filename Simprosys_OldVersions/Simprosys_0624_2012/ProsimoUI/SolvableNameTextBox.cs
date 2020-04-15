using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SolvableNameTextBox.
	/// </summary>
	public class SolvableNameTextBox : System.Windows.Forms.TextBox
	{
      private Solvable solvable;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SolvableNameTextBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public SolvableNameTextBox(Solvable solvable)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.solvable = solvable;
         this.Text = solvable.Name;
         UI.SetStatusColor(this, this.solvable.SolveState);
         this.solvable.NameChanged += new NameChangedEventHandler(solvable_NameChanged);
         this.solvable.SolveComplete += new SolveCompleteEventHandler(solvable_SolveComplete);
         this.Validating += new CancelEventHandler(SolvableNameTextBox_Validating);
         this.KeyUp += new KeyEventHandler(SolvableNameTextBox_KeyUp);
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.solvable != null)
         {
            this.solvable.NameChanged -= new NameChangedEventHandler(solvable_NameChanged);
            this.solvable.SolveComplete -= new SolveCompleteEventHandler(solvable_SolveComplete);
         }

         if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         // 
         // SolvableNameTextBox
         // 
         this.Name = "SolvableNameTextBox";
         this.Size = new System.Drawing.Size(80, 20);

      }
      #endregion

      public void SetSolvable(Solvable solvable)
      {
         this.solvable = solvable;
         this.Text = solvable.Name;
         UI.SetStatusColor(this, this.solvable.SolveState);
         this.solvable.NameChanged += new NameChangedEventHandler(solvable_NameChanged);
         this.solvable.SolveComplete += new SolveCompleteEventHandler(solvable_SolveComplete);
         this.Validating += new CancelEventHandler(SolvableNameTextBox_Validating);
         this.KeyUp += new KeyEventHandler(SolvableNameTextBox_KeyUp);
      }

      public void UnsetSolvable()
      {
         if (this.solvable != null)
         {
            this.Text = ".";
            UI.SetStatusColor(this, SolveState.NotSolved);
            this.solvable.NameChanged -= new NameChangedEventHandler(solvable_NameChanged);
            this.solvable.SolveComplete -= new SolveCompleteEventHandler(solvable_SolveComplete);
            this.Validating -= new CancelEventHandler(SolvableNameTextBox_Validating);
            this.KeyUp -= new KeyEventHandler(SolvableNameTextBox_KeyUp);
         }
      }

      private void solvable_NameChanged(object sender, string name, string oldName)
      {
         this.Text = name;
      }

      private void solvable_SolveComplete(object sender, SolveState solveState)
      {
         UI.SetStatusColor(this, solveState);
      }

      private void SolvableNameTextBox_Validating(object sender, CancelEventArgs e)
      {
         TextBox tb = (TextBox)sender;
         if (tb.Text.Trim().Equals(""))
         {
            e.Cancel = true;
            string message = "Please specify a name!";
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         else
         {
            ErrorMessage error = solvable.SpecifyName(this.Text);
            if (error != null)
               UI.ShowError(error);
         }
      }

      private void SolvableNameTextBox_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.GetContainerControl().ActiveControl = null;
         }
      }
   }
}
