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
	/// Summary description for TeeEditor.
	/// </summary>
	public class TeeEditor : UnitOpEditor
	{
      private int formHeight = 368;
      private int groupBoxHeight = 280;

      private const int INITIAL_LOCATION = 196;
      private const int VALUE_WIDTH = 80;
      private const int INITIAL_GROUPBOX_WIDTH = 200;
      private const int INITIAL_FORM_WIDTH = 216;

      public TeeControl TeeCtrl
      {
         get {return (TeeControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

      private UserControl userCtrlFractions;
      private GroupBox groupBoxFractions;
      private GroupBox groupBoxStreams;
      private Hashtable outletControls;
      private ProsimoUI.SolvableNameTextBox textBoxStreamInName;

      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TeeEditor(TeeControl teeCtrl) : base(teeCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
         //   InitializeComponent();

         //this.Size = new Size(296, this.formHeight);
         //this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = "Tee";
         //this.panel.AutoScroll = true;
            Tee tee = this.TeeCtrl.Tee;
            this.Text = "Tee: " + tee.Name;

         //this.userCtrlFractions = new UserControl();
         //this.userCtrlFractions.AutoScroll = true;
         //this.userCtrlFractions.Size = new Size(242, this.groupBoxHeight - 40);
         //this.userCtrlFractions.Location = new Point(4, 12 + 20 + 2);

         //this.groupBoxStreams = new GroupBox();
         //this.groupBoxStreams.Text = "Inlet/Outlets";
         //this.groupBoxStreams.Location = new System.Drawing.Point(4, 24);
         //this.groupBoxStreams.Size = new System.Drawing.Size(280, this.groupBoxHeight);
         //this.panel.Controls.Add(this.groupBoxStreams);

         //this.groupBoxFractions = new GroupBox();
         //this.groupBoxFractions.Text = "Outlet Fractions";
         //this.groupBoxFractions.Location = new System.Drawing.Point(this.groupBoxStreams.Width + 4, 24);
         //this.groupBoxFractions.Size = new System.Drawing.Size(250, this.groupBoxHeight);
         //this.groupBoxFractions.Controls.Add(this.userCtrlFractions);
         //this.panel.Controls.Add(this.groupBoxFractions);

         //textBoxStreamInName = new ProsimoUI.SolvableNameTextBox();

         //this.outletControls = new Hashtable();

         //if (tee.Inlet is DryingGasStream)
         //{
         //   this.formHeight = 368;
         //   this.groupBoxHeight = 280;
         //   this.ClientSize = new System.Drawing.Size(292, 253);
         //}
         //else if (tee.Inlet is DryingMaterialStream)
         //{
         //   this.formHeight = 388;
         //   this.groupBoxHeight = 300;
         //   this.ClientSize = new System.Drawing.Size(292, 273);
         //}
         //this.groupBoxStreams.Size = new System.Drawing.Size(360, this.groupBoxHeight);
         //this.groupBoxFractions.Location = new System.Drawing.Point(this.groupBoxStreams.Width + 4, 24);
         //this.groupBoxFractions.Size = new System.Drawing.Size(250, this.groupBoxHeight);

         this.UpdateStreamsUI();
         initializeGrid(teeCtrl, columnIndex, false, "Outlet Fractions");
         teeCtrl.Tee.StreamAttached += new StreamAttachedEventHandler(Tee_StreamAttached);
         teeCtrl.Tee.StreamDetached += new StreamDetachedEventHandler(Tee_StreamDetached);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.TeeCtrl != null && this.TeeCtrl.Tee != null)
         {
            this.TeeCtrl.Tee.StreamAttached -= new StreamAttachedEventHandler(Tee_StreamAttached);
            this.TeeCtrl.Tee.StreamDetached -= new StreamDetachedEventHandler(Tee_StreamDetached);
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
         // TeeEditor
         // 
         this.ClientSize = new System.Drawing.Size(292, 273);
         this.Name = "TeeEditor";
         this.ResumeLayout(false);

      }
		#endregion

      private void Tee_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamsUI();
      }

      private void Tee_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI()
      {

         Tee tee = this.TeeCtrl.Tee;
         bool hasStreamIn = false;
         bool hasStreamOut = false;

         ProcessStreamBase streamOut = null;
         if (tee.OutletStreams.Count > 0)
         {
             hasStreamOut = true;
             streamOut = (ProcessStreamBase)tee.OutletStreams[0];
             //this.userCtrlFractions.Visible = true;
         }

         ProcessStreamBase streamIn = tee.Inlet;
         if (streamIn != null)
            hasStreamIn = true;

         if (hasStreamIn)
         {
             ProcessStreamBaseControl processInCtrl = (ProcessStreamBaseControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamIn.Name);
             initializeGrid(processInCtrl, columnIndex, false, "Inlet/Outlets");
             columnIndex += 2;
         }

         if (hasStreamOut)
         {
             Boolean bFirstControl = true;
             if (hasStreamIn)
                 bFirstControl = false;
            IEnumerator e = tee.OutletStreams.GetEnumerator();
            while (e.MoveNext())
            {
               ProcessStreamBase processStreamBase = (ProcessStreamBase)e.Current;
               ProcessStreamBaseControl processOutCtrl = (ProcessStreamBaseControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(processStreamBase.Name);
               if (bFirstControl)
               {
                   initializeGrid(processOutCtrl, columnIndex, false, "Inlet/Outlets");
                   columnIndex += 2;
                   bFirstControl = false;
               }
               else
               {
                   initializeGrid(processOutCtrl, columnIndex, true, "Inlet/Outlets");
                   columnIndex++;
               }
              
               //this.groupBoxStreams.Controls.Add(textBoxStreamOutName);
               //UI.SetStatusColor(this,statusBar, processStreamBase.SolveState);
            }

            // build the fractions
            for (int i=0; i<this.TeeCtrl.Tee.OutletStreamAndFractions.Count; i++)
            {
               StreamAndFraction sf = (StreamAndFraction)this.TeeCtrl.Tee.OutletStreamAndFractions[i];
               TeeStreamAndFractionControl sfCtrl = new TeeStreamAndFractionControl(this.TeeCtrl.Flowsheet, sf);
               
               //sfCtrl.Location = new Point(0, i*sfCtrl.Height);
               //this.userCtrlFractions.Controls.Add(sfCtrl);
               //sfCtrl.textBoxFraction.KeyUp += new KeyEventHandler(KeyUpNavigator);
            }
         }
      }

      private void KeyUpNavigator(object sender, KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         IEnumerator en = this.userCtrlFractions.Controls.GetEnumerator();
         while (en.MoveNext())
         {
            TeeStreamAndFractionControl sfCtrl = (TeeStreamAndFractionControl)en.Current;
            list.Add(sfCtrl.textBoxFraction);
         }

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this.userCtrlFractions, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this.userCtrlFractions, KeyboardNavigation.Up);
         }
      }
   }
}
