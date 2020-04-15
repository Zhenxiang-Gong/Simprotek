using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;

using Prosimo.UnitOperations;
using ProsimoUI.ProcessStreamsUI;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;
using Prosimo.UnitSystems;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo;

namespace ProsimoUI.GlobalEditor
{
	/// <summary>
	/// Summary description for SystemEditor.
	/// </summary>
	public class SystemEditor : System.Windows.Forms.Form
	{
      private const string EVAPORATION_DRYING_SYSTEM = "Edit Flowsheet Data";

      private GasStreamsEditor gasStreamsEditor;
      private MaterialStreamsEditor materialStreamsEditor;
      private AirFiltersEditor airFiltersEditor;
      private BagFiltersEditor bagFiltersEditor;
      private CompressorsEditor compressorsEditor;
      private CoolersEditor coolersEditor;
      private ElectrostaticPrecipitatorsEditor electrostaticPrecipitatorsEditor;
      private CyclonesEditor cyclonesEditor;
      private EjectorsEditor ejectorsEditor;
      private WetScrubbersEditor wetScrubbersEditor;
      private ScrubberCondensersEditor scrubberCondensersEditor;
      private DryersEditor dryersEditor;
      private FansEditor fansEditor;
      private HeatersEditor heatersEditor;
      private HeatExchangersEditor heatExchangersEditor;
      private PumpsEditor pumpsEditor;
      private ValvesEditor valvesEditor;

      private Flowsheet flowsheet;
      private int y;

      private Font printFont;
      private int pageNumber;
      private StringReader stringReader;

      private System.Windows.Forms.StatusBar statusBar;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panelUnitOps;
      private System.Windows.Forms.Splitter splitterStreams_UnitOps;
      private System.Windows.Forms.Panel panelStreams;
      private System.Windows.Forms.StatusBarPanel statusBarPanel;
      private System.Windows.Forms.MenuItem menuItemPageSetup;
      private System.Windows.Forms.MenuItem menuItemPrintPreview;
      private System.Windows.Forms.MenuItem menuItemPrint;
      private System.Drawing.Printing.PrintDocument printDocument;
      private System.Windows.Forms.SaveFileDialog saveFileDialog;
      private System.Windows.Forms.MenuItem menuItemReport;
      private System.Windows.Forms.MenuItem menuItemSave;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItemName;
      private MenuItem menuItemCustomize;
      private IContainer components;

		public SystemEditor(Flowsheet flowsheet)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.flowsheet = flowsheet;
         this.UpdateStreamsUI();
         this.UpdateUnitOpsUI();
         
         this.Text = SystemEditor.EVAPORATION_DRYING_SYSTEM;
         this.flowsheet.EvaporationAndDryingSystem.NameChanged += new NameChangedEventHandler(EvaporationAndDryingSystem_NameChanged);

         this.flowsheet.EvaporationAndDryingSystem.StreamAdded += new StreamAddedEventHandler(EvaporationAndDryingSystem_StreamAdded);
         this.flowsheet.EvaporationAndDryingSystem.StreamDeleted += new StreamDeletedEventHandler(EvaporationAndDryingSystem_StreamDeleted);

         this.flowsheet.EvaporationAndDryingSystem.UnitOpAdded += new UnitOpAddedEventHandler(EvaporationAndDryingSystem_UnitOpAdded);
         this.flowsheet.EvaporationAndDryingSystem.UnitOpDeleted += new UnitOpDeletedEventHandler(EvaporationAndDryingSystem_UnitOpDeleted);

         this.ResizeEnd += new EventHandler(SystemEditor_ResizeEnd);
      }

      void SystemEditor_ResizeEnd(object sender, EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.ConnectionManager.DrawConnections();
         }
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.flowsheet.EvaporationAndDryingSystem != null)
         {
            this.flowsheet.EvaporationAndDryingSystem.NameChanged -= new NameChangedEventHandler(EvaporationAndDryingSystem_NameChanged);

            this.flowsheet.EvaporationAndDryingSystem.StreamAdded -= new StreamAddedEventHandler(EvaporationAndDryingSystem_StreamAdded);
            this.flowsheet.EvaporationAndDryingSystem.StreamDeleted -= new StreamDeletedEventHandler(EvaporationAndDryingSystem_StreamDeleted);

            this.flowsheet.EvaporationAndDryingSystem.UnitOpAdded -= new UnitOpAddedEventHandler(EvaporationAndDryingSystem_UnitOpAdded);
            this.flowsheet.EvaporationAndDryingSystem.UnitOpDeleted -= new UnitOpDeletedEventHandler(EvaporationAndDryingSystem_UnitOpDeleted);
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemEditor));
         this.statusBar = new System.Windows.Forms.StatusBar();
         this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItemName = new System.Windows.Forms.MenuItem();
         this.menuItemReport = new System.Windows.Forms.MenuItem();
         this.menuItemPageSetup = new System.Windows.Forms.MenuItem();
         this.menuItemPrintPreview = new System.Windows.Forms.MenuItem();
         this.menuItemPrint = new System.Windows.Forms.MenuItem();
         this.menuItemSave = new System.Windows.Forms.MenuItem();
         this.panelUnitOps = new System.Windows.Forms.Panel();
         this.splitterStreams_UnitOps = new System.Windows.Forms.Splitter();
         this.panelStreams = new System.Windows.Forms.Panel();
         this.printDocument = new System.Drawing.Printing.PrintDocument();
         this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
         this.menuItemCustomize = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.SuspendLayout();
         // 
         // statusBar
         // 
         this.statusBar.Location = new System.Drawing.Point(0, 563);
         this.statusBar.Name = "statusBar";
         this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
         this.statusBar.ShowPanels = true;
         this.statusBar.Size = new System.Drawing.Size(660, 22);
         this.statusBar.TabIndex = 3;
         // 
         // statusBarPanel
         // 
         this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
         this.statusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
         this.statusBarPanel.Name = "statusBarPanel";
         this.statusBarPanel.Width = 643;
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemClose,
            this.menuItem1,
            this.menuItemReport,
            this.menuItemCustomize});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // menuItem1
         // 
         this.menuItem1.Enabled = false;
         this.menuItem1.Index = 1;
         this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemName});
         this.menuItem1.Text = "Edit";
         this.menuItem1.Visible = false;
         // 
         // menuItemName
         // 
         this.menuItemName.Enabled = false;
         this.menuItemName.Index = 0;
         this.menuItemName.Text = "Name...";
         this.menuItemName.Click += new System.EventHandler(this.menuItemName_Click);
         // 
         // menuItemReport
         // 
         this.menuItemReport.Index = 2;
         this.menuItemReport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemPageSetup,
            this.menuItemPrintPreview,
            this.menuItemPrint,
            this.menuItemSave});
         this.menuItemReport.Text = "Report";
         // 
         // menuItemPageSetup
         // 
         this.menuItemPageSetup.Index = 0;
         this.menuItemPageSetup.Text = "Page Setup...";
         this.menuItemPageSetup.Click += new System.EventHandler(this.menuItemPageSetup_Click);
         // 
         // menuItemPrintPreview
         // 
         this.menuItemPrintPreview.Index = 1;
         this.menuItemPrintPreview.Text = "Print Preview...";
         this.menuItemPrintPreview.Click += new System.EventHandler(this.menuItemPrintPreview_Click);
         // 
         // menuItemPrint
         // 
         this.menuItemPrint.Index = 2;
         this.menuItemPrint.Text = "Print...";
         this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
         // 
         // menuItemSave
         // 
         this.menuItemSave.Index = 3;
         this.menuItemSave.Text = "Save...";
         this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
         // 
         // panelUnitOps
         // 
         this.panelUnitOps.AutoScroll = true;
         this.panelUnitOps.BackColor = System.Drawing.SystemColors.Control;
         this.panelUnitOps.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelUnitOps.Dock = System.Windows.Forms.DockStyle.Right;
         this.panelUnitOps.Location = new System.Drawing.Point(364, 0);
         this.panelUnitOps.Name = "panelUnitOps";
         this.panelUnitOps.Size = new System.Drawing.Size(296, 563);
         this.panelUnitOps.TabIndex = 4;
         // 
         // splitterStreams_UnitOps
         // 
         this.splitterStreams_UnitOps.BackColor = System.Drawing.SystemColors.Control;
         this.splitterStreams_UnitOps.Dock = System.Windows.Forms.DockStyle.Right;
         this.splitterStreams_UnitOps.Location = new System.Drawing.Point(362, 0);
         this.splitterStreams_UnitOps.MinExtra = 0;
         this.splitterStreams_UnitOps.MinSize = 0;
         this.splitterStreams_UnitOps.Name = "splitterStreams_UnitOps";
         this.splitterStreams_UnitOps.Size = new System.Drawing.Size(2, 563);
         this.splitterStreams_UnitOps.TabIndex = 5;
         this.splitterStreams_UnitOps.TabStop = false;
         // 
         // panelStreams
         // 
         this.panelStreams.AutoScroll = true;
         this.panelStreams.BackColor = System.Drawing.SystemColors.Control;
         this.panelStreams.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelStreams.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelStreams.Location = new System.Drawing.Point(0, 0);
         this.panelStreams.Name = "panelStreams";
         this.panelStreams.Size = new System.Drawing.Size(362, 563);
         this.panelStreams.TabIndex = 6;
         // 
         // printDocument
         // 
         this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
         // 
         // menuItemCustomize
         // 
         this.menuItemCustomize.Index = 3;
         this.menuItemCustomize.Text = "Customize...";
         this.menuItemCustomize.Click += new System.EventHandler(this.menuItemCustomize_Click);
         // 
         // SystemEditor
         // 
         this.ClientSize = new System.Drawing.Size(660, 585);
         this.Controls.Add(this.panelStreams);
         this.Controls.Add(this.splitterStreams_UnitOps);
         this.Controls.Add(this.panelUnitOps);
         this.Controls.Add(this.statusBar);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "SystemEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
         // we need to explicitly call Dispose() because when we delete
         // all the solvables on the flowsheet, Dispose() is not called
         // by Close()
         this.Dispose(true);
      }

      private void menuItemPageSetup_Click(object sender, System.EventArgs e)
      {
         UI.PageSetup(this.printDocument);
      }

      private void menuItemPrintPreview_Click(object sender, System.EventArgs e)
      {
         try
         {
            try
            {
               this.PrepareToPrint();
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
         finally 
         {
            if (this.stringReader != null)
               this.stringReader.Close();
         }
      }

      private void menuItemPrint_Click(object sender, System.EventArgs e)
      {
         try
         {
            this.PrepareToPrint();
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
         finally 
         {
            if (this.stringReader != null)
               this.stringReader.Close();
         }
      }

      private void PrepareToPrint()
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(""); // TO DO: put a title
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE2);
         sb.Append("\r\n");
         sb.Append("\r\n");

         IList list = this.flowsheet.StreamManager.GetShowableInEditorStreamControls<DryingGasStream>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.StreamManager.GetShowableInEditorStreamControls<DryingMaterialStream>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<AirFilter>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<BagFilter>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Compressor>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Cooler>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<ElectrostaticPrecipitator>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Cyclone>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Ejector>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<WetScrubber>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<ScrubberCondenser>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Dryer>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Heater>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Fan>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Pump>();
         this.AddListToStringBuilder(list, sb);

         list = this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Valve>();
         this.AddListToStringBuilder(list, sb);

         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         sb.Append("\r\n");
         sb.Append("* Specified Values");

         this.printFont = new Font("Arial", 10);
         this.pageNumber = 1;
         this.stringReader = new StringReader(sb.ToString());
      }

      private void AddListToStringBuilder(IList list, StringBuilder sb)
      {
         IEnumerator en = list.GetEnumerator();
         while (en.MoveNext())
         {
            IPrintable printable = (IPrintable)en.Current;
            sb.Append(printable.ToPrint());
            sb.Append("\r\n");
         }
      }

      private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
      {
         float linesPerPage = 0;
         float yPos = 0;
         int count = 0;
         float leftMargin = e.MarginBounds.Left;
         float topMargin = e.MarginBounds.Top;
         string line = null;

         // Calculate the number of lines per page.
         linesPerPage = e.MarginBounds.Height / this.printFont.GetHeight(e.Graphics);

         // Print each line of the file.
         while(count < linesPerPage - 2 && ((line = this.stringReader.ReadLine()) != null)) 
         {
            yPos = topMargin + (count * this.printFont.GetHeight(e.Graphics));
            e.Graphics.DrawString(line, this.printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            count++;
         }

         // print the page #
         string pageNumberStr = "Page " + this.pageNumber.ToString();
         yPos = topMargin + ((linesPerPage - 1) * this.printFont.GetHeight(e.Graphics));
         e.Graphics.DrawString("", this.printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
         yPos = topMargin + (linesPerPage * this.printFont.GetHeight(e.Graphics));
         e.Graphics.DrawString(pageNumberStr , this.printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
         this.pageNumber++;

         // If more lines exist, print another page.
         if(line != null)
            e.HasMorePages = true;
         else
            e.HasMorePages = false;
      }

      private void menuItemName_Click(object sender, System.EventArgs e)
      {
         SystemNameForm snf = new SystemNameForm(this.flowsheet.EvaporationAndDryingSystem);
         snf.ShowDialog();
      }

      private void EvaporationAndDryingSystem_NameChanged(object sender, string name, string oldName)
      {
         // future
      }

      private void menuItemSave_Click(object sender, System.EventArgs e)
      {
         this.saveFileDialog.Filter = "TXT|*.txt";
         this.saveFileDialog.AddExtension = true;
         this.saveFileDialog.DefaultExt = "txt";
         this.saveFileDialog.Title = "Save to File";
         System.Windows.Forms.DialogResult dr = this.saveFileDialog.ShowDialog();
         if (dr == System.Windows.Forms.DialogResult.OK)
         {
            if (this.saveFileDialog.FileName != null && this.saveFileDialog.FileName.Length > 0)
            {
               this.PrepareToPrint();
               string fileName = this.saveFileDialog.FileName;
               StreamWriter streamWriter = new StreamWriter(fileName, false);
               streamWriter.Write(this.stringReader.ReadToEnd());
               streamWriter.Flush();
               this.stringReader.Close();
               streamWriter.Close();
            }
         }
      }

      public void UpdateStreamsUI()
      {
         this.Cursor = Cursors.WaitCursor;
         this.panelStreams.Visible = false;
         this.panelStreams.Controls.Clear();
         this.y = 0;
         
         if (this.flowsheet.StreamManager.HasShowableInEditorStreamControls<DryingGasStream>())
         {
            if (this.gasStreamsEditor == null)
               this.gasStreamsEditor = new GasStreamsEditor(this.flowsheet);
            else
               this.gasStreamsEditor.InitializeUI();
            this.gasStreamsEditor.Location = new Point(0, y);
            this.panelStreams.Controls.Add(this.gasStreamsEditor);
            y += this.gasStreamsEditor.Height;
         }
         else
         {
            if (this.gasStreamsEditor != null)
               this.gasStreamsEditor.Dispose();
            this.gasStreamsEditor = null;
         }

         if (this.flowsheet.StreamManager.HasShowableInEditorStreamControls<DryingMaterialStream>())
         {
            if (this.materialStreamsEditor == null)
               this.materialStreamsEditor = new MaterialStreamsEditor(this.flowsheet);
            else
               this.materialStreamsEditor.InitializeUI();
            this.materialStreamsEditor.Location = new Point(0, y);
            this.panelStreams.Controls.Add(this.materialStreamsEditor);
            y += this.materialStreamsEditor.Height;
         }
         else
         {
            if (this.materialStreamsEditor != null)
               this.materialStreamsEditor.Dispose();
            this.materialStreamsEditor = null;
         }

         this.Cursor = Cursors.Default;
         this.panelStreams.Visible = true;
      }

      public void UpdateUnitOpsUI()
      {
         this.Cursor = Cursors.WaitCursor;
         this.panelUnitOps.Visible = false;
         this.panelUnitOps.Controls.Clear();
         this.y = 0;

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<AirFilter>())
         {
            if (this.airFiltersEditor == null)
               this.airFiltersEditor = new AirFiltersEditor(this.flowsheet);
            else
               this.airFiltersEditor.InitializeUI();
            this.airFiltersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.airFiltersEditor);
            y += this.airFiltersEditor.Height;
         }
         else
         {
            if (this.airFiltersEditor != null)
               this.airFiltersEditor.Dispose();
            this.airFiltersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<BagFilter>())
         {
            if (this.bagFiltersEditor == null)
               this.bagFiltersEditor = new BagFiltersEditor(this.flowsheet);
            else
               this.bagFiltersEditor.InitializeUI();
            this.bagFiltersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.bagFiltersEditor);
            y += this.bagFiltersEditor.Height;
         }
         else
         {
            if (this.bagFiltersEditor != null)
               this.bagFiltersEditor.Dispose();
            this.bagFiltersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Compressor>())
         {
            if (this.compressorsEditor == null)
               this.compressorsEditor = new CompressorsEditor(this.flowsheet);
            else
               this.compressorsEditor.InitializeUI();
            this.compressorsEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.compressorsEditor);
            y += this.compressorsEditor.Height;
         }
         else
         {
            if (this.compressorsEditor != null)
               this.compressorsEditor.Dispose();
            this.compressorsEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Cooler>())
         {
            if (this.coolersEditor == null)
               this.coolersEditor = new CoolersEditor(this.flowsheet);
            else
               this.coolersEditor.InitializeUI();
            this.coolersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.coolersEditor);
            y += this.coolersEditor.Height;
         }
         else
         {
            if (this.coolersEditor != null)
               this.coolersEditor.Dispose();
            this.coolersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<ElectrostaticPrecipitator>())
         {
            if (this.electrostaticPrecipitatorsEditor == null)
               this.electrostaticPrecipitatorsEditor = new ElectrostaticPrecipitatorsEditor(this.flowsheet);
            else
               this.electrostaticPrecipitatorsEditor.InitializeUI();
            this.electrostaticPrecipitatorsEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.electrostaticPrecipitatorsEditor);
            y += this.electrostaticPrecipitatorsEditor.Height;
         }
         else
         {
            if (this.electrostaticPrecipitatorsEditor != null)
               this.electrostaticPrecipitatorsEditor.Dispose();
            this.electrostaticPrecipitatorsEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Cyclone>())
         {
            if (this.cyclonesEditor == null)
               this.cyclonesEditor = new CyclonesEditor(this.flowsheet);
            else
               this.cyclonesEditor.InitializeUI();
            this.cyclonesEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.cyclonesEditor);
            y += this.cyclonesEditor.Height;
         }
         else
         {
            if (this.cyclonesEditor != null)
               this.cyclonesEditor.Dispose();
            this.cyclonesEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Ejector>())
         {
            if (this.ejectorsEditor == null)
               this.ejectorsEditor = new EjectorsEditor(this.flowsheet);
            else
               this.ejectorsEditor.InitializeUI();
            this.ejectorsEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.ejectorsEditor);
            y += this.ejectorsEditor.Height;
         }
         else
         {
            if (this.ejectorsEditor != null)
               this.ejectorsEditor.Dispose();
            this.ejectorsEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<WetScrubber>())
         {
            if (this.wetScrubbersEditor == null)
               this.wetScrubbersEditor = new WetScrubbersEditor(this.flowsheet);
            else
               this.wetScrubbersEditor.InitializeUI();
            this.wetScrubbersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.wetScrubbersEditor);
            y += this.wetScrubbersEditor.Height;
         }
         else
         {
            if (this.wetScrubbersEditor != null)
               this.wetScrubbersEditor.Dispose();
            this.wetScrubbersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<ScrubberCondenser>())
         {
            if (this.scrubberCondensersEditor == null)
               this.scrubberCondensersEditor = new ScrubberCondensersEditor(this.flowsheet);
            else
               this.scrubberCondensersEditor.InitializeUI();
            this.scrubberCondensersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.scrubberCondensersEditor);
            y += this.scrubberCondensersEditor.Height;
         }
         else
         {
            if (this.scrubberCondensersEditor != null)
               this.scrubberCondensersEditor.Dispose();
            this.scrubberCondensersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Dryer>())
         {
            if (this.dryersEditor == null)
               this.dryersEditor = new DryersEditor(this.flowsheet);
            else
               this.dryersEditor.InitializeUI();
            this.dryersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.dryersEditor);
            y += this.dryersEditor.Height;
         }
         else
         {
            if (this.dryersEditor != null)
               this.dryersEditor.Dispose();
            this.dryersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Fan>())
         {
            if (this.fansEditor == null)
               this.fansEditor = new FansEditor(this.flowsheet);
            else
               this.fansEditor.InitializeUI();
            this.fansEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.fansEditor);
            y += this.fansEditor.Height;
         }
         else
         {
            if (this.fansEditor != null)
               this.fansEditor.Dispose();
            this.fansEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Heater>())
         {
            if (this.heatersEditor == null)
               this.heatersEditor = new HeatersEditor(this.flowsheet);
            else
               this.heatersEditor.InitializeUI();
            this.heatersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.heatersEditor);
            y += this.heatersEditor.Height;
         }
         else
         {
            if (this.heatersEditor != null)
               this.heatersEditor.Dispose();
            this.heatersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<HeatExchanger>())
         {
            if (this.heatExchangersEditor == null)
               this.heatExchangersEditor = new HeatExchangersEditor(this.flowsheet);
            else
               this.heatExchangersEditor.InitializeUI();
            this.heatExchangersEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.heatExchangersEditor);
            y += this.heatExchangersEditor.Height;
         }
         else
         {
            if (this.heatExchangersEditor != null)
               this.heatExchangersEditor.Dispose();
            this.heatExchangersEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Pump>())
         {
            if (this.pumpsEditor == null)
               this.pumpsEditor = new PumpsEditor(this.flowsheet);
            else
               this.pumpsEditor.InitializeUI();
            this.pumpsEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.pumpsEditor);
            y += this.pumpsEditor.Height;
         }
         else
         {
            if (this.pumpsEditor != null)
               this.pumpsEditor.Dispose();
            this.pumpsEditor = null;
         }

         if (this.flowsheet.UnitOpManager.HasShowableInEditorUnitOpControls<Valve>())
         {
            if (this.valvesEditor == null)
               this.valvesEditor = new ValvesEditor(this.flowsheet);
            else
               this.valvesEditor.InitializeUI();
            this.valvesEditor.Location = new Point(0, y);
            this.panelUnitOps.Controls.Add(this.valvesEditor);
            y += this.valvesEditor.Height;
         }
         else
         {
            if (this.valvesEditor != null)
               this.valvesEditor.Dispose();
            this.valvesEditor = null;
         }

         // if the unit op has been created with streams, there are no events fired
         // for their creation, so we are responsible to update the streams panel too!
         if (this.flowsheet.EvaporationAndDryingSystem.Preferences.UnitOpCreationType != UnitOpCreationType.WithoutInputAndOutput)
            this.UpdateStreamsUI();

         this.Cursor = Cursors.Default;
         this.panelUnitOps.Visible = true;
      }

      private void EvaporationAndDryingSystem_StreamAdded(ProcessStreamBase processStream)
      {
         this.UpdateStreamsUI();
      }

      private void EvaporationAndDryingSystem_StreamDeleted(string streamName)
      {
         this.UpdateStreamsUI();
      }

      private void EvaporationAndDryingSystem_UnitOpAdded(UnitOperation unitOp)
      {
         this.UpdateUnitOpsUI();
      }

      private void EvaporationAndDryingSystem_UnitOpDeleted(string streamName)
      {
         this.UpdateUnitOpsUI();
      }

      private void menuItemCustomize_Click(object sender, EventArgs e)
      {
         if (this.flowsheet != null)
         {
            CustomizeFlowsheetForm form = new CustomizeFlowsheetForm(this.flowsheet);
            form.ShowDialog();
         }
      }
   }
}
