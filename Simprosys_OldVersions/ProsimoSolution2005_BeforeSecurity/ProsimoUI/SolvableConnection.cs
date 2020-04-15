using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

using ProsimoUI.ProcessStreamsUI;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SolvableConnection.
	/// </summary>
   [Serializable]
   public class SolvableConnection : ISerializable
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private Flowsheet flowsheet;

      private StreamingContext streamingContext;
      public StreamingContext StreamingContext
      {
         get {return streamingContext;}
         set {streamingContext = value;}
      }

      private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo
      {
         get {return serializationInfo;}
         set {serializationInfo = value;}
      }

      private StreamType streamType;
      public StreamType StreamType
      {
         get {return streamType;}
         set {streamType = value;}
      }

      private ConnectionPoint streamPoint;
      public ConnectionPoint StreamPoint
      {
         get {return streamPoint;}
         set {streamPoint = value;}
      }

      private ConnectionPoint unitOpPoint;
      public ConnectionPoint UnitOpPoint
      {
         get {return unitOpPoint;}
         set {unitOpPoint = value;}
      }

      public SolvableConnection(Flowsheet flowsheet, ConnectionPoint streamPoint, ConnectionPoint unitOpPoint, StreamType streamType)
      {
         this.flowsheet = flowsheet;
         this.streamPoint = streamPoint;
         this.unitOpPoint = unitOpPoint;
         this.streamType = streamType;
      }

      public SolvableConnection(Flowsheet flowsheet)
      {
         this.flowsheet = flowsheet;
      }

      public bool Equals(SolvableConnection conn)
      {
         bool isEqual = false;

         if (this.streamPoint.Equals(conn.StreamPoint) && this.unitOpPoint.Equals(conn.UnitOpPoint) && 
            this.streamType == conn.StreamType)
         {
            isEqual = true;
         }
         return isEqual;
      }

      public void DrawConnection()
      {
         GraphicsPath gp = this.ConstructGraphicsPath();
         Graphics g = this.flowsheet.CreateGraphics();

         UI ui = new UI();
         Pen pen = null;

         if (this.streamType == StreamType.Material)
         {
            pen = new Pen(ui.CONNECTION_COLOR_MATERIAL, UI.CONNECTION_WIDTH);
         }
         else if (this.streamType == StreamType.Gas)
         {
            pen = new Pen(ui.CONNECTION_COLOR_GAS, UI.CONNECTION_WIDTH);
         }
         else if (this.streamType == StreamType.Process)
         {
            pen = new Pen(ui.CONNECTION_COLOR_PROCESS, UI.CONNECTION_WIDTH);
         }
         g.DrawPath(pen, gp);
      }

      public bool HitTest(Point p)
      {
         bool hit = false;
         GraphicsPath gp = this.ConstructGraphicsPath();
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 10));
         return hit;
      }

      private GraphicsPath ConstructGraphicsPath()
      {
         GraphicsPath gp = null;
         
         if (this.streamPoint.Orientation == PointOrientation.E &&
            this.unitOpPoint.Orientation == PointOrientation.W)
         {
            return this.ConstructGraphicsPathEtoW();
         }
         else if (this.streamPoint.Orientation == PointOrientation.S &&
            this.unitOpPoint.Orientation == PointOrientation.W)
         {
            return this.ConstructGraphicsPathStoW();
         }
         else if (this.streamPoint.Orientation == PointOrientation.W &&
            this.unitOpPoint.Orientation == PointOrientation.W)
         {
            return this.ConstructGraphicsPathWtoW();
         }
         else if (this.streamPoint.Orientation == PointOrientation.N &&
            this.unitOpPoint.Orientation == PointOrientation.W)
         {
            return this.ConstructGraphicsPathNtoW();
         }

         else if (this.streamPoint.Orientation == PointOrientation.E &&
            this.unitOpPoint.Orientation == PointOrientation.N)
         {
            return this.ConstructGraphicsPathEtoN();
         }
         else if (this.streamPoint.Orientation == PointOrientation.S &&
            this.unitOpPoint.Orientation == PointOrientation.N)
         {
            return this.ConstructGraphicsPathStoN();
         }
         else if (this.streamPoint.Orientation == PointOrientation.W &&
            this.unitOpPoint.Orientation == PointOrientation.N)
         {
            return this.ConstructGraphicsPathWtoN();
         }
         else if (this.streamPoint.Orientation == PointOrientation.N &&
            this.unitOpPoint.Orientation == PointOrientation.N)
         {
            return this.ConstructGraphicsPathNtoN();
         }

         else if (this.streamPoint.Orientation == PointOrientation.E &&
            this.unitOpPoint.Orientation == PointOrientation.E)
         {
            return this.ConstructGraphicsPathEtoE();
         }
         else if (this.streamPoint.Orientation == PointOrientation.S &&
            this.unitOpPoint.Orientation == PointOrientation.E)
         {
            return this.ConstructGraphicsPathStoE();
         }
         else if (this.streamPoint.Orientation == PointOrientation.W &&
            this.unitOpPoint.Orientation == PointOrientation.E)
         {
            return this.ConstructGraphicsPathWtoE();
         }
         else if (this.streamPoint.Orientation == PointOrientation.N &&
            this.unitOpPoint.Orientation == PointOrientation.E)
         {
            return this.ConstructGraphicsPathNtoE();
         }

         else if (this.streamPoint.Orientation == PointOrientation.E &&
            this.unitOpPoint.Orientation == PointOrientation.S)
         {
            return this.ConstructGraphicsPathEtoS();
         }
         else if (this.streamPoint.Orientation == PointOrientation.S &&
            this.unitOpPoint.Orientation == PointOrientation.S)
         {
            return this.ConstructGraphicsPathStoS();
         }
         else if (this.streamPoint.Orientation == PointOrientation.W &&
            this.unitOpPoint.Orientation == PointOrientation.S)
         {
            return this.ConstructGraphicsPathWtoS();
         }
         else if (this.streamPoint.Orientation == PointOrientation.N &&
            this.unitOpPoint.Orientation == PointOrientation.S)
         {
            return this.ConstructGraphicsPathNtoS();
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathEtoW()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath1(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath2(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath4(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath3(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathStoW()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath5(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath6(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath8(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath7(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathWtoW()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath9(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath10(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath10(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath9(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathNtoW()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath13(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath14(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath16(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath15(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathEtoN()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath17(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath18(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath20(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath19(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathStoN()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath21(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath22(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath24(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath23(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathWtoN()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath15(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath16(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath14(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath13(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathNtoN()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath25(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath26(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath26(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath25(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathEtoE()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath27(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath28(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath28(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath27(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathStoE()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath29(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath30(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath32(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath31(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathWtoE()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath3(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath4(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath2(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath1(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathNtoE()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath19(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath20(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath18(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath17(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathEtoS()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath31(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath32(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath30(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath29(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathStoS()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath11(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath12(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath12(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath11(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathWtoS()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath7(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath8(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath6(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath5(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPathNtoS()
      {
         GraphicsPath gp = new GraphicsPath();

         Point s = this.streamPoint.Point;
         Point u = this.unitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath23(s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y)
         {
            return this.ConstructGraphicsPath24(u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath22(s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y)
         {
            return this.ConstructGraphicsPath21(u, s);
         }

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath1(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point((s.X + e.X)/2, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);
         
         return gp;
      }

      private GraphicsPath ConstructGraphicsPath2(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y)/2);
         Point p3 = new Point(e.X + UI.CONNECTION_DELTA, p2.Y);
         Point p4 = new Point(p3.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, p4);
         gp.AddLine(p4, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath3(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y)/2);
         Point p3 = new Point(e.X + UI.CONNECTION_DELTA, p2.Y);
         Point p4 = new Point(p3.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, p4);
         gp.AddLine(p4, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath4(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point((s.X + e.X)/2, s.Y);
         Point p2 = new Point(p1.X,  e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath5(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, e);
         
         return gp;
      }

      private GraphicsPath ConstructGraphicsPath6(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y)/2);
         Point p3 = new Point(e.X, p2.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath7(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y + UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath8(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point((s.X + e.X)/2, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath9(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath10(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath11(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath12(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath13(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point((s.X + e.X)/2, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath14(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y - UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath15(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y)/2);
         Point p3 = new Point(e.X, p2.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath16(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath17(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X, s.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath18(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y)/2);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath19(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath20(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point((s.X +e.X)/2, s.Y);
         Point p2 = new Point(p1.X,  e.Y - UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath21(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y)/2);
         Point p2 = new Point(e.X, p1.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath22(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y)/2);
         Point p2 = new Point(e.X, p1.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath23(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point((s.X + e.X)/2, p1.Y);
         Point p3 = new Point(p2.X, e.Y + UI.CONNECTION_DELTA);
         Point p4 = new Point(e.X, p3.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, p4);
         gp.AddLine(p4, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath24(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point((s.X + e.X)/2, p1.Y);
         Point p3 = new Point(p2.X, e.Y - UI.CONNECTION_DELTA);
         Point p4 = new Point(e.X, p3.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, p4);
         gp.AddLine(p4, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath25(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath26(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath27(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X + UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath28(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X + UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath29(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y)/2);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, (s.Y + e.Y)/2);
         Point p3 = new Point(p2.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath30(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X, s.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath31(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point((s.X + e.X)/2, s.Y);
         Point p2 = new Point(p1.X, e.Y + UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      private GraphicsPath ConstructGraphicsPath32(Point s, Point e)
      {
         // s = start, e = end
         GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         gp.AddLine(s, p1);
         gp.AddLine(p1, p2);
         gp.AddLine(p2, p3);
         gp.AddLine(p3, e);

         return gp;
      }

      protected SolvableConnection(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionSolvableConnection", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.StreamType = (StreamType)info.GetValue("StreamType", typeof(StreamType));
               this.StreamPoint = (ConnectionPoint)info.GetValue("StreamPoint", typeof(ConnectionPoint));
               this.StreamPoint.SetObjectData(this.StreamPoint.SerializationInfo, this.StreamPoint.StreamingContext);
               this.UnitOpPoint = (ConnectionPoint)info.GetValue("UnitOpPoint", typeof(ConnectionPoint));
               this.UnitOpPoint.SetObjectData(this.UnitOpPoint.SerializationInfo, this.UnitOpPoint.StreamingContext);
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionSolvableConnection", SolvableConnection.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("StreamType", this.StreamType, typeof(StreamType));
         info.AddValue("StreamPoint", this.StreamPoint, typeof(ConnectionPoint));
         info.AddValue("UnitOpPoint", this.UnitOpPoint, typeof(ConnectionPoint));
      }
   }
}
