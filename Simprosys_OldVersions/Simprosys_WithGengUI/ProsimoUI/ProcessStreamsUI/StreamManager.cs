using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using Prosimo.UnitOperations;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;
using Prosimo.UnitSystems;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for StreamManager.
	/// </summary>
	public class StreamManager
	{
      private Flowsheet flowsheet;

      public bool HasStreamControls()
      {
         return this.HasStreamControls<GasStreamControl>() || this.HasStreamControls<MaterialStreamControl>();
      }

      public bool HasStreamControls<T>()
      {
         if (this.GetStreamControls<T>().Count > 0)
            return true;
         else
            return false;
      }

      public bool HasShowableInEditorStreamControls<T>()
      {
         if (this.GetShowableInEditorStreamControls<T>().Count > 0)
            return true;
         else
            return false;
      }

      public StreamManager(Flowsheet flowsheet)
		{
         this.flowsheet = flowsheet;

         this.flowsheet.EvaporationAndDryingSystem.StreamAdded += new StreamAddedEventHandler(EvaporationAndDryingSystem_StreamAdded);
         this.flowsheet.EvaporationAndDryingSystem.StreamDeleted += new StreamDeletedEventHandler(EvaporationAndDryingSystem_StreamDeleted);

		}

      public ProcessStreamBaseControl GetProcessStreamBaseControl(ProcessStreamBase ps)
      {
         return this.GetProcessStreamBaseControl(ps.Name);
      }

      public ProcessStreamBaseControl GetProcessStreamBaseControl(string name)
      {
         ProcessStreamBaseControl psCtrl = null;
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is SolvableControl)
            {
               SolvableControl sCtrl = (SolvableControl)e.Current;
               if (sCtrl is ProcessStreamBaseControl)
               {
                  if (sCtrl is GasStreamControl)
                  {
                     GasStreamControl sc = (GasStreamControl)sCtrl;
                     if (sc.GasStream.Name.Equals(name))
                     {
                        psCtrl = sc;
                        break;
                     }
                  }
                  if (sCtrl is MaterialStreamControl)
                  {
                     MaterialStreamControl sc = (MaterialStreamControl)sCtrl;
                     if (sc.MaterialStream.Name.Equals(name))
                     {
                        psCtrl = sc;
                        break;
                     }
                  }
                  if (sCtrl is ProcessStreamControl)
                  {
                     ProcessStreamControl sc = (ProcessStreamControl)sCtrl;
                     if (sc.ProcessStream.Name.Equals(name))
                     {
                        psCtrl = sc;
                        break;
                     }
                  }
               }
            }
         }
         return psCtrl;
      }

      public IList<T> GetStreamControls<T>()
      {
         IList<T> ctrls = new List<T>();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext())
         {
            if (e.Current is T)
            {
               ctrls.Add((T)e.Current);
            }
         }
         return ctrls;
      }

      public ArrayList GetShowableInEditorStreamControls<T>()
      {
         if (typeof(T).BaseType == typeof(ProcessStreamBase) ||
            typeof(T).BaseType.BaseType == typeof(ProcessStreamBase))
         {
            ArrayList ctrls = new ArrayList();
            IEnumerator e = this.flowsheet.EvaporationAndDryingSystem.GetStreamList(typeof(T)).GetEnumerator();
            while (e.MoveNext())
            {
               ProcessStreamBase obj = (ProcessStreamBase)e.Current;
               SolvableControl ctrl = GetProcessStreamBaseControl(obj.Name);
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
            return ctrls;
         }
         else
            return null;
      }
     
      public ArrayList GetShowableInEditorStreamControls()
      {
         ArrayList ctrls = new ArrayList();
         IEnumerator e = this.flowsheet.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is ProcessStreamBaseControl)
            {
               SolvableControl ctrl = (SolvableControl)e.Current;
               if (ctrl.IsShownInEditor)
                  ctrls.Add(ctrl);
            }
         }
         return ctrls;
      }

      public void DeleteSelectedStreamControls()
      {
         if (this.HasStreamControls()) 
         {
            ArrayList toDeleteControls = new ArrayList();

            IEnumerator e = this.flowsheet.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is ProcessStreamBaseControl)
               {
                  ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)e.Current;
                  if (ctrl.IsSelected)
                  {
                     toDeleteControls.Add(ctrl);
                  }
               }
            }

            if (toDeleteControls.Count > 0)
            {
               string message = "Are you sure that you want to delete the selected Streams?";
               DialogResult dr = MessageBox.Show(this.flowsheet, message, "Delete: " + this.flowsheet.Text,
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

               switch (dr)
               {
                  case System.Windows.Forms.DialogResult.Yes:
                     IEnumerator e2 = toDeleteControls.GetEnumerator();
                     while (e2.MoveNext())
                     {
                        ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)e2.Current;
                        // delete from the model, the UI will be updated in the event listener
                        ProcessStreamBase processStream = ctrl.ProcessStreamBase;
                        this.flowsheet.EvaporationAndDryingSystem.DeleteStream(processStream);
                     }
                     break;
                  case System.Windows.Forms.DialogResult.No:
                     break;
               }
            }
         }
      }

      public void EditSelectedStreamControl()
      {
         if (this.HasStreamControls()) 
         {   
            ArrayList toEditControls = new ArrayList();

            IEnumerator e = this.flowsheet.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is ProcessStreamBaseControl)
               {
                  ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)e.Current;
                  if (ctrl.IsSelected)
                  {
                     toEditControls.Add(ctrl);
                  }
               }
            }

            if (toEditControls.Count < 1)
            { 
               string message = "Please select a Stream."; 
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (toEditControls.Count > 1)
            {
               string message = "Please select only one Stream."; 
               MessageBox.Show(message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               ((IEditable)toEditControls[0]).Edit();
            }
         }
      }

      public void RotateStreamControls(RotationDirection rotationDirection)
      {
         if (this.HasStreamControls()) 
         {
            ArrayList toRotateControls = new ArrayList();
            IEnumerator e = this.flowsheet.Controls.GetEnumerator();
            while (e.MoveNext()) 
            {
               if (e.Current is ProcessStreamBaseControl)
               {
                  ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)e.Current;
                  if (ctrl.IsSelected)
                  {
                     toRotateControls.Add(ctrl);
                  }
               }
            }

            if (toRotateControls.Count > 0)
            {
               IEnumerator e2 = toRotateControls.GetEnumerator();
               while (e2.MoveNext())
               {
                  ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)e2.Current;
                  if (rotationDirection.Equals(RotationDirection.Clockwise))
                  {
                     ctrl.RotateClockwise();
                  }
                  else if (rotationDirection.Equals(RotationDirection.Counterclockwise))
                  {
                     ctrl.RotateCounterclockwise();
                  }
               }
            }
         }
      }

      private void EvaporationAndDryingSystem_StreamAdded(ProcessStreamBase processStreamBase)
      {
         Point location = new System.Drawing.Point(this.flowsheet.X, this.flowsheet.Y);
         ProcessStreamBaseControl control = null;
         
         if (processStreamBase is DryingGasStream)
         {
            DryingGasStream stream = (DryingGasStream)processStreamBase;
            control = new GasStreamControl(this.flowsheet, location, stream);
         }
         else if (processStreamBase is DryingMaterialStream)
         {
            DryingMaterialStream stream = (DryingMaterialStream)processStreamBase;
            control = new MaterialStreamControl(this.flowsheet, location, stream);
         }
         else if (processStreamBase is ProcessStream)
         {
            ProcessStream stream = (ProcessStream)processStreamBase;
            control = new ProcessStreamControl(this.flowsheet, location, stream);
         }

         // adjust the location if at the limit of the flowsheet
         if (this.flowsheet.X > this.flowsheet.Width - control.Width/2)
         {
            int newX = this.flowsheet.Width - control.Width;
            Point newLocation = new Point(newX, control.Location.Y);
            control.Location = newLocation;
         }
         if (this.flowsheet.Y > this.flowsheet.Height - control.Height/2)
         {
            int newY = this.flowsheet.Height - control.Height;
            Point newLocation = new Point(control.Location.X, newY);
            control.Location = newLocation;
         }

         this.flowsheet.Controls.Add(control);
         this.flowsheet.IsDirty = true;
      }

      private void EvaporationAndDryingSystem_StreamDeleted(string streamName)
      {
         ProcessStreamBaseControl ctrl = GetProcessStreamBaseControl(streamName);
         this.flowsheet.RemoveSolvableControl(ctrl);
      }
   }
}
