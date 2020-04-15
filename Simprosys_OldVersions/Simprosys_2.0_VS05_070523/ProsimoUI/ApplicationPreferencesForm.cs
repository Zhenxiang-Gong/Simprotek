using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ProsimoUI.UnitSystemsUI;
using Prosimo.UnitSystems;

namespace ProsimoUI
{
   public partial class ApplicationPreferencesForm : Form
   {
      private string CURRENT_UNIT_SYS = "";

      private UnitSystemsControl unitSystemsCtrl;
      private ApplicationPreferences appPrefs;
      private MainForm mainForm;

      public ApplicationPreferencesForm(MainForm mainForm, ApplicationPreferences appPrefs, AppPrefsTab tab)
      {
         InitializeComponent();

         this.mainForm = mainForm;
         this.appPrefs = appPrefs;

         // 
         unitSystemsCtrl = new UnitSystemsControl();
         this.tabPageUnits.Controls.Add(unitSystemsCtrl);
         unitSystemsCtrl.Location = new Point(0, 0);

         UnitSystem unitSystem = unitSystemsCtrl.GetSelectedUnitSystem();
         int selIdx = unitSystemsCtrl.GetSelectedIndex();
         this.buttonCurrentUnitSys.Enabled = false;
         if (selIdx >= 0)
         {
            if (unitSystem != null)
            {
               this.buttonCurrentUnitSys.Enabled = true;
            }
         }

         UnitSystem currentUnitSystem = UnitSystemService.GetInstance().CurrentUnitSystem;
         this.labelCurrent.Text = this.CURRENT_UNIT_SYS + currentUnitSystem.Name;
         unitSystemsCtrl.SelectedUnitSystemChanged += new SelectedUnitSystemChangedEventHandler(unitSystemsCtrl_SelectedUnitSystemChanged);

         //
         this.domainUpDownDecPlaces.SelectedItem = this.appPrefs.DecimalPlaces;
         if (this.appPrefs.NumericFormat.Equals(NumericFormat.FixedPoint))
         {
            this.radioButtonFixedPoint.Checked = true;
         }
         else if (this.appPrefs.NumericFormat.Equals(NumericFormat.Scientific))
         {
            this.radioButtonScientific.Checked = true;
         }

         this.ResizeEnd += new EventHandler(ApplicationPreferencesForm_ResizeEnd);

         if (tab == AppPrefsTab.NumericFormat)
         {
            this.tabControlPrefs.SelectTab(tabPageNumberFormat);
         }
         else if (tab == AppPrefsTab.UnitSystems)
         {
            this.tabControlPrefs.SelectTab(tabPageUnits);
         }
      }

      void ApplicationPreferencesForm_ResizeEnd(object sender, EventArgs e)
      {
         if (this.mainForm.Flowsheet != null)
         {
            this.mainForm.Flowsheet.ConnectionManager.DrawConnections();
         }
      }

      private void menuItemClose_Click(object sender, EventArgs e)
      {
         this.Close();      
      }

      private void buttonCurrentUnitSys_Click(object sender, EventArgs e)
      {
         UnitSystem unitSystem = this.unitSystemsCtrl.GetSelectedUnitSystem();
         if (unitSystem != null && unitSystem != UnitSystemService.GetInstance().CurrentUnitSystem)
         {
            UnitSystemService.GetInstance().CurrentUnitSystem = unitSystem;
            this.labelCurrent.Text = this.CURRENT_UNIT_SYS + unitSystem.Name;
            this.appPrefs.CurrentUnitSystemName = unitSystem.Name;
         }
      }

      private void unitSystemsCtrl_SelectedUnitSystemChanged(object sender)
      {
         UnitSystemsControl usc = (UnitSystemsControl)sender;
         UnitSystem unitSystem = usc.GetSelectedUnitSystem();
         int selIdx = usc.GetSelectedIndex();
         this.buttonCurrentUnitSys.Enabled = false;
         if (selIdx >= 0)
         {
            if (unitSystem != null)
            {
               this.buttonCurrentUnitSys.Enabled = true;
            }
         }
      }

      private void domainUpDownDecPlaces_SelectedItemChanged(object sender, EventArgs e)
      {
         this.appPrefs.DecimalPlaces = (string)this.domainUpDownDecPlaces.SelectedItem;
      }

      private void radioButtonFixedPoint_CheckedChanged(object sender, EventArgs e)
      {
         this.appPrefs.NumericFormat = NumericFormat.FixedPoint;

      }

      private void radioButtonScientific_CheckedChanged(object sender, EventArgs e)
      {
         this.appPrefs.NumericFormat = NumericFormat.Scientific;
      }
   }
}