using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

using ProsimoUI.ProcessStreamsUI;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for SolvableConnection.
   /// </summary>
   [Serializable]
   public class SolvableConnection : System.Windows.Forms.UserControl, ISerializable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private Flowsheet flowsheet;
      private GraphicsPath regionGP;

      private StreamingContext streamingContext;
      public StreamingContext StreamingContext {
         get { return streamingContext; }
         set { streamingContext = value; }
      }

      private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo {
         get { return serializationInfo; }
         set { serializationInfo = value; }
      }

      private StreamType streamType;
      public StreamType StreamType {
         get { return streamType; }
         set { streamType = value; }
      }
      private ConnectionPoint streamPoint;
      public ConnectionPoint StreamPoint {
         get { return streamPoint; }
         set {
            streamPoint = value;
         }
      }
      private ConnectionPoint relStreamPoint;
      private ConnectionPoint relUnitOpPoint;

      private ConnectionPoint unitOpPoint;
      public ConnectionPoint UnitOpPoint {
         get { return unitOpPoint; }
         set {
            unitOpPoint = value;
         }
      }
      #region constructor method
      public SolvableConnection() {

      }
      public SolvableConnection(Flowsheet flowsheet, ConnectionPoint streamPoint, ConnectionPoint unitOpPoint, StreamType streamType) {
         this.flowsheet = flowsheet;
         this.streamPoint = streamPoint;
         this.unitOpPoint = unitOpPoint;
         this.relStreamPoint = new ConnectionPoint(streamPoint.Index, streamPoint.Name, streamPoint.Point, streamPoint.Orientation);
         this.relUnitOpPoint = new ConnectionPoint(unitOpPoint.Index, unitOpPoint.Name, unitOpPoint.Point, unitOpPoint.Orientation);
         this.streamType = streamType;
         InitializeComponent();
         UpdateConnection();
      }

      public SolvableConnection(Flowsheet flowsheet) {
         this.flowsheet = flowsheet;
         this.relStreamPoint = new ConnectionPoint();
         this.relUnitOpPoint = new ConnectionPoint();
         InitializeComponent();

      }
      protected SolvableConnection(SerializationInfo info, StreamingContext context) {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }
      #endregion
      /// <summary>
      /// check if two connection line are the same
      /// </summary>
      /// <param name="conn">connection line control</param>
      /// <returns>true:equal, false: not equal</returns>
      public bool Equals(SolvableConnection conn) {
         bool isEqual = false;

         if (this.streamPoint.Equals(conn.streamPoint) && this.unitOpPoint.Equals(conn.unitOpPoint) &&
            this.streamType == conn.StreamType) {
            isEqual = true;
         }
         return isEqual;
      }
      /// <summary>
      /// check particular point is in the control range or not, not used now
      /// </summary>
      /// <param name="p"></param>
      /// <returns></returns>
      public bool HitTest(Point p) {
         bool hit = false;
         //this.ConstructGraphicsPath(null);
         GraphicsPath gp = new GraphicsPath();
         Pen pen = new Pen(Color.Black, UI.CONNECTION_WIDTH);
         //hit = regionGP.IsOutlineVisible(p, pen);
         hit = gp.IsOutlineVisible(p, pen);

         pen.Dispose();
         gp.Dispose();

         return hit;
      }

      /// <summary>
      /// update the location for the connecton line. This function is called when the solvable control is moved
      /// </summary>
      public void UpdateConnection() {
         Point topLeft = new Point();
         topLeft.X = (streamPoint.Point.X < unitOpPoint.Point.X) ? streamPoint.Point.X : unitOpPoint.Point.X;
         topLeft.Y = (streamPoint.Point.Y < unitOpPoint.Point.Y) ? streamPoint.Point.Y : unitOpPoint.Point.Y;
         Point botRight = new Point();
         botRight.X = (streamPoint.Point.X > unitOpPoint.Point.X) ? streamPoint.Point.X : unitOpPoint.Point.X;
         botRight.Y = (streamPoint.Point.Y > unitOpPoint.Point.Y) ? streamPoint.Point.Y : unitOpPoint.Point.Y;

         topLeft.X = (topLeft.X - 2 * UI.CONNECTION_DELTA > 0) ? topLeft.X - 2 * UI.CONNECTION_DELTA : 0;
         topLeft.Y = (topLeft.Y - 2 * UI.CONNECTION_DELTA > 0) ? topLeft.Y - 2 * UI.CONNECTION_DELTA : 0;
         this.Location = topLeft;

         this.Width = botRight.X - topLeft.X + 2 * UI.CONNECTION_DELTA;
         this.Height = botRight.Y - topLeft.Y + 2 * UI.CONNECTION_DELTA;

         this.relStreamPoint.Point = new Point(this.streamPoint.Point.X - topLeft.X, this.streamPoint.Point.Y - topLeft.Y);
         this.relStreamPoint.Name = streamPoint.Name;
         this.relStreamPoint.Orientation = streamPoint.Orientation;
         this.relStreamPoint.Index = streamPoint.Index;
         this.relUnitOpPoint.Point = new Point(this.unitOpPoint.Point.X - topLeft.X, this.unitOpPoint.Point.Y - topLeft.Y);
         this.relUnitOpPoint.Name = unitOpPoint.Name;
         this.relUnitOpPoint.Orientation = unitOpPoint.Orientation;
         this.relUnitOpPoint.Index = unitOpPoint.Index;

         this.DrawConnection(null);
         this.Region = new System.Drawing.Region(regionGP);
      }

      /// <summary>
      /// method to draw connection line between solvable controls
      /// </summary>
      /// <param name="g"></param>
      public void DrawConnection(Graphics g) {
         this.ConstructGraphicsPath(g);
      }
      /// <summary>
      /// draw the connection line and construct control region
      /// </summary>
      /// <param name="g"></param>
      /// <param name="start"></param>
      /// <param name="end"></param>
      private void AddLine(Graphics g, Point start, Point end) {
         //UI ui = new UI();
         Pen pen = null;
         Point p1, p2, p3, p4;

         if (this.streamType == StreamType.Material) {
            pen = new Pen(UI.CONNECTION_COLOR_MATERIAL, UI.CONNECTION_WIDTH);
         }
         else if (this.streamType == StreamType.Gas) {
            pen = new Pen(UI.CONNECTION_COLOR_GAS, UI.CONNECTION_WIDTH);
         }
         else if (this.streamType == StreamType.Process) {
            pen = new Pen(UI.CONNECTION_COLOR_PROCESS, UI.CONNECTION_WIDTH);
         }
         if (g != null) {
            g.DrawLine(pen, start, end);
         }

         if (pen != null) {
            pen.Dispose();
         }

         if (start.X == end.X) {//vertical
            if (start.Y < end.Y) {
               p1 = new Point(start.X - UI.CONNECTION_REGION_WIDTH, start.Y + UI.CONNECTION_REGION_WIDTH);
               p2 = new Point(start.X + UI.CONNECTION_REGION_WIDTH, start.Y + UI.CONNECTION_REGION_WIDTH);
               p3 = new Point(end.X + UI.CONNECTION_REGION_WIDTH, end.Y - UI.CONNECTION_REGION_WIDTH);
               p4 = new Point(end.X - UI.CONNECTION_REGION_WIDTH, end.Y - UI.CONNECTION_REGION_WIDTH);
            }
            else {
               p1 = new Point(start.X + UI.CONNECTION_REGION_WIDTH, start.Y - UI.CONNECTION_REGION_WIDTH);
               p2 = new Point(start.X - UI.CONNECTION_REGION_WIDTH, start.Y - UI.CONNECTION_REGION_WIDTH);
               p3 = new Point(end.X - UI.CONNECTION_REGION_WIDTH, end.Y + UI.CONNECTION_REGION_WIDTH);
               p4 = new Point(end.X + UI.CONNECTION_REGION_WIDTH, end.Y + UI.CONNECTION_REGION_WIDTH);
            }
         }
         else if (start.Y == end.Y) {//horizontal
            if (start.X < end.X) {
               p1 = new Point(start.X - UI.CONNECTION_REGION_WIDTH, start.Y + UI.CONNECTION_REGION_WIDTH);
               p2 = new Point(end.X + UI.CONNECTION_REGION_WIDTH, end.Y + UI.CONNECTION_REGION_WIDTH);
               p3 = new Point(end.X + UI.CONNECTION_REGION_WIDTH, end.Y - UI.CONNECTION_REGION_WIDTH);
               p4 = new Point(start.X - UI.CONNECTION_REGION_WIDTH, start.Y - UI.CONNECTION_REGION_WIDTH);

            }
            else {
               p1 = new Point(end.X - UI.CONNECTION_REGION_WIDTH, end.Y + UI.CONNECTION_REGION_WIDTH);
               p2 = new Point(start.X + UI.CONNECTION_REGION_WIDTH, start.Y + UI.CONNECTION_REGION_WIDTH);
               p3 = new Point(start.X + UI.CONNECTION_REGION_WIDTH, start.Y - UI.CONNECTION_REGION_WIDTH);
               p4 = new Point(end.X - UI.CONNECTION_REGION_WIDTH, end.Y - UI.CONNECTION_REGION_WIDTH);
            }
         }
         else {//error, shouldn't go here, log it down
            p1 = new Point(start.X, start.Y - UI.CONNECTION_REGION_WIDTH);
            p2 = new Point(end.X + UI.CONNECTION_REGION_WIDTH, end.Y - UI.CONNECTION_REGION_WIDTH);
            p3 = new Point(end.X + UI.CONNECTION_REGION_WIDTH, end.Y + UI.CONNECTION_REGION_WIDTH);
            p4 = new Point(start.X, start.Y + UI.CONNECTION_REGION_WIDTH);
         }
         regionGP.AddPolygon(new Point[] { p1, p2, p3, p4 });
         return;
      }

      #region construct graphics path method
      private void ConstructGraphicsPath(Graphics g) {
         //GraphicsPath gp = null;
         if (regionGP != null) {
            regionGP.Dispose();
         }

         regionGP = new GraphicsPath();

         if (this.relStreamPoint.Orientation == PointOrientation.E &&
            this.relUnitOpPoint.Orientation == PointOrientation.W) {
            this.ConstructGraphicsPathEtoW(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.S &&
            this.relUnitOpPoint.Orientation == PointOrientation.W) {
            this.ConstructGraphicsPathStoW(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.W &&
            this.relUnitOpPoint.Orientation == PointOrientation.W) {
            this.ConstructGraphicsPathWtoW(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.N &&
            this.relUnitOpPoint.Orientation == PointOrientation.W) {
            this.ConstructGraphicsPathNtoW(g);
         }

         else if (this.relStreamPoint.Orientation == PointOrientation.E &&
            this.relUnitOpPoint.Orientation == PointOrientation.N) {
            this.ConstructGraphicsPathEtoN(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.S &&
            this.relUnitOpPoint.Orientation == PointOrientation.N) {
            this.ConstructGraphicsPathStoN(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.W &&
            this.relUnitOpPoint.Orientation == PointOrientation.N) {
            this.ConstructGraphicsPathWtoN(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.N &&
            this.relUnitOpPoint.Orientation == PointOrientation.N) {
            this.ConstructGraphicsPathNtoN(g);
         }

         else if (this.relStreamPoint.Orientation == PointOrientation.E &&
            this.relUnitOpPoint.Orientation == PointOrientation.E) {
            this.ConstructGraphicsPathEtoE(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.S &&
            this.relUnitOpPoint.Orientation == PointOrientation.E) {
            this.ConstructGraphicsPathStoE(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.W &&
            this.relUnitOpPoint.Orientation == PointOrientation.E) {
            this.ConstructGraphicsPathWtoE(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.N &&
            this.relUnitOpPoint.Orientation == PointOrientation.E) {
            this.ConstructGraphicsPathNtoE(g);
         }

         else if (this.relStreamPoint.Orientation == PointOrientation.E &&
            this.relUnitOpPoint.Orientation == PointOrientation.S) {
            this.ConstructGraphicsPathEtoS(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.S &&
            this.relUnitOpPoint.Orientation == PointOrientation.S) {
            this.ConstructGraphicsPathStoS(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.W &&
            this.relUnitOpPoint.Orientation == PointOrientation.S) {
            this.ConstructGraphicsPathWtoS(g);
         }
         else if (this.relStreamPoint.Orientation == PointOrientation.N &&
            this.relUnitOpPoint.Orientation == PointOrientation.S) {
            this.ConstructGraphicsPathNtoS(g);
         }

         //return gp;
      }

      private void ConstructGraphicsPathEtoW(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath1(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath2(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath4(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath3(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathStoW(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath5(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath6(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath8(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath7(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathWtoW(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath9(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath10(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath10(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath9(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathNtoW(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath13(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath14(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath16(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath15(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathEtoN(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath17(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath18(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath20(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath19(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathStoN(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath21(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath22(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath24(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath23(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathWtoN(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath15(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath16(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath14(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath13(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathNtoN(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath25(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath26(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath26(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath25(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathEtoE(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath27(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath28(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath28(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath27(g, u, s);
         }

         //return null;
      }

      private void ConstructGraphicsPathStoE(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath29(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath30(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath32(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath31(g, u, s);
         }

         //return gp;
      }

      private void ConstructGraphicsPathWtoE(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath3(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath4(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath2(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath1(g, u, s);
         }

         //return gp;
      }

      private void ConstructGraphicsPathNtoE(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath19(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath20(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath18(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath17(g, u, s);
         }

         //return gp;
      }

      private void ConstructGraphicsPathEtoS(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath31(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath32(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath30(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath29(g, u, s);
         }

         //return gp;
      }

      private void ConstructGraphicsPathStoS(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath11(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath12(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath12(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath11(g, u, s);
         }

         //return gp;
      }

      private void ConstructGraphicsPathWtoS(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath7(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath8(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath6(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath5(g, u, s);
         }

         //return gp;
      }

      private void ConstructGraphicsPathNtoS(Graphics g) {
         //GraphicsPath gp = new GraphicsPath();

         Point s = this.relStreamPoint.Point;
         Point u = this.relUnitOpPoint.Point;
         if (s.X < u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath23(g, s, u);
         }
         else if (s.X >= u.X && s.Y < u.Y) {
            this.ConstructGraphicsPath24(g, u, s);
         }
         else if (s.X < u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath22(g, s, u);
         }
         else if (s.X >= u.X && s.Y >= u.Y) {
            this.ConstructGraphicsPath21(g, u, s);
         }

         //return gp;
      }

      private void ConstructGraphicsPath1(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();


         Point p1 = new Point((s.X + e.X) / 2, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath2(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y) / 2);
         Point p3 = new Point(e.X + UI.CONNECTION_DELTA, p2.Y);
         Point p4 = new Point(p3.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, p4);
         AddLine(g, p4, e);

         //return gp;
      }

      private void ConstructGraphicsPath3(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y) / 2);
         Point p3 = new Point(e.X + UI.CONNECTION_DELTA, p2.Y);
         Point p4 = new Point(p3.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, p4);
         AddLine(g, p4, e);

         //return gp;
      }

      private void ConstructGraphicsPath4(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point((s.X + e.X) / 2, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath5(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, e);

         //return gp;
      }

      private void ConstructGraphicsPath6(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y) / 2);
         Point p3 = new Point(e.X, p2.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath7(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y + UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath8(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point((s.X + e.X) / 2, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath9(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath10(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath11(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath12(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath13(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point((s.X + e.X) / 2, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath14(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y - UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath15(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, (s.Y + e.Y) / 2);
         Point p3 = new Point(e.X, p2.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath16(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, e);

         //return gp;
      }

      private void ConstructGraphicsPath17(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X, s.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, e);

         //return gp;
      }

      private void ConstructGraphicsPath18(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y) / 2);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath19(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath20(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point((s.X + e.X) / 2, s.Y);
         Point p2 = new Point(p1.X, e.Y - UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath21(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y) / 2);
         Point p2 = new Point(e.X, p1.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath22(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y) / 2);
         Point p2 = new Point(e.X, p1.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath23(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X - UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point((s.X + e.X) / 2, p1.Y);
         Point p3 = new Point(p2.X, e.Y + UI.CONNECTION_DELTA);
         Point p4 = new Point(e.X, p3.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, p4);
         AddLine(g, p4, e);

         //return gp;
      }

      private void ConstructGraphicsPath24(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point((s.X + e.X) / 2, p1.Y);
         Point p3 = new Point(p2.X, e.Y - UI.CONNECTION_DELTA);
         Point p4 = new Point(e.X, p3.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, p4);
         AddLine(g, p4, e);

         //return gp;
      }

      private void ConstructGraphicsPath25(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath26(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, e.Y - UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X, p1.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath27(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X + UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath28(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X + UI.CONNECTION_DELTA, s.Y);
         Point p2 = new Point(p1.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, e);

         //return gp;
      }

      private void ConstructGraphicsPath29(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, (s.Y + e.Y) / 2);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, (s.Y + e.Y) / 2);
         Point p3 = new Point(p2.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath30(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(e.X, s.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, e);

         //return gp;
      }

      private void ConstructGraphicsPath31(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point((s.X + e.X) / 2, s.Y);
         Point p2 = new Point(p1.X, e.Y + UI.CONNECTION_DELTA);
         Point p3 = new Point(e.X, p2.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);

         //return gp;
      }

      private void ConstructGraphicsPath32(Graphics g, Point s, Point e) {
         // s = start, e = end
         //GraphicsPath gp = new GraphicsPath();

         Point p1 = new Point(s.X, s.Y + UI.CONNECTION_DELTA);
         Point p2 = new Point(e.X + UI.CONNECTION_DELTA, p1.Y);
         Point p3 = new Point(p2.X, e.Y);
         AddLine(g, s, p1);
         AddLine(g, p1, p2);
         AddLine(g, p2, p3);
         AddLine(g, p3, e);
         //return gp;
      }
      #endregion


      #region Serialization method
      public virtual void SetObjectData(SerializationInfo info, StreamingContext context) {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionSolvableConnection", typeof(int));
         switch (persistedClassVersion) {
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
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionSolvableConnection", SolvableConnection.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("StreamType", this.StreamType, typeof(StreamType));
         info.AddValue("StreamPoint", this.StreamPoint, typeof(ConnectionPoint));
         info.AddValue("UnitOpPoint", this.UnitOpPoint, typeof(ConnectionPoint));
      }
      #endregion
      //protected override CreateParams CreateParams
      //{

      //    get
      //    {

      //        CreateParams cp = base.CreateParams;

      //        cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT

      //        return cp;

      //    }

      //}

      private void InitializeComponent() {
         this.SuspendLayout();
         // 
         // SolvableConnection
         // 
         this.BackColor = System.Drawing.Color.Transparent;
         this.DoubleBuffered = true;
         this.Name = "SolvableConnection";
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SolvableConnection_MouseDown);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.SolvableConnection_Paint);
         this.MouseHover += new System.EventHandler(this.SolvableConnection_MouseHover);
         this.ResumeLayout(false);

      }
      #region event handling method
      /// <summary>
      /// handle the paint event of this control
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void SolvableConnection_Paint(object sender, PaintEventArgs e) {
         //base.OnPaint(e);
         DrawConnection(e.Graphics);
      }
      //private void DrawBorder()
      //{
      //    UI  ui = new UI();
      //    Graphics g = this.CreateGraphics();
      //    Pen pen = new Pen(ui.SELECTION_COLOR, 0.5f);
      //    g.DrawPath(pen, this.regionGP);
      //}
      /// <summary>
      /// handle the left mouse click event
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void SolvableConnection_MouseDown(object sender, MouseEventArgs e) {
         if (e.Button == System.Windows.Forms.MouseButtons.Left) {
            if (this.flowsheet.Activity == FlowsheetActivity.DeletingConnection) {
               //this.flowsheet.Controls.Remove(this);
               this.flowsheet.ConnectionManager.DeleteConnection(this);
               this.flowsheet.ResetActivity();
            }
            //else
            //    this.DrawBorder();
         }
      }
      /// <summary>
      /// handle the mouse hover event
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void SolvableConnection_MouseHover(object sender, EventArgs e) {
         if (this.flowsheet.Activity == FlowsheetActivity.DeletingConnection)
            this.flowsheet.Cursor = Cursors.Cross;
      }
      #endregion
   }
}
