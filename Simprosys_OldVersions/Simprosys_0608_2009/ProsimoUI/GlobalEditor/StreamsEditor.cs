using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.GlobalEditor
{
	/// <summary>
	/// Summary description for StreamsEditor.
	/// </summary>
	public class StreamsEditor : System.Windows.Forms.UserControl
	{
      protected Flowsheet flowsheet;
      protected int x;
      protected int y;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StreamsEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public StreamsEditor(Flowsheet flowsheet)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.flowsheet = flowsheet;
         this.InitializeUI();
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
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
			components = new System.ComponentModel.Container();
		}
		#endregion

      public void InitializeUI()
      {
         this.Controls.Clear();
         x = 0;
         y = 0;
         
         y += 20; // make place for name

         Label label = new Label();
         label.Text = this.GetTitle();
         label.Width = 192;
         label.Height = 20;
         label.Location = new Point(x,y-20);
         label.BorderStyle = BorderStyle.FixedSingle;
         label.TextAlign = ContentAlignment.MiddleRight;
         label.BackColor = Color.DarkGray;
         this.Controls.Add(label);

         //
         IList list = this.GetStreamList();
         IEnumerator e2 = list.GetEnumerator();
         ProcessStreamBase solvab = null;
         while (e2.MoveNext())
         {
            solvab = (ProcessStreamBase)e2.Current;
            break; // need only one
         }
         UserControl labelsCtrl = this.GetNewStreamLabelsControl(solvab);
         labelsCtrl.Location = new Point(x,y);
         this.Controls.Add(labelsCtrl);

         x += labelsCtrl.Width;

         e2 = this.GetShowableInEditorStreamControls().GetEnumerator();
         while (e2.MoveNext())
         {
            ProcessStreamBaseControl streamCtrl =  (ProcessStreamBaseControl)e2.Current;
            Solvable solvable = streamCtrl.Solvable;

            SolvableNameTextBox textBoxName = new SolvableNameTextBox(solvable);
            textBoxName.Location = new Point(x,y-20);
            this.Controls.Add(textBoxName);
            
            UserControl valuesCtrl = this.GetNewStreamValuesControl(streamCtrl);
            valuesCtrl.Location = new Point(x,y);
            this.Controls.Add(valuesCtrl);
            
            x += valuesCtrl.Width;
            this.Width = x;
            this.Height = y + labelsCtrl.Height;
         }
      }

      protected virtual string GetTitle()
      {
         return null;
      }

      protected virtual IList GetStreamList()
      {
         return null;
      }

      protected virtual UserControl GetNewStreamLabelsControl(Solvable solvable)
      {
         return null;
      }

      protected virtual IList GetShowableInEditorStreamControls()
      {
         return null;
      }

      protected virtual UserControl GetNewStreamValuesControl(ProcessStreamBaseControl streamCtrl)
      {
         return null;
      }
   }
}
