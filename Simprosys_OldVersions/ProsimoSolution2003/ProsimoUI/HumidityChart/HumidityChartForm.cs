using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using ProsimoUI.Plots;
using Prosimo.UnitSystems;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.Drying;
using Prosimo;
using Prosimo.Plots;

namespace ProsimoUI.HumidityChart
{
	/// <summary>
	/// Summary description for HumidityChartForm.
	/// </summary>
   public class HumidityChartForm : System.Windows.Forms.Form, IPlot
   {
      private UnitSystem unitSystem;

      // need to keep this junk for the PhysicalQuantity
      private PlotVariable xPlotVariable;
      private PlotVariable yPlotVariable;
      private ArrayList xPlotVariables;
      private ArrayList yPlotVariables;

      private HumidityChartState currentState;
      private HumidityChartProcess process;
      
      private Flowsheet flowsheet;
      private PsychrometricChartModel psychrometricChartModel;

      private HumidityChartCurrentStateEditor hcCurrentStateEditor; 
      private HumidityChartProcessEditor hcProcessEditor; 

      private StateControl currentStateCtrl;
      private StateControl inStateCtrl;
      private StateControl outStateCtrl;

      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.RadioButton radioButtonProcess;
      private System.Windows.Forms.RadioButton radioButtonState;
      private System.Windows.Forms.RadioButton radioButtonChartOnly;
      private System.Windows.Forms.GroupBox groupBoxHumidityChartType;
      private System.Windows.Forms.GroupBox groupBoxHumidityChartParameter;
      private ProsimoUI.Plots.HCPlotControl plotCtrl;
      private System.Windows.Forms.Panel panel;
      private ProcessVarLabel labelPressureName;
      private ProcessVarTextBox textBoxPressureValue;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public HumidityChartForm(Flowsheet flowsheet, PsychrometricChartModel psychrometricChartModel)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.flowsheet = flowsheet;
         this.unitSystem = UnitSystemService.GetInstance().CurrentUnitSystem;
         this.psychrometricChartModel = psychrometricChartModel;

         this.labelPressureName.InitializeVariable(this.psychrometricChartModel.Pressure);
         this.textBoxPressureValue.InitializeVariable(flowsheet, this.psychrometricChartModel.Pressure);

         this.hcCurrentStateEditor = new HumidityChartCurrentStateEditor(this.flowsheet, psychrometricChartModel.StateStream);
         this.hcCurrentStateEditor.Location = new Point(0, 254);
         this.panel.Controls.Add(this.hcCurrentStateEditor);
         this.hcCurrentStateEditor.Visible = false;
         
         this.hcProcessEditor = new HumidityChartProcessEditor(this.flowsheet, psychrometricChartModel.ProcessInputStream, psychrometricChartModel.ProcessOutputStream); 
         this.hcProcessEditor.Location = new Point(0, 254);
         this.panel.Controls.Add(this.hcProcessEditor);
         this.hcProcessEditor.Visible = false;

         this.psychrometricChartModel.HumidityChartChanged += new HumidityChartChangedEventHandler(psychrometricChartModel_HumidityChartChanged);
         this.psychrometricChartModel.SolveComplete += new SolveCompleteEventHandler(psychrometricChartModel_SolveComplete);

         UnitSystemService.GetInstance().CurrentUnitSystemChanged += new CurrentUnitSystemChangedEventHandler(HumidityChartForm_CurrentUnitSystemChanged);
        
         this.flowsheet.NumericFormatStringChanged += new NumericFormatStringChangedEventHandler(flowsheet_NumericFormatStringChanged);

         // 
         this.currentStateCtrl = new StateControl(this.plotCtrl.Graph);
         this.currentStateCtrl.Visible = false;
         this.currentStateCtrl.MouseDown += new MouseEventHandler(currentStateCtrl_MouseDown);
         this.currentStateCtrl.LocationChanged += new EventHandler(currentStateCtrl_LocationChanged);
         this.currentStateCtrl.MouseUp += new MouseEventHandler(currentStateCtrl_MouseUp);
         this.plotCtrl.Graph.Controls.Add(this.currentStateCtrl);

         this.inStateCtrl = new StateControl(this.plotCtrl.Graph);
         this.inStateCtrl.Visible = false;
         this.inStateCtrl.MouseDown += new MouseEventHandler(inStateCtrl_MouseDown);
         this.inStateCtrl.LocationChanged += new EventHandler(inStateCtrl_LocationChanged);
         this.inStateCtrl.MouseUp += new MouseEventHandler(inStateCtrl_MouseUp);
         this.plotCtrl.Graph.Controls.Add(this.inStateCtrl);

         this.outStateCtrl = new StateControl(this.plotCtrl.Graph);
         this.outStateCtrl.Visible = false;
         this.outStateCtrl.MouseDown += new MouseEventHandler(outStateCtrl_MouseDown);
         this.outStateCtrl.LocationChanged += new EventHandler(outStateCtrl_LocationChanged);
         this.outStateCtrl.MouseUp += new MouseEventHandler(outStateCtrl_MouseUp);
         this.plotCtrl.Graph.Controls.Add(this.outStateCtrl);

         this.plotCtrl.Graph.NumericFormatString = this.flowsheet.NumericFormatString;
         this.plotCtrl.InitializePlotControl(this);

         this.psychrometricChartModel.HCTypeChanged += new HCTypeChangedEventHandler(psychrometricChartModel_HCTypeChanged);
         if (this.psychrometricChartModel.HCType == HCType.ChartOnly)
            this.radioButtonChartOnly.Checked = true;
         else if (this.psychrometricChartModel.HCType == HCType.Process)
            this.radioButtonProcess.Checked = true;
         else if (this.psychrometricChartModel.HCType == HCType.State)
            this.radioButtonState.Checked = true;
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         if (this.psychrometricChartModel != null)
         {
            this.psychrometricChartModel.HumidityChartChanged -= new HumidityChartChangedEventHandler(psychrometricChartModel_HumidityChartChanged);
            this.psychrometricChartModel.SolveComplete -= new SolveCompleteEventHandler(psychrometricChartModel_SolveComplete);
            this.psychrometricChartModel.HCTypeChanged -= new HCTypeChangedEventHandler(psychrometricChartModel_HCTypeChanged);
         }
         UnitSystemService.GetInstance().CurrentUnitSystemChanged -= new CurrentUnitSystemChangedEventHandler(HumidityChartForm_CurrentUnitSystemChanged);
         if (this.flowsheet != null)
            this.flowsheet.NumericFormatStringChanged -= new NumericFormatStringChangedEventHandler(flowsheet_NumericFormatStringChanged);
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HumidityChartForm));
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.textBoxPressureValue = new ProsimoUI.ProcessVarTextBox();
         this.labelPressureName = new ProsimoUI.ProcessVarLabel();
         this.radioButtonProcess = new System.Windows.Forms.RadioButton();
         this.radioButtonState = new System.Windows.Forms.RadioButton();
         this.radioButtonChartOnly = new System.Windows.Forms.RadioButton();
         this.groupBoxHumidityChartType = new System.Windows.Forms.GroupBox();
         this.groupBoxHumidityChartParameter = new System.Windows.Forms.GroupBox();
         this.plotCtrl = new ProsimoUI.Plots.HCPlotControl();
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxHumidityChartType.SuspendLayout();
         this.groupBoxHumidityChartParameter.SuspendLayout();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // textBoxPressureValue
         // 
         this.textBoxPressureValue.Location = new System.Drawing.Point(152, 24);
         this.textBoxPressureValue.Name = "textBoxPressureValue";
         this.textBoxPressureValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxPressureValue.TabIndex = 10;
         this.textBoxPressureValue.Text = "";
         this.textBoxPressureValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPressureValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxPressureValue_KeyUp);
         // 
         // labelPressureName
         // 
         this.labelPressureName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressureName.Location = new System.Drawing.Point(4, 24);
         this.labelPressureName.Name = "labelPressureName";
         this.labelPressureName.Size = new System.Drawing.Size(148, 20);
         this.labelPressureName.TabIndex = 9;
         this.labelPressureName.Text = "PRESSURE";
         this.labelPressureName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // radioButtonProcess
         // 
         this.radioButtonProcess.Location = new System.Drawing.Point(8, 68);
         this.radioButtonProcess.Name = "radioButtonProcess";
         this.radioButtonProcess.Size = new System.Drawing.Size(116, 20);
         this.radioButtonProcess.TabIndex = 11;
         this.radioButtonProcess.Text = "Process (2 States)";
         this.radioButtonProcess.CheckedChanged += new System.EventHandler(this.radioButtonProcess_CheckedChanged);
         // 
         // radioButtonState
         // 
         this.radioButtonState.Location = new System.Drawing.Point(8, 44);
         this.radioButtonState.Name = "radioButtonState";
         this.radioButtonState.Size = new System.Drawing.Size(116, 20);
         this.radioButtonState.TabIndex = 10;
         this.radioButtonState.Text = "One State";
         this.radioButtonState.CheckedChanged += new System.EventHandler(this.radioButtonState_CheckedChanged);
         // 
         // radioButtonChartOnly
         // 
         this.radioButtonChartOnly.Checked = true;
         this.radioButtonChartOnly.Location = new System.Drawing.Point(8, 20);
         this.radioButtonChartOnly.Name = "radioButtonChartOnly";
         this.radioButtonChartOnly.Size = new System.Drawing.Size(116, 20);
         this.radioButtonChartOnly.TabIndex = 9;
         this.radioButtonChartOnly.TabStop = true;
         this.radioButtonChartOnly.Text = "Chart Only";
         this.radioButtonChartOnly.CheckedChanged += new System.EventHandler(this.radioButtonChartOnly_CheckedChanged);
         // 
         // groupBoxHumidityChartType
         // 
         this.groupBoxHumidityChartType.Controls.Add(this.radioButtonProcess);
         this.groupBoxHumidityChartType.Controls.Add(this.radioButtonState);
         this.groupBoxHumidityChartType.Controls.Add(this.radioButtonChartOnly);
         this.groupBoxHumidityChartType.Location = new System.Drawing.Point(4, 12);
         this.groupBoxHumidityChartType.Name = "groupBoxHumidityChartType";
         this.groupBoxHumidityChartType.Size = new System.Drawing.Size(128, 92);
         this.groupBoxHumidityChartType.TabIndex = 16;
         this.groupBoxHumidityChartType.TabStop = false;
         this.groupBoxHumidityChartType.Text = "Type";
         // 
         // groupBoxHumidityChartParameter
         // 
         this.groupBoxHumidityChartParameter.Controls.Add(this.textBoxPressureValue);
         this.groupBoxHumidityChartParameter.Controls.Add(this.labelPressureName);
         this.groupBoxHumidityChartParameter.Location = new System.Drawing.Point(136, 12);
         this.groupBoxHumidityChartParameter.Name = "groupBoxHumidityChartParameter";
         this.groupBoxHumidityChartParameter.Size = new System.Drawing.Size(236, 52);
         this.groupBoxHumidityChartParameter.TabIndex = 17;
         this.groupBoxHumidityChartParameter.TabStop = false;
         this.groupBoxHumidityChartParameter.Text = "Chart Parameter";
         // 
         // plotCtrl
         // 
         this.plotCtrl.Location = new System.Drawing.Point(376, 4);
         this.plotCtrl.Name = "plotCtrl";
         this.plotCtrl.Size = new System.Drawing.Size(596, 456);
         this.plotCtrl.TabIndex = 18;
         // 
         // panel
         // 
         this.panel.AutoScroll = true;
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.groupBoxHumidityChartType);
         this.panel.Controls.Add(this.groupBoxHumidityChartParameter);
         this.panel.Controls.Add(this.plotCtrl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(980, 467);
         this.panel.TabIndex = 19;
         // 
         // HumidityChartForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(980, 467);
         this.Controls.Add(this.panel);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "HumidityChartForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Humidity Chart";
         this.groupBoxHumidityChartType.ResumeLayout(false);
         this.groupBoxHumidityChartParameter.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void HumidityChartTypeChanged()
      {
         this.ResetActivity();
         if (this.psychrometricChartModel.HCType == HCType.ChartOnly)
         {
            this.hcCurrentStateEditor.Visible = false;
            this.hcProcessEditor.Visible = false;
            this.currentStateCtrl.Visible = false;
            this.inStateCtrl.Visible = false;
            this.outStateCtrl.Visible = false;
         }
         else if (this.psychrometricChartModel.HCType == HCType.State)
         {
            this.hcCurrentStateEditor.Visible = true;
            this.hcProcessEditor.Visible = false;
            this.currentStateCtrl.Visible = true;
            this.inStateCtrl.Visible = false;
            this.outStateCtrl.Visible = false;
            this.currentState = new HumidityChartState(HCStateType.CurrentState, this.flowsheet);
            this.UpdateCurrentState();
         }
         else if (this.psychrometricChartModel.HCType == HCType.Process)
         {
            this.hcCurrentStateEditor.Visible = false;
            this.hcProcessEditor.Visible = true;
            this.currentStateCtrl.Visible = false;
            this.inStateCtrl.Visible = true;
            this.outStateCtrl.Visible = true;
            this.process = new HumidityChartProcess(this.flowsheet);
            this.UpdateProcess();
         }
      }

      private void UpdateCurrentState()
      {
         if (this.currentState != null)
         {
            PointF siPoint = this.psychrometricChartModel.GetCurrentState();             
            PointF currentPoint = this.ConvertFromSI(siPoint);
            this.currentState.Variables = currentPoint;
            this.plotCtrl.Graph.ShowCurrentCoordinate(this.currentState.ToString());

            //
            PointF pageCurrentPoint = this.plotCtrl.Graph.ConvertFromDomainToPage(currentPoint);
            this.currentStateCtrl.Location = new Point((int)(pageCurrentPoint.X - StateControl.RADIUS + 1), (int)(pageCurrentPoint.Y - StateControl.RADIUS));

            PointF[] points = new PointF[] {currentPoint};
            this.plotCtrl.Graph.Points = points;
            this.plotCtrl.Graph.ShowPoints = true;
            this.plotCtrl.RefreshPlot();
         }
      }

      private void UpdateProcess()
      {
         if (this.process != null)
         {
            CurveFamilyF curves = this.psychrometricChartModel.GetProcessStates();
            if (curves != null)
            {
               // display the process curve(s) on the plot
               this.plotCtrl.Graph.ShowOtherData = true;
               PlotData otherData = this.ConvertToPlotData(curves);
               this.plotCtrl.Graph.OtherData = otherData;
            }
               
            PointF siPoint = this.psychrometricChartModel.GetProcessInputState();             
            PointF currentInPoint = this.ConvertFromSI(siPoint);
            this.process.Input.Variables = currentInPoint;
            siPoint = this.psychrometricChartModel.GetProcessOutputState();             
            PointF currentOutPoint = this.ConvertFromSI(siPoint);
            this.process.Output.Variables = currentOutPoint;
            this.plotCtrl.Graph.ShowCurrentCoordinate(this.process.ToString());

            //
            PointF pageCurrentInPoint = this.plotCtrl.Graph.ConvertFromDomainToPage(currentInPoint);
            this.inStateCtrl.Location = new Point((int)(pageCurrentInPoint.X - StateControl.RADIUS + 1), (int)(pageCurrentInPoint.Y - StateControl.RADIUS));
            PointF pageCurrentOutPoint = this.plotCtrl.Graph.ConvertFromDomainToPage(currentOutPoint);
            this.outStateCtrl.Location = new Point((int)(pageCurrentOutPoint.X - StateControl.RADIUS + 1), (int)(pageCurrentOutPoint.Y - StateControl.RADIUS));

            PointF[] points = new PointF[] {currentInPoint, currentOutPoint};
            this.plotCtrl.Graph.Points = points;
            this.plotCtrl.Graph.ShowPoints = true;
            this.plotCtrl.RefreshPlot();
         }
      }

      private void ResetActivity()
      {
         this.plotCtrl.Graph.ShowCurrentCoordinate("");
         this.currentState = null;
         this.process = null;
         this.plotCtrl.Graph.ShowOtherData = false;
         this.plotCtrl.Graph.OtherData = null;
         this.plotCtrl.Graph.ShowPoints = false;
         this.plotCtrl.Graph.Points = null;
         this.plotCtrl.RefreshPlot();
      }

      private void radioButtonChartOnly_CheckedChanged(object sender, System.EventArgs e)
      {
         ErrorMessage error = this.psychrometricChartModel.SpecifyHCType(HCType.ChartOnly);
         if (error != null)
            UI.ShowError(error);
      }

      private void radioButtonState_CheckedChanged(object sender, System.EventArgs e)
      {
         ErrorMessage error = this.psychrometricChartModel.SpecifyHCType(HCType.State);
         if (error != null)
            UI.ShowError(error);
      }

      private void radioButtonProcess_CheckedChanged(object sender, System.EventArgs e)
      {
         ErrorMessage error = this.psychrometricChartModel.SpecifyHCType(HCType.Process);
         if (error != null)
            UI.ShowError(error);
      }

      private void HumidityChartForm_CurrentUnitSystemChanged(UnitSystem unitSystem)
      {
         this.unitSystem = unitSystem;
         this.plotCtrl.InitializePlotControl(this);
      }

      #region IPlot Members

      public event ProsimoUI.Plots.PlotChangedEventHandler PlotChanged;

      public ArrayList GetOxVariables()
      {
         // remember the first element on the list of PlotVariable
         this.xPlotVariables = this.psychrometricChartModel.GetOxVariables();
         IEnumerator en = this.xPlotVariables.GetEnumerator();
         while (en.MoveNext())
         {
            this.xPlotVariable = (PlotVariable)en.Current;
            break;
         }

         // convert the list         
         return this.ConvertToAxisVariables(this.xPlotVariables);
      }

      public ArrayList GetOyVariables()
      {
         // remember the first element on the list of PlotVariable
         this.yPlotVariables = this.psychrometricChartModel.GetOyVariables();
         IEnumerator en = this.yPlotVariables.GetEnumerator();
         while (en.MoveNext())
         {
            this.yPlotVariable = (PlotVariable)en.Current;
            break;
         }

         // convert the list         
         return this.ConvertToAxisVariables(this.yPlotVariables);
      }

      public void SetOxVariable(AxisVariable x)
      {
         this.UpdateLocalOxPlotVariable(x);
         this.psychrometricChartModel.SetOxVariable(this.xPlotVariable);
         this.UpdateCurrentStateOrProcess();
      }

      public void SetOyVariable(AxisVariable y)
      {
         this.UpdateLocalOyPlotVariable(y);
         this.psychrometricChartModel.SetOyVariable(this.yPlotVariable);
         this.UpdateCurrentStateOrProcess();
      }

      public void SetOxMin(float min)
      {
         PhysicalQuantity pq = this.xPlotVariable.Variable.Type;         
         double newMin = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)min);
         this.psychrometricChartModel.SetOxMin((double)newMin);
         this.UpdateCurrentStateOrProcess();
      }

      public void SetOxMax(float max)
      {
         PhysicalQuantity pq = this.xPlotVariable.Variable.Type;         
         double newMax = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)max);
         this.psychrometricChartModel.SetOxMax((double)newMax);
         this.UpdateCurrentStateOrProcess();
      }

      public void SetOyMin(float min)
      {
         PhysicalQuantity pq = this.yPlotVariable.Variable.Type;         
         double newMin = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)min);
         this.psychrometricChartModel.SetOyMin((double)newMin);
         this.UpdateCurrentStateOrProcess();
      }

      public void SetOyMax(float max)
      {
         PhysicalQuantity pq = this.yPlotVariable.Variable.Type;         
         double newMax = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)max);
         this.psychrometricChartModel.SetOyMax((double)newMax);
         this.UpdateCurrentStateOrProcess();
      }

      public PlotData GetPlotData(AxisVariable x, AxisVariable y)
      {
         this.UpdateLocalOxPlotVariable(x);
         this.UpdateLocalOyPlotVariable(y);
         HumidityChartData hcData = this.psychrometricChartModel.GetPlotData(this.xPlotVariable, this.yPlotVariable);
         return this.ConvertToPlotData(hcData);
      }

      #endregion

      private void UpdateCurrentStateOrProcess()
      {
         if (this.psychrometricChartModel.HCType == HCType.State)
         {
            this.UpdateCurrentState();
         }
         else if (this.psychrometricChartModel.HCType == HCType.Process)
         {
            this.UpdateProcess();
         }
      }

      private PlotData ConvertToPlotData(HumidityChartData hcData)
      {
         PlotData plotData = new PlotData();
         plotData.Name = hcData.Name;
         CurveFamilyF[] oldFamilies = hcData.CurveFamilies;
         CurveFamily[] newFamilies = UI.ConvertCurveFamilies(oldFamilies, this.xPlotVariable.Variable.Type, this.yPlotVariable.Variable.Type);               
         
         // add the new falilies to the PlotData
         plotData.CurveFamilies = newFamilies;
         return plotData;
      }

      private void UpdateLocalOxPlotVariable(AxisVariable x)
      {
         // set the local xPlotVariable if changed
         if (!x.Name.Equals(this.xPlotVariable.Variable.Name))
         {
            this.xPlotVariable = this.GetPlotVariableFromList(x.Name, this.xPlotVariables);
         }
         
         PhysicalQuantity pq = this.xPlotVariable.Variable.Type;         
         this.xPlotVariable.Min = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)x.Range.Min);
         this.xPlotVariable.Max = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)x.Range.Max);
      }

      private void UpdateLocalOyPlotVariable(AxisVariable y)
      {
         // set the local yPlotVariable if changed
         if (!y.Name.Equals(this.yPlotVariable.Variable.Name))
         {
            this.yPlotVariable = this.GetPlotVariableFromList(y.Name, this.yPlotVariables);
         }
         
         PhysicalQuantity pq = this.yPlotVariable.Variable.Type;         
         this.yPlotVariable.Min = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)y.Range.Min);
         this.yPlotVariable.Max = UnitSystemService.GetInstance().ConvertToSIValue(pq, (double)y.Range.Max);
      }

      private PlotVariable GetPlotVariableFromList(string name, ArrayList list)
      {
         PlotVariable pv = null;
         bool found = false;
         IEnumerator en = list.GetEnumerator();
         while (en.MoveNext())
         {
            pv = (PlotVariable)en.Current;
            if (pv.Variable.Name.Equals(name))
            {
               found = true;
               break;
            }
         }
         if (!found)
            pv = null;
         return pv;
      }

      private ArrayList ConvertToAxisVariables(ArrayList plotList)
      {
         ArrayList axisList = new ArrayList();
         IEnumerator en = plotList.GetEnumerator();
         while (en.MoveNext())
         {
            PlotVariable pv = (PlotVariable)en.Current;
            AxisVariable av = UI.ConvertToAxisVariable(pv);
            axisList.Add(av);
         }
         return axisList;
      }

      protected void OnPlotChanged(AxisVariable ox, AxisVariable oy, PlotData data) 
      {
         if (PlotChanged != null) 
         {
            PlotChanged(ox, oy, data);
         }
      }

      private void psychrometricChartModel_HumidityChartChanged(PlotVariable ox, PlotVariable oy, HumidityChartData data)
      {
         this.xPlotVariable = ox;
         this.yPlotVariable = oy;
         AxisVariable x = UI.ConvertToAxisVariable(ox);
         AxisVariable y = UI.ConvertToAxisVariable(oy);
         PlotData pd = this.ConvertToPlotData(data);
         this.OnPlotChanged(x, y, pd);
      }

      private PlotData ConvertToPlotData(CurveFamilyF curves)
      {
         PlotData plotData = new PlotData();
         plotData.Name = "Process";

         CurveFamilyF[] oldFamilies = new CurveFamilyF[1];               
         oldFamilies[0] = curves;
         CurveFamily[] newFamilies = UI.ConvertCurveFamilies(oldFamilies, this.xPlotVariable.Variable.Type, this.yPlotVariable.Variable.Type);               

         // add the new families to the PlotData
         plotData.CurveFamilies = newFamilies;
         return plotData;
      }

      private void psychrometricChartModel_SolveComplete(object sender, SolveState solveState)
      {
         if (solveState.Equals(SolveState.Solved) || solveState.Equals(SolveState.PartiallySolved))
         {
            if (this.psychrometricChartModel.HCType == HCType.State)
            {
               this.UpdateCurrentState();
            }
            else if (this.psychrometricChartModel.HCType == HCType.Process)
            {
               this.UpdateProcess();
            }
         }
         else
         {
            this.ResetActivity();
            if (this.psychrometricChartModel.HCType == HCType.State)
            {
               this.currentState = new HumidityChartState(HCStateType.CurrentState, flowsheet);
               this.UpdateCurrentState();
            }
            else if (this.psychrometricChartModel.HCType == HCType.Process)
            {
               this.process = new HumidityChartProcess(this.flowsheet);
               this.UpdateProcess();
            }
         }
      }

      private PointF ConvertFromSI(PointF siPoint)
      {
         float newX = (float)UnitSystemService.GetInstance().
            ConvertFromSIValue(this.xPlotVariable.Variable.Type, siPoint.X);
         float newY = (float)UnitSystemService.GetInstance().
            ConvertFromSIValue(this.yPlotVariable.Variable.Type, siPoint.Y);
         return new PointF(newX, newY);
      }

      private void flowsheet_NumericFormatStringChanged(INumericFormat iNumericFormat)
      {
         this.plotCtrl.Graph.NumericFormatString = iNumericFormat.NumericFormatString;
         if (this.psychrometricChartModel.HCType == HCType.State)
         {
            this.plotCtrl.Graph.ShowCurrentCoordinate(this.currentState.ToString());
         }
         else if (this.psychrometricChartModel.HCType == HCType.Process)
         {
            this.plotCtrl.Graph.ShowCurrentCoordinate(this.process.ToString());
         }
      }

      //
      private void StateCtrlMouseDown(MouseEventArgs e, HumidityChartState hcState, StateControl stateCtrl, string coordinates)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            hcState.Variables = this.plotCtrl.Graph.ConvertMousePosition(StateControl.RADIUS + stateCtrl.Location.X, StateControl.RADIUS + stateCtrl.Location.Y);
            this.plotCtrl.Graph.ShowCurrentCoordinate(coordinates);
         }
      }
      
      private void currentStateCtrl_MouseDown(object sender, MouseEventArgs e)
      {
         this.StateCtrlMouseDown(e, this.currentState, this.currentStateCtrl, this.currentState.ToString());
      }

      private void inStateCtrl_MouseDown(object sender, MouseEventArgs e)
      {
         this.StateCtrlMouseDown(e, this.process.Input, this.inStateCtrl, this.process.ToString());
      }

      private void outStateCtrl_MouseDown(object sender, MouseEventArgs e)
      {
         this.StateCtrlMouseDown(e, this.process.Output, this.outStateCtrl, this.process.ToString());
      }

      //      
      private void StateCtrlLocationChanged(HumidityChartState hcState, StateControl stateCtrl, string coordinates)
      {
         hcState.Variables = this.plotCtrl.Graph.ConvertMousePosition(StateControl.RADIUS + stateCtrl.Location.X, StateControl.RADIUS + stateCtrl.Location.Y);
         this.plotCtrl.Graph.ShowCurrentCoordinate(coordinates);
      }     
      
      private void currentStateCtrl_LocationChanged(object sender, EventArgs e)
      {
         this.StateCtrlLocationChanged(this.currentState, this.currentStateCtrl, this.currentState.ToString());
      }

      private void inStateCtrl_LocationChanged(object sender, EventArgs e)
      {
         this.StateCtrlLocationChanged(this.process.Input, this.inStateCtrl, this.process.ToString());
      }

      private void outStateCtrl_LocationChanged(object sender, EventArgs e)
      {
         this.StateCtrlLocationChanged(this.process.Output, this.outStateCtrl, this.process.ToString());
      }
      
      //
      private PointF Get_SI_DomainCoordinate(StateControl stateCtrl)
      {
         PointF domainCoordinate = this.plotCtrl.Graph.ConvertMousePosition(StateControl.RADIUS + stateCtrl.Location.X, StateControl.RADIUS + stateCtrl.Location.Y);
         double x = UnitSystemService.GetInstance().ConvertToSIValue(this.xPlotVariable.Variable.Type, (double)domainCoordinate.X);
         double y = UnitSystemService.GetInstance().ConvertToSIValue(this.yPlotVariable.Variable.Type, (double)domainCoordinate.Y);
         PointF p = new PointF((float)x, (float)y);
         return p;
      }
      
      private void currentStateCtrl_MouseUp(object sender, MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {	
            ErrorMessage em = this.psychrometricChartModel.SetCurrentState(this.Get_SI_DomainCoordinate(this.currentStateCtrl));
            UI.ShowError(em);
         }
      }

      private void inStateCtrl_MouseUp(object sender, MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {	
            ErrorMessage em = this.psychrometricChartModel.SetInputState(this.Get_SI_DomainCoordinate(this.inStateCtrl));
            UI.ShowError(em);
         }
      }

      private void outStateCtrl_MouseUp(object sender, MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            ErrorMessage em = this.psychrometricChartModel.SetOutputState(this.Get_SI_DomainCoordinate(this.outStateCtrl));
            UI.ShowError(em);
         }
      }

      private void textBoxPressureValue_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ActiveControl = null;
            this.ActiveControl = this.textBoxPressureValue;
         }
      }

      private void psychrometricChartModel_HCTypeChanged(HCType hcType)
      {
         this.HumidityChartTypeChanged();
      }
   }
}
