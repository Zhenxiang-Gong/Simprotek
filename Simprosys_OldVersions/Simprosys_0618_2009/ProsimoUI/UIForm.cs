using System.Drawing;
using System.Windows.Forms;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for UIForm.
	/// </summary>
   public class UIForm : System.Windows.Forms.Form
   {
      private const int NOT_SOLVED = 0;
      private const int SOLVED_WITH_WARNING = 1;
      private const int SOLVE_FAILED = 2;
      private const int SOLVED = 3;

      // solve icons
      private Icon notSolvedIcon;
      public Icon NOT_SOLVED_ICON {get{return this.notSolvedIcon;}}

      private Icon solvedWithWarningIcon;
      public Icon SOLVED_WITH_WARNING_ICON {get {return this.solvedWithWarningIcon;}}
      
      private Icon solveFailedIcon;
      public Icon SOLVE_FAILED_ICON {get {return this.solveFailedIcon;}}
      
      private Icon solvedIcon;
      public Icon SOLVED_ICON {get {return this.solvedIcon;}}

      // gas
      public Image GAS_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListGasDown.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_SOLVED_WITH_WARNING_DOWN_IMG {get {return this.imageListGasDown.Images[SOLVED_WITH_WARNING];}}
      public Image GAS_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListGasDown.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_DOWN_IMG {get {return this.imageListGasDown.Images[SOLVED];}}
      
      public Image GAS_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListGasLeft.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_SOLVED_WITH_WARNING_LEFT_IMG {get {return this.imageListGasLeft.Images[SOLVED_WITH_WARNING];}}
      public Image GAS_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListGasLeft.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_LEFT_IMG {get {return this.imageListGasLeft.Images[SOLVED];}}

      public Image GAS_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListGasUp.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_SOLVED_WITH_WARNING_UP_IMG {get {return this.imageListGasUp.Images[SOLVED_WITH_WARNING];}}
      public Image GAS_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListGasUp.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_UP_IMG {get {return this.imageListGasUp.Images[SOLVED];}}

      public Image GAS_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListGasRight.Images[NOT_SOLVED];}}
      public Image GAS_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG {get {return this.imageListGasRight.Images[SOLVED_WITH_WARNING];}}
      public Image GAS_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListGasRight.Images[SOLVE_FAILED];}}
      public Image GAS_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListGasRight.Images[SOLVED];}}

      // material solid
      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_DOWN_IMG {get {return this.imageListMaterialSolidDown.Images[SOLVED];}}

      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_LEFT_IMG {get {return this.imageListMaterialSolidLeft.Images[SOLVED];}}
 
      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListMaterialSolidUp.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_UP_IMG {get {return this.imageListMaterialSolidUp.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListMaterialSolidUp.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_UP_IMG {get {return this.imageListMaterialSolidUp.Images[SOLVED];}}

      public Image MATERIAL_SOLID_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[NOT_SOLVED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_SOLID_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[SOLVE_FAILED];}}
      public Image MATERIAL_SOLID_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListMaterialSolidRight.Images[SOLVED];}}

      // material liquid
      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_DOWN_IMG {get {return this.imageListMaterialLiquidDown.Images[SOLVED];}}

      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_LEFT_IMG {get {return this.imageListMaterialLiquidLeft.Images[SOLVED];}}
 
      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_UP_IMG {get {return this.imageListMaterialLiquidUp.Images[SOLVED];}}

      public Image MATERIAL_LIQUID_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[NOT_SOLVED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[SOLVED_WITH_WARNING];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[SOLVE_FAILED];}}
      public Image MATERIAL_LIQUID_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListMaterialLiquidRight.Images[SOLVED];}}

      // process
      public Image PROCESS_CTRL_NOT_SOLVED_DOWN_IMG {get {return this.imageListProcessDown.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_SOLVED_WITH_WARNING_DOWN_IMG {get {return this.imageListProcessDown.Images[SOLVED_WITH_WARNING];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_DOWN_IMG {get {return this.imageListProcessDown.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_DOWN_IMG {get {return this.imageListProcessDown.Images[SOLVED];}}

      public Image PROCESS_CTRL_NOT_SOLVED_LEFT_IMG {get {return this.imageListProcessLeft.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_SOLVED_WITH_WARNING_LEFT_IMG {get {return this.imageListProcessLeft.Images[SOLVED_WITH_WARNING];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_LEFT_IMG {get {return this.imageListProcessLeft.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_LEFT_IMG {get {return this.imageListProcessLeft.Images[SOLVED];}}

      public Image PROCESS_CTRL_NOT_SOLVED_UP_IMG {get {return this.imageListProcessUp.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_SOLVED_WITH_WARNING_UP_IMG {get {return this.imageListProcessUp.Images[SOLVED_WITH_WARNING];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_UP_IMG {get {return this.imageListProcessUp.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_UP_IMG {get {return this.imageListProcessUp.Images[SOLVED];}}

      public Image PROCESS_CTRL_NOT_SOLVED_RIGHT_IMG {get {return this.imageListProcessRight.Images[NOT_SOLVED];}}
      public Image PROCESS_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG {get {return this.imageListProcessRight.Images[SOLVED_WITH_WARNING];}}
      public Image PROCESS_CTRL_SOLVE_FAILED_RIGHT_IMG {get {return this.imageListProcessRight.Images[SOLVE_FAILED];}}
      public Image PROCESS_CTRL_SOLVED_RIGHT_IMG {get {return this.imageListProcessRight.Images[SOLVED];}}

      // continuous dryer
      public Image DRYER_LIQUID_CTRL_NOT_SOLVED_IMG {get {return this.imageListDryerLiquid.Images[NOT_SOLVED];}}
      public Image DRYER_LIQUID_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListDryerLiquid.Images[SOLVED_WITH_WARNING];}}
      public Image DRYER_LIQUID_CTRL_SOLVE_FAILED_IMG {get {return this.imageListDryerLiquid.Images[SOLVE_FAILED];}}
      public Image DRYER_LIQUID_CTRL_SOLVED_IMG {get {return this.imageListDryerLiquid.Images[SOLVED];}}

      // batch dryer
      public Image DRYER_SOLID_CTRL_NOT_SOLVED_IMG {get {return this.imageListDryerSolid.Images[NOT_SOLVED];}}
      public Image DRYER_SOLID_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListDryerSolid.Images[SOLVED_WITH_WARNING];}}
      public Image DRYER_SOLID_CTRL_SOLVE_FAILED_IMG {get {return this.imageListDryerSolid.Images[SOLVE_FAILED];}}
      public Image DRYER_SOLID_CTRL_SOLVED_IMG {get {return this.imageListDryerSolid.Images[SOLVED];}}

      // heat exchanger
      public Image HEATEXCHANGER_CTRL_NOT_SOLVED_IMG {get {return this.imageListHeatExchanger.Images[NOT_SOLVED];}}
      public Image HEATEXCHANGER_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListHeatExchanger.Images[SOLVED_WITH_WARNING];}}
      public Image HEATEXCHANGER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListHeatExchanger.Images[SOLVE_FAILED];}}
      public Image HEATEXCHANGER_CTRL_SOLVED_IMG {get {return this.imageListHeatExchanger.Images[SOLVED];}}

      // fan
      public Image FAN_CTRL_NOT_SOLVED_IMG {get {return this.imageListFan.Images[NOT_SOLVED];}}
      public Image FAN_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListFan.Images[SOLVED_WITH_WARNING];}}
      public Image FAN_CTRL_SOLVE_FAILED_IMG {get {return this.imageListFan.Images[SOLVE_FAILED];}}
      public Image FAN_CTRL_SOLVED_IMG {get {return this.imageListFan.Images[SOLVED];}}

      // valve
      public Image VALVE_CTRL_NOT_SOLVED_IMG {get {return this.imageListValve.Images[NOT_SOLVED];}}
      public Image VALVE_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListValve.Images[SOLVED_WITH_WARNING];}}
      public Image VALVE_CTRL_SOLVE_FAILED_IMG {get {return this.imageListValve.Images[SOLVE_FAILED];}}
      public Image VALVE_CTRL_SOLVED_IMG {get {return this.imageListValve.Images[SOLVED];}}

      // bag filter
      public Image BAGFILTER_CTRL_NOT_SOLVED_IMG {get {return this.imageListBagFilter.Images[NOT_SOLVED];}}
      public Image BAGFILTER_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListBagFilter.Images[SOLVED_WITH_WARNING];}}
      public Image BAGFILTER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListBagFilter.Images[SOLVE_FAILED];}}
      public Image BAGFILTER_CTRL_SOLVED_IMG {get {return this.imageListBagFilter.Images[SOLVED];}}

      // air filter
      public Image AIRFILTER_CTRL_NOT_SOLVED_IMG {get {return this.imageListAirFilter.Images[NOT_SOLVED];}}
      public Image AIRFILTER_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListAirFilter.Images[SOLVED_WITH_WARNING];}}
      public Image AIRFILTER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListAirFilter.Images[SOLVE_FAILED];}}
      public Image AIRFILTER_CTRL_SOLVED_IMG {get {return this.imageListAirFilter.Images[SOLVED];}}

      // compressor
      public Image COMPRESSOR_CTRL_NOT_SOLVED_IMG {get {return this.imageListCompressor.Images[NOT_SOLVED];}}
      public Image COMPRESSOR_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListCompressor.Images[SOLVED_WITH_WARNING];}}
      public Image COMPRESSOR_CTRL_SOLVE_FAILED_IMG {get {return this.imageListCompressor.Images[SOLVE_FAILED];}}
      public Image COMPRESSOR_CTRL_SOLVED_IMG {get {return this.imageListCompressor.Images[SOLVED];}}

      // heater
      public Image HEATER_CTRL_NOT_SOLVED_IMG {get {return this.imageListHeater.Images[NOT_SOLVED];}}
      public Image HEATER_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListHeater.Images[SOLVED_WITH_WARNING];}}
      public Image HEATER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListHeater.Images[SOLVE_FAILED];}}
      public Image HEATER_CTRL_SOLVED_IMG {get {return this.imageListHeater.Images[SOLVED];}}

      // cooler
      public Image COOLER_CTRL_NOT_SOLVED_IMG {get {return this.imageListCooler.Images[NOT_SOLVED];}}
      public Image COOLER_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListCooler.Images[SOLVED_WITH_WARNING];}}
      public Image COOLER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListCooler.Images[SOLVE_FAILED];}}
      public Image COOLER_CTRL_SOLVED_IMG {get {return this.imageListCooler.Images[SOLVED];}}

      // electrostatic precipitator
      public Image ELECTROSTATICPRECIPITATOR_CTRL_NOT_SOLVED_IMG {get {return this.imageListElectrostaticPrecipitator.Images[NOT_SOLVED];}}
      public Image ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListElectrostaticPrecipitator.Images[SOLVED_WITH_WARNING];}}
      public Image ELECTROSTATICPRECIPITATOR_CTRL_SOLVE_FAILED_IMG {get {return this.imageListElectrostaticPrecipitator.Images[SOLVE_FAILED];}}
      public Image ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_IMG {get {return this.imageListElectrostaticPrecipitator.Images[SOLVED];}}

      // pump
      public Image PUMP_CTRL_NOT_SOLVED_IMG {get {return this.imageListPump.Images[NOT_SOLVED];}}
      public Image PUMP_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListPump.Images[SOLVED_WITH_WARNING];}}
      public Image PUMP_CTRL_SOLVE_FAILED_IMG {get {return this.imageListPump.Images[SOLVE_FAILED];}}
      public Image PUMP_CTRL_SOLVED_IMG {get {return this.imageListPump.Images[SOLVED];}}

      // cyclone
      public Image CYCLONE_CTRL_NOT_SOLVED_IMG {get {return this.imageListCyclone.Images[NOT_SOLVED];}}
      public Image CYCLONE_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListCyclone.Images[SOLVED_WITH_WARNING];}}
      public Image CYCLONE_CTRL_SOLVE_FAILED_IMG {get {return this.imageListCyclone.Images[SOLVE_FAILED];}}
      public Image CYCLONE_CTRL_SOLVED_IMG {get {return this.imageListCyclone.Images[SOLVED];}}

      // ejector
      public Image EJECTOR_CTRL_NOT_SOLVED_IMG {get {return this.imageListEjector.Images[NOT_SOLVED];}}
      public Image EJECTOR_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListEjector.Images[SOLVED_WITH_WARNING];}}
      public Image EJECTOR_CTRL_SOLVE_FAILED_IMG {get {return this.imageListEjector.Images[SOLVE_FAILED];}}
      public Image EJECTOR_CTRL_SOLVED_IMG {get {return this.imageListEjector.Images[SOLVED];}}

      // wet scrubber
      public Image WETSCRUBBER_CTRL_NOT_SOLVED_IMG {get {return this.imageListWetScrubber.Images[NOT_SOLVED];}}
      public Image WETSCRUBBER_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListWetScrubber.Images[SOLVED_WITH_WARNING];}}
      public Image WETSCRUBBER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListWetScrubber.Images[SOLVE_FAILED];}}
      public Image WETSCRUBBER_CTRL_SOLVED_IMG {get {return this.imageListWetScrubber.Images[SOLVED];}}

      // scrubber condenser
      public Image SCRUBBERCONDENSER_CTRL_NOT_SOLVED_IMG { get { return this.imageListScrubberCondenser.Images[NOT_SOLVED]; } }
      public Image SCRUBBERCONDENSER_CTRL_SOLVED_WITH_WARNING_IMG { get { return this.imageListScrubberCondenser.Images[SOLVED_WITH_WARNING]; } }
      public Image SCRUBBERCONDENSER_CTRL_SOLVE_FAILED_IMG { get { return this.imageListScrubberCondenser.Images[SOLVE_FAILED]; } }
      public Image SCRUBBERCONDENSER_CTRL_SOLVED_IMG { get { return this.imageListScrubberCondenser.Images[SOLVED]; } }

      // mixer
      public Image MIXER_CTRL_NOT_SOLVED_IMG {get {return this.imageListMixer.Images[NOT_SOLVED];}}
      public Image MIXER_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListMixer.Images[SOLVED_WITH_WARNING];}}
      public Image MIXER_CTRL_SOLVE_FAILED_IMG {get {return this.imageListMixer.Images[SOLVE_FAILED];}}
      public Image MIXER_CTRL_SOLVED_IMG {get {return this.imageListMixer.Images[SOLVED];}}

      // tee
      public Image TEE_CTRL_NOT_SOLVED_IMG {get {return this.imageListTee.Images[NOT_SOLVED];}}
      public Image TEE_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListTee.Images[SOLVED_WITH_WARNING];}}
      public Image TEE_CTRL_SOLVE_FAILED_IMG {get {return this.imageListTee.Images[SOLVE_FAILED];}}
      public Image TEE_CTRL_SOLVED_IMG {get {return this.imageListTee.Images[SOLVED];}}

      // flash tank
      public Image FLASHTANK_CTRL_NOT_SOLVED_IMG {get {return this.imageListFlashTank.Images[NOT_SOLVED];}}
      public Image FLASHTANK_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListFlashTank.Images[SOLVED_WITH_WARNING];}}
      public Image FLASHTANK_CTRL_SOLVE_FAILED_IMG {get {return this.imageListFlashTank.Images[SOLVE_FAILED];}}
      public Image FLASHTANK_CTRL_SOLVED_IMG {get {return this.imageListFlashTank.Images[SOLVED];}}

      // recycle
      public Image RECYCLE_CTRL_NOT_SOLVED_IMG {get {return this.imageListRecycle.Images[NOT_SOLVED];}}
      public Image RECYCLE_CTRL_SOLVED_WITH_WARNING_IMG {get {return this.imageListRecycle.Images[SOLVED_WITH_WARNING];}}
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
      private ImageList imageListScrubberCondenser;
      private System.ComponentModel.IContainer components;

		public UIForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.notSolvedIcon = this.GetIconFromImage(this.imageListSolveIcons.Images[NOT_SOLVED]);
         this.solvedWithWarningIcon = this.GetIconFromImage(this.imageListSolveIcons.Images[SOLVED_WITH_WARNING]);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
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
         this.imageListScrubberCondenser = new System.Windows.Forms.ImageList(this.components);
         this.SuspendLayout();
         // 
         // imageListGasLeft
         // 
         this.imageListGasLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasLeft.ImageStream")));
         this.imageListGasLeft.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListGasLeft.Images.SetKeyName(0, "");
         this.imageListGasLeft.Images.SetKeyName(1, "");
         this.imageListGasLeft.Images.SetKeyName(2, "");
         this.imageListGasLeft.Images.SetKeyName(3, "");
         // 
         // imageListGasUp
         // 
         this.imageListGasUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasUp.ImageStream")));
         this.imageListGasUp.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListGasUp.Images.SetKeyName(0, "");
         this.imageListGasUp.Images.SetKeyName(1, "");
         this.imageListGasUp.Images.SetKeyName(2, "");
         this.imageListGasUp.Images.SetKeyName(3, "");
         // 
         // imageListGasRight
         // 
         this.imageListGasRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasRight.ImageStream")));
         this.imageListGasRight.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListGasRight.Images.SetKeyName(0, "");
         this.imageListGasRight.Images.SetKeyName(1, "");
         this.imageListGasRight.Images.SetKeyName(2, "");
         this.imageListGasRight.Images.SetKeyName(3, "");
         // 
         // imageListGasDown
         // 
         this.imageListGasDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGasDown.ImageStream")));
         this.imageListGasDown.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListGasDown.Images.SetKeyName(0, "");
         this.imageListGasDown.Images.SetKeyName(1, "");
         this.imageListGasDown.Images.SetKeyName(2, "");
         this.imageListGasDown.Images.SetKeyName(3, "");
         // 
         // imageListMaterialSolidLeft
         // 
         this.imageListMaterialSolidLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidLeft.ImageStream")));
         this.imageListMaterialSolidLeft.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialSolidLeft.Images.SetKeyName(0, "");
         this.imageListMaterialSolidLeft.Images.SetKeyName(1, "");
         this.imageListMaterialSolidLeft.Images.SetKeyName(2, "");
         this.imageListMaterialSolidLeft.Images.SetKeyName(3, "");
         // 
         // imageListMaterialSolidUp
         // 
         this.imageListMaterialSolidUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidUp.ImageStream")));
         this.imageListMaterialSolidUp.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialSolidUp.Images.SetKeyName(0, "");
         this.imageListMaterialSolidUp.Images.SetKeyName(1, "");
         this.imageListMaterialSolidUp.Images.SetKeyName(2, "");
         this.imageListMaterialSolidUp.Images.SetKeyName(3, "");
         // 
         // imageListMaterialSolidRight
         // 
         this.imageListMaterialSolidRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidRight.ImageStream")));
         this.imageListMaterialSolidRight.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialSolidRight.Images.SetKeyName(0, "");
         this.imageListMaterialSolidRight.Images.SetKeyName(1, "");
         this.imageListMaterialSolidRight.Images.SetKeyName(2, "");
         this.imageListMaterialSolidRight.Images.SetKeyName(3, "");
         // 
         // imageListMaterialSolidDown
         // 
         this.imageListMaterialSolidDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialSolidDown.ImageStream")));
         this.imageListMaterialSolidDown.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialSolidDown.Images.SetKeyName(0, "");
         this.imageListMaterialSolidDown.Images.SetKeyName(1, "");
         this.imageListMaterialSolidDown.Images.SetKeyName(2, "");
         this.imageListMaterialSolidDown.Images.SetKeyName(3, "");
         // 
         // imageListProcessLeft
         // 
         this.imageListProcessLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessLeft.ImageStream")));
         this.imageListProcessLeft.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListProcessLeft.Images.SetKeyName(0, "");
         this.imageListProcessLeft.Images.SetKeyName(1, "");
         this.imageListProcessLeft.Images.SetKeyName(2, "");
         this.imageListProcessLeft.Images.SetKeyName(3, "");
         // 
         // imageListProcessUp
         // 
         this.imageListProcessUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessUp.ImageStream")));
         this.imageListProcessUp.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListProcessUp.Images.SetKeyName(0, "");
         this.imageListProcessUp.Images.SetKeyName(1, "");
         this.imageListProcessUp.Images.SetKeyName(2, "");
         this.imageListProcessUp.Images.SetKeyName(3, "");
         // 
         // imageListProcessRight
         // 
         this.imageListProcessRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessRight.ImageStream")));
         this.imageListProcessRight.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListProcessRight.Images.SetKeyName(0, "");
         this.imageListProcessRight.Images.SetKeyName(1, "");
         this.imageListProcessRight.Images.SetKeyName(2, "");
         this.imageListProcessRight.Images.SetKeyName(3, "");
         // 
         // imageListProcessDown
         // 
         this.imageListProcessDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcessDown.ImageStream")));
         this.imageListProcessDown.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListProcessDown.Images.SetKeyName(0, "");
         this.imageListProcessDown.Images.SetKeyName(1, "");
         this.imageListProcessDown.Images.SetKeyName(2, "");
         this.imageListProcessDown.Images.SetKeyName(3, "");
         // 
         // imageListSolveIcons
         // 
         this.imageListSolveIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSolveIcons.ImageStream")));
         this.imageListSolveIcons.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListSolveIcons.Images.SetKeyName(0, "");
         this.imageListSolveIcons.Images.SetKeyName(1, "");
         this.imageListSolveIcons.Images.SetKeyName(2, "");
         this.imageListSolveIcons.Images.SetKeyName(3, "");
         // 
         // imageListDryerLiquid
         // 
         this.imageListDryerLiquid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDryerLiquid.ImageStream")));
         this.imageListDryerLiquid.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListDryerLiquid.Images.SetKeyName(0, "");
         this.imageListDryerLiquid.Images.SetKeyName(1, "");
         this.imageListDryerLiquid.Images.SetKeyName(2, "");
         this.imageListDryerLiquid.Images.SetKeyName(3, "");
         // 
         // imageListDryerSolid
         // 
         this.imageListDryerSolid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDryerSolid.ImageStream")));
         this.imageListDryerSolid.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListDryerSolid.Images.SetKeyName(0, "");
         this.imageListDryerSolid.Images.SetKeyName(1, "");
         this.imageListDryerSolid.Images.SetKeyName(2, "");
         this.imageListDryerSolid.Images.SetKeyName(3, "");
         // 
         // imageListHeatExchanger
         // 
         this.imageListHeatExchanger.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHeatExchanger.ImageStream")));
         this.imageListHeatExchanger.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListHeatExchanger.Images.SetKeyName(0, "");
         this.imageListHeatExchanger.Images.SetKeyName(1, "");
         this.imageListHeatExchanger.Images.SetKeyName(2, "");
         this.imageListHeatExchanger.Images.SetKeyName(3, "");
         // 
         // imageListFan
         // 
         this.imageListFan.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFan.ImageStream")));
         this.imageListFan.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListFan.Images.SetKeyName(0, "");
         this.imageListFan.Images.SetKeyName(1, "");
         this.imageListFan.Images.SetKeyName(2, "");
         this.imageListFan.Images.SetKeyName(3, "");
         // 
         // imageListValve
         // 
         this.imageListValve.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListValve.ImageStream")));
         this.imageListValve.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListValve.Images.SetKeyName(0, "");
         this.imageListValve.Images.SetKeyName(1, "");
         this.imageListValve.Images.SetKeyName(2, "");
         this.imageListValve.Images.SetKeyName(3, "");
         // 
         // imageListBagFilter
         // 
         this.imageListBagFilter.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListBagFilter.ImageStream")));
         this.imageListBagFilter.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListBagFilter.Images.SetKeyName(0, "");
         this.imageListBagFilter.Images.SetKeyName(1, "");
         this.imageListBagFilter.Images.SetKeyName(2, "");
         this.imageListBagFilter.Images.SetKeyName(3, "");
         // 
         // imageListAirFilter
         // 
         this.imageListAirFilter.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListAirFilter.ImageStream")));
         this.imageListAirFilter.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListAirFilter.Images.SetKeyName(0, "");
         this.imageListAirFilter.Images.SetKeyName(1, "");
         this.imageListAirFilter.Images.SetKeyName(2, "");
         this.imageListAirFilter.Images.SetKeyName(3, "");
         // 
         // imageListCompressor
         // 
         this.imageListCompressor.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCompressor.ImageStream")));
         this.imageListCompressor.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListCompressor.Images.SetKeyName(0, "");
         this.imageListCompressor.Images.SetKeyName(1, "");
         this.imageListCompressor.Images.SetKeyName(2, "");
         this.imageListCompressor.Images.SetKeyName(3, "");
         // 
         // imageListHeater
         // 
         this.imageListHeater.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHeater.ImageStream")));
         this.imageListHeater.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListHeater.Images.SetKeyName(0, "");
         this.imageListHeater.Images.SetKeyName(1, "");
         this.imageListHeater.Images.SetKeyName(2, "");
         this.imageListHeater.Images.SetKeyName(3, "");
         // 
         // imageListCooler
         // 
         this.imageListCooler.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCooler.ImageStream")));
         this.imageListCooler.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListCooler.Images.SetKeyName(0, "");
         this.imageListCooler.Images.SetKeyName(1, "");
         this.imageListCooler.Images.SetKeyName(2, "");
         this.imageListCooler.Images.SetKeyName(3, "");
         // 
         // imageListPump
         // 
         this.imageListPump.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPump.ImageStream")));
         this.imageListPump.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListPump.Images.SetKeyName(0, "");
         this.imageListPump.Images.SetKeyName(1, "");
         this.imageListPump.Images.SetKeyName(2, "");
         this.imageListPump.Images.SetKeyName(3, "");
         // 
         // imageListCyclone
         // 
         this.imageListCyclone.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCyclone.ImageStream")));
         this.imageListCyclone.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListCyclone.Images.SetKeyName(0, "");
         this.imageListCyclone.Images.SetKeyName(1, "");
         this.imageListCyclone.Images.SetKeyName(2, "");
         this.imageListCyclone.Images.SetKeyName(3, "");
         // 
         // imageListMixer
         // 
         this.imageListMixer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMixer.ImageStream")));
         this.imageListMixer.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMixer.Images.SetKeyName(0, "");
         this.imageListMixer.Images.SetKeyName(1, "");
         this.imageListMixer.Images.SetKeyName(2, "");
         this.imageListMixer.Images.SetKeyName(3, "");
         // 
         // imageListTee
         // 
         this.imageListTee.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTee.ImageStream")));
         this.imageListTee.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListTee.Images.SetKeyName(0, "");
         this.imageListTee.Images.SetKeyName(1, "");
         this.imageListTee.Images.SetKeyName(2, "");
         this.imageListTee.Images.SetKeyName(3, "");
         // 
         // imageListFlashTank
         // 
         this.imageListFlashTank.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFlashTank.ImageStream")));
         this.imageListFlashTank.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListFlashTank.Images.SetKeyName(0, "");
         this.imageListFlashTank.Images.SetKeyName(1, "");
         this.imageListFlashTank.Images.SetKeyName(2, "");
         this.imageListFlashTank.Images.SetKeyName(3, "");
         // 
         // imageListRecycle
         // 
         this.imageListRecycle.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListRecycle.ImageStream")));
         this.imageListRecycle.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListRecycle.Images.SetKeyName(0, "");
         this.imageListRecycle.Images.SetKeyName(1, "");
         this.imageListRecycle.Images.SetKeyName(2, "");
         this.imageListRecycle.Images.SetKeyName(3, "");
         // 
         // imageListElectrostaticPrecipitator
         // 
         this.imageListElectrostaticPrecipitator.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListElectrostaticPrecipitator.ImageStream")));
         this.imageListElectrostaticPrecipitator.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListElectrostaticPrecipitator.Images.SetKeyName(0, "");
         this.imageListElectrostaticPrecipitator.Images.SetKeyName(1, "");
         this.imageListElectrostaticPrecipitator.Images.SetKeyName(2, "");
         this.imageListElectrostaticPrecipitator.Images.SetKeyName(3, "");
         // 
         // imageListEjector
         // 
         this.imageListEjector.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListEjector.ImageStream")));
         this.imageListEjector.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListEjector.Images.SetKeyName(0, "");
         this.imageListEjector.Images.SetKeyName(1, "");
         this.imageListEjector.Images.SetKeyName(2, "");
         this.imageListEjector.Images.SetKeyName(3, "");
         // 
         // imageListWetScrubber
         // 
         this.imageListWetScrubber.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListWetScrubber.ImageStream")));
         this.imageListWetScrubber.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListWetScrubber.Images.SetKeyName(0, "");
         this.imageListWetScrubber.Images.SetKeyName(1, "");
         this.imageListWetScrubber.Images.SetKeyName(2, "");
         this.imageListWetScrubber.Images.SetKeyName(3, "");
         // 
         // imageListMaterialLiquidLeft
         // 
         this.imageListMaterialLiquidLeft.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidLeft.ImageStream")));
         this.imageListMaterialLiquidLeft.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialLiquidLeft.Images.SetKeyName(0, "");
         this.imageListMaterialLiquidLeft.Images.SetKeyName(1, "");
         this.imageListMaterialLiquidLeft.Images.SetKeyName(2, "");
         this.imageListMaterialLiquidLeft.Images.SetKeyName(3, "");
         // 
         // imageListMaterialLiquidUp
         // 
         this.imageListMaterialLiquidUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidUp.ImageStream")));
         this.imageListMaterialLiquidUp.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialLiquidUp.Images.SetKeyName(0, "");
         this.imageListMaterialLiquidUp.Images.SetKeyName(1, "");
         this.imageListMaterialLiquidUp.Images.SetKeyName(2, "");
         this.imageListMaterialLiquidUp.Images.SetKeyName(3, "");
         // 
         // imageListMaterialLiquidRight
         // 
         this.imageListMaterialLiquidRight.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidRight.ImageStream")));
         this.imageListMaterialLiquidRight.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialLiquidRight.Images.SetKeyName(0, "");
         this.imageListMaterialLiquidRight.Images.SetKeyName(1, "");
         this.imageListMaterialLiquidRight.Images.SetKeyName(2, "");
         this.imageListMaterialLiquidRight.Images.SetKeyName(3, "");
         // 
         // imageListMaterialLiquidDown
         // 
         this.imageListMaterialLiquidDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaterialLiquidDown.ImageStream")));
         this.imageListMaterialLiquidDown.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListMaterialLiquidDown.Images.SetKeyName(0, "");
         this.imageListMaterialLiquidDown.Images.SetKeyName(1, "");
         this.imageListMaterialLiquidDown.Images.SetKeyName(2, "");
         this.imageListMaterialLiquidDown.Images.SetKeyName(3, "");
         // 
         // imageListScrubberCondenser
         // 
         this.imageListScrubberCondenser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListScrubberCondenser.ImageStream")));
         this.imageListScrubberCondenser.TransparentColor = System.Drawing.Color.Transparent;
         this.imageListScrubberCondenser.Images.SetKeyName(0, "scrubbercondenser_ctrl_not_solved.bmp");
         this.imageListScrubberCondenser.Images.SetKeyName(1, "scrubbercondenser_ctrl_solved_with_warning.bmp");
         this.imageListScrubberCondenser.Images.SetKeyName(2, "scrubbercondenser_ctrl_solve_failed.bmp");
         this.imageListScrubberCondenser.Images.SetKeyName(3, "scrubbercondenser_ctrl_solved.bmp");
         // 
         // UIForm
         // 
         this.ClientSize = new System.Drawing.Size(292, 272);
         this.Name = "UIForm";
         this.Text = "UIForm";
         this.ResumeLayout(false);

      }
		#endregion

      private Icon GetIconFromImage(Image image)
      {
         Bitmap bitmap = new Bitmap(image);
         return Icon.FromHandle(bitmap.GetHicon());
      }
	}
}
