using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
   /// <summary>
   /// Summary description for ElectrostaticPrecipitatorEditor.
   /// </summary>
   public class ElectrostaticPrecipitatorEditor : TwoStreamUnitOpEditor
   {
      public ElectrostaticPrecipitatorControl ElectrostaticPrecipitatorCtrl
      {
         get {return (ElectrostaticPrecipitatorControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ElectrostaticPrecipitatorEditor(ElectrostaticPrecipitatorControl electrostaticPrecipitatorCtrl) : base(electrostaticPrecipitatorCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         ElectrostaticPrecipitator electrostaticPrecipitator = this.ElectrostaticPrecipitatorCtrl.ElectrostaticPrecipitator;
         this.Text = "Electrostatic Precipitator: " + electrostaticPrecipitator.Name;
         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 200);
         this.groupBoxTwoStreamUnitOp.Text = "Electrostatic Precipitator";

         ProcessVarLabelsControl electrostaticPrecipitatorLabelsCtrl = new ProcessVarLabelsControl(this.ElectrostaticPrecipitatorCtrl.ElectrostaticPrecipitator.VarList);
         this.groupBoxTwoStreamUnitOp.Controls.Add(electrostaticPrecipitatorLabelsCtrl);
         electrostaticPrecipitatorLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ProcessVarValuesControl electrostaticPrecipitatorValuesCtrl = new ProcessVarValuesControl(this.ElectrostaticPrecipitatorCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(electrostaticPrecipitatorValuesCtrl);
         electrostaticPrecipitatorValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
         this.Text = "Electrostatic Precipitator";
      }
      #endregion

   }
}
