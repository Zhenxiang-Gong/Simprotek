namespace Crypto
{
   partial class MainForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.buttonEncode = new System.Windows.Forms.Button();
         this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.buttonDecode = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // buttonEncode
         // 
         this.buttonEncode.Location = new System.Drawing.Point(56, 215);
         this.buttonEncode.Name = "buttonEncode";
         this.buttonEncode.Size = new System.Drawing.Size(75, 23);
         this.buttonEncode.TabIndex = 0;
         this.buttonEncode.Text = "Encode";
         this.buttonEncode.UseVisualStyleBackColor = true;
         this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
         // 
         // openFileDialog
         // 
         this.openFileDialog.FileName = "openFileDialog1";
         // 
         // buttonDecode
         // 
         this.buttonDecode.Location = new System.Drawing.Point(167, 215);
         this.buttonDecode.Name = "buttonDecode";
         this.buttonDecode.Size = new System.Drawing.Size(75, 23);
         this.buttonDecode.TabIndex = 1;
         this.buttonDecode.Text = "Decode";
         this.buttonDecode.UseVisualStyleBackColor = true;
         this.buttonDecode.Click += new System.EventHandler(this.buttonDecode_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(292, 266);
         this.Controls.Add(this.buttonDecode);
         this.Controls.Add(this.buttonEncode);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button buttonEncode;
      private System.Windows.Forms.OpenFileDialog openFileDialog;
      private System.Windows.Forms.Button buttonDecode;
   }
}

