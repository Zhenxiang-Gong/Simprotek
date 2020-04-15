using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Drying.UnitSystems;

namespace Drying
{
	/// <summary>
	/// Summary description for UnitSystemControl.
	/// </summary>
	public class UnitSystemControl : System.Windows.Forms.UserControl
	{
      private UnitSystem unitSystem;
      private System.Windows.Forms.TextBox textBoxUnitSystem;
      public TextBox TextBox
      {
         get {return textBoxUnitSystem;}
         set {textBoxUnitSystem = value;}
      }

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UnitSystemControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
         this.textBoxUnitSystem = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // textBoxUnitSystem
         // 
         this.textBoxUnitSystem.AcceptsReturn = true;
         this.textBoxUnitSystem.AcceptsTab = true;
         this.textBoxUnitSystem.Location = new System.Drawing.Point(0, 3);
         this.textBoxUnitSystem.Multiline = true;
         this.textBoxUnitSystem.Name = "textBoxUnitSystem";
         this.textBoxUnitSystem.ReadOnly = true;
         this.textBoxUnitSystem.Size = new System.Drawing.Size(312, 277);
         this.textBoxUnitSystem.TabIndex = 14;
         this.textBoxUnitSystem.Text = "";
         // 
         // UnitSystemControl
         // 
         this.Controls.Add(this.textBoxUnitSystem);
         this.Name = "UnitSystemControl";
         this.Size = new System.Drawing.Size(320, 288);
         this.ResumeLayout(false);

      }
		#endregion

      public void UpdateUnitSystem(UnitSystem unitSystem)
      {
         this.unitSystem = unitSystem;
         this.TextBox.Clear();
         if (unitSystem != null)
         {
            string temperature = "Temperature: \t\t" + TemperatureConverter.GetUnitAsString(unitSystem.TemperatureUnit);
            string pressure = "Pressure: \t\t\t" + PressureConverter.GetUnitAsString(unitSystem.PressureUnit);
            string massFlowRate = "Mass Flow Rate: \t\t" + MassFlowRateConverter.GetUnitAsString(unitSystem.MassFlowRateUnit);
            string volumeFlowRate = "Volume Flow Rate: \t\t" + VolumeFlowRateConverter.GetUnitAsString(unitSystem.VolumeFlowRateUnit); 
            string moistureContent = "Moisture Content: \t\t" + MoistureContentConverter.GetUnitAsString(unitSystem.MoistureContentUnit);
            string relativeHumidity = "Fraction: \t\t\t" + FractionConverter.GetUnitAsString(unitSystem.FractionUnit);
            string enthalpy = "Specific Energy: \t\t" + SpecificEnergyConverter.GetUnitAsString(unitSystem.SpecificEnergyUnit);
            string specificHeat = "Specific Heat: \t\t" + SpecificHeatConverter.GetUnitAsString(unitSystem.SpecificHeatUnit);
            string energy = "Energy: \t\t\t" + EnergyConverter.GetUnitAsString(unitSystem.EnergyUnit);
            string power = "Power: \t\t\t" + PowerConverter.GetUnitAsString(unitSystem.PowerUnit);
            string density = "Density: \t\t\t" + DensityConverter.GetUnitAsString(unitSystem.DensityUnit);
            string dynamicViscosity = "Dynamic Viscosity: \t\t" + DynamicViscosityConverter.GetUnitAsString(unitSystem.DynamicViscosityUnit);
            string kinematicViscosity = "Kinematic Viscosity: \t" + KinematicViscosityConverter.GetUnitAsString(unitSystem.KinematicViscosityUnit);
            string conductivity = "Thermal Conductivity: \t" + ThermalConductivityConverter.GetUnitAsString(unitSystem.ThermalConductivityUnit);
            string diffusivity = "Diffusivity: \t\t" + DiffusivityConverter.GetUnitAsString(unitSystem.DiffusivityUnit);
            string mass = "Mass: \t\t\t" + MassConverter.GetUnitAsString(unitSystem.MassUnit);
            string length = "Length: \t\t\t" + LengthConverter.GetUnitAsString(unitSystem.LengthUnit);
            string area = "Area: \t\t\t" + AreaConverter.GetUnitAsString(unitSystem.AreaUnit);
            string volume = "Volume: \t\t\t" + VolumeConverter.GetUnitAsString(unitSystem.VolumeUnit);
            string time = "Time: \t\t\t" + TimeConverter.GetUnitAsString(unitSystem.TimeUnit);

            this.TextBox.AppendText(temperature + "\r\n");
            this.TextBox.AppendText(pressure + "\r\n");
            this.TextBox.AppendText(massFlowRate + "\r\n");
            this.TextBox.AppendText(volumeFlowRate + "\r\n");
            this.TextBox.AppendText(moistureContent + "\r\n");
            this.TextBox.AppendText(relativeHumidity + "\r\n");
            this.TextBox.AppendText(enthalpy + "\r\n");
            this.TextBox.AppendText(specificHeat + "\r\n");
            this.TextBox.AppendText(energy + "\r\n");
            this.TextBox.AppendText(power + "\r\n");
            this.TextBox.AppendText(density + "\r\n");
            this.TextBox.AppendText(dynamicViscosity + "\r\n");
            this.TextBox.AppendText(kinematicViscosity + "\r\n");
            this.TextBox.AppendText(conductivity + "\r\n");
            this.TextBox.AppendText(diffusivity + "\r\n");
            this.TextBox.AppendText(mass + "\r\n");
            this.TextBox.AppendText(length + "\r\n");
            this.TextBox.AppendText(area + "\r\n");
            this.TextBox.AppendText(volume + "\r\n");
            this.TextBox.AppendText(time);
         }
      }
	}
}
