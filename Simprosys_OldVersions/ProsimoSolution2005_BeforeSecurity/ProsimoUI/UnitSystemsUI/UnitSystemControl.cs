using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;

namespace ProsimoUI.UnitSystemsUI
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
         this.textBoxUnitSystem.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.textBoxUnitSystem.Size = new System.Drawing.Size(240, 277);
         this.textBoxUnitSystem.TabIndex = 14;
         this.textBoxUnitSystem.Text = "";
         this.textBoxUnitSystem.WordWrap = false;
         // 
         // UnitSystemControl
         // 
         this.Controls.Add(this.textBoxUnitSystem);
         this.Name = "UnitSystemControl";
         this.Size = new System.Drawing.Size(240, 280);
         this.ResumeLayout(false);

      }
      #endregion

      public void UpdateUnitSystem(UnitSystem unitSystem)
      {
         this.unitSystem = unitSystem;
         this.TextBox.Clear();
         if (unitSystem != null)
         {
            this.textBoxUnitSystem.ScrollBars = System.Windows.Forms.ScrollBars.None;

            string temperature = "Temperature: \t\t" + TemperatureUnit.GetUnitAsString(unitSystem.TemperatureUnitType);
            string pressure = "Pressure: \t\t\t" + PressureUnit.GetUnitAsString(unitSystem.PressureUnitType);
            string massFlowRate = "Mass Flow Rate: \t\t" + MassFlowRateUnit.GetUnitAsString(unitSystem.MassFlowRateUnitType);
            string volumeFlowRate = "Volume Flow Rate: \t\t" + VolumeFlowRateUnit.GetUnitAsString(unitSystem.VolumeFlowRateUnitType); 
            string moistureContent = "Moisture Content: \t\t" + MoistureContentUnit.GetUnitAsString(unitSystem.MoistureContentUnitType);
            string relativeHumidity = "Fraction: \t\t\t" + FractionUnit.GetUnitAsString(unitSystem.FractionUnitType);
            string enthalpy = "Specific Energy: \t\t" + SpecificEnergyUnit.GetUnitAsString(unitSystem.SpecificEnergyUnitType);
            string specificHeat = "Specific Heat: \t\t" + SpecificHeatUnit.GetUnitAsString(unitSystem.SpecificHeatUnitType);
            string energy = "Energy: \t\t\t" + EnergyUnit.GetUnitAsString(unitSystem.EnergyUnitType);
            string power = "Power: \t\t\t" + PowerUnit.GetUnitAsString(unitSystem.PowerUnitType);
            string density = "Density: \t\t\t" + DensityUnit.GetUnitAsString(unitSystem.DensityUnitType);
            string dynamicViscosity = "Dynamic Viscosity: \t\t" + DynamicViscosityUnit.GetUnitAsString(unitSystem.DynamicViscosityUnitType);
            string kinematicViscosity = "Kinematic Viscosity: \t" + KinematicViscosityUnit.GetUnitAsString(unitSystem.KinematicViscosityUnitType);
            string conductivity = "Thermal Conductivity: \t" + ThermalConductivityUnit.GetUnitAsString(unitSystem.ThermalConductivityUnitType);
            string diffusivity = "Diffusivity: \t\t" + DiffusivityUnit.GetUnitAsString(unitSystem.DiffusivityUnitType);
            string mass = "Mass: \t\t\t" + MassUnit.GetUnitAsString(unitSystem.MassUnitType);
            string length = "Length: \t\t\t" + LengthUnit.GetUnitAsString(unitSystem.LengthUnitType);
            string area = "Area: \t\t\t" + AreaUnit.GetUnitAsString(unitSystem.AreaUnitType);
            string volume = "Volume: \t\t\t" + VolumeUnit.GetUnitAsString(unitSystem.VolumeUnitType);
            string time = "Time: \t\t\t" + TimeUnit.GetUnitAsString(unitSystem.TimeUnitType);
            string smallLength = "Small Length: \t\t" + SmallLengthUnit.GetUnitAsString(unitSystem.SmallLengthUnitType);
            string microLength = "Micro Length: \t\t" + MicroLengthUnit.GetUnitAsString(unitSystem.MicroLengthUnitType);
            string liquidHead = "Liquid Head: \t\t" + LiquidHeadUnit.GetUnitAsString(unitSystem.LiquidHeadUnitType);
            string volumeRateFlowLiquids = "Volume Rate Flow Liquids: \t" + VolumeRateFlowLiquidsUnit.GetUnitAsString(unitSystem.VolumeRateFlowLiquidsUnitType);
            string volumeRateFlowGases = "Volume Rate Flow Gases: \t" + VolumeRateFlowGasesUnit.GetUnitAsString(unitSystem.VolumeRateFlowGasesUnitType);
            string heatTransferCoeff = "Heat Transfer Coeff.: \t" + HeatTransferCoefficientUnit.GetUnitAsString(unitSystem.HeatTransferCoefficientUnitType);
            string surfaceTension = "Surface Tension: \t\t" + SurfaceTensionUnit.GetUnitAsString(unitSystem.SurfaceTensionUnitType);
            string velocity = "Velocity: \t\t\t" + VelocityUnit.GetUnitAsString(unitSystem.VelocityUnitType);
            string foulingFactor = "Fouling Factor: \t\t" + FoulingFactorUnit.GetUnitAsString(unitSystem.FoulingFactorUnitType);
            string specificVolume = "Specific Volume: \t\t" + SpecificVolumeUnit.GetUnitAsString(unitSystem.SpecificVolumeUnitType);
            string massVolumeConcentration = "Mass Volume Concentration: \t" + MassVolumeConcentrationUnit.GetUnitAsString(unitSystem.MassVolumeConcentrationUnitType);
            string planeAngle = "Plane Angle: \t\t" + PlaneAngleUnit.GetUnitAsString(unitSystem.PlaneAngleUnitType);
            string volumeHeatTransferCoefficient = "Volume Heat Transfer Coeff.: \t" + VolumeHeatTransferCoefficientUnit.GetUnitAsString(unitSystem.VolumeHeatTransferCoefficientUnitType);

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
            this.TextBox.AppendText(time + "\r\n");
            this.TextBox.AppendText(smallLength + "\r\n");
            this.TextBox.AppendText(microLength + "\r\n");
            this.TextBox.AppendText(liquidHead + "\r\n");
            this.TextBox.AppendText(volumeRateFlowLiquids + "\r\n");
            this.TextBox.AppendText(volumeRateFlowGases + "\r\n");
            this.TextBox.AppendText(heatTransferCoeff + "\r\n");
            this.TextBox.AppendText(surfaceTension + "\r\n");
            this.TextBox.AppendText(velocity + "\r\n");
            this.TextBox.AppendText(foulingFactor + "\r\n");
            this.TextBox.AppendText(specificVolume + "\r\n");
            this.TextBox.AppendText(massVolumeConcentration + "\r\n");
            this.TextBox.AppendText(planeAngle + "\r\n");
            this.TextBox.AppendText(volumeHeatTransferCoefficient + "\r\n");

            this.textBoxUnitSystem.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         }
      }
   }
}
