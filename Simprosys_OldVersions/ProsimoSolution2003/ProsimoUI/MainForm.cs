using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

using Prosimo.SoftwareProtection;
using ProsimoUI.UnitSystemsUI;
using ProsimoUI.Help;
using ProsimoUI.MaterialsUI;
using ProsimoUI.Plots;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for MainWindow.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
      private const string USCAT_XML = "uscat.xml";
      private const string USCAT_BAK = "uscat.bak";

      private const string MATCAT_XML = "matcat.xml";
      private const string MATCAT_BAK = "matcat.bak";
      
      public const string APP_PREFS = "appprefs.xml";

      private System.Windows.Forms.MenuItem menuItem5;
      private System.Windows.Forms.MenuItem menuItem6;
      private System.Windows.Forms.MenuItem menuItem7;
      private System.Windows.Forms.MenuItem menuItemAddRecycle;
      private System.Windows.Forms.ToolBarButton toolBarButtonSystemEditor;
      private System.Windows.Forms.ToolBarButton toolBarButton4;
      private System.Windows.Forms.MenuItem menuItem8;
      private System.Windows.Forms.MenuItem menuItemHumidityChart;
      private System.Windows.Forms.MenuItem menuItemUnitSystems;
      private System.Windows.Forms.MenuItem menuItem11;
      private System.Windows.Forms.ToolBarButton toolBarButtonDelete;
      private System.Windows.Forms.ToolBarButton toolBarButton5;
      private System.Windows.Forms.MenuItem menuItem9;
      private System.Windows.Forms.MenuItem menuItemConnect;
      private System.Windows.Forms.MenuItem menuItemDisconnect;
      private System.Windows.Forms.MenuItem menuItem13;
      private System.Windows.Forms.MenuItem menuItem14;
      private System.Windows.Forms.MenuItem menuItemUserManual;

      private UserManualForm userManualForm;
      private Lease lease;
      private TutorialForm tutorialForm;
      private ChooseTutorialForm chooseTutorialForm;
      
      private System.Windows.Forms.MenuItem menuItemUnitConverter;
      private System.Windows.Forms.ToolBarButton toolBarButtonUnitConverter;
      private System.Windows.Forms.MenuItem menuItemAddElectrostaticPrecipitator;
      private System.Windows.Forms.MenuItem menuItemAddEjector;
      private System.Windows.Forms.MenuItem menuItemAddWetScrubber;
      private System.Windows.Forms.MenuItem menuItemFind;
      private System.Windows.Forms.MenuItem menuItemTutorials;
      private System.Windows.Forms.MenuItem menuItemMaterials;
      private System.Windows.Forms.MenuItem menuItemDryingMaterials;
      private System.Windows.Forms.MenuItem menuItemDryingGases;
      private System.Windows.Forms.MenuItem menuItemNewSystemPrefs;
      private System.Windows.Forms.MenuItem menuItem12;
      private System.Windows.Forms.MenuItem menuItemSubstances;
      private System.Windows.Forms.MenuItem menuItemPlots;
      private System.Windows.Forms.MenuItem menuItemAddLiquidMaterialStream;
      private System.Windows.Forms.MenuItem menuItemAddSolidMatDryer;
      private System.Windows.Forms.MenuItem menuItemAddLiquidMatDryer;

      private static SplashForm splash = null;
      public static void StartSplash()
      {
         splash = new SplashForm();
         Application.Run(splash);
      }

      private bool toolboxVisible;
      public bool ToolboxVisible
      {
         get {return toolboxVisible;}
         set {toolboxVisible = value;}
      }

      private Point toolboxLocation;
      public Point ToolboxLocation
      {
         get {return toolboxLocation;}
         set {toolboxLocation = value;}
      }

      private Flowsheet flowsheet;
      public Flowsheet Flowsheet
      {
         get {return Flowsheet;}
      }

      private string exePathName;
      public string ExePathName
      {
         get {return exePathName;}
      }

      private NewSystemPreferences newSystemPrefs;
      public NewSystemPreferences NewSystemPrefs
      {
         get {return newSystemPrefs;}
         set {newSystemPrefs = value;}
      }

      private System.Windows.Forms.ToolBarButton toolBarButtonHelp;
      private System.Windows.Forms.MenuItem menuItemView;
      private System.Windows.Forms.MenuItem menuItemHelp;
      private System.Windows.Forms.MenuItem menuItemSeparatorHelp1;
      private System.Windows.Forms.MenuItem menuItemViewToolbar;
      private System.Windows.Forms.MenuItem menuItemViewStatusbar;
      private System.Windows.Forms.ToolBarButton toolBarButtonNewFlowsheet;
      private System.Windows.Forms.ToolBarButton toolBarButtonOpenFlowsheet;
      private System.Windows.Forms.ToolBarButton toolBarButtonSaveFlowsheet;
      private System.Windows.Forms.OpenFileDialog openFileDialog;
      private System.Windows.Forms.SaveFileDialog saveFileDialog;
      private System.Windows.Forms.MenuItem menuItemHelpAbout;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.ImageList imageList;
      private System.Windows.Forms.HelpProvider helpProvider;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.ToolBarButton toolBarButtonUnitSystems;
      private System.Windows.Forms.ToolBar toolBar;
      private System.Windows.Forms.StatusBar statusBar;
      private System.Windows.Forms.ToolBarButton toolBarButton1;
      private System.Windows.Forms.ToolBarButton toolBarButtonHumidityChart;
      private System.Windows.Forms.ToolBarButton toolBarButtonToolbox;
      private System.Windows.Forms.ToolBarButton toolBarButtonAddConnection;
      private System.Windows.Forms.ToolBarButton toolBarButtonRotateClockwise;
      private System.Windows.Forms.ToolBarButton toolBarButtonRotateCounterclockwise;
      private System.Windows.Forms.ToolBarButton toolBarButtonCloseFlowsheet;
      private System.Windows.Forms.ToolBarButton toolBarButtonFlowsheetPreferences;
      private System.Windows.Forms.ToolBarButton toolBarButton2;
      private System.Windows.Forms.ToolBarButton toolBarButton3;
      private System.Windows.Forms.ToolBarButton toolBarButtonCutConnection;
      private System.Windows.Forms.StatusBarPanel statusBarPanel;
      private System.Windows.Forms.MenuItem menuItemFile;
      private System.Windows.Forms.MenuItem menuItemNewFlowsheet;
      private System.Windows.Forms.MenuItem menuItemOpenFlowsheet;
      private System.Windows.Forms.MenuItem menuItemSaveFlowsheet;
      private System.Windows.Forms.MenuItem menuItemSaveAsFlowsheet;
      private System.Windows.Forms.MenuItem menuItemCloseFlowsheet;
      private System.Windows.Forms.MenuItem menuItemExit;
      private System.Windows.Forms.MenuItem menuItemFlowsheetPreferences;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItemFlowsheet;
      private System.Windows.Forms.MenuItem menuItemEdit;
      private System.Windows.Forms.MenuItem menuItemEditSys;
      private System.Windows.Forms.MenuItem menuItemDelete;
      private System.Windows.Forms.MenuItem menuItemSelectAll;
      private System.Windows.Forms.MenuItem menuItemViewToolbox;
      private System.Windows.Forms.MenuItem menuItemAdd;
      private System.Windows.Forms.MenuItem menuItemAddUnitOp;
      private System.Windows.Forms.MenuItem menuItemAddStream;
      private System.Windows.Forms.MenuItem menuItemAddCyclone;
      private System.Windows.Forms.MenuItem menuItemAddBagFilter;
      private System.Windows.Forms.MenuItem menuItemAddCompressor;
      private System.Windows.Forms.MenuItem menuItemAddCooler;
      private System.Windows.Forms.MenuItem menuItemAddFan;
      private System.Windows.Forms.MenuItem menuItemAddHeater;
      private System.Windows.Forms.MenuItem menuItemAddPump;
      private System.Windows.Forms.MenuItem menuItemAddHeatExchanger;
      private System.Windows.Forms.MenuItem menuItemAddFlashTank;
      private System.Windows.Forms.MenuItem menuItemAddMediumStream;
      private System.Windows.Forms.MenuItem menuItemAddMaterialStream;
      private System.Windows.Forms.MenuItem menuItemRotate;
      private System.Windows.Forms.MenuItem menuItemRotateClockwise;
      private System.Windows.Forms.MenuItem menuItemRotateCounterclockwise;
      private System.Windows.Forms.Panel panelBackground;
      private System.Windows.Forms.ToolBarButton toolBarButtonPrintPreview;
      private System.Windows.Forms.ToolBarButton toolBarButtonPrint;
      private System.Windows.Forms.MenuItem menuItemPrint;
      private System.Windows.Forms.MenuItem menuItemPrintPreview;
      private System.Windows.Forms.ToolBarButton toolBarButtonCustomEditor;
      private System.Windows.Forms.ToolBarButton toolBarButtonFormulaEditor;
      private System.Windows.Forms.MenuItem menuItemCustomEditor;
      private System.Windows.Forms.MenuItem menuItemFormulaEditor;
      private System.Windows.Forms.MenuItem menuItemAddValve;
      private System.Windows.Forms.MenuItem menuItemAddAirFilter;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem menuItemAddMixer;
      private System.Windows.Forms.MenuItem menuItemAddTee;
      private System.Windows.Forms.MenuItem menuItemPageSetup;
      private System.Drawing.Printing.PrintDocument printDocument;
      private System.Windows.Forms.MenuItem menuItemSnapshot;
      private System.Windows.Forms.ToolBarButton toolBarButtonFlowsheetSnapshot;
      private System.Windows.Forms.ToolBarButton toolBarButtonSelectionSnapshot;
      private System.Windows.Forms.MenuItem menuItemSnapshotFlowsheet;
      private System.Windows.Forms.MenuItem menuItemSnapshotSelection;
      private System.ComponentModel.IContainer components;

		public MainForm()
		{
         //
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.toolboxVisible = false;
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemFile = new System.Windows.Forms.MenuItem();
         this.menuItemNewFlowsheet = new System.Windows.Forms.MenuItem();
         this.menuItemOpenFlowsheet = new System.Windows.Forms.MenuItem();
         this.menuItemSaveFlowsheet = new System.Windows.Forms.MenuItem();
         this.menuItemSaveAsFlowsheet = new System.Windows.Forms.MenuItem();
         this.menuItemCloseFlowsheet = new System.Windows.Forms.MenuItem();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItemNewSystemPrefs = new System.Windows.Forms.MenuItem();
         this.menuItem12 = new System.Windows.Forms.MenuItem();
         this.menuItemPageSetup = new System.Windows.Forms.MenuItem();
         this.menuItemPrintPreview = new System.Windows.Forms.MenuItem();
         this.menuItemPrint = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItemExit = new System.Windows.Forms.MenuItem();
         this.menuItemEdit = new System.Windows.Forms.MenuItem();
         this.menuItemUnitSystems = new System.Windows.Forms.MenuItem();
         this.menuItem11 = new System.Windows.Forms.MenuItem();
         this.menuItemEditSys = new System.Windows.Forms.MenuItem();
         this.menuItemCustomEditor = new System.Windows.Forms.MenuItem();
         this.menuItemFormulaEditor = new System.Windows.Forms.MenuItem();
         this.menuItem9 = new System.Windows.Forms.MenuItem();
         this.menuItemDelete = new System.Windows.Forms.MenuItem();
         this.menuItemSelectAll = new System.Windows.Forms.MenuItem();
         this.menuItemFind = new System.Windows.Forms.MenuItem();
         this.menuItemView = new System.Windows.Forms.MenuItem();
         this.menuItemViewToolbox = new System.Windows.Forms.MenuItem();
         this.menuItemViewToolbar = new System.Windows.Forms.MenuItem();
         this.menuItemViewStatusbar = new System.Windows.Forms.MenuItem();
         this.menuItemMaterials = new System.Windows.Forms.MenuItem();
         this.menuItemDryingMaterials = new System.Windows.Forms.MenuItem();
         this.menuItemDryingGases = new System.Windows.Forms.MenuItem();
         this.menuItemSubstances = new System.Windows.Forms.MenuItem();
         this.menuItemFlowsheet = new System.Windows.Forms.MenuItem();
         this.menuItemAdd = new System.Windows.Forms.MenuItem();
         this.menuItemAddUnitOp = new System.Windows.Forms.MenuItem();
         this.menuItemAddLiquidMatDryer = new System.Windows.Forms.MenuItem();
         this.menuItemAddSolidMatDryer = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItemAddFan = new System.Windows.Forms.MenuItem();
         this.menuItemAddCompressor = new System.Windows.Forms.MenuItem();
         this.menuItemAddPump = new System.Windows.Forms.MenuItem();
         this.menuItemAddValve = new System.Windows.Forms.MenuItem();
         this.menuItemAddEjector = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.menuItemAddCyclone = new System.Windows.Forms.MenuItem();
         this.menuItemAddBagFilter = new System.Windows.Forms.MenuItem();
         this.menuItemAddAirFilter = new System.Windows.Forms.MenuItem();
         this.menuItemAddElectrostaticPrecipitator = new System.Windows.Forms.MenuItem();
         this.menuItemAddWetScrubber = new System.Windows.Forms.MenuItem();
         this.menuItem5 = new System.Windows.Forms.MenuItem();
         this.menuItemAddHeater = new System.Windows.Forms.MenuItem();
         this.menuItemAddCooler = new System.Windows.Forms.MenuItem();
         this.menuItemAddHeatExchanger = new System.Windows.Forms.MenuItem();
         this.menuItem6 = new System.Windows.Forms.MenuItem();
         this.menuItemAddMixer = new System.Windows.Forms.MenuItem();
         this.menuItemAddTee = new System.Windows.Forms.MenuItem();
         this.menuItem7 = new System.Windows.Forms.MenuItem();
         this.menuItemAddFlashTank = new System.Windows.Forms.MenuItem();
         this.menuItemAddStream = new System.Windows.Forms.MenuItem();
         this.menuItemAddMediumStream = new System.Windows.Forms.MenuItem();
         this.menuItemAddMaterialStream = new System.Windows.Forms.MenuItem();
         this.menuItemAddLiquidMaterialStream = new System.Windows.Forms.MenuItem();
         this.menuItemAddRecycle = new System.Windows.Forms.MenuItem();
         this.menuItem13 = new System.Windows.Forms.MenuItem();
         this.menuItemConnect = new System.Windows.Forms.MenuItem();
         this.menuItemDisconnect = new System.Windows.Forms.MenuItem();
         this.menuItem14 = new System.Windows.Forms.MenuItem();
         this.menuItemRotate = new System.Windows.Forms.MenuItem();
         this.menuItemRotateClockwise = new System.Windows.Forms.MenuItem();
         this.menuItemRotateCounterclockwise = new System.Windows.Forms.MenuItem();
         this.menuItemSnapshot = new System.Windows.Forms.MenuItem();
         this.menuItemSnapshotFlowsheet = new System.Windows.Forms.MenuItem();
         this.menuItemSnapshotSelection = new System.Windows.Forms.MenuItem();
         this.menuItemPlots = new System.Windows.Forms.MenuItem();
         this.menuItemFlowsheetPreferences = new System.Windows.Forms.MenuItem();
         this.menuItem8 = new System.Windows.Forms.MenuItem();
         this.menuItemHumidityChart = new System.Windows.Forms.MenuItem();
         this.menuItemUnitConverter = new System.Windows.Forms.MenuItem();
         this.menuItemHelp = new System.Windows.Forms.MenuItem();
         this.menuItemUserManual = new System.Windows.Forms.MenuItem();
         this.menuItemTutorials = new System.Windows.Forms.MenuItem();
         this.menuItemSeparatorHelp1 = new System.Windows.Forms.MenuItem();
         this.menuItemHelpAbout = new System.Windows.Forms.MenuItem();
         this.imageList = new System.Windows.Forms.ImageList(this.components);
         this.statusBar = new System.Windows.Forms.StatusBar();
         this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
         this.toolBar = new System.Windows.Forms.ToolBar();
         this.toolBarButtonNewFlowsheet = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonOpenFlowsheet = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonSaveFlowsheet = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonCloseFlowsheet = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonPrintPreview = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonPrint = new System.Windows.Forms.ToolBarButton();
         this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonUnitSystems = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonSystemEditor = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonCustomEditor = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonFormulaEditor = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonDelete = new System.Windows.Forms.ToolBarButton();
         this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonToolbox = new System.Windows.Forms.ToolBarButton();
         this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonAddConnection = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonCutConnection = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonRotateClockwise = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonRotateCounterclockwise = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonFlowsheetSnapshot = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonSelectionSnapshot = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonFlowsheetPreferences = new System.Windows.Forms.ToolBarButton();
         this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonHumidityChart = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonUnitConverter = new System.Windows.Forms.ToolBarButton();
         this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
         this.toolBarButtonHelp = new System.Windows.Forms.ToolBarButton();
         this.helpProvider = new System.Windows.Forms.HelpProvider();
         this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
         this.panelBackground = new System.Windows.Forms.Panel();
         this.printDocument = new System.Drawing.Printing.PrintDocument();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.SuspendLayout();
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemFile,
                                                                                 this.menuItemEdit,
                                                                                 this.menuItemView,
                                                                                 this.menuItemMaterials,
                                                                                 this.menuItemFlowsheet,
                                                                                 this.menuItem8,
                                                                                 this.menuItemHelp});
         // 
         // menuItemFile
         // 
         this.menuItemFile.Index = 0;
         this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItemNewFlowsheet,
                                                                                     this.menuItemOpenFlowsheet,
                                                                                     this.menuItemSaveFlowsheet,
                                                                                     this.menuItemSaveAsFlowsheet,
                                                                                     this.menuItemCloseFlowsheet,
                                                                                     this.menuItem1,
                                                                                     this.menuItemNewSystemPrefs,
                                                                                     this.menuItem12,
                                                                                     this.menuItemPageSetup,
                                                                                     this.menuItemPrintPreview,
                                                                                     this.menuItemPrint,
                                                                                     this.menuItem2,
                                                                                     this.menuItemExit});
         this.menuItemFile.Text = "File";
         this.menuItemFile.Click += new System.EventHandler(this.mainMenu_Click);
         // 
         // menuItemNewFlowsheet
         // 
         this.menuItemNewFlowsheet.Index = 0;
         this.menuItemNewFlowsheet.Text = "New";
         this.menuItemNewFlowsheet.Click += new System.EventHandler(this.menuItemNewFlowsheet_Click);
         // 
         // menuItemOpenFlowsheet
         // 
         this.menuItemOpenFlowsheet.Index = 1;
         this.menuItemOpenFlowsheet.Text = "Open...";
         this.menuItemOpenFlowsheet.Click += new System.EventHandler(this.menuItemOpenFlowsheet_Click);
         // 
         // menuItemSaveFlowsheet
         // 
         this.menuItemSaveFlowsheet.Index = 2;
         this.menuItemSaveFlowsheet.Text = "Save";
         this.menuItemSaveFlowsheet.Click += new System.EventHandler(this.menuItemSaveFlowsheet_Click);
         // 
         // menuItemSaveAsFlowsheet
         // 
         this.menuItemSaveAsFlowsheet.Index = 3;
         this.menuItemSaveAsFlowsheet.Text = "Save As...";
         this.menuItemSaveAsFlowsheet.Click += new System.EventHandler(this.menuItemSaveAsFlowsheet_Click);
         // 
         // menuItemCloseFlowsheet
         // 
         this.menuItemCloseFlowsheet.Index = 4;
         this.menuItemCloseFlowsheet.Text = "Close";
         this.menuItemCloseFlowsheet.Click += new System.EventHandler(this.menuItemCloseFlowsheet_Click);
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 5;
         this.menuItem1.Text = "-";
         // 
         // menuItemNewSystemPrefs
         // 
         this.menuItemNewSystemPrefs.Index = 6;
         this.menuItemNewSystemPrefs.Text = "New System Preferences...";
         this.menuItemNewSystemPrefs.Click += new System.EventHandler(this.menuItemNewSystemPrefs_Click);
         // 
         // menuItem12
         // 
         this.menuItem12.Index = 7;
         this.menuItem12.Text = "-";
         // 
         // menuItemPageSetup
         // 
         this.menuItemPageSetup.Index = 8;
         this.menuItemPageSetup.Text = "Page Setup...";
         this.menuItemPageSetup.Click += new System.EventHandler(this.menuItemPageSetup_Click);
         // 
         // menuItemPrintPreview
         // 
         this.menuItemPrintPreview.Index = 9;
         this.menuItemPrintPreview.Text = "Print Preview...";
         this.menuItemPrintPreview.Click += new System.EventHandler(this.menuItemPrintPreview_Click);
         // 
         // menuItemPrint
         // 
         this.menuItemPrint.Index = 10;
         this.menuItemPrint.Text = "Print...";
         this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 11;
         this.menuItem2.Text = "-";
         // 
         // menuItemExit
         // 
         this.menuItemExit.Index = 12;
         this.menuItemExit.Text = "Exit";
         this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
         // 
         // menuItemEdit
         // 
         this.menuItemEdit.Index = 1;
         this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItemUnitSystems,
                                                                                     this.menuItem11,
                                                                                     this.menuItemEditSys,
                                                                                     this.menuItemCustomEditor,
                                                                                     this.menuItemFormulaEditor,
                                                                                     this.menuItem9,
                                                                                     this.menuItemDelete,
                                                                                     this.menuItemSelectAll,
                                                                                     this.menuItemFind});
         this.menuItemEdit.Text = "Edit";
         // 
         // menuItemUnitSystems
         // 
         this.menuItemUnitSystems.Index = 0;
         this.menuItemUnitSystems.Text = "Unit Systems...";
         this.menuItemUnitSystems.Click += new System.EventHandler(this.menuItemViewUnitSys_Click);
         // 
         // menuItem11
         // 
         this.menuItem11.Index = 1;
         this.menuItem11.Text = "-";
         // 
         // menuItemEditSys
         // 
         this.menuItemEditSys.Index = 2;
         this.menuItemEditSys.Text = "System...";
         this.menuItemEditSys.Click += new System.EventHandler(this.menuItemEditSys_Click);
         // 
         // menuItemCustomEditor
         // 
         this.menuItemCustomEditor.Index = 3;
         this.menuItemCustomEditor.Text = "Custom...";
         this.menuItemCustomEditor.Click += new System.EventHandler(this.menuItemCustomEditor_Click);
         // 
         // menuItemFormulaEditor
         // 
         this.menuItemFormulaEditor.Index = 4;
         this.menuItemFormulaEditor.Text = "Formula...";
         this.menuItemFormulaEditor.Click += new System.EventHandler(this.menuItemFormulaEditor_Click);
         // 
         // menuItem9
         // 
         this.menuItem9.Index = 5;
         this.menuItem9.Text = "-";
         // 
         // menuItemDelete
         // 
         this.menuItemDelete.Index = 6;
         this.menuItemDelete.Text = "Delete";
         this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
         // 
         // menuItemSelectAll
         // 
         this.menuItemSelectAll.Index = 7;
         this.menuItemSelectAll.Text = "Select All";
         this.menuItemSelectAll.Click += new System.EventHandler(this.menuItemSelectAll_Click);
         // 
         // menuItemFind
         // 
         this.menuItemFind.Index = 8;
         this.menuItemFind.Text = "Find...";
         this.menuItemFind.Click += new System.EventHandler(this.menuItemFind_Click);
         // 
         // menuItemView
         // 
         this.menuItemView.Index = 2;
         this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItemViewToolbox,
                                                                                     this.menuItemViewToolbar,
                                                                                     this.menuItemViewStatusbar});
         this.menuItemView.MergeOrder = 1;
         this.menuItemView.Text = "View";
         // 
         // menuItemViewToolbox
         // 
         this.menuItemViewToolbox.Index = 0;
         this.menuItemViewToolbox.Text = "Toolbox";
         this.menuItemViewToolbox.Click += new System.EventHandler(this.menuItemViewToolbox_Click);
         // 
         // menuItemViewToolbar
         // 
         this.menuItemViewToolbar.Checked = true;
         this.menuItemViewToolbar.Index = 1;
         this.menuItemViewToolbar.Text = "Toolbar";
         this.menuItemViewToolbar.Click += new System.EventHandler(this.menuItemViewToolbar_Click);
         // 
         // menuItemViewStatusbar
         // 
         this.menuItemViewStatusbar.Checked = true;
         this.menuItemViewStatusbar.Index = 2;
         this.menuItemViewStatusbar.Text = "Statusbar";
         this.menuItemViewStatusbar.Click += new System.EventHandler(this.menuItemViewStatusbar_Click);
         // 
         // menuItemMaterials
         // 
         this.menuItemMaterials.Index = 3;
         this.menuItemMaterials.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.menuItemDryingMaterials,
                                                                                          this.menuItemDryingGases,
                                                                                          this.menuItemSubstances});
         this.menuItemMaterials.Text = "Materials";
         // 
         // menuItemDryingMaterials
         // 
         this.menuItemDryingMaterials.Index = 0;
         this.menuItemDryingMaterials.Text = "Drying Materials...";
         this.menuItemDryingMaterials.Click += new System.EventHandler(this.menuItemDryingMaterials_Click);
         // 
         // menuItemDryingGases
         // 
         this.menuItemDryingGases.Enabled = false;
         this.menuItemDryingGases.Index = 1;
         this.menuItemDryingGases.Text = "Drying Gases...";
         this.menuItemDryingGases.Click += new System.EventHandler(this.menuItemDryingGases_Click);
         // 
         // menuItemSubstances
         // 
         this.menuItemSubstances.Enabled = false;
         this.menuItemSubstances.Index = 2;
         this.menuItemSubstances.Text = "Substances...";
         this.menuItemSubstances.Click += new System.EventHandler(this.menuItemSubstances_Click);
         // 
         // menuItemFlowsheet
         // 
         this.menuItemFlowsheet.Index = 4;
         this.menuItemFlowsheet.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.menuItemAdd,
                                                                                          this.menuItem13,
                                                                                          this.menuItemConnect,
                                                                                          this.menuItemDisconnect,
                                                                                          this.menuItem14,
                                                                                          this.menuItemRotate,
                                                                                          this.menuItemSnapshot,
                                                                                          this.menuItemPlots,
                                                                                          this.menuItemFlowsheetPreferences});
         this.menuItemFlowsheet.Text = "Flowsheet";
         // 
         // menuItemAdd
         // 
         this.menuItemAdd.Index = 0;
         this.menuItemAdd.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.menuItemAddUnitOp,
                                                                                    this.menuItemAddStream,
                                                                                    this.menuItemAddRecycle});
         this.menuItemAdd.Text = "Add";
         // 
         // menuItemAddUnitOp
         // 
         this.menuItemAddUnitOp.Index = 0;
         this.menuItemAddUnitOp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.menuItemAddSolidMatDryer,
                                                                                          this.menuItemAddLiquidMatDryer,
                                                                                          this.menuItem3,
                                                                                          this.menuItemAddFan,
                                                                                          this.menuItemAddCompressor,
                                                                                          this.menuItemAddPump,
                                                                                          this.menuItemAddValve,
                                                                                          this.menuItemAddEjector,
                                                                                          this.menuItem4,
                                                                                          this.menuItemAddCyclone,
                                                                                          this.menuItemAddBagFilter,
                                                                                          this.menuItemAddAirFilter,
                                                                                          this.menuItemAddElectrostaticPrecipitator,
                                                                                          this.menuItemAddWetScrubber,
                                                                                          this.menuItem5,
                                                                                          this.menuItemAddHeater,
                                                                                          this.menuItemAddCooler,
                                                                                          this.menuItemAddHeatExchanger,
                                                                                          this.menuItem6,
                                                                                          this.menuItemAddMixer,
                                                                                          this.menuItemAddTee,
                                                                                          this.menuItem7,
                                                                                          this.menuItemAddFlashTank});
         this.menuItemAddUnitOp.Text = "Unit Operations";
         // 
         // menuItemAddLiquidMatDryer
         // 
         this.menuItemAddLiquidMatDryer.Index = 1;
         this.menuItemAddLiquidMatDryer.Text = "Liquid Material Dryer";
         this.menuItemAddLiquidMatDryer.Click += new System.EventHandler(this.menuItemAddLiquidMatDryer_Click);
         // 
         // menuItemAddSolidMatDryer
         // 
         this.menuItemAddSolidMatDryer.Index = 0;
         this.menuItemAddSolidMatDryer.Text = "Solid Material Dryer";
         this.menuItemAddSolidMatDryer.Click += new System.EventHandler(this.menuItemAddSolidMatDryer_Click);
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "-";
         // 
         // menuItemAddFan
         // 
         this.menuItemAddFan.Index = 3;
         this.menuItemAddFan.Text = "Fan";
         this.menuItemAddFan.Click += new System.EventHandler(this.menuItemAddFan_Click);
         // 
         // menuItemAddCompressor
         // 
         this.menuItemAddCompressor.Index = 4;
         this.menuItemAddCompressor.Text = "Compressor";
         this.menuItemAddCompressor.Click += new System.EventHandler(this.menuItemAddCompressor_Click);
         // 
         // menuItemAddPump
         // 
         this.menuItemAddPump.Index = 5;
         this.menuItemAddPump.Text = "Pump";
         this.menuItemAddPump.Click += new System.EventHandler(this.menuItemAddPump_Click);
         // 
         // menuItemAddValve
         // 
         this.menuItemAddValve.Index = 6;
         this.menuItemAddValve.Text = "Valve";
         this.menuItemAddValve.Click += new System.EventHandler(this.menuItemAddValve_Click);
         // 
         // menuItemAddEjector
         // 
         this.menuItemAddEjector.Index = 7;
         this.menuItemAddEjector.Text = "Ejector";
         this.menuItemAddEjector.Click += new System.EventHandler(this.menuItemAddEjector_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 8;
         this.menuItem4.Text = "-";
         // 
         // menuItemAddCyclone
         // 
         this.menuItemAddCyclone.Index = 9;
         this.menuItemAddCyclone.Text = "Cyclone";
         this.menuItemAddCyclone.Click += new System.EventHandler(this.menuItemAddCyclone_Click);
         // 
         // menuItemAddBagFilter
         // 
         this.menuItemAddBagFilter.Index = 10;
         this.menuItemAddBagFilter.Text = "Bag Filter";
         this.menuItemAddBagFilter.Click += new System.EventHandler(this.menuItemAddBagFilter_Click);
         // 
         // menuItemAddAirFilter
         // 
         this.menuItemAddAirFilter.Index = 11;
         this.menuItemAddAirFilter.Text = "Air Filter";
         this.menuItemAddAirFilter.Click += new System.EventHandler(this.menuItemAddAirFilter_Click);
         // 
         // menuItemAddElectrostaticPrecipitator
         // 
         this.menuItemAddElectrostaticPrecipitator.Index = 12;
         this.menuItemAddElectrostaticPrecipitator.Text = "Electrostatic Precipitator";
         this.menuItemAddElectrostaticPrecipitator.Click += new System.EventHandler(this.menuItemAddElectrostaticPrecipitator_Click);
         // 
         // menuItemAddWetScrubber
         // 
         this.menuItemAddWetScrubber.Index = 13;
         this.menuItemAddWetScrubber.Text = "Wet Scrubber";
         this.menuItemAddWetScrubber.Click += new System.EventHandler(this.menuItemAddWetScrubber_Click);
         // 
         // menuItem5
         // 
         this.menuItem5.Index = 14;
         this.menuItem5.Text = "-";
         // 
         // menuItemAddHeater
         // 
         this.menuItemAddHeater.Index = 15;
         this.menuItemAddHeater.Text = "Heater";
         this.menuItemAddHeater.Click += new System.EventHandler(this.menuItemAddHeater_Click);
         // 
         // menuItemAddCooler
         // 
         this.menuItemAddCooler.Index = 16;
         this.menuItemAddCooler.Text = "Cooler";
         this.menuItemAddCooler.Click += new System.EventHandler(this.menuItemAddCooler_Click);
         // 
         // menuItemAddHeatExchanger
         // 
         this.menuItemAddHeatExchanger.Index = 17;
         this.menuItemAddHeatExchanger.Text = "Heat Exchanger";
         this.menuItemAddHeatExchanger.Click += new System.EventHandler(this.menuItemAddHeatExchanger_Click);
         // 
         // menuItem6
         // 
         this.menuItem6.Index = 18;
         this.menuItem6.Text = "-";
         // 
         // menuItemAddMixer
         // 
         this.menuItemAddMixer.Index = 19;
         this.menuItemAddMixer.Text = "Mixer";
         this.menuItemAddMixer.Click += new System.EventHandler(this.menuItemAddMixer_Click);
         // 
         // menuItemAddTee
         // 
         this.menuItemAddTee.Index = 20;
         this.menuItemAddTee.Text = "Tee";
         this.menuItemAddTee.Click += new System.EventHandler(this.menuItemAddTee_Click);
         // 
         // menuItem7
         // 
         this.menuItem7.Index = 21;
         this.menuItem7.Text = "-";
         // 
         // menuItemAddFlashTank
         // 
         this.menuItemAddFlashTank.Index = 22;
         this.menuItemAddFlashTank.Text = "Flash Tank";
         this.menuItemAddFlashTank.Click += new System.EventHandler(this.menuItemAddFlashTank_Click);
         // 
         // menuItemAddStream
         // 
         this.menuItemAddStream.Index = 1;
         this.menuItemAddStream.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.menuItemAddMediumStream,
                                                                                          this.menuItemAddMaterialStream,
                                                                                          this.menuItemAddLiquidMaterialStream});
         this.menuItemAddStream.Text = "Streams";
         // 
         // menuItemAddMediumStream
         // 
         this.menuItemAddMediumStream.Index = 0;
         this.menuItemAddMediumStream.Text = "Drying Gas";
         this.menuItemAddMediumStream.Click += new System.EventHandler(this.menuItemAddMediumStream_Click);
         // 
         // menuItemAddMaterialStream
         // 
         this.menuItemAddMaterialStream.Index = 1;
         this.menuItemAddMaterialStream.Text = "Solid Material";
         this.menuItemAddMaterialStream.Click += new System.EventHandler(this.menuItemAddSolidMaterialStream_Click);
         // 
         // menuItemAddLiquidMaterialStream
         // 
         this.menuItemAddLiquidMaterialStream.Index = 2;
         this.menuItemAddLiquidMaterialStream.Text = "Liquid Material";
         this.menuItemAddLiquidMaterialStream.Click += new System.EventHandler(this.menuItemAddLiquidMaterialStream_Click);
         // 
         // menuItemAddRecycle
         // 
         this.menuItemAddRecycle.Index = 2;
         this.menuItemAddRecycle.Text = "Recycle";
         this.menuItemAddRecycle.Click += new System.EventHandler(this.menuItemAddRecycle_Click);
         // 
         // menuItem13
         // 
         this.menuItem13.Index = 1;
         this.menuItem13.Text = "-";
         // 
         // menuItemConnect
         // 
         this.menuItemConnect.Index = 2;
         this.menuItemConnect.Text = "Connect";
         this.menuItemConnect.Click += new System.EventHandler(this.menuItemConnect_Click);
         // 
         // menuItemDisconnect
         // 
         this.menuItemDisconnect.Index = 3;
         this.menuItemDisconnect.Text = "Disconnect";
         this.menuItemDisconnect.Click += new System.EventHandler(this.menuItemDisconnect_Click);
         // 
         // menuItem14
         // 
         this.menuItem14.Index = 4;
         this.menuItem14.Text = "-";
         // 
         // menuItemRotate
         // 
         this.menuItemRotate.Index = 5;
         this.menuItemRotate.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                       this.menuItemRotateClockwise,
                                                                                       this.menuItemRotateCounterclockwise});
         this.menuItemRotate.Text = "Rotate Streams";
         // 
         // menuItemRotateClockwise
         // 
         this.menuItemRotateClockwise.Index = 0;
         this.menuItemRotateClockwise.Text = "Clockwise";
         this.menuItemRotateClockwise.Click += new System.EventHandler(this.menuItemRotateClockwise_Click);
         // 
         // menuItemRotateCounterclockwise
         // 
         this.menuItemRotateCounterclockwise.Index = 1;
         this.menuItemRotateCounterclockwise.Text = "Counterclockwise";
         this.menuItemRotateCounterclockwise.Click += new System.EventHandler(this.menuItemRotateCounterclockwise_Click);
         // 
         // menuItemSnapshot
         // 
         this.menuItemSnapshot.Index = 6;
         this.menuItemSnapshot.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItemSnapshotFlowsheet,
                                                                                         this.menuItemSnapshotSelection});
         this.menuItemSnapshot.Text = "Snapshot";
         // 
         // menuItemSnapshotFlowsheet
         // 
         this.menuItemSnapshotFlowsheet.Index = 0;
         this.menuItemSnapshotFlowsheet.Text = "Flowsheet...";
         this.menuItemSnapshotFlowsheet.Click += new System.EventHandler(this.menuItemSnapshotFlowsheet_Click);
         // 
         // menuItemSnapshotSelection
         // 
         this.menuItemSnapshotSelection.Index = 1;
         this.menuItemSnapshotSelection.Text = "Selection";
         this.menuItemSnapshotSelection.Click += new System.EventHandler(this.menuItemSnapshotSelection_Click);
         // 
         // menuItemPlots
         // 
         this.menuItemPlots.Index = 7;
         this.menuItemPlots.Text = "Plots...";
         this.menuItemPlots.Click += new System.EventHandler(this.menuItemPlots_Click);
         // 
         // menuItemFlowsheetPreferences
         // 
         this.menuItemFlowsheetPreferences.Index = 8;
         this.menuItemFlowsheetPreferences.Text = "Preferences...";
         this.menuItemFlowsheetPreferences.Click += new System.EventHandler(this.menuItemFlowsheetPreferences_Click);
         // 
         // menuItem8
         // 
         this.menuItem8.Index = 5;
         this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this.menuItemHumidityChart,
                                                                                  this.menuItemUnitConverter});
         this.menuItem8.Text = "Utilities";
         // 
         // menuItemHumidityChart
         // 
         this.menuItemHumidityChart.Index = 0;
         this.menuItemHumidityChart.Text = "Humidity Chart...";
         this.menuItemHumidityChart.Click += new System.EventHandler(this.menuItemViewHumidityChart_Click);
         // 
         // menuItemUnitConverter
         // 
         this.menuItemUnitConverter.Index = 1;
         this.menuItemUnitConverter.Text = "Unit Converter...";
         this.menuItemUnitConverter.Click += new System.EventHandler(this.menuItemUnitConverter_Click);
         // 
         // menuItemHelp
         // 
         this.menuItemHelp.Index = 6;
         this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItemUserManual,
                                                                                     this.menuItemTutorials,
                                                                                     this.menuItemSeparatorHelp1,
                                                                                     this.menuItemHelpAbout});
         this.menuItemHelp.MergeOrder = 1;
         this.menuItemHelp.Text = "Help";
         // 
         // menuItemUserManual
         // 
         this.menuItemUserManual.Index = 0;
         this.menuItemUserManual.Text = "User\'s Manual";
         this.menuItemUserManual.Click += new System.EventHandler(this.menuItemUserManual_Click);
         // 
         // menuItemTutorials
         // 
         this.menuItemTutorials.Index = 1;
         this.menuItemTutorials.Text = "Tutorials...";
         this.menuItemTutorials.Click += new System.EventHandler(this.menuItemTutorials_Click);
         // 
         // menuItemSeparatorHelp1
         // 
         this.menuItemSeparatorHelp1.Index = 2;
         this.menuItemSeparatorHelp1.Text = "-";
         // 
         // menuItemHelpAbout
         // 
         this.menuItemHelpAbout.Index = 3;
         this.menuItemHelpAbout.Text = "About...";
         this.menuItemHelpAbout.Click += new System.EventHandler(this.menuItemHelpAbout_Click);
         // 
         // imageList
         // 
         this.imageList.ImageSize = new System.Drawing.Size(20, 20);
         this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
         this.imageList.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // statusBar
         // 
         this.statusBar.Location = new System.Drawing.Point(0, 459);
         this.statusBar.Name = "statusBar";
         this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                     this.statusBarPanel});
         this.statusBar.ShowPanels = true;
         this.statusBar.Size = new System.Drawing.Size(684, 22);
         this.statusBar.TabIndex = 1;
         this.statusBar.Click += new System.EventHandler(this.statusBar_Click);
         // 
         // statusBarPanel
         // 
         this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
         this.statusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
         this.statusBarPanel.Width = 668;
         // 
         // toolBar
         // 
         this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                   this.toolBarButtonNewFlowsheet,
                                                                                   this.toolBarButtonOpenFlowsheet,
                                                                                   this.toolBarButtonSaveFlowsheet,
                                                                                   this.toolBarButtonCloseFlowsheet,
                                                                                   this.toolBarButtonPrintPreview,
                                                                                   this.toolBarButtonPrint,
                                                                                   this.toolBarButton1,
                                                                                   this.toolBarButtonUnitSystems,
                                                                                   this.toolBarButtonSystemEditor,
                                                                                   this.toolBarButtonCustomEditor,
                                                                                   this.toolBarButtonFormulaEditor,
                                                                                   this.toolBarButtonDelete,
                                                                                   this.toolBarButton2,
                                                                                   this.toolBarButtonToolbox,
                                                                                   this.toolBarButton3,
                                                                                   this.toolBarButtonAddConnection,
                                                                                   this.toolBarButtonCutConnection,
                                                                                   this.toolBarButtonRotateClockwise,
                                                                                   this.toolBarButtonRotateCounterclockwise,
                                                                                   this.toolBarButtonFlowsheetSnapshot,
                                                                                   this.toolBarButtonSelectionSnapshot,
                                                                                   this.toolBarButtonFlowsheetPreferences,
                                                                                   this.toolBarButton4,
                                                                                   this.toolBarButtonHumidityChart,
                                                                                   this.toolBarButtonUnitConverter,
                                                                                   this.toolBarButton5,
                                                                                   this.toolBarButtonHelp});
         this.toolBar.ButtonSize = new System.Drawing.Size(24, 24);
         this.toolBar.DropDownArrows = true;
         this.toolBar.ImageList = this.imageList;
         this.toolBar.Location = new System.Drawing.Point(0, 0);
         this.toolBar.Name = "toolBar";
         this.toolBar.ShowToolTips = true;
         this.toolBar.Size = new System.Drawing.Size(684, 32);
         this.toolBar.TabIndex = 2;
         this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
         // 
         // toolBarButtonNewFlowsheet
         // 
         this.toolBarButtonNewFlowsheet.ImageIndex = 0;
         this.toolBarButtonNewFlowsheet.ToolTipText = "New Flowsheet";
         // 
         // toolBarButtonOpenFlowsheet
         // 
         this.toolBarButtonOpenFlowsheet.ImageIndex = 1;
         this.toolBarButtonOpenFlowsheet.ToolTipText = "Open Flowsheet";
         // 
         // toolBarButtonSaveFlowsheet
         // 
         this.toolBarButtonSaveFlowsheet.ImageIndex = 2;
         this.toolBarButtonSaveFlowsheet.ToolTipText = "Save Flowsheet";
         // 
         // toolBarButtonCloseFlowsheet
         // 
         this.toolBarButtonCloseFlowsheet.ImageIndex = 3;
         this.toolBarButtonCloseFlowsheet.ToolTipText = "Close Flowsheet";
         // 
         // toolBarButtonPrintPreview
         // 
         this.toolBarButtonPrintPreview.ImageIndex = 4;
         this.toolBarButtonPrintPreview.ToolTipText = "Print Preview";
         // 
         // toolBarButtonPrint
         // 
         this.toolBarButtonPrint.ImageIndex = 5;
         this.toolBarButtonPrint.ToolTipText = "Print";
         // 
         // toolBarButton1
         // 
         this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
         // 
         // toolBarButtonUnitSystems
         // 
         this.toolBarButtonUnitSystems.ImageIndex = 10;
         this.toolBarButtonUnitSystems.ToolTipText = "Unit Systems";
         // 
         // toolBarButtonSystemEditor
         // 
         this.toolBarButtonSystemEditor.ImageIndex = 6;
         this.toolBarButtonSystemEditor.ToolTipText = "System Editor";
         // 
         // toolBarButtonCustomEditor
         // 
         this.toolBarButtonCustomEditor.ImageIndex = 7;
         this.toolBarButtonCustomEditor.ToolTipText = "Custom Editor";
         // 
         // toolBarButtonFormulaEditor
         // 
         this.toolBarButtonFormulaEditor.ImageIndex = 8;
         this.toolBarButtonFormulaEditor.ToolTipText = "Formula Editor";
         // 
         // toolBarButtonDelete
         // 
         this.toolBarButtonDelete.ImageIndex = 9;
         this.toolBarButtonDelete.ToolTipText = "Delete";
         // 
         // toolBarButton2
         // 
         this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
         // 
         // toolBarButtonToolbox
         // 
         this.toolBarButtonToolbox.ImageIndex = 11;
         this.toolBarButtonToolbox.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
         this.toolBarButtonToolbox.ToolTipText = "Toolbox";
         // 
         // toolBarButton3
         // 
         this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
         // 
         // toolBarButtonAddConnection
         // 
         this.toolBarButtonAddConnection.ImageIndex = 13;
         this.toolBarButtonAddConnection.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
         this.toolBarButtonAddConnection.ToolTipText = "Connect";
         // 
         // toolBarButtonCutConnection
         // 
         this.toolBarButtonCutConnection.ImageIndex = 14;
         this.toolBarButtonCutConnection.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
         this.toolBarButtonCutConnection.ToolTipText = "Disconnect";
         // 
         // toolBarButtonRotateClockwise
         // 
         this.toolBarButtonRotateClockwise.ImageIndex = 15;
         this.toolBarButtonRotateClockwise.ToolTipText = "Rotate Stream Clockwise";
         // 
         // toolBarButtonRotateCounterclockwise
         // 
         this.toolBarButtonRotateCounterclockwise.ImageIndex = 16;
         this.toolBarButtonRotateCounterclockwise.ToolTipText = "Rotate Stream Counterclockwise";
         // 
         // toolBarButtonFlowsheetSnapshot
         // 
         this.toolBarButtonFlowsheetSnapshot.ImageIndex = 17;
         this.toolBarButtonFlowsheetSnapshot.ToolTipText = "Save Flowsheet as Image";
         // 
         // toolBarButtonSelectionSnapshot
         // 
         this.toolBarButtonSelectionSnapshot.ImageIndex = 18;
         this.toolBarButtonSelectionSnapshot.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
         this.toolBarButtonSelectionSnapshot.ToolTipText = "Snapshot Selection";
         // 
         // toolBarButtonFlowsheetPreferences
         // 
         this.toolBarButtonFlowsheetPreferences.ImageIndex = 12;
         this.toolBarButtonFlowsheetPreferences.ToolTipText = "Flowsheet Preferences";
         // 
         // toolBarButton4
         // 
         this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
         // 
         // toolBarButtonHumidityChart
         // 
         this.toolBarButtonHumidityChart.ImageIndex = 19;
         this.toolBarButtonHumidityChart.ToolTipText = "Humidity Chart";
         // 
         // toolBarButtonUnitConverter
         // 
         this.toolBarButtonUnitConverter.ImageIndex = 20;
         this.toolBarButtonUnitConverter.ToolTipText = "Unit Converter";
         // 
         // toolBarButton5
         // 
         this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
         // 
         // toolBarButtonHelp
         // 
         this.toolBarButtonHelp.ImageIndex = 21;
         this.toolBarButtonHelp.ToolTipText = "User\'s Manual";
         // 
         // helpProvider
         // 
         this.helpProvider.HelpNamespace = "where the .chm file is";
         // 
         // panelBackground
         // 
         this.panelBackground.BackColor = System.Drawing.Color.Silver;
         this.panelBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelBackground.Location = new System.Drawing.Point(0, 32);
         this.panelBackground.Name = "panelBackground";
         this.panelBackground.Size = new System.Drawing.Size(684, 427);
         this.panelBackground.TabIndex = 3;
         // 
         // printDocument
         // 
         this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
         // 
         // MainForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.BackColor = System.Drawing.SystemColors.Control;
         this.ClientSize = new System.Drawing.Size(684, 481);
         this.Controls.Add(this.panelBackground);
         this.Controls.Add(this.toolBar);
         this.Controls.Add(this.statusBar);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Menu = this.mainMenu;
         this.Name = "MainForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Click += new System.EventHandler(this.ClickHandler);
         this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingHandler);
         this.Load += new System.EventHandler(this.MainForm_Load);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

      private void NewFlowsheet()
      {
         if (this.flowsheet != null)
         {
            this.CloseFlowsheet();
         }
         if (this.flowsheet == null)
         {
            this.flowsheet = new Flowsheet(this.NewSystemPrefs, null);
            this.InitializeFlowsheet();
            this.Text = Application.ProductName + " - " + UI.UNTITLED;
         }
      }

      private void OpenFlowsheet()
      {
         if (this.flowsheet != null)
         {
            this.CloseFlowsheet();
         }
         if (this.flowsheet == null)
         {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
               this.Cursor = Cursors.WaitCursor;
               string fileName = openFileDialog.FileName;
               if (fileName != null && fileName.Length > 0)
               {
                  FileInfo fileInfo = new FileInfo(fileName);
                  string fext = fileInfo.Extension.ToLower();
                  if (fext.Equals(".xml"))
                  {
                     this.flowsheet = PersistenceManager.GetInstance().UnpersistFlowsheet(this.NewSystemPrefs, fileName);
                     if (this.flowsheet != null)
                     {
                        this.InitializeFlowsheet();
                        this.Text = Application.ProductName + " - " + fileName;
                     }
                  }
               }
               if (this.flowsheet == null)
               {
                  string message = "Could not open the file!"; 
                  MessageBox.Show(message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
               this.Cursor = Cursors.Default;
            }
         }
      }

      private void ShowToolbox()
      {
         // respect user preferences
         if (this.ToolboxVisible)
         {
            this.flowsheet.ShowToolbox(this.ToolboxLocation);
         }
      }

      private void InitializeFlowsheet()
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.Location = new Point(0, 0);
            this.flowsheet.Dock = DockStyle.Fill;
            this.flowsheet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowsheet.Parent = this;
            this.flowsheet.ActivityChanged += new ActivityChangedEventHandler(flowsheet_ActivityChanged);
            this.flowsheet.SaveFlowsheet += new SaveFlowsheetEventHandler(flowsheet_SaveFlowsheet);
            this.flowsheet.SnapshotTaken += new SnapshotTakenEventHandler(flowsheet_SnapshotTaken);
            this.flowsheet.ToolboxAliveChanged += new ToolboxAliveChangedEventHandler(flowsheet_ToolboxAliveChanged);
            this.flowsheet.ToolboxLocationChanged += new ToolboxLocationChangedEventHandler(flowsheet_ToolboxLocationChanged);
            this.flowsheet.ToolboxVisibleChanged += new ToolboxVisibleChangedEventHandler(flowsheet_ToolboxVisibleChanged);
            this.Controls.Add(this.flowsheet);
            this.flowsheet.BringToFront();

            this.toolBarButtonToolbox.Pushed = false;
            this.toolBarButtonAddConnection.Pushed = false;
            this.toolBarButtonCutConnection.Pushed = false;
            this.toolBarButtonSelectionSnapshot.Pushed = false;
            this.ShowToolbox();
         }
      }

      private void SaveFlowsheet()
      {
         if (this.flowsheet != null)
         {
            if (flowsheet.IsDirty)
            {
               if (this.Text == null || this.Text.Length < 1 ||
                  this.Text.Equals(Application.ProductName + " - " + UI.UNTITLED) ||
                  this.Text.Equals(Application.ProductName + " - " + UI.UNTITLED + UI.DIRTY)) 
               {
                  SaveAsFlowsheet();
               }
               else
               {
                  string fileName = UI.GetFileNameFromTitle(this.Text);
                  PersistenceManager.GetInstance().PersistFlowsheet(this.flowsheet, fileName.Substring(0, fileName.Length-1));
               }
            }
         }
      }

      private void SaveAsFlowsheet()
      {
         if (this.flowsheet != null)
         {
            if (!this.Text.Equals(Application.ProductName + " - " + UI.UNTITLED))
            {
               this.saveFileDialog.Filter = "XML|*.xml";
               this.saveFileDialog.AddExtension = true;
               this.saveFileDialog.DefaultExt = "xml";
               this.saveFileDialog.Title = "Save Flowsheet";
               System.Windows.Forms.DialogResult dr = this.saveFileDialog.ShowDialog();
               if (dr == System.Windows.Forms.DialogResult.OK) 
               {
                  if (this.saveFileDialog.FileName != null && this.saveFileDialog.FileName.Length > 0 &&
                     !this.saveFileDialog.FileName.Equals(Application.ProductName + " - " + UI.UNTITLED) &&
                     !this.saveFileDialog.FileName.Equals(Application.ProductName + " - " + UI.UNTITLED + UI.DIRTY)) 
                  {
                     this.Text = Application.ProductName + " - " + this.saveFileDialog.FileName;
                     PersistenceManager.GetInstance().PersistFlowsheet(this.flowsheet, this.saveFileDialog.FileName);
                  }
               }
            }
         }
      }

      private void CloseFlowsheet()
      {
         if (this.flowsheet != null)
         {
            this.Cursor = Cursors.WaitCursor;
            if (this.flowsheet.Close())
            {
               this.flowsheet.ActivityChanged -= new ActivityChangedEventHandler(flowsheet_ActivityChanged);
               this.flowsheet.SaveFlowsheet -= new SaveFlowsheetEventHandler(flowsheet_SaveFlowsheet);
               this.flowsheet.SnapshotTaken -= new SnapshotTakenEventHandler(flowsheet_SnapshotTaken);
               this.flowsheet.ToolboxAliveChanged -= new ToolboxAliveChangedEventHandler(flowsheet_ToolboxAliveChanged);
               this.flowsheet.ToolboxLocationChanged -= new ToolboxLocationChangedEventHandler(flowsheet_ToolboxLocationChanged);
               this.flowsheet.ToolboxVisibleChanged -= new ToolboxVisibleChangedEventHandler(flowsheet_ToolboxVisibleChanged);
               this.Controls.Remove(this.flowsheet);
               this.flowsheet.Dispose();
               this.flowsheet = null;
               this.Text = Application.ProductName;
            }
            this.Cursor = Cursors.Default;
         }
      }

      private bool Exit()
      {
         bool exit = false;
         if (this.flowsheet != null)
         {
            if (this.flowsheet.Close())
            {
               this.Controls.Remove(this.flowsheet);
               this.flowsheet.Dispose();
               this.flowsheet = null;
               exit = true;
            }
         }
         else
            exit = true;
         return exit;
      }

      private void UnitSystems()
      {
         UnitSystemsForm unitSysEditor = new UnitSystemsForm();
         unitSysEditor.ShowDialog();
      }

      private void ShowUserManual()
      {
         if (this.userManualForm == null)
         {
            this.userManualForm = new UserManualForm();
            this.userManualForm.Closed += new EventHandler(userManualForm_Closed);
            this.userManualForm.Show();
         }
         else
         {
            if (this.userManualForm.WindowState.Equals(FormWindowState.Minimized))
               this.userManualForm.WindowState = FormWindowState.Normal;
            this.userManualForm.Activate();
         }
      }

      public void ShowTutorial(string tutorialName, string tutorialFile)
      {
         if (this.tutorialForm == null)
         {
            this.tutorialForm = new TutorialForm(tutorialName, tutorialFile);
            this.tutorialForm.Closed += new EventHandler(tutorialForm_Closed);
            this.tutorialForm.Show();
         }
         else
         {
            if (this.tutorialForm.WindowState.Equals(FormWindowState.Minimized))
               this.tutorialForm.WindowState = FormWindowState.Normal;
            this.tutorialForm.SetTutorial(tutorialName, tutorialFile);
            this.tutorialForm.Activate();
         }
      }

      private void About()
      {
         AboutForm af = new AboutForm();
         StringBuilder sb = new StringBuilder();
         Version v = new Version(Application.ProductVersion);                  sb.Append("Company Name: ");         sb.Append(Application.CompanyName);         sb.Append("\r\n");         sb.Append("Product Name: ");         sb.Append(Application.ProductName);         sb.Append("\r\n");         sb.Append("Product Version: ");         sb.Append(v.Major);         sb.Append(".");         sb.Append(v.Minor);         sb.Append("\r\n");                  sb.Append("Build: ");         sb.Append(v.Build);         sb.Append("\r\n");         sb.Append("\r\n");         if (this.lease != null)         {            sb.Append("Start of Lease: ");            sb.Append(this.lease.LeaseStart.ToString());            sb.Append("\r\n");
            sb.Append("End of Lease: ");            sb.Append(this.lease.LeaseEnd.ToString());            sb.Append("\r\n");
            sb.Append("Serial #: ");            sb.Append(this.lease.SerialNumber);            sb.Append("\r\n");         }
         af.Message = sb.ToString();
         af.ShowDialog();
      }

      private void ClosingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         // Make sure the splash screen is closed
         CloseSplash();

         if (!this.Exit())
            e.Cancel = true;
         else
         {
            // store the unit system catalog
            string usFileName = this.exePathName + MainForm.USCAT_XML;
            string bakUsFileName = this.exePathName + MainForm.USCAT_BAK;
            PersistenceManager.GetInstance().PersistUnitSystemCatalog(usFileName, bakUsFileName);

            // store the material catalog
            string matFileName = this.exePathName + MainForm.MATCAT_XML;
            string bakMatFileName = this.exePathName + MainForm.MATCAT_BAK;
            PersistenceManager.GetInstance().PersistMaterialCatalog(matFileName, bakMatFileName);

            // store the app preferences
            PersistenceManager.GetInstance().PersistAppPreferences(this);
         }
      }

      private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if(e.Button == this.toolBarButtonNewFlowsheet)	
         {
            this.NewFlowsheet();
         }
         else if(e.Button == this.toolBarButtonOpenFlowsheet)
         {
            this.OpenFlowsheet();
         }
         else if(e.Button == this.toolBarButtonSaveFlowsheet)
         {
            this.SaveFlowsheet();
         }
         else if(e.Button == this.toolBarButtonCloseFlowsheet)
         {
            this.CloseFlowsheet();
         }
         else if(e.Button == this.toolBarButtonPrintPreview)
         {
            this.PrintPreview();
         }
         else if(e.Button == this.toolBarButtonPrint)
         {
            this.Print();
         }
         else if(e.Button == this.toolBarButtonFlowsheetPreferences)
         {
            this.FlowsheetPreferences();
         }
         else if(e.Button == this.toolBarButtonHelp)
         {
            this.ShowUserManual();
         }
         else if(e.Button == this.toolBarButtonUnitSystems)
         {
            this.UnitSystems();
         }
         else if(e.Button == this.toolBarButtonHumidityChart)
         {
            if (this.flowsheet != null)
               this.flowsheet.CreateHumidityChart();
         }
         else if(e.Button == this.toolBarButtonToolbox)
         {
            if (this.flowsheet != null)
            {
               if (this.toolBarButtonToolbox.Pushed)
                  this.flowsheet.ShowToolbox(this.ToolboxLocation);
               else
                  this.flowsheet.Toolbox.Close();
            }
         }
         else if(e.Button == this.toolBarButtonSystemEditor)
         {
            if (this.flowsheet != null)
               this.flowsheet.EditSystem();
         }
         else if(e.Button == this.toolBarButtonCustomEditor)
         {
            if (this.flowsheet != null)
               this.flowsheet.ShowCustomEditor();
         }
         else if(e.Button == this.toolBarButtonFormulaEditor)
         {
            if (this.flowsheet != null)
               this.flowsheet.ShowFormulaEditor();
         }
         else if(e.Button == this.toolBarButtonDelete)
         {
            if (this.flowsheet != null)
               this.flowsheet.DeleteSelectedSolvables();
         }
         else if(e.Button == this.toolBarButtonAddConnection)
         {
            if (this.flowsheet != null)
            {
               if (this.toolBarButtonAddConnection.Pushed)
               {
                  this.toolBarButtonCutConnection.Pushed = false;
                  this.toolBarButtonSelectionSnapshot.Pushed = false;
                  this.flowsheet.AddConnection();
               }
               else
                  this.flowsheet.ResetActivity();
            }
         }
         else if(e.Button == this.toolBarButtonCutConnection)
         {
            if (this.flowsheet != null)
            {
               if (this.toolBarButtonCutConnection.Pushed)
               {
                  this.toolBarButtonAddConnection.Pushed = false;
                  this.toolBarButtonSelectionSnapshot.Pushed = false;
                  this.flowsheet.CutConnection();
               }
               else
                  this.flowsheet.ResetActivity();
            }
         }
         else if(e.Button == this.toolBarButtonRotateClockwise)
         {
            if (this.flowsheet != null)
               this.flowsheet.StreamManager.RotateStreamControls(RotationDirection.Clockwise);
         }
         else if(e.Button == this.toolBarButtonRotateCounterclockwise)
         {
            if (this.flowsheet != null)
               this.flowsheet.StreamManager.RotateStreamControls(RotationDirection.Counterclockwise);
         }
         else if(e.Button == this.toolBarButtonFlowsheetSnapshot)
         {
            this.FlowsheetSnapshot();
         }
         else if(e.Button == this.toolBarButtonSelectionSnapshot)
         {
            if (this.flowsheet != null)
            {
               if (this.toolBarButtonSelectionSnapshot.Pushed)
               {
                  this.toolBarButtonAddConnection.Pushed = false;
                  this.toolBarButtonCutConnection.Pushed = false;
                  this.flowsheet.SelectSnapshot();
               }
               else
                  this.flowsheet.ResetActivity();
            }
         }
         else if(e.Button == this.toolBarButtonUnitConverter)
         {
            this.ShowUnitConverter();
         }
         else
         {
            this.SetDefaultActivityOnFlowsheet();
         }
      }

      private void ClickHandler(object sender, System.EventArgs e)
      {
         this.SetDefaultActivityOnFlowsheet();
      }

      private void SetDefaultActivityOnFlowsheet()
      {
            if (this.flowsheet != null)
               this.flowsheet.ResetActivity();
      }

      private void statusBar_Click(object sender, System.EventArgs e)
      {
         this.SetDefaultActivityOnFlowsheet();
      }

      private void mainMenu_Click(object sender, System.EventArgs e)
      {
         if (sender == this.mainMenu)
         {
            this.SetDefaultActivityOnFlowsheet();
         }
      }

      private void menuItemViewToolbar_Click(object sender, System.EventArgs e)
      {
         this.toolBar.Visible  = this.menuItemViewToolbar.Checked  = !this.toolBar.Visible ;
      }

      private void menuItemViewStatusbar_Click(object sender, System.EventArgs e)
      {
         this.statusBar.Visible = this.menuItemViewStatusbar.Checked = !this.statusBar.Visible;
      }

      private void menuItemUserManual_Click(object sender, System.EventArgs e)
      {
         this.ShowUserManual();
      }

      private void menuItemHelpAbout_Click(object sender, System.EventArgs e)
      {
         this.About();
      }

      private void menuItemFlowsheetPreferences_Click(object sender, System.EventArgs e)
      {
         this.FlowsheetPreferences();
      }

      private void FlowsheetPreferences()
      {
         if (this.flowsheet != null)
         {
            FlowsheetPreferencesForm prefs = new FlowsheetPreferencesForm(this.flowsheet);
            prefs.ShowDialog();
         }
      }

      private void flowsheet_ActivityChanged(FlowsheetActivity flowsheetActivity)
      {
         this.statusBarPanel.Text = UI.GetFlowsheetActivityAsString(flowsheetActivity); 
         if (flowsheetActivity == FlowsheetActivity.AddingConnStepOne ||
            flowsheetActivity == FlowsheetActivity.AddingConnStepTwo)
         {
            if (!this.toolBarButtonAddConnection.Pushed)
            {
               this.toolBarButtonAddConnection.Pushed = true;
               this.toolBarButtonCutConnection.Pushed = false;
               this.toolBarButtonSelectionSnapshot.Pushed = false;
            }
         }
         else if (flowsheetActivity == FlowsheetActivity.DeletingConnection)
         {
            if (!this.toolBarButtonCutConnection.Pushed)
            {
               this.toolBarButtonCutConnection.Pushed = true;
               this.toolBarButtonAddConnection.Pushed = false;
               this.toolBarButtonSelectionSnapshot.Pushed = false;
            }
         }
         else if (flowsheetActivity == FlowsheetActivity.SelectingSnapshot)
         {
            if (!this.toolBarButtonSelectionSnapshot.Pushed)
            {
               this.toolBarButtonSelectionSnapshot.Pushed = true;
               this.toolBarButtonCutConnection.Pushed = false;
               this.toolBarButtonAddConnection.Pushed = false;
            }
         }
         else
         {
            this.toolBarButtonSelectionSnapshot.Pushed = false;
            this.toolBarButtonAddConnection.Pushed = false;
            this.toolBarButtonCutConnection.Pushed = false;
         }
      }

      private void flowsheet_SaveFlowsheet(Flowsheet flowsheet)
      {
         this.SaveFlowsheet();
      }

      private void menuItemNewFlowsheet_Click(object sender, System.EventArgs e)
      {
         this.NewFlowsheet();
      }

      private void menuItemOpenFlowsheet_Click(object sender, System.EventArgs e)
      {
         this.OpenFlowsheet();
      }

      private void menuItemSaveFlowsheet_Click(object sender, System.EventArgs e)
      {
         this.SaveFlowsheet();
      }

      private void menuItemSaveAsFlowsheet_Click(object sender, System.EventArgs e)
      {
         this.SaveAsFlowsheet();
      }

      private void menuItemCloseFlowsheet_Click(object sender, System.EventArgs e)
      {
         this.CloseFlowsheet();
      }

      private void menuItemExit_Click(object sender, System.EventArgs e)
      {
         if (this.Exit())
         {
            this.Close();
            Application.Exit();
         }
      }

      private void menuItemEditSys_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.EditSystem();
      }

      private void menuItemDelete_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.DeleteSelectedSolvables();
      }

      private void menuItemSelectAll_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.SetSolvableControlsSelection(true);
      }

      private void menuItemViewToolbox_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            if (this.menuItemViewToolbox.Checked)
               this.flowsheet.Toolbox.Close();
            else
               this.flowsheet.ShowToolbox(this.ToolboxLocation);
         }
      }

      private void menuItemViewUnitSys_Click(object sender, System.EventArgs e)
      {
         this.UnitSystems();
      }

      private void menuItemViewHumidityChart_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.CreateHumidityChart();
         }
      }

      private void menuItemAddHeatExchanger_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddHeatExchanger();
         }
      }

      private void menuItemAddCyclone_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddCyclone();
         }
      }

      private void menuItemAddEjector_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddEjector();
         }
      }

      private void menuItemAddWetScrubber_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddWetScrubber();
         }
      }

      private void menuItemAddFlashTank_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddFlashTank();
         }
      }

      private void menuItemAddBagFilter_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddBagFilter();
         }
      }

      private void menuItemAddCompressor_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddCompressor();
         }
      }

      private void menuItemAddCooler_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddCooler();
         }
      }

      private void menuItemAddElectrostaticPrecipitator_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddElectrostaticPrecipitator();
         }
      }

      private void menuItemAddFan_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddFan();
         }
      }

      private void menuItemAddValve_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddValve();
         }
      }

      private void menuItemAddHeater_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddHeater();
         }
      }

      private void menuItemAddPump_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddPump();
         }
      }

      private void menuItemAddMediumStream_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddGasStream();
         }
      }

      private void menuItemRotateClockwise_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.StreamManager.RotateStreamControls(RotationDirection.Clockwise);
         }
      }

      private void menuItemRotateCounterclockwise_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.StreamManager.RotateStreamControls(RotationDirection.Counterclockwise);
         }
      }

      private void menuItemCustomEditor_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.ShowCustomEditor();
      }

      private void menuItemFormulaEditor_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.ShowFormulaEditor();
      }

      private void menuItemAddAirFilter_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddAirFilter();
         }
      }

      private void menuItemAddMixer_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddMixer();
         }
      }

      private void menuItemAddTee_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddTee();
         }
      }

      private void Print()
      {
         if (this.flowsheet != null)
         {
            int w = this.flowsheet.ClientRectangle.Width;
            int h = this.flowsheet.ClientRectangle.Height;
            this.flowsheet.CaptureImage(0,0,w,h);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = this.printDocument;
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
               try
               {
                  this.printDocument.PrinterSettings = printDialog.PrinterSettings;
                  this.printDocument.Print();
               }
               catch (Win32Exception)
               {
               }
            }
         }
      }

      private void PageSetup()
      {
         if (this.flowsheet != null)
         {
            UI.PageSetup(this.printDocument);
         }
      }

      private void PrintPreview()
      {
         if (this.flowsheet != null)
         {
            try
            {
               int w = this.flowsheet.ClientRectangle.Width;
               int h = this.flowsheet.ClientRectangle.Height;
               this.flowsheet.CaptureImage(0,0,w,h);
            
               Icon icon = (Icon)this.Icon.Clone();
               PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
               (printPreviewDialog as Form).Text = "Print Preview";  
               (printPreviewDialog as Form).Icon = icon;
               printPreviewDialog.Document = this.printDocument;
               printPreviewDialog.Document.DefaultPageSettings.PrinterSettings.PrintToFile = false;
               printPreviewDialog.Document.PrinterSettings.PrintToFile = false;
               printPreviewDialog.ShowDialog();
            }
            catch (InvalidPrinterException)
            {
            }
         }
      }

      private void menuItemPageSetup_Click(object sender, System.EventArgs e)
      {
         this.PageSetup();
      }

      private void menuItemPrintPreview_Click(object sender, System.EventArgs e)
      {
         this.PrintPreview();
      }

      private void menuItemPrint_Click(object sender, System.EventArgs e)
      {
         this.Print();
      }

      private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
      {
         e.Graphics.DrawImage(this.flowsheet.Image, 0, 0);
      }

      private void menuItemSnapshotFlowsheet_Click(object sender, System.EventArgs e)
      {
         this.FlowsheetSnapshot();
      }

      private void FlowsheetSnapshot()
      {
         if (this.flowsheet != null)
         {
            int w = this.flowsheet.ClientRectangle.Width;
            int h = this.flowsheet.ClientRectangle.Height;
            this.flowsheet.CaptureImage(0,0,w,h);
            this.SaveFlowsheetImage();
         }
      }

      private void menuItemSnapshotSelection_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.SelectSnapshot();
      }

      private void flowsheet_SnapshotTaken(Bitmap image)
      {
         this.SaveFlowsheetImage();
      }

      private void SaveFlowsheetImage()
      {
         this.saveFileDialog.Filter = "JPEG|*.jpg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tiff";
         this.saveFileDialog.Title = "Save Image";
         System.Windows.Forms.DialogResult dr = this.saveFileDialog.ShowDialog();
         if (dr == System.Windows.Forms.DialogResult.OK) 
         {
            if (this.saveFileDialog.FileName != null && this.saveFileDialog.FileName.Length > 0)
            {
               FileStream fs = (FileStream)this.saveFileDialog.OpenFile();
               switch(this.saveFileDialog.FilterIndex)
               {
                  case 1 :
                     this.flowsheet.Image.Save(fs, ImageFormat.Jpeg);
                     break;
                  case 2 : 
                     this.flowsheet.Image.Save(fs, ImageFormat.Bmp);
                     break;
                  case 3 : 
                     this.flowsheet.Image.Save(fs, ImageFormat.Gif);
                     break;
                  case 4 : 
                     this.flowsheet.Image.Save(fs, ImageFormat.Png);
                     break;
                  case 5 : 
                     this.flowsheet.Image.Save(fs, ImageFormat.Tiff);
                     break;
               }
               fs.Close();
            }
         }
         this.flowsheet.ResetActivity();
         this.flowsheet.Invalidate();
      }

      private void MainForm_Load(object sender, System.EventArgs e)
      {
         // Create a new thread from which to start the splash screen form
         Thread splashThread = new Thread(new ThreadStart(StartSplash));
         splashThread.Start();

         while (MainForm.splash == null)
         {
            Thread.Sleep(10);
         }

         // Initialize the application
         this.exePathName = Application.StartupPath + Path.DirectorySeparatorChar;
         MainForm.splash.DisplayMessage("Loading the application...");    
         Thread.Sleep(3000);

         // Do here all the initialization!!!
         this.Text = Application.ProductName;

         // restore the unit system catalog
         string usFileName = this.exePathName + MainForm.USCAT_XML;
         string bakUsFileName = this.exePathName + MainForm.USCAT_BAK;
         PersistenceManager.GetInstance().UnpersistUnitSystemCatalog(usFileName, bakUsFileName);

         // restore the material catalog
         string matFileName = this.exePathName + MainForm.MATCAT_XML;
         string bakMatFileName = this.exePathName + MainForm.MATCAT_BAK;
         PersistenceManager.GetInstance().UnpersistMaterialCatalog(matFileName, bakMatFileName);
         
         // restore the app preferences
         PersistenceManager.GetInstance().UnpersistAppPreferences(this);

         MainForm.splash.DisplayMessage("Application loaded");         

         CloseSplash();

         // NOTE:
         // Here we check the lease. If we don't want to check the lease then
         // comment out that code and uncomment the next two lines:
         // this.Activate();
         // this.NewFlowsheet();

         // start lease check
         SoftwareProtectionManager spm = new SoftwareProtectionManager();
         if (spm.ReadyToRun)
         {
            this.lease = spm.Lease;
            this.Activate();
            this.NewFlowsheet();
         }
         else
         {
            ActivationForm aif = new ActivationForm();
            aif.Message = spm.Message;
            aif.ShowDialog();
            this.Close();
            Application.Exit();
         }
         // end lease check
      }

      /// <summary>
      /// Paint the form only if the splash screen is gone
      /// </summary>
      /// <param name="e">Paint event arguments</param>
      protected override void OnPaint(PaintEventArgs e)
      {
         if (splash != null)
            return;

         base.OnPaint (e);
      }

      /// <summary>
      /// Paint the form background only if the splash screen is gone
      /// </summary>
      /// <param name="e">Paint event arguments</param>
      protected override void OnPaintBackground(PaintEventArgs e)
      {
         if (splash != null)
            return;

         base.OnPaintBackground (e);
      }

      /// <summary>
      /// Shuts down and cleans up the splash screen
      /// </summary>
      private void CloseSplash()
      {
         if (splash == null)
            return;

         // Shut down the splash screen
         splash.Invoke(new EventHandler(splash.KillMe));
         splash.Dispose();
         splash = null;
      }

      private void menuItemAddRecycle_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddRecycle();
         }
      }

      private void menuItemConnect_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.AddConnection();
      }

      private void menuItemDisconnect_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
            this.flowsheet.CutConnection();
      }

      private void userManualForm_Closed(object sender, EventArgs e)
      {
         this.userManualForm.Closed -= new EventHandler(userManualForm_Closed);
         this.userManualForm = null;
      }

      private void tutorialForm_Closed(object sender, EventArgs e)
      {
         this.tutorialForm.Closed -= new EventHandler(tutorialForm_Closed);
         this.tutorialForm = null;
      }

      private void flowsheet_ToolboxAliveChanged(bool alive)
      {
         this.toolBarButtonToolbox.Pushed = this.menuItemViewToolbox.Checked = alive;
      }

      private void flowsheet_ToolboxLocationChanged(Point location)
      {
         this.ToolboxLocation = location;
      }

      private void flowsheet_ToolboxVisibleChanged(bool visible)
      {
         this.toolboxVisible = visible;
      }

      private void menuItemUnitConverter_Click(object sender, System.EventArgs e)
      {
         this.ShowUnitConverter();
      }

      private void ShowUnitConverter()
      {
         UnitConverterForm unitConverter = new UnitConverterForm();
         unitConverter.ShowDialog();
      }

      private void menuItemFind_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.ShowFindForm();
         }
      }

      private void menuItemTutorials_Click(object sender, System.EventArgs e)
      {
         if (this.chooseTutorialForm == null)
         {
            this.chooseTutorialForm = new ChooseTutorialForm();
            this.chooseTutorialForm.Closed += new EventHandler(chooseTutorialForm_Closed);
            this.chooseTutorialForm.Show();
         }
         else
         {
            if (this.chooseTutorialForm.WindowState.Equals(FormWindowState.Minimized))
               this.chooseTutorialForm.WindowState = FormWindowState.Normal;
            this.chooseTutorialForm.Activate();
         }
      }

      private void chooseTutorialForm_Closed(object sender, EventArgs e)
      {
         ChooseTutorialForm chooseTutorialForm = sender as ChooseTutorialForm;
         chooseTutorialForm.Closed -= new EventHandler(chooseTutorialForm_Closed);
         if (chooseTutorialForm.TutorialFile != null)
            this.ShowTutorial(chooseTutorialForm.TutorialName, chooseTutorialForm.TutorialFile);
         this.chooseTutorialForm = null;
      }

      private void menuItemDryingMaterials_Click(object sender, System.EventArgs e)
      {
         DryingMaterialsForm form = new DryingMaterialsForm();
         form.ShowDialog();
      }

      private void menuItemNewSystemPrefs_Click(object sender, System.EventArgs e)
      {
         NewSystemPreferencesForm form = new NewSystemPreferencesForm(this.newSystemPrefs);
         form.ShowDialog();
      }

      private void menuItemDryingGases_Click(object sender, System.EventArgs e)
      {
         // TO DO
      }

      private void menuItemSubstances_Click(object sender, System.EventArgs e)
      {
         // TODO
      }

      private void menuItemPlots_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            if (this.flowsheet.EvaporationAndDryingSystem.IsSolved)
            {
               PlotsForm pf = new PlotsForm(this.flowsheet.EvaporationAndDryingSystem);
               pf.ShowDialog();
            }
            else
            {
               string message = "The system is not solved! The Plots can not be used in this case.";
               ErrorMessage error = new ErrorMessage(ErrorType.SimpleGeneric, "Plots Warning", message);
               UI.ShowError(error);
            }
         }
      }

      private void menuItemAddLiquidMaterialStream_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddLiquidMaterialStream();
         }
      }

      private void menuItemAddSolidMaterialStream_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddSolidMaterialStream();
         }
      }

      private void menuItemAddSolidMatDryer_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddSolidDryer();
         }
      }

      private void menuItemAddLiquidMatDryer_Click(object sender, System.EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.AddLiquidDryer();
         }
      }
   }
}
