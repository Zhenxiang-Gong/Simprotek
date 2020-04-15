using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Plots {
   public delegate void Plot2DAddedEventHandler(Plot2D plot2D);
   public delegate void Plot2DDeletedEventHandler(string name);
   //public delegate void Plot2DChangedEventHandler(Plot2D plot2D);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   [Serializable]
   public class PlotCatalog : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private IList plotList;
      //private static PlotCatalog self;

      public event Plot2DAddedEventHandler Plot2DAdded;
      public event Plot2DDeletedEventHandler Plot2DDeleted;
      public event Plot2DChangedEventHandler Plot2DChanged;

      public IList Plot2DList {
         get { return plotList; }
      }

      //public static PlotCatalog GetInstance() {
      //   if (self == null) {
      //      self = new PlotCatalog ();
      //   }
      //   return self;
      //}

      public PlotCatalog() {
         plotList = new ArrayList();
      }

      //public PlotCatalog(IList list) {
      //   plotList = list;
      //}

      public void AddPlot2D(Plot2D plot) {
         if (!IsInCatalog(plot)) {
            plotList.Add(plot);
            OnPlot2DAdded(plot);
         }
      }

      public void RemovePlot2D(string name) {
         foreach (Plot2D plot in plotList) {
            if (plot.Name.Equals(name)) {
               plotList.Remove(plot);
               OnPlot2DDeleted(name);
               break;
            }
         }
      }

      public void RemovePlot2D(Plot2D plot) {
         string name = plot.Name;
         plotList.Remove(plot);
         OnPlot2DDeleted(name);
      }

      public void UpdatePlot2D(Plot2D plot) {
         OnPlot2DChanged(plot);
      }

      public bool IsInCatalog(Plot2D aPlot) {
         bool isInCatalog = false;
         foreach (Plot2D plot in plotList) {
            if (plot.Name.Equals(aPlot.Name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      public bool IsInCatalog(string name) {
         bool isInCatalog = false;
         foreach (Plot2D plot in plotList) {
            if (plot.Name.Equals(name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      //      public void Remove(int index) {
      //         if (index < materialList.Count && index >= 0) {
      //            Plot2D material = (Plot2D) materialList[index];
      //            if (material.IsUserDefined) {
      //               string name = material.Name;
      //               materialList.RemoveAt(index);
      //               OnPlot2DDeleted(name);
      //            }
      //         }
      //      }
      //
      public Plot2D GetPlot2D(string name) {
         Plot2D ret = null;
         foreach (Plot2D plot in plotList) {
            if (plot.Name.Equals(name)) {
               ret = plot;
               break;
            }
         }
         return ret;
      }

      private void OnPlot2DDeleted(string name) {
         if (Plot2DDeleted != null)
            Plot2DDeleted(name);
      }

      private void OnPlot2DAdded(Plot2D plot) {
         if (Plot2DAdded != null)
            Plot2DAdded(plot);
      }

      private void OnPlot2DChanged(Plot2D plot) {
         if (Plot2DChanged != null)
            Plot2DChanged(plot);
      }

      protected PlotCatalog(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionPlotCatalog", typeof(int));
         if (persistedClassVersion == 1) {
            this.plotList = RecallArrayListObject("PlotList") as IList;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionPlotCatalog", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("PlotList", (ArrayList)plotList, typeof(ArrayList));
      }
   }
}
