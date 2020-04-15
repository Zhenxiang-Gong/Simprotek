using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ClassLibrary1;

namespace ProtectTestUI
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();

         Class1 class1 = new Class1("Hello world!");
         this.textBox1.Text = class1.Name;
      }
   }
}