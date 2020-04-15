using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for UIForm.
	/// </summary>
   public class UIForm : System.Windows.Forms.Form
   {
      private const int NOT_SOLVED = 0;
      private const int PARTIALLY_SOLVED = 1;
      private const int SOLVE_FAILED = 2;
      private const int SOLVED = 3;

      // solve icons
      private Icon notSolvedIcon;
      public Icon NOT_SOLVED_ICON {get{return this.notSolvedIcon;}}

      private Icon partiallySolvedIcon;
      public Icon PARTIALLY_SOLVED_ICON {get {return this.partiallySolvedIcon;}}
      
      private Icon solveFailedIcon;
      public Icon SOLVE_FAILED_ICON {get {return this.solveFailedIcon;}}
      
      private Icon solvedIcon;
      public Icon SOLVED_ICON {get {return this.solvedIcon;}}

      // gas
      public Image GAS_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListGasDown.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_PARTIALLY_SOLVED_DOWN_IMG {get {return this.imageListGasDown.Images[PARTIALLY_SOLVED];}}
      public Image GAS_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListGasDown.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_DOWN_IMG {get {return this.imageListGasDown.Images[SOLVED];}}
      
      public Image GAS_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListGasLeft.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_PARTIALLY_SOLVED_LEFT_IMG {get {return this.imageListGasLeft.Images[PARTIALLY_SOLVED];}}
      public Image GAS_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListGasLeft.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_LEFT_IMG {get {return this.imageListGasLeft.Images[SOLVED];}}

      public Image GAS_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListGasUp.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_PARTIALLY_SOLVED_UP_IMG {get {return this.imageListGasUp.Images[PARTIALLY_SOLVED];}}
      public Image GAS_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListGasUp.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_UP_IMG {get {return this.imageListGasUp.Images[SOLVED];}}

      public Image GAS_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListGasRight.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_PARTIALLY_SOLVED_RIGHT_IMG {get {return this.imageListGasRight.Images[PARTIALLY_SOLVED];}}
      public Image GAS_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListGasRight.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListGasRight.Images[SOLVED];}}

      // material solid
      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_PARTIALLY_SOLVED_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[SOLVED];}}

      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_PARTIALLY_SOLVED_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[SOLVED];}}
 
      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListMaterialSolidUp.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_PARTIALLY_SOLVED_UP_IMG {get {return this.imageListMaterialSolidUp.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListMaterialSolidUp.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_UP_IMG {get {return this.imageListMaterialSolidUp.Images[SOLVED];}}

      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_PARTIALLY_SOLVED_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[SOLVED];}}

      // material liquid
      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_PARTIALLY_SOLVED_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[SOLVED];}}

      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_PARTIALLY_SOLVED_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[SOLVED];}}
 
      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_PARTIALLY_SOLVED_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[SOLVED];}}

      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_PARTIALLY_SOLVED_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[PARTIALLY_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[SOLVED];}}

      // process
      public Image PROCESS_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListProcessDown.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_PARTIALLY_SOLVED_DOWN_IMG {get {return this.imageListProcessDown.Images[PARTIALLY_SOLVED];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListProcessDown.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_DOWN_IMG {get {return this.imageListProcessDown.Images[SOLVED];}}

      public Image PROCESS_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListProcessLeft.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_PARTIALLY_SOLVED_LEFT_IMG {get {return this.imageListProcessLeft.Images[PARTIALLY_SOLVED];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListProcessLeft.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_LEFT_IMG {get {return this.imageListProcessLeft.Images[SOLVED];}}

      public Image PROCESS_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListProcessUp.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_PARTIALLY_SOLVED_UP_IMG {get {return this.imageListProcessUp.Images[PARTIALLY_SOLVED];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListProcessUp.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_UP_IMG {get {return this.imageListProcessUp.Images[SOLVED];}}

      public Image PROCESS_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListProcessRight.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_PARTIALLY_SOLVED_RIGHT_IMG {get {return this.imageListProcessRight.Images[PARTIALLY_SOLVED];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListProcessRight.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListProcessRight.Images[SOLVED];}}

      // continuous dryer
      public Image DRYER_LIQUID_CTRL_NOT_SOLVED_IMG {get {return this.imageListDryerLiquid.Images[NOT_SOLVED];}}
      public Image DRYER_LIQUID_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListDryerLiquid.Images[PARTIALLY_SOLVED];}}
      public Image DRYER_LIQUID_CTRL_SOLVE_FAILED_IMG {get {return this.imageListDryerLiquid.Images[SOLVE_FAILED];}}
      public Image DRYER_LIQUID_CTRL_SOLVED_IMG {get {return this.imageListDryerLiquid.Images[SOLVED];}}

      // batch dryer
      public Image DRYER_SOLID_CTRL_NOT_SOLVED_IMG {get {return this.imageListDryerSolid.Images[NOT_SOLVED];}}
      public Image DRYER_SOLID_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListDryerSolid.Images[PARTIALLY_SOLVED];}}
      public Image DRYER_SOLID_CTRL_SOLVE_FAILED_IMG {get {return this.imageListDryerSolid.Images[SOLVE_FAILED];}}
      public Image DRYER_SOLID_CTRL_SOLVED_IMG {get {return this.imageListDryerSolid.Images[SOLVED];}}

      // heat exchanger
      public Image HEATEXCHANGER_CTRL_NOT_SOLVED_IMG {get {return this.imageListHeatExchanger.Images[NOT_SOLVED];}}
      public Image HEATEXCHANGER_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListHeatExchanger.Images[PARTIALLY_SOLVED];}}
      public Image HEATEXCHANGER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListHeatExchanger.Images[SOLVE_FAILED];}}
      public Image HEATEXCHANGER_CTRL_SOLVED_IMG {get {return this.imageListHeatExchanger.Images[SOLVED];}}

      // fan
      public Image FAN_CTRL_NOT_SOLVED_IMG {get {return this.imageListFan.Images[NOT_SOLVED];}}
      public Image FAN_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListFan.Images[PARTIALLY_SOLVED];}}
      public Image FAN_CTRL_SOLVE_FAILED_IMG {get {return this.imageListFan.Images[SOLVE_FAILED];}}
      public Image FAN_CTRL_SOLVED_IMG {get {return this.imageListFan.Images[SOLVED];}}

      // valve
      public Image VALVE_CTRL_NOT_SOLVED_IMG {get {return this.imageListValve.Images[NOT_SOLVED];}}
      public Image VALVE_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListValve.Images[PARTIALLY_SOLVED];}}
      public Image VALVE_CTRL_SOLVE_FAILED_IMG {get {return this.imageListValve.Images[SOLVE_FAILED];}}
      public Image VALVE_CTRL_SOLVED_IMG {get {return this.imageListValve.Images[SOLVED];}}

      // bag filter
      public Image BAGFILTER_CTRL_NOT_SOLVED_IMG {get {return this.imageListBagFilter.Images[NOT_SOLVED];}}
      public Image BAGFILTER_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListBagFilter.Images[PARTIALLY_SOLVED];}}
      public Image BAGFILTER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListBagFilter.Images[SOLVE_FAILED];}}
      public Image BAGFILTER_CTRL_SOLVED_IMG {get {return this.imageListBagFilter.Images[SOLVED];}}

      // air filter
      public Image AIRFILTER_CTRL_NOT_SOLVED_IMG {get {return this.imageListAirFilter.Images[NOT_SOLVED];}}
      public Image AIRFILTER_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListAirFilter.Images[PARTIALLY_SOLVED];}}
      public Image AIRFILTER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListAirFilter.Images[SOLVE_FAILED];}}
      public Image AIRFILTER_CTRL_SOLVED_IMG {get {return this.imageListAirFilter.Images[SOLVED];}}

      // compressor
      public Image COMPRESSOR_CTRL_NOT_SOLVED_IMG {get {return this.imageListCompressor.Images[NOT_SOLVED];}}
      public Image COMPRESSOR_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListCompressor.Images[PARTIALLY_SOLVED];}}
      public Image COMPRESSOR_CTRL_SOLVE_FAILED_IMG {get {return this.imageListCompressor.Images[SOLVE_FAILED];}}
      public Image COMPRESSOR_CTRL_SOLVED_IMG {get {return this.imageListCompressor.Images[SOLVED];}}

      // heater
      public Image HEATER_CTRL_NOT_SOLVED_IMG {get {return this.imageListHeater.Images[NOT_SOLVED];}}
      public Image HEATER_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListHeater.Images[PARTIALLY_SOLVED];}}
      public Image HEATER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListHeater.Images[SOLVE_FAILED];}}
      public Image HEATER_CTRL_SOLVED_IMG {get {return this.imageListHeater.Images[SOLVED];}}

      // cooler
      public Image COOLER_CTRL_NOT_SOLVED_IMG {get {return this.imageListCooler.Images[NOT_SOLVED];}}
      public Image COOLER_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListCooler.Images[PARTIALLY_SOLVED];}}
      public Image COOLER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListCooler.Images[SOLVE_FAILED];}}
      public Image COOLER_CTRL_SOLVED_IMG {get {return this.imageListCooler.Images[SOLVED];}}

      // electrostatic precipitator
      public Image ELECTROSTATICPRECIPITATOR_CTRL_NOT_SOLVED_IMG {get {return this.imageListElectrostaticPrecipitator.Images[NOT_SOLVED];}}
      public Image ELECTROSTATICPRECIPITATOR_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListElectrostaticPrecipitator.Images[PARTIALLY_SOLVED];}}
      public Image ELECTROSTATICPRECIPITATOR_CTRL_SOLVE_FAILED_IMG {get {return this.imageListElectrostaticPrecipitator.Images[SOLVE_FAILED];}}
      public Image ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_IMG {get {return this.imageListElectrostaticPrecipitator.Images[SOLVED];}}

      // pump
      public Image PUMP_CTRL_NOT_SOLVED_IMG {get {return this.imageListPump.Images[NOT_SOLVED];}}
      public Image PUMP_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListPump.Images[PARTIALLY_SOLVED];}}
      public Image PUMP_CTRL_SOLVE_FAILED_IMG {get {return this.imageListPump.Images[SOLVE_FAILED];}}
      public Image PUMP_CTRL_SOLVED_IMG {get {return this.imageListPump.Images[SOLVED];}}

      // cyclone
      public Image CYCLONE_CTRL_NOT_SOLVED_IMG {get {return this.imageListCyclone.Images[NOT_SOLVED];}}
      public Image CYCLONE_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListCyclone.Images[PARTIALLY_SOLVED];}}
      public Image CYCLONE_CTRL_SOLVE_FAILED_IMG {get {return this.imageListCyclone.Images[SOLVE_FAILED];}}
      public Image CYCLONE_CTRL_SOLVED_IMG {get {return this.imageListCyclone.Images[SOLVED];}}

      // ejector
      public Image EJECTOR_CTRL_NOT_SOLVED_IMG {get {return this.imageListEjector.Images[NOT_SOLVED];}}
      public Image EJECTOR_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListEjector.Images[PARTIALLY_SOLVED];}}
      public Image EJECTOR_CTRL_SOLVE_FAILED_IMG {get {return this.imageListEjector.Images[SOLVE_FAILED];}}
      public Image EJECTOR_CTRL_SOLVED_IMG {get {return this.imageListEjector.Images[SOLVED];}}

      // wet scrubber
      public Image WETSCRUBBER_CTRL_NOT_SOLVED_IMG {get {return this.imageListWetScrubber.Images[NOT_SOLVED];}}
      public Image WETSCRUBBER_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListWetScrubber.Images[PARTIALLY_SOLVED];}}
      public Image WETSCRUBBER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListWetScrubber.Images[SOLVE_FAILED];}}
      public Image WETSCRUBBER_CTRL_SOLVED_IMG {get {return this.imageListWetScrubber.Images[SOLVED];}}

      // mixer
      public Image MIXER_CTRL_NOT_SOLVED_IMG {get {return this.imageListMixer.Images[NOT_SOLVED];}}
      public Image MIXER_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListMixer.Images[PARTIALLY_SOLVED];}}
      public Image MIXER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListMixer.Images[SOLVE_FAILED];}}
      public Image MIXER_CTRL_SOLVED_IMG {get {return this.imageListMixer.Images[SOLVED];}}

      // tee
      public Image TEE_CTRL_NOT_SOLVED_IMG {get {return this.imageListTee.Images[NOT_SOLVED];}}
      public Image TEE_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListTee.Images[PARTIALLY_SOLVED];}}
      public Image TEE_CTRL_SOLVE_FAILED_IMG {get {return this.imageListTee.Images[SOLVE_FAILED];}}
      public Image TEE_CTRL_SOLVED_IMG {get {return this.imageListTee.Images[SOLVED];}}

      // flash tank
      public Image FLASHTANK_CTRL_NOT_SOLVED_IMG {get {return this.imageListFlashTank.Images[NOT_SOLVED];}}
      public Image FLASHTANK_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListFlashTank.Images[PARTIALLY_SOLVED];}}
      public Image FLASHTANK_CTRL_SOLVE_FAILED_IMG {get {return this.imageListFlashTank.Images[SOLVE_FAILED];}}
      public Image FLASHTANK_CTRL_SOLVED_IMG {get {return this.imageListFlashTank.Images[SOLVED];}}

      // recycle
      public Image RECYCLE_CTRL_NOT_SOLVED_IMG {get {return this.imageListRecycle.Images[NOT_SOLVED];}}
      public Image RECYCLE_CTRL_PARTIALLY_SOLVED_IMG {get {return this.imageListRecycle.Images[PARTIALLY_SOLVED];}}
      public Image RECYCLE_CTRL_SOLVE_FAILED_IMG {get {return this.imageListRecycle.Images[SOLVE_FAILED];}}
      public Image RECYCLE_CTRL_SOLVED_IMG {get {return this.imageListRecycle.Images[SOLVED];}}

      private System.Windows.Forms.ImageList imageListGasLeft;
      private System.Windows.Forms.ImageList imageListGasUp;
      private System.Windows.Forms.ImageList imageListGasRight;
      private System.Windows.Forms.ImageList imageListGasDown;
      private System.Windows.Forms.ImageList imageListProcessLeft;
      private System.Windows.Forms.ImageList imageListProcessUp;
      private System.Windows.Forms.ImageList imageListProcessRight;
      private System.Windows.Forms.ImageList imageListProcessDown;
      private System.Windows.Forms.ImageList imageListSolveIcons;
      private System.Windows.Forms.ImageList imageListHeatExchanger;
      private System.Windows.Forms.ImageList imageListFan;
      private System.Windows.Forms.ImageList imageListValve;
      private System.Windows.Forms.ImageList imageListBagFilter;
      private System.Windows.Forms.ImageList imageListAirFilter;
      private System.Windows.Forms.ImageList imageListCompressor;
      private System.Windows.Forms.ImageList imageListHeater;
      private System.Windows.Forms.ImageList imageListCooler;
      private System.Windows.Forms.ImageList imageListPump;
      private System.Windows.Forms.ImageList imageListCyclone;
      private System.Windows.Forms.ImageList imageListMixer;
      private System.Windows.Forms.ImageList imageListTee;
      private System.Windows.Forms.ImageList imageListFlashTank;
      private System.Windows.Forms.ImageList imageListRecycle;
      private System.Windows.Forms.ImageList imageListElectrostaticPrecipitator;
      private System.Windows.Forms.ImageList imageListEjector;
      private System.Windows.Forms.ImageList imageListWetScrubber;
      private System.Windows.Forms.ImageList imageListMaterialSolidLeft;
      private System.Windows.Forms.ImageList imageListMaterialSolidUp;
      private System.Windows.Forms.ImageList imageListMaterialSolidRight;
      private System.Windows.Forms.ImageList imageListMaterialSolidDown;
      private System.Windows.Forms.ImageList imageListMaterialLiquidLeft;
      private System.Windows.Forms.ImageList imageListMaterialLiquidUp;
      private System.Windows.Forms.ImageList imageListMaterialLiquidRight;
      private System.Windows.Forms.ImageList imageListMaterialLiquidDown;
      private System.Windows.Forms.ImageList imageListDryerLiquid;
      private System.Windows.Forms.ImageList imageListDryerSolid;
      private System.ComponentModel.IContainer components;

		public UIForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.notSolvedIcon = this.GetIconFromImage(this.imageListSolveIcons.Images[NOT_SOLVED]);
         this.partiallySolvedIcon = this.GetIconFromImage(this.imageListSolveIcons.Images[PARTIALLY_SOLVED]);
         this.solveFailedIcon = this.GetIconFromImage(this.imageListSolveIcons.Images[SOLVE_FAILED]);
         this.solvedIcon = this.GetIconFromImage(this.imageListSolveIcons.Images[SOLVED]);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.components = new System.ComponentModel.Container();
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIForm));
         this.imageListGasLeft = new System.Windows.Forms.ImageList(this.components);
         this.imageListGasUp = new System.Windows.Forms.ImageList(this.components);
         this.imageListGasRight = new System.Windows.Forms.ImageList(this.components);
         this.imageListGasDown = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialSolidLeft = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialSolidUp = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialSolidRight = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialSolidDown = new System.Windows.Forms.ImageList(this.components);
         this.imageListProcessLeft = new System.Windows.Forms.ImageList(this.components);
         this.imageListProcessUp = new System.Windows.Forms.ImageList(this.components);
         this.imageListProcessRight = new System.Windows.Forms.ImageList(this.components);
         this.imageListProcessDown = new System.Windows.Forms.ImageList(this.components);
         this.imageListSolveIcons = new System.Windows.Forms.ImageList(this.components);
         this.imageListDryerLiquid = new System.Windows.Forms.ImageList(this.components);
         this.imageListDryerSolid = new System.Windows.Forms.ImageList(this.components);
         this.imageListHeatExchanger = new System.Windows.Forms.ImageList(this.components);
         this.imageListFan = new System.Windows.Forms.ImageList(this.components);
         this.imageListValve = new System.Windows.Forms.ImageList(this.components);
         this.imageListBagFilter = new System.Windows.Forms.ImageList(this.components);
         this.imageListAirFilter = new System.Windows.Forms.ImageList(this.components);
         this.imageListCompressor = new System.Windows.Forms.ImageList(this.components);
         this.imageListHeater = new System.Windows.Forms.ImageList(this.components);
         this.imageListCooler = new System.Windows.Forms.ImageList(this.components);
         this.imageListPump = new System.Windows.Forms.ImageList(this.components);
         this.imageListCyclone = new System.Windows.Forms.ImageList(this.components);
         this.imageListMixer = new System.Windows.Forms.ImageList(this.components);
         this.imageListTee = new System.Windows.Forms.ImageList(this.components);
         this.imageListFlashTank = new System.Windows.Forms.ImageList(this.components);
         this.imageListRecycle = new System.Windows.Forms.ImageList(this.components);
         this.imageListElectrostaticPrecipitator = new System.Windows.Forms.ImageList(this.components);
         this.imageListEjector = new System.Windows.Forms.ImageList(this.components);
         this.imageListWetScrubber = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialLiquidLeft = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialLiquidUp = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialLiquidRight = new System.Windows.Forms.ImageList(this.components);
         this.imageListMaterialLiquidDown = new System.Windows.Forms.ImageList(this.components);
         // 
         // imageListGasLeft
         // 
         this.imageListGasLeft.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListGasLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasLeft.ImageStream")));
         this.imageListGasLeft.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListGasUp
         // 
         this.imageListGasUp.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListGasUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasUp.ImageStream")));
         this.imageListGasUp.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListGasRight
         // 
         this.imageListGasRight.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListGasRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasRight.ImageStream")));
         this.imageListGasRight.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListGasDown
         // 
         this.imageListGasDown.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListGasDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasDown.ImageStream")));
         this.imageListGasDown.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialSolidLeft
         // 
         this.imageListMaterialSolidLeft.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialSolidLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidLeft.ImageStream")));
         this.imageListMaterialSolidLeft.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialSolidUp
         // 
         this.imageListMaterialSolidUp.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialSolidUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidUp.ImageStream")));
         this.imageListMaterialSolidUp.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialSolidRight
         // 
         this.imageListMaterialSolidRight.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialSolidRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidRight.ImageStream")));
         this.imageListMaterialSolidRight.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialSolidDown
         // 
         this.imageListMaterialSolidDown.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialSolidDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidDown.ImageStream")));
         this.imageListMaterialSolidDown.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListProcessLeft
         // 
         this.imageListProcessLeft.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListProcessLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessLeft.ImageStream")));
         this.imageListProcessLeft.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListProcessUp
         // 
         this.imageListProcessUp.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListProcessUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessUp.ImageStream")));
         this.imageListProcessUp.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListProcessRight
         // 
         this.imageListProcessRight.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListProcessRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessRight.ImageStream")));
         this.imageListProcessRight.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListProcessDown
         // 
         this.imageListProcessDown.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListProcessDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessDown.ImageStream")));
         this.imageListProcessDown.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListSolveIcons
         // 
         this.imageListSolveIcons.ImageSize = new System.Drawing.Size(16, 16);
         this.imageListSolveIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSolveIcons.ImageStream")));
         this.imageListSolveIcons.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListDryerLiquid
         // 
         this.imageListDryerLiquid.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListDryerLiquid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDryerLiquid.ImageStream")));
         this.imageListDryerLiquid.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListDryerSolid
         // 
         this.imageListDryerSolid.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListDryerSolid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDryerSolid.ImageStream")));
         this.imageListDryerSolid.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListHeatExchanger
         // 
         this.imageListHeatExchanger.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListHeatExchanger.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHeatExchanger.ImageStream")));
         this.imageListHeatExchanger.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListFan
         // 
         this.imageListFan.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListFan.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFan.ImageStream")));
         this.imageListFan.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListValve
         // 
         this.imageListValve.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListValve.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListValve.ImageStream")));
         this.imageListValve.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListBagFilter
         // 
         this.imageListBagFilter.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListBagFilter.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListBagFilter.ImageStream")));
         this.imageListBagFilter.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListAirFilter
         // 
         this.imageListAirFilter.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListAirFilter.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListAirFilter.ImageStream")));
         this.imageListAirFilter.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListCompressor
         // 
         this.imageListCompressor.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListCompressor.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCompressor.ImageStream")));
         this.imageListCompressor.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListHeater
         // 
         this.imageListHeater.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListHeater.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHeater.ImageStream")));
         this.imageListHeater.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListCooler
         // 
         this.imageListCooler.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListCooler.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCooler.ImageStream")));
         this.imageListCooler.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListPump
         // 
         this.imageListPump.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListPump.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPump.ImageStream")));
         this.imageListPump.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListCyclone
         // 
         this.imageListCyclone.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListCyclone.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCyclone.ImageStream")));
         this.imageListCyclone.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMixer
         // 
         this.imageListMixer.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListMixer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMixer.ImageStream")));
         this.imageListMixer.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListTee
         // 
         this.imageListTee.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListTee.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTee.ImageStream")));
         this.imageListTee.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListFlashTank
         // 
         this.imageListFlashTank.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListFlashTank.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFlashTank.ImageStream")));
         this.imageListFlashTank.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListRecycle
         // 
         this.imageListRecycle.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListRecycle.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListRecycle.ImageStream")));
         this.imageListRecycle.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListElectrostaticPrecipitator
         // 
         this.imageListElectrostaticPrecipitator.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListElectrostaticPrecipitator.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListElectrostaticPrecipitator.ImageStream")));
         this.imageListElectrostaticPrecipitator.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListEjector
         // 
         this.imageListEjector.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListEjector.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListEjector.ImageStream")));
         this.imageListEjector.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListWetScrubber
         // 
         this.imageListWetScrubber.ImageSize = new System.Drawing.Size(40, 40);
         this.imageListWetScrubber.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListWetScrubber.ImageStream")));
         this.imageListWetScrubber.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialLiquidLeft
         // 
         this.imageListMaterialLiquidLeft.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialLiquidLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidLeft.ImageStream")));
         this.imageListMaterialLiquidLeft.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialLiquidUp
         // 
         this.imageListMaterialLiquidUp.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialLiquidUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidUp.ImageStream")));
         this.imageListMaterialLiquidUp.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialLiquidRight
         // 
         this.imageListMaterialLiquidRight.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialLiquidRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidRight.ImageStream")));
         this.imageListMaterialLiquidRight.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // imageListMaterialLiquidDown
         // 
         this.imageListMaterialLiquidDown.ImageSize = new System.Drawing.Size(28, 28);
         this.imageListMaterialLiquidDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidDown.ImageStream")));
         this.imageListMaterialLiquidDown.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // UIForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(292, 272);
         this.Name = "UIForm";
         this.Text = "UIForm";

      }
		#endregion

      private Icon GetIconFromImage(Image image)
      {
         Bitmap bitmap = new Bitmap(image);
         return Icon.FromHandle(bitmap.GetHicon());
      }
	}
}
