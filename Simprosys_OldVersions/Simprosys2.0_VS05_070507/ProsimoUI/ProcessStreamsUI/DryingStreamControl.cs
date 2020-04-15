using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;
using ProsimoUI;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for DryingStreamControl.
	/// </summary>
   [Serializable]
   public class DryingStreamControl : ProcessStreamBaseControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public DryingStream DryingStream
      {
         get {return (DryingStream)this.Solvable;}
         set {this.Solvable = value;}
      
      }

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public DryingStreamControl()
      {
      }

      public DryingStreamControl(Flowsheet flowsheet, Point location, DryingStream dryingStream) :
         base(flowsheet, location, dryingStream)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
      #endregion

      protected DryingStreamControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryingStreamControl", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingStreamControl", DryingStreamControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
