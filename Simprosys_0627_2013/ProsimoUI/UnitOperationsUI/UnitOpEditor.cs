using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Text;
using System.IO;

using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for UnitOpEditor.
   /// </summary>
   public class UnitOpEditor : SolvableEditor {
      protected const int INDEX_BALANCE = 0;
      protected const int INDEX_RATING = 1;

      public UnitOpControl UnitOpCtrl {
         get { return (UnitOpControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      private Font printFont;
      private int pageNumber;
      private StringReader stringReader;

      protected System.Windows.Forms.MenuItem menuItemReport;
      protected System.Windows.Forms.MenuItem menuItemPageSetup;
      protected System.Windows.Forms.MenuItem menuItemPrintPreview;
      protected System.Windows.Forms.MenuItem menuItemPrint;
      protected System.Windows.Forms.MenuItem menuItemSave;
      private System.Drawing.Printing.PrintDocument printDocument;
      private System.Windows.Forms.SaveFileDialog saveFileDialog;

      public UnitOpEditor() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public UnitOpEditor(UnitOpControl unitOpCtrl)
         : base(unitOpCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.menuItemReport = new System.Windows.Forms.MenuItem();
         this.menuItemPageSetup = new System.Windows.Forms.MenuItem();
         this.menuItemPrintPreview = new System.Windows.Forms.MenuItem();
         this.menuItemPrint = new System.Windows.Forms.MenuItem();
         this.menuItemSave = new System.Windows.Forms.MenuItem();
         this.printDocument = new System.Drawing.Printing.PrintDocument();
         this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();

         // 
         // menuItemReport
         // 
         this.menuItemReport.Index = 1;
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

         this.mainMenu.MenuItems.Add(this.menuItemReport);

         // 
         // printDocument
         // 
         this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);

         unitOpCtrl.UnitOperation.StreamAttached += new StreamAttachedEventHandler(UnitOp_StreamAttached);
         unitOpCtrl.UnitOperation.StreamDetached += new StreamDetachedEventHandler(UnitOp_StreamDetached);
      }

      protected virtual void UnitOp_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamsUI();
      }

      protected virtual void UnitOp_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamsUI();
      }

      protected virtual void UpdateStreamsUI() {
      }

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         UnitOperation unitOp = UnitOpCtrl.UnitOperation;
         TextBox tb = (TextBox)sender;
         if(tb.Text != null)
         {
            if(tb.Text.Trim().Equals(""))
            {
               if(sender == this.textBoxName)
               {
                  e.Cancel = true;
                  string message3 = "Please specify a name!";
                  MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
            else
            {
               if(sender == this.textBoxName)
               {
                  ErrorMessage error = unitOp.SpecifyName(this.textBoxName.Text);
                  if(error != null)
                     UI.ShowError(error);
               }
            }
         }
      }
      
      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (UnitOpCtrl.UnitOperation != null) {
            UnitOpCtrl.UnitOperation.StreamAttached -= new StreamAttachedEventHandler(UnitOp_StreamAttached);
            UnitOpCtrl.UnitOperation.StreamDetached -= new StreamDetachedEventHandler(UnitOp_StreamDetached);
         }
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitOpEditor));
         this.SuspendLayout();
         // 
         // UnitOpEditor
         // 
         this.ClientSize = new System.Drawing.Size(292, 266);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "UnitOpEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "UnitOpEditor";
         this.ResumeLayout(false);

      }
      #endregion

      private void menuItemPageSetup_Click(object sender, System.EventArgs e) {
         UI.PageSetup(this.printDocument);
      }

      private void menuItemPrintPreview_Click(object sender, System.EventArgs e) {
         try {
            try {
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
            catch (InvalidPrinterException) {
            }
         }
         finally {
            if (this.stringReader != null)
               this.stringReader.Close();
         }
      }

      private void PrepareToPrint() {
         StringBuilder sb = new StringBuilder();
         sb.Append("Unit Operation: " + this.UnitOpCtrl.UnitOperation.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE2);
         sb.Append("\r\n");
         sb.Append("\r\n");

         IEnumerator en = this.UnitOpCtrl.UnitOperation.InOutletStreams.GetEnumerator();
         while (en.MoveNext()) {
            ProcessStreamBase psb = (ProcessStreamBase)en.Current;
            ProcessStreamBaseControl psbCtrl = this.UnitOpCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(psb.Name);
            sb.Append(psbCtrl.ToPrint());
            sb.Append("\r\n");
         }

         sb.Append(this.UnitOpCtrl.ToPrint());
         sb.Append("\r\n");

         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         sb.Append("\r\n");
         sb.Append("* Specified Values");

         this.printFont = new Font("Arial", 10);
         this.pageNumber = 1;
         this.stringReader = new StringReader(sb.ToString());
      }

      private void menuItemPrint_Click(object sender, System.EventArgs e) {
         try {
            this.PrepareToPrint();
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = this.printDocument;
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK) {
               try {
                  this.printDocument.PrinterSettings = printDialog.PrinterSettings;
                  this.printDocument.Print();
               }
               catch (Win32Exception) {
               }
            }
         }
         finally {
            if (this.stringReader != null)
               this.stringReader.Close();
         }
      }

      private void menuItemSave_Click(object sender, System.EventArgs e) {
         this.saveFileDialog.Filter = "TXT|*.txt";
         this.saveFileDialog.AddExtension = true;
         this.saveFileDialog.DefaultExt = "txt";
         this.saveFileDialog.Title = "Save to File";
         System.Windows.Forms.DialogResult dr = this.saveFileDialog.ShowDialog();
         if (dr == System.Windows.Forms.DialogResult.OK) {
            if (this.saveFileDialog.FileName != null && this.saveFileDialog.FileName.Length > 0) {
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

      private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
         float linesPerPage = 0;
         float yPos = 0;
         int count = 0;
         float leftMargin = e.MarginBounds.Left;
         float topMargin = e.MarginBounds.Top;
         string line = null;

         // Calculate the number of lines per page.
         linesPerPage = e.MarginBounds.Height / this.printFont.GetHeight(e.Graphics);

         // Print each line of the file.
         while (count < linesPerPage - 2 && ((line = this.stringReader.ReadLine()) != null)) {
            yPos = topMargin + (count * this.printFont.GetHeight(e.Graphics));
            e.Graphics.DrawString(line, this.printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            count++;
         }

         // print the page #
         string pageNumberStr = "Page " + this.pageNumber.ToString();
         yPos = topMargin + ((linesPerPage - 1) * this.printFont.GetHeight(e.Graphics));
         e.Graphics.DrawString("", this.printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
         yPos = topMargin + (linesPerPage * this.printFont.GetHeight(e.Graphics));
         e.Graphics.DrawString(pageNumberStr, this.printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
         this.pageNumber++;

         // If more lines exist, print another page.
         if (line != null)
            e.HasMorePages = true;
         else
            e.HasMorePages = false;
      }
   }
}
