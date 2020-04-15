using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Miscellaneous;

namespace ProsimoUI.UnitOperationsUI
{
	/// <summary>
	/// Summary description for MixerEditor.
	/// </summary>
	public class MixerEditor : UnitOpEditor
	{
      private int formHeight = 368;
      private int groupBoxHeight = 280;

      private const int INITIAL_LOCATION = 196;
      private const int VALUE_WIDTH = 80;
      private const int INITIAL_GROUPBOX_WIDTH = 200;
      private const int INITIAL_FORM_WIDTH = 216;

      public MixerControl MixerCtrl
      {
         get {return (MixerControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

      private GroupBox groupBox;
      private Hashtable inletControls;
      private ProsimoUI.SolvableNameTextBox textBoxStreamOutName;

      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MixerEditor(MixerControl mixerCtrl) : base(mixerCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
         //   InitializeComponent();

         //this.Size = new Size(296, this.formHeight);
         //this.FormBorderStyle = FormBorderStyle.Sizable;
         //this.Text = "Mixer";
         //this.panel.AutoScroll = true;
         //Mixer mixer = this.MixerCtrl.Mixer;
         //this.Text = "Mixer: " + mixer.Name;
         //this.groupBox = new GroupBox();
         //this.groupBox.Text = "Inlets/Outlet";
         //this.groupBox.Location = new System.Drawing.Point(4, 24);
         //this.groupBox.Size = new System.Drawing.Size(280, this.groupBoxHeight);
         //this.panel.Controls.Add(this.groupBox);

         //this.textBoxStreamOutName = new ProsimoUI.SolvableNameTextBox();

         //this.inletControls = new Hashtable();

         //if (mixer.Outlet is DryingGasStream)
         //{
         //   this.formHeight = 368;
         //   this.groupBoxHeight = 280;
         //   this.ClientSize = new System.Drawing.Size(292, 253);
         //}
         //else if (mixer.Outlet is DryingMaterialStream)
         //{
         //   this.formHeight = 388;
         //   this.groupBoxHeight = 300;
         //   this.ClientSize = new System.Drawing.Size(292, 273);
         //}
         //this.groupBox.Size = new System.Drawing.Size(360, this.groupBoxHeight);

         this.UpdateStreamUI();
         mixerCtrl.Mixer.StreamAttached += new StreamAttachedEventHandler(Mixer_StreamAttached);
         mixerCtrl.Mixer.StreamDetached += new StreamDetachedEventHandler(Mixer_StreamDetached);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.MixerCtrl != null && this.MixerCtrl.Mixer != null)
         {
            this.MixerCtrl.Mixer.StreamAttached -= new StreamAttachedEventHandler(Mixer_StreamAttached);
            this.MixerCtrl.Mixer.StreamDetached -= new StreamDetachedEventHandler(Mixer_StreamDetached);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.SuspendLayout();
         // 
         // MixerEditor
         // 
         this.ClientSize = new System.Drawing.Size(292, 273);
         this.Name = "MixerEditor";
         this.ResumeLayout(false);

      }
		#endregion

      private void Mixer_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamUI();
      }

      private void Mixer_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamUI();
      }

      private void UpdateStreamUI()
      {
         //this.panel.Visible = false;
         //this.groupBox.Visible = false;
         //this.groupBox.Controls.Clear();
         //this.inletControls.Clear();
         //int x = INITIAL_LOCATION;
         //int w = VALUE_WIDTH;
         //int s = INITIAL_GROUPBOX_WIDTH;
         //int p = INITIAL_FORM_WIDTH;

         Mixer mixer = this.MixerCtrl.Mixer;
         bool hasStreamIn = false;
         bool hasStreamOut = false;

         ProcessStreamBase streamIn = null;
         if (mixer.InletStreams.Count > 0)
         {
            hasStreamIn = true;
            streamIn = (ProcessStreamBase)mixer.InletStreams[0];
         }

         ProcessStreamBase streamOut = mixer.Outlet;
         Boolean bFirstControl = true;
         if (streamOut != null)
            hasStreamOut = true;

         if (hasStreamIn)
         {
            IEnumerator e = mixer.InletStreams.GetEnumerator();
            while (e.MoveNext())
            {
               ProcessStreamBase processStreamBase = (ProcessStreamBase)e.Current;
               ProcessStreamBaseControl processInCtrl = (ProcessStreamBaseControl)this.MixerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(processStreamBase.Name);
               if (bFirstControl)
               {
                   initializeGrid(processInCtrl, columnIndex, false, "Inlet/Outlet");
                   columnIndex += 2;
                   bFirstControl = false;

               }
               else
               {
                   initializeGrid(processInCtrl, columnIndex, true, "Inlet/Outlet");
                   columnIndex++;
               }
               //this.groupBox.Controls.Add(textBoxStreamInName);
               //UI.SetStatusColor(textBoxStreamInName, processStreamBase.SolveState);

            }
         }

         if (hasStreamOut)
         {
             ProcessStreamBaseControl processOutCtrl = (ProcessStreamBaseControl)this.MixerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamOut.Name);
             if (bFirstControl)
             {
                 initializeGrid(processOutCtrl, columnIndex, false, "Inlet/Outlet");
                 columnIndex += 2;
                 bFirstControl = false;
             }
             else
             {
                 initializeGrid(processOutCtrl, columnIndex, true, "Inlet/Outlet");
                 columnIndex++;
             }
            //UserControl ctrl = null;
          //  if (streamOut is ProcessStream))
          //  {
          //     ProcessStreamControl processOutCtrl = (ProcessStreamControl)this.MixerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamOut.Name);
     
          //ctrl = new ProcessStreamValuesControl(processOutCtrl);
          //  }
          //  else if (streamOut is DryingGasStream)
          //  {
          //     GasStreamControl gasOutCtrl = (GasStreamControl)this.MixerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamOut.Name);
          //     ctrl = new GasStreamValuesControl(gasOutCtrl);
          //  }
          //  else if (streamOut is DryingMaterialStream)
          //  {
          //     MaterialStreamControl materialOutCtrl = (MaterialStreamControl)this.MixerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamOut.Name);
          //     ctrl = new MaterialStreamValuesControl(materialOutCtrl);
          //  }
          //  ctrl.Location = new Point(x, 12 + 20 + 2);
          //  this.groupBox.Size = new Size(s, this.groupBoxHeight);
          //  this.groupBox.Controls.Add(ctrl);
          //  this.Size = new Size(p, this.formHeight);

          //  textBoxStreamOutName.Text = streamOut.Name;
          //  textBoxStreamOutName.Location = new Point(x, 12);

          //  this.textBoxStreamOutName.SetSolvable(streamOut);
          //  this.groupBox.Controls.Add(this.textBoxStreamOutName);
            //UI.SetStatusColor(this.statusBar, streamOut.BSolveState);
         }
         //this.panel.Visible = true;
      }
   }
}
