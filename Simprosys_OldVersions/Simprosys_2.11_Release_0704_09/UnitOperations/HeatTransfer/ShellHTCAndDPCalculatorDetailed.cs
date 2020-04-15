using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Plots;

namespace Prosimo.UnitOperations.HeatTransfer
{
//	/// <summary>
//	/// Summary description for ShellHTCAndDPCalculator.
//	/// </summary>
//   [Serializable]
//   public class ShellHTCAndDPCalculatorDetailed : ShellHTCAndDPCalculator 
//   {
//      //private const int CLASS_PERSISTENCE_VERSION = 1; 
//      //private HXRatingModelShellAndTube ratingModel;
//
//      //Calculated variables
//      private double baffleCutValue;
//      private double tubePitchParalellToFlow;
//      private double tubePitchNormalToFlow;
//      private double fractionOfTotalTubesInCrossFlow;
//      private int crossFlowRowsInEachWindow;
//      private int tubeRowsInOneCrossFlowSetion;
//      private double crossFlowArea;
//      private double tubeToBaffleLeakageArea;
//      private double shellToBaffleLeakageArea;
//      private double fractionOfCrossFlowAreaForBypass;
//      private double areaForFlowThroughWindow;
//      private double equivalentDiameterOfWindow;
//
//      //Perry's Chemical Engineers Handbook, Page 11-9, Fig 11-10
//      //Curve: (0, 0.525), (0.4, 0.86), (0.6, 0.95), (0.78, 1.1), (0.9, 1.15), (0.945, 1.14), (0.973, 1.1), (0.99, 1.07), (1.0, 1.0) 
//      static readonly PointF[] baffleConfigCurve = {new PointF(0, 0.525f), new PointF(0.4f, 0.86f), new PointF (0.6f, 0.95f), 
//                                             new PointF (0.78f, 1.1f),  new PointF(0.9f, 1.15f), new PointF(0.945f, 1.14f),
//                                             new PointF(0.973f, 1.1f),  new PointF(0.99f, 1.07f), new PointF(1.0f, 1.0f)};
//      
//      static readonly PointF[] jFactorCurve1 = new PointF[6];
//      static readonly PointF[] jFactorCurve2 = new PointF[6];
//      static readonly PointF[] jFactorCurve3 = new PointF[7];
//
//      static readonly CurveF[] baffleLeakageCurves = new CurveF[5]; 
//      static readonly CurveF[] baffleBypassCurvesRe1 = new CurveF[6]; 
//      static readonly CurveF[] baffleBypassCurvesRe2 = new CurveF[6]; 
//      static readonly CurveF[] lowReCorrectionCurves = new CurveF[7]; 
//      static readonly CurveF[] intermediateReCorrectionCurves = new CurveF[9]; 
//
//      static readonly CurveF[] baffleLeakageDpCurves = new CurveF[5]; 
//      static readonly CurveF[] baffleBypassDpCurvesRe1 = new CurveF[6]; 
//      static readonly CurveF[] baffleBypassDpCurvesRe2 = new CurveF[6];
//      static readonly PointF[] fkFactorCurve1 = new PointF[5];
//      static readonly PointF[] fkFactorCurve2 = new PointF[7];
//      static readonly PointF[] fkFactorCurve3 = new PointF[11];
//
////      public ShellHTCAndDPCalculator(HXRatingModelShellAndTube ratingModel) {
////         this.ratingModel = ratingModel;
////      }
//      public ShellHTCAndDPCalculatorDetailed(HXRatingModelShellAndTube ratingModel) : base (ratingModel) 
//      {
//         InitializeVarList();
//         InitializeGeometryParams();
//      }
//
//      private void InitializeVarList() 
//      {
//         procVarList.Add(ratingModel.TubePitch);
//         procVarList.Add(ratingModel.ShellInnerDiameter);
//         procVarList.Add(ratingModel.ShellOuterTubeLimit);
//         procVarList.Add(ratingModel.BaffleCut);
//         procVarList.Add(ratingModel.BaffleSpacing);
//         procVarList.Add(ratingModel.NumberOfBaffles);
//         procVarList.Add(ratingModel.EntranceExitBaffleSpacing);
//         procVarList.Add(ratingModel.DiametralShellToBaffleClearance);
//         procVarList.Add(ratingModel.SealingStrips);
//
//         ratingModel.ProcVarList.AddRange(procVarList);
//         owner.AddVarsOnListAndRegisterInSystem(procVarList);
//      }
//
//      static ShellHTCAndDPCalculatorDetailed() 
//      {
//         //Perry's Chemical Engineers Handbook, Page 11-9, Fig 11-9
//         //Curve 1: (2.37, 1.0), (10, 0.33), (100, 0.0689),              (1000, 0.021), (1.0e4, 9.0e-3), (1.0e5, 4.0e-3)
//         //Curve 2: (1.8, 1.0), (10, 0.298), (100, 0.0578),              (1000, 0.021), (1.0e4, 9.0e-3), (1.0e5, 4.0e-3)
//         //Curve 3: (1.0, 0.8), (10, 0.198), (200, 0.0298), (500, 0.02), (1000, 0.016), (1.0e4, 9.0e-3), (1.0e5, 4.0e-3)
//         PointF p4 = new PointF(3.0f, (float)Math.Log10(0.021));
//         PointF p6 = new PointF(4.0f, (float)Math.Log10(9.0e-3));
//         PointF p7 = new PointF(5.0f, (float)Math.Log10(4.0e-3));
//         jFactorCurve1[0] = new PointF((float)Math.Log10(2.3), 0.0f);
//         jFactorCurve1[1] = new PointF(1.0f, (float)Math.Log10(0.33));
//         jFactorCurve1[2] = new PointF(2.0f, (float)Math.Log10(0.0689f));
//         jFactorCurve1[3] = p4;
//         jFactorCurve1[4] = p6;
//         jFactorCurve1[5] = p7; 
//
//         jFactorCurve2[0] = new PointF((float)Math.Log10(1.8), 0.0f);
//         jFactorCurve2[1] = new PointF(1.0f, (float)Math.Log10(0.298));
//         jFactorCurve2[2] = new PointF(2.0f, (float)Math.Log10(0.0578));
//         jFactorCurve2[3] = p4;
//         jFactorCurve2[4] = p6;
//         jFactorCurve2[5] = p7;
//
//         jFactorCurve3[0] = new PointF(0.0f, (float)Math.Log10(0.8));
//         jFactorCurve3[1] = new PointF(1.0f, (float)Math.Log10(0.198));
//         jFactorCurve3[2] = new PointF((float)Math.Log10(200), (float)Math.Log10(0.0298));
//         jFactorCurve3[3] = new PointF((float)Math.Log10(500), (float)Math.Log10(0.02));
//         jFactorCurve3[4] = new PointF(4.0f, (float)Math.Log10(0.016));
//         jFactorCurve3[5] = p6;
//         jFactorCurve3[6] = p7;
//
//         //Perry's Chemical Engineers Handbook, Page 11-9, Fig 11-11
//         //curve 1 - 0:    (0.0, 1.0), (0.036, 0.92), (0.06, 0.88),  (0.1, 0.865), (0.125, 0.845), (0.9, 0.49)
//         //curve 2 - 0.25: (0.0, 1.0), (0.036, 0.90), (0.06, 0.86),  (0.1, 0.830), (0.125, 0.805), (0.9, 0.37)
//         //curve 3 - 0.50: (0.0, 1.0), (0.036, 0.88), (0.06, 0.84),  (0.1, 0.790), (0.125, 0.775), (0.9, 0.245)
//         //curve 4 - 0.75: (0.0, 1.0), (0.036, 0.86), (0.06, 0.805), (0.1, 0.75),  (0.125, 0.730), (0.9, 0.120)
//         //curve 5 - 1.0:  (0.0, 1.0), (0.036, 0.82), (0.06, 0.765), (0.1, 0.715), (0.125, 0.685), (0.9, 0.025)
//         PointF p0 = new PointF(0.0f, 1.0f);
//         PointF[] ps00 = {p0, new PointF(0.036f, 0.92f), new PointF(0.06f, 0.88f),  new PointF(0.1f, 0.865f), new PointF(0.125f, 0.845f), new PointF(0.9f, 0.49f)};
//         PointF[] ps01 = {p0, new PointF(0.036f, 0.90f), new PointF(0.06f, 0.86f),  new PointF(0.1f, 0.830f), new PointF(0.125f, 0.805f), new PointF(0.9f, 0.37f)};
//         PointF[] ps02 = {p0, new PointF(0.036f, 0.88f), new PointF(0.06f, 0.84f),  new PointF(0.1f, 0.790f), new PointF(0.125f, 0.775f), new PointF(0.9f, 0.245f)};
//         PointF[] ps03 = {p0, new PointF(0.036f, 0.86f), new PointF(0.06f, 0.805f), new PointF(0.1f, 0.75f), new PointF(0.125f, 0.730f), new PointF(0.9f, 0.120f)};
//         PointF[] ps04 = {p0, new PointF(0.036f, 0.82f), new PointF(0.06f, 0.765f), new PointF(0.1f, 0.715f), new PointF(0.125f, 0.685f), new PointF(0.9f, 0.025f)};
//         baffleLeakageCurves[0] = new CurveF(0.0f, ps00);
//         baffleLeakageCurves[1] = new CurveF(0.25f, ps01);
//         baffleLeakageCurves[2] = new CurveF(0.50f, ps02);
//         baffleLeakageCurves[3] = new CurveF(0.75f, ps03);
//         baffleLeakageCurves[4] = new CurveF(1.0f, ps04);
//         
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-12
//         //curve 0 - Nss/Nc >= 0.5:   (0, 1.0), (0.7, 1.0)
//         //curve 1a - Nss/Nc = 0.3:   (0, 1.0), (0.7, 0.88)
//         //curve 1b:                  (0, 1.0), (0.7, 0.85)
//         //curve 2a - Nss/Nc = 0.167: (0, 1.0), (0.7, 0.77)
//         //curve 2b:                  (0, 1.0), (0.7, 0.72)
//         //curve 3a - Nss/Nc = 0.1:   (0, 1.0), (0.7, 0.695)
//         //curve 3b:                  (0, 1.0), (0.7, 0.675)
//         //curve 4a - Nss/Nc = 0.05:  (0, 1.0), (0.7, 0.62)
//         //curve 4b:                  (0, 1.0), (0.7, 0.595)
//         //curve 5a  - Nss/Nc = 0.0:  (0, 1.0), (0.7, 0.418)
//         //curve 5b:                  (0, 1.0), (0.7, 0.385)
//         p0 = new PointF(0.0f, 0.0f);
//         PointF[] ps10 = {p0, new PointF(0.7f, (float)Math.Log10(1.0))};
//         PointF[] ps11 = {p0, new PointF(0.7f, (float)Math.Log10(0.88))};
//         PointF[] ps12 = {p0, new PointF(0.7f, (float)Math.Log10(0.77))};
//         PointF[] ps13 = {p0, new PointF(0.7f, (float)Math.Log10(0.695))};
//         PointF[] ps14 = {p0, new PointF(0.7f, (float)Math.Log10(0.62))};
//         PointF[] ps15 = {p0, new PointF(0.7f, (float)Math.Log10(0.418))};
//         baffleBypassCurvesRe1[0] = new CurveF(0.0f, ps15);
//         baffleBypassCurvesRe1[1] = new CurveF(0.05f, ps14);
//         baffleBypassCurvesRe1[2] = new CurveF(0.1f, ps13);
//         baffleBypassCurvesRe1[3] = new CurveF(0.167f, ps12);
//         baffleBypassCurvesRe1[4] = new CurveF(0.3f, ps11);
//         baffleBypassCurvesRe1[5] = new CurveF(0.5f, ps10);
//
//         PointF[] ps20 = {p0, new PointF(0.7f, (float)Math.Log10(1.0))};
//         PointF[] ps21 = {p0, new PointF(0.7f, (float)Math.Log10(0.85))};
//         PointF[] ps22 = {p0, new PointF(0.7f, (float)Math.Log10(0.72))};
//         PointF[] ps23 = {p0, new PointF(0.7f, (float)Math.Log10(0.675))};
//         PointF[] ps24 = {p0, new PointF(0.7f, (float)Math.Log10(0.595))};
//         PointF[] ps25 = {p0, new PointF(0.7f, (float)Math.Log10(0.385))};
//         baffleBypassCurvesRe2[0] = new CurveF(0.0f, ps25);
//         baffleBypassCurvesRe2[1] = new CurveF(0.05f, ps24);
//         baffleBypassCurvesRe2[2] = new CurveF(0.1f, ps23);
//         baffleBypassCurvesRe2[3] = new CurveF(0.167f, ps22);
//         baffleBypassCurvesRe2[4] = new CurveF(0.3f, ps21);
//         baffleBypassCurvesRe2[5] = new CurveF(0.5f, ps20);
//
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-13
//         //curve 1 - Nc + Ncw = 3: (2, 0.98),  (5, 0.9),   (10, 0.802), (15, 0.76),  (20, 0.72),  (30, 0.675), (50, 0.61)
//         //curve 2 - Nc + Ncw = 5: (2, 0.90),  (5, 0.815), (10, 0.735), (15, 0.69),  (20, 0.665), (30, 0.61),  (50, 0.575)
//         //curve 3 - Nc + Ncw = 10 (2, 0.80),  (5, 0.73),  (10, 0.66),  (15, 0.605), (20, 0.58),  (30, 0.53),  (50, 0.50)
//         //curve 4 - Nc + Ncw = 20 (2, 0.705), (5, 0.65),  (10, 0.58),  (15, 0.54),  (20, 0.515), (30, 0.475), (50, 0.435)
//         //curve 5 - Nc + Ncw = 30 (2, 0.65),  (5, 0.60),  (10, 0.535), (15, 0.50),  (20, 0.47),  (30, 0.435), (50, 0.40)
//         //curve 6 - Nc + Ncw = 40 (2, 0.63),  (5, 0.57),  (10, 0.51),  (15, 0.475), (20, 0.44),  (30, 0.415), (50, 0.38)
//         //curve 7 - Nc + Ncw = 50 (2, 0.60),  (5, 0.56),  (10, 0.495), (15, 0.465), (20, 0.43),  (30, 0.40),  (50, 0.365)
//         PointF[] ps30 = {new PointF(2f, 0.98f),  new PointF(5f, 0.9f),  new PointF(10f, 0.802f),  new PointF(15f, 0.76f),  new PointF(20f, 0.72f),  new PointF(30f, 0.675f), new PointF(50f, 0.61f)};
//         PointF[] ps31 = {new PointF(2f, 0.90f),  new PointF(5f, 0.815f), new PointF(10f, 0.735f), new PointF(15f, 0.69f),  new PointF(20f, 0.665f), new PointF(30f, 0.61f),  new PointF(50f, 0.575f)};
//         PointF[] ps32 = {new PointF(2f, 0.80f),  new PointF(5f, 0.73f),  new PointF(10f, 0.66f),  new PointF(15f, 0.605f), new PointF(20f, 0.58f),  new PointF(30f, 0.53f),  new PointF(50f, 0.50f)};
//         PointF[] ps33 = {new PointF(2f, 0.705f), new PointF(5f, 0.65f),  new PointF(10f, 0.58f),  new PointF(15f, 0.54f),  new PointF(20f, 0.515f), new PointF(30f, 0.475f), new PointF(50f, 0.435f)};
//         PointF[] ps34 = {new PointF(2f, 0.65f),  new PointF(5f, 0.60f),  new PointF(10f, 0.535f), new PointF(15f, 0.50f),  new PointF(20f, 0.47f),  new PointF(30f, 0.435f), new PointF(50f, 0.40f)};
//         PointF[] ps35 = {new PointF(2f, 0.63f),  new PointF(5f, 0.57f),  new PointF(10f, 0.51f),  new PointF(15f, 0.475f), new PointF(20f, 0.44f),  new PointF(30f, 0.415f), new PointF(50f, 0.38f)};
//         PointF[] ps36 = {new PointF(2f, 0.60f),  new PointF(5f, 0.56f),  new PointF(10f, 0.495f), new PointF(15f, 0.465f), new PointF(20f, 0.43f),  new PointF(30f, 0.40f),  new PointF(50f, 0.365f)};
//         lowReCorrectionCurves[0] = new CurveF(3.0f, ps30);
//         lowReCorrectionCurves[1] = new CurveF(5.0f, ps31);
//         lowReCorrectionCurves[2] = new CurveF(10.0f, ps32);
//         lowReCorrectionCurves[3] = new CurveF(20.0f, ps33);
//         lowReCorrectionCurves[4] = new CurveF(30.0f, ps34);
//         lowReCorrectionCurves[5] = new CurveF(40.0f, ps35);
//         lowReCorrectionCurves[6] = new CurveF(50.0f, ps36);
//
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-14
//         //curve 1 - Re >= 100 (0.3, 1.0), (1.0, 1 .0)
//         //curve 2 - Re = 90   (0.3, 0.915), (1.0, 1 .0)
//         //curve 3 - Re = 80   (0.3, 0.825), (1.0, 1 .0)
//         //curve 4 - Re = 70   (0.3, 0.735), (1.0, 1 .0)
//         //curve 5 - Re = 60   (0.3, 0.66),  (1.0, 1 .0)
//         //curve 6 - Re = 50   (0.3, 0.57),  (1.0, 1 .0)
//         //curve 7 - Re = 40   (0.3, 0.475), (1.0, 1 .0)
//         //curve 8 - Re = 30   (0.3, 0.39),  (1.0, 1 .0)
//         //curve 9 - Re = 20   (0.3, 0.3), (1.0, 1 .0)
//         PointF p1 = new PointF(1.0f, 1.0f);
//         PointF[] ps40 = {new PointF(0.3f, 1.0f), p1};
//         PointF[] ps41 = {new PointF(0.3f, 0.915f), p1};
//         PointF[] ps42 = {new PointF(0.3f, 0.825f), p1};
//         PointF[] ps43 = {new PointF(0.3f, 0.735f), p1};
//         PointF[] ps44 = {new PointF(0.3f, 0.66f), p1};
//         PointF[] ps45 = {new PointF(0.3f, 0.57f), p1};
//         PointF[] ps46 = {new PointF(0.3f, 0.475f), p1};
//         PointF[] ps47 = {new PointF(0.3f, 0.39f), p1};
//         PointF[] ps48 = {new PointF(0.3f, 0.3f), p1};
//         intermediateReCorrectionCurves[8] = new CurveF(100.0f, ps40);
//         intermediateReCorrectionCurves[7] = new CurveF(90.0f, ps41);
//         intermediateReCorrectionCurves[6] = new CurveF(80.0f, ps42);
//         intermediateReCorrectionCurves[5] = new CurveF(70.0f, ps43);
//         intermediateReCorrectionCurves[4] = new CurveF(60.0f, ps44);
//         intermediateReCorrectionCurves[3] = new CurveF(50.0f, ps45);
//         intermediateReCorrectionCurves[2] = new CurveF(40.0f, ps46);
//         intermediateReCorrectionCurves[1] = new CurveF(30.0f, ps47);
//         intermediateReCorrectionCurves[0] = new CurveF(20.0f, ps48);
//
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-15a
//         //curve 1: (1, 70), (90, 0.8),  (200, 0.365), (500, 0.3),  (1.0e5, 0.12)
//         //curve 2: (1, 54), (125, 0.5), (200, 0.34),  (400, 0.22), (800, 0.196), (3000, 0.154), (1.0e5, 0.09)
//         //curve 3: (1, 49), (50, 1.0),  (125, 0.5),   (300, 0.3),  (500, 0.25),  (1000, 0.22),  (1.0e5, 0.12)
//         //curve 4: (1, 40), (70, 0.6),  (200, 0.295), (400, 0.205), (700, 0.17), (1000, 0.168), (3000, 0.154), (1.0e5, 0.09)
//         
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-15b
//         //curve 1: (1, 68),    (100, 0.55), (200, 0.33), (400, 0.21),  (700, 0.155), (1000, 0.17)  (1300, 0.155), (2000, 0.165), (3000, 0.199), (6000, 0.2), (1.0e5, 0.18)
//         //curve 2: (1, 50.5), (100, 0.45),  (200, 0.28), (400, 0.185), (700, 0.14),  (1000, 0.16), (1300, 0.14),  (2000, 0.18),  (3000, 0.175), (6000, 0.17),(1.0e5, 0.175)
//         fkFactorCurve1[0] = new PointF(0.0f, (float)Math.Log10(70.0));
//         fkFactorCurve1[1] = new PointF((float)Math.Log10(90), (float)Math.Log10(0.8));
//         fkFactorCurve1[2] = new PointF((float)Math.Log10(200.0), (float)Math.Log10(0.0365));
//         fkFactorCurve1[3] = new PointF((float)Math.Log10(500), (float)Math.Log10(0.3));
//         fkFactorCurve1[4] = new PointF(5.0f, (float)Math.Log10(0.12));
//
//         fkFactorCurve2[0] = new PointF(0.0f, (float)Math.Log10(54.0));
//         fkFactorCurve2[1] = new PointF((float)Math.Log10(125.0), (float)Math.Log10(0.5));
//         fkFactorCurve2[2] = new PointF((float)Math.Log10(200.0), (float)Math.Log10(0.34));
//         fkFactorCurve2[3] = new PointF((float)Math.Log10(400), (float)Math.Log10(0.22));
//         fkFactorCurve2[4] = new PointF((float)Math.Log10(800), (float)Math.Log10(0.196));
//         fkFactorCurve2[5] = new PointF((float)Math.Log10(3000), (float)Math.Log10(0.154));
//         fkFactorCurve2[6] = new PointF(5.0f, (float)Math.Log10(0.09));
//
//         fkFactorCurve3[0] = new PointF(0.0f, (float)Math.Log10(68));
//         fkFactorCurve3[1] = new PointF(2.0f, (float)Math.Log10(0.55));
//         fkFactorCurve3[2] = new PointF((float)Math.Log10(200), (float)Math.Log10(0.33));
//         fkFactorCurve3[3] = new PointF((float)Math.Log10(400), (float)Math.Log10(0.21));
//         fkFactorCurve3[4] = new PointF((float)Math.Log10(700), (float)Math.Log10(0.155));
//         fkFactorCurve3[5] = new PointF(4.0f, (float)Math.Log10(0.17));
//         fkFactorCurve3[6] = new PointF((float)Math.Log10(1300), (float)Math.Log10(0.155));
//         fkFactorCurve3[7] = new PointF((float)Math.Log10(2000), (float)Math.Log10(0.165));
//         fkFactorCurve3[8] = new PointF((float)Math.Log10(3000), (float)Math.Log10(0.199));
//         fkFactorCurve3[9] = new PointF((float)Math.Log10(6000), (float)Math.Log10(0.2));
//         fkFactorCurve3[10] = new PointF(5.0f, (float)Math.Log10(0.18));
//         
//         //Perry's Chemical Engineers Handbook, Page 11-11, Fig 11-16
//         //curve 1 - 0:    (0.0, 1.0), (0.05, 0.8),   (0.1, 0.72),  (0.15, 0.675), (0.2, 0.62),  (0.3, 0.47),  (0.75, 0.3)
//         //curve 2 - 0.25: (0.0, 1.0), (0.05, 0.75),  (0.1, 0.65),  (0.15, 0.58),  (0.2, 0.525), (0.3, 0.4650, (0.75, 0.135)
//         //curve 3 - 0.50: (0.0, 1.0), (0.05, 0.70),  (0.1, 0.575), (0.15, 0.485), (0.2, 0.425), (0.3, 0.345), (0.58, 0.10)
//         //curve 4 - 0.75: (0.0, 1.0), (0.05, 0.67),  (0.1, 0.52),  (0.15, 0.415), (0.2, 0.35),  (0.3, 0.225). (0.4, 0.135)
//         //curve 5 - 1.0:  (0.0, 1.0), (0.05, 0.585), (0.1, 0.435), (0.15, 0.32),  (0.2, 0.245), (0.31, 0.1),  (0.9, 0.025)
//         p0 = new PointF(0.0f, 1.0f);
//         PointF[] ps50 = {p0, new PointF(0.05f, 0.8f),   new PointF(0.1f, 0.72f),  new PointF(0.15f, 0.675f), new PointF(0.2f, 0.62f),  new PointF(0.3f, 0.47f),  new PointF(0.75f, 0.3f)};
//         PointF[] ps51 = {p0, new PointF(0.05f, 0.75f),  new PointF(0.1f, 0.65f),  new PointF(0.15f, 0.58f),  new PointF(0.2f, 0.525f), new PointF(0.3f, 0.4650f), new PointF(0.75f, 0.135f)};
//         PointF[] ps52 = {p0, new PointF(0.05f, 0.70f),  new PointF(0.1f, 0.575f), new PointF(0.15f, 0.485f), new PointF(0.2f, 0.425f), new PointF(0.3f, 0.345f), new PointF(0.58f, 0.10f)};
//         PointF[] ps53 = {p0, new PointF(0.05f, 0.67f),  new PointF(0.1f, 0.52f),  new PointF(0.15f, 0.415f), new PointF(0.2f, 0.35f),  new PointF(0.3f, 0.225f), new PointF(0.4f, 0.135f)};
//         PointF[] ps54 = {p0, new PointF(0.05f, 0.585f), new PointF(0.1f, 0.435f), new PointF(0.15f, 0.32f),  new PointF(0.2f, 0.245f), new PointF(0.31f, 0.1f),  new PointF(0.9f, 0.025f)};
//         baffleLeakageDpCurves[0] = new CurveF(0.0f, ps50);
//         baffleLeakageDpCurves[1] = new CurveF(0.25f, ps51);
//         baffleLeakageDpCurves[2] = new CurveF(0.50f, ps52);
//         baffleLeakageDpCurves[3] = new CurveF(0.75f, ps53);
//         baffleLeakageDpCurves[4] = new CurveF(1.0f, ps54);
//         
//         //Perry's Chemical Engineers Handbook, Page 11-11, Fig 11-17
//         //curve 0  - Nss/Nc = 0.5:   (0, 1.0), (0.7, 1.0)
//         //curve 1a - Nss/Nc = 0.3:   (0, 1.0), (0.7, 0.65)
//         //curve 1b:                  (0, 1.0), (0.7, 0.60)
//         //curve 2a - Nss/Nc = 0.167: (0, 1.0), (0.7, 0.44)
//         //curve 2b:                  (0, 1.0), (0.7, 0.398)
//         //curve 3a - Nss/Nc = 0.1:   (0, 1.0), (0.7, 0.336)
//         //curve 3b:                  (0, 1.0), (0.7, 0.27)
//         //curve 4a - Nss/Nc = 0.05:  (0, 1.0), (0.7, 0.24)
//         //curve 4b:                  (0, 1.0), (0.7, 0.183)
//         //curve 5a  - Nss/Nc = 0.0:  (0, 1.0), (0.605, 0.1)
//         //curve 5b:                  (0, 1.0), (0.51, 0.1)
//         p0 = new PointF(0.0f, 0.0f);
//         PointF[] ps60 = {p0, new PointF(0.7f, (float)Math.Log10(1.0))};
//         PointF[] ps61 = {p0, new PointF(0.7f, (float)Math.Log10(0.65))};
//         PointF[] ps62 = {p0, new PointF(0.7f, (float)Math.Log10(0.44))};
//         PointF[] ps63 = {p0, new PointF(0.7f, (float)Math.Log10(0.336))};
//         PointF[] ps64 = {p0, new PointF(0.7f, (float)Math.Log10(0.24))};
//         PointF[] ps65 = {p0, new PointF(0.605f, (float)Math.Log10(0.1))};
//         baffleBypassDpCurvesRe1[5] = new CurveF(0.5f, ps60);
//         baffleBypassDpCurvesRe1[4] = new CurveF(0.3f, ps61);
//         baffleBypassDpCurvesRe1[3] = new CurveF(0.167f, ps63);
//         baffleBypassDpCurvesRe1[2] = new CurveF(0.1f, ps64);
//         baffleBypassDpCurvesRe1[1] = new CurveF(0.05f, ps64);
//         baffleBypassDpCurvesRe1[0] = new CurveF(0.0f, ps65);
//         
//         PointF[] ps70 = {p0, new PointF(0.7f, (float)Math.Log10(1.0))};
//         PointF[] ps71 = {p0, new PointF(0.7f, (float)Math.Log10(0.60))};
//         PointF[] ps72 = {p0, new PointF(0.7f, (float)Math.Log10(0.398))};
//         PointF[] ps73 = {p0, new PointF(0.7f, (float)Math.Log10(0.27))};
//         PointF[] ps74 = {p0, new PointF(0.7f, (float)Math.Log10(0.183))};
//         PointF[] ps75 = {p0, new PointF(0.51f, (float)Math.Log10(0.1))};
//         baffleBypassDpCurvesRe2[5] = new CurveF(0.5f, ps70);
//         baffleBypassDpCurvesRe2[4] = new CurveF(0.3f, ps71);
//         baffleBypassDpCurvesRe2[3] = new CurveF(0.167f, ps72);
//         baffleBypassDpCurvesRe2[2] = new CurveF(0.1f, ps73);
//         baffleBypassDpCurvesRe2[1] = new CurveF(0.05f, ps74);
//         baffleBypassDpCurvesRe2[0] = new CurveF(0.0f, ps75);
//      }
//
//      internal override double CalculateSinglePhaseHTC(double massFlowRate, double density, double bulkViscosity, double wallViscosity, double thermCond, double specificHeat) {
//         double p = ratingModel.TubePitch.Value;
//         double pp = tubePitchParalellToFlow;
//         double pn = tubePitchNormalToFlow;
//
//         double ls = ratingModel.BaffleSpacing.Value;
//         double lc = ratingModel.BaffleCut.Value;
//         double Nc = tubeRowsInOneCrossFlowSetion;
//         double Fc = fractionOfTotalTubesInCrossFlow;
//         double Ncw = crossFlowRowsInEachWindow;
//         double Sm = crossFlowArea;
//         double Fbp = fractionOfCrossFlowAreaForBypass;
//         double Stb = tubeToBaffleLeakageArea;
//         double Ssb = shellToBaffleLeakageArea;
//         double Sw = areaForFlowThroughWindow;
//         double Dw = equivalentDiameterOfWindow;
//         int Nb = ratingModel.NumberOfBaffles.Value;
//         double Do = ratingModel.TubeOuterDiameter.Value;
//         int Nss = ratingModel.SealingStrips.Value;
//         TubeLayout layout = ratingModel.TubeLayout;
//
//         double Re = Do*massFlowRate/(bulkViscosity*Sm);
//         double h = specificHeat*massFlowRate/Sm*Math.Pow(thermCond/(specificHeat*bulkViscosity), 2.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
//         double jk = CalculatejFactorForIdealTubeBank(Re, layout);
//         double hk = jk*h;
//         double Jc = CalculateBaffleConfigFactor(Fc);
//         double Jl = CalculateBaffleLeakageFactor(Sm, Ssb, Stb);
//         double Jb = CalculateBundleBypassFactor(Re, Fbp, Nss, Nc);
//         double Jr = 1.0;
//         if (Re < 100) {
//            Jr = CalculateAdverseTempGradientFactor(Nb, Nc, Ncw);
//         }
//         if (Re > 20 && Re < 100) {
//            Jr = CalculateCorrectedJr(Jr, Re);
//         }
//         double hs = hk*Jc*Jl*Jb*Jr;
//
//         return hs;
//      }
//
////      internal override double CalculateVerticalCondensingHTC_Colburn(double massFlowRate, double diameter, double liqDensity, double liqViscosity, double liqThermalCond) {
////         /*double gamma = massFlowRate/(Math.PI*diameter);
////         double tempValue = gamma/Math.Pow(3.0*viscosity*gamma/(density*density*9.8065), 1.0/3.0);
////         double h = 5.35*tempValue*thermalCond/(4.0*gamma);
////         return h;*/
////         double h = CondensationHeatTransferCoeffCalculator.CalculateHorizontalTubeHTC_Colburn(massFlowRate, diameter, liqDensity, liqViscosity, liqThermalCond);
////         return h;
////      }
////
////      public override double CalculateVerticalCondensingHTC_Nusselt(double massFlowRate, double diameter, double length, double liqDensity, double liqViscosity, double liqThermalCond) {
////         /*double gamma = massFlowRate/(Math.PI*diameter);
////         double tempValue = length * length * length * density * density * 9.8065/(viscosity*gamma);
////         double h = thermalCond/length * 0.925 * Math.Pow(tempValue, 1/3);
////         return h;*/
////         double h = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Nusselt(massFlowRate, length, liqDensity, liqViscosity, liqThermalCond);
////         return h;
////      }
////
////      public override double CalculateHorizontalCondensingHTC_Colburn(double massFlowRate, double length, double liqDensity, double liqViscosity, double liqThermalCond) {
////         /*double gamma = massFlowRate/(2.0*length);
////         double tempValue = gamma/Math.Pow(3.0*viscosity*gamma/(density*density*9.8065), 1.0/3.0);
////         double h = 4.4*tempValue*thermalCond/(4.0*gamma);
////         return h;*/
////         double h = CondensationHeatTransferCoeffCalculator.CalculateHorizontalTubeHTC_Colburn(massFlowRate, length, liqDensity, liqViscosity, liqThermalCond);
////         return h;
////      }
////
////      public override double CalculateHorizontalCondensingHTC_Nusselt(double massFlowRate, double diameter, double length, double liqDensity, double liqViscosity, double liqThermalCond) {
////         /*double gamma = massFlowRate/(2.0*length);
////         double tempValue = diameter * diameter * diameter * density * density * 9.8065/(viscosity*gamma);
////         double h = thermalCond/diameter * 0.73 * Math.Pow(tempValue, 1/3);
////         return h;*/
////         double h = CondensationHeatTransferCoeffCalculator.CalculateHorizontalTubeHTC_Nusselt(massFlowRate, length, liqDensity, liqViscosity, liqThermalCond);
////         return h;
////      }
////
////      public override double CalculateNucleateBoilingHTC(double heatFlux, double pressure, double criticalPressure) {
////         /*double gamma = massFlowRate/(2.0*length);
////         double tempValue = diameter * diameter * diameter * density * density * 9.8065/(viscosity*gamma);
////         double h = thermalCond/diameter * 0.73 * Math.Pow(tempValue, 1/3);
////         return h;*/
////         double h = BoilingHeatTransferCoeffCalculator.CalculateNucleateBoilingHTC_Mostinski(heatFlux, pressure, criticalPressure);
////         return h;
////      }
////      
//      internal override double CalculateSinglePhaseDP(double massFlowRate, double density, double bulkViscosity, double wallViscosity) {
//         double p = ratingModel.TubePitch.Value;
//         //double pp = ratingModel.TubePitchParalellToFlow;
//         //double pn = ratingModel.TubePitchNormalToFlow;
//
//         double ls = ratingModel.BaffleSpacing.Value;
//         //double lc = ratingModel.BaffleCut;
//         double Nc = tubeRowsInOneCrossFlowSetion;
//         //double Fc = ratingModel.FractionOfTotalTubesInCrossFlow;
//         double Ncw = crossFlowRowsInEachWindow;
//         double Sm = crossFlowArea;
//         double Fbp = fractionOfCrossFlowAreaForBypass;
//         double Stb = tubeToBaffleLeakageArea;
//         double Ssb = shellToBaffleLeakageArea;
//         double Sw = areaForFlowThroughWindow;
//         double Dw = equivalentDiameterOfWindow;
//         int Nb = ratingModel.NumberOfBaffles.Value;
//         double Do = ratingModel.TubeOuterDiameter.Value;
//         int Nss = ratingModel.SealingStrips.Value;
//         TubeLayout layout = ratingModel.TubeLayout;
//
//         double Re = Do*massFlowRate/(bulkViscosity*Sm);
//
//         double fk = CalculateTubeBankFrictionFactor(Re, layout);
//         double dPbk = 2.0e-3*fk*massFlowRate*massFlowRate*Nc/(density*Sm*Sm) * Math.Pow(wallViscosity/bulkViscosity, 0.14);
//         double dPwk;
//         if (Re > 100) {
//            dPwk = 5.0e-4*massFlowRate*massFlowRate*(2.0+0.6*Ncw)/(Sm*Sw*density);
//         }
//         else {
//            dPwk = 1.681e-5*bulkViscosity*massFlowRate/Sm*Sw*density*(Ncw/(p-Do) + ls/(Dw*Dw)) + 9.99e-4*massFlowRate*massFlowRate/(Sm*Sw*density);
//         }
//
//         double Rl = CalculateBaffleLeakageDpFactor(Sm, Ssb, Stb);
//         double Rb = CalculateBundleBypassDpFactor(Re, Fbp, Nss, Nc);
//         double dp = ((Nb - 1.0)*dPbk*Rb+Nb*dPwk)*Rl + 2.0*dPbk*Rb*(1.0+Ncw/Nc);
//
//         return dp;
//      }
//
//      private double CalculatejFactorForIdealTubeBank(double Re, TubeLayout layout) {
//         //Perry's Chemical Engineers Handbook, Page 11-9, Fig 11-9
//         //Curve 1: (2.37, 1.0), (10, 0.33), (100, 0.0689),              (1000, 0.021), 
//         //Curve 2: (1.8, 1.0), (10, 0.298), (100, 0.0578),              (1000, 0.021), 
//         //Curve 3: (1.0, 0.8), (10, 0.198), (200, 0.0298), (500, 0.02), (1000, 0.016), (1.0e4, 9.0e-3), (1.0e5, 4.0e-3)
//         //double tempValue;
//         double jFactor = 0.004;
//         Re = Math.Log10(Re);
//         if (layout == TubeLayout.Triangular) {
//            jFactor = ChartUtil.GetInterpolateValue(jFactorCurve1, Re);
//         }
//         else if (layout == TubeLayout.RotatedSquare) { 
//            jFactor = ChartUtil.GetInterpolateValue(jFactorCurve2, Re);
//         }
//         else if (layout == TubeLayout.InlineSquare) { 
//            jFactor = ChartUtil.GetInterpolateValue(jFactorCurve2, Re);
//         }
//         return Math.Pow(10, jFactor);
//      }
//      
//      private double CalculateBaffleConfigFactor(double Fc) {
//         //Perry's Chemical Engineers Handbook, Page 11-9, Fig 11-10
//         //Curve: (0, 0.525), (0.4, 0.86), (0.6, 0.95), (0.78, 1.1), (0.9, 1.15), (0.945, 1.14), (0.973, 1.1), (0.99, 1.07), (1.0, 1.0) 
//         return ChartUtil.GetInterpolateValue(baffleConfigCurve, Fc);
//      }
//
//      private double CalculateBaffleLeakageFactor(double Sm, double Ssb, double Stb) {
//         //Perry's Chemical Engineers Handbook, Page 11-9, Fig 11-11
//         //curve 1 - 0:    (0.0, 1.0), (0.036, 0.92), (0.06, 0.88),  (0.1, 0.865), (0.125, 0.845), (0.9, 0.49)
//         //curve 2 - 0.25: (0.0, 1.0), (0.036, 0.90), (0.06, 0.86),  (0.1, 0.830), (0.125, 0.805), (0.9, 0.37)
//         //curve 3 - 0.50: (0.0, 1.0), (0.036, 0.88), (0.06, 0.84),  (0.1, 0.790), (0.125, 0.775), (0.9, 0.245)
//         //curve 4 - 0.75: (0.0, 1.0), (0.036, 0.86), (0.06, 0.805), (0.1, 0.75),  (0.125, 0.730), (0.9, 0.120)
//         //curve 5 - 1.0:  (0.0, 1.0), (0.036, 0.82), (0.06, 0.765), (0.1, 0.715), (0.125, 0.685), (0.9, 0.025)
//         double x = (Ssb+Stb)/Sm;
//         double y = Ssb/(Ssb+Stb);
//         return ChartUtil.GetInterpolatedValue(baffleLeakageCurves, x, y);
//      }
//
//      private double CalculateBundleBypassFactor(double Re, double Fbp, int Nss, double Nc) {
//         double y = (double)Nss/(double)Nc;
//         double retValue = 0.0;
//         if (y < 0.5) {
//            if (Re >= 100) {
//               retValue = ChartUtil.GetInterpolatedValue(baffleBypassCurvesRe1, Fbp, y);
//            }
//            else {
//               retValue = ChartUtil.GetInterpolatedValue(baffleBypassCurvesRe2, Fbp, y);
//            }
//         }
//         return Math.Pow(10, retValue);
//      }
//
//      private double CalculateAdverseTempGradientFactor(double Nb, double Nc, double Ncw) {
//         return ChartUtil.GetInterpolatedValue(lowReCorrectionCurves, Nb, Nc+Ncw);
//      }
//
//      private double CalculateCorrectedJr(double Jr, double Re) {
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-14
//         //curve 1 - Re >= 100 (0.3, 1.0), (1.0, 1 .0)
//         //curve 2 - Re = 90   (0.3, 915), (1.0, 1 .0)
//         //curve 3 - Re = 80   (0.3, 825), (1.0, 1 .0)
//         //curve 4 - Re = 70   (0.3, 735), (1.0, 1 .0)
//         //curve 5 - Re = 60   (0.3, 66),  (1.0, 1 .0)
//         //curve 6 - Re = 50   (0.3, 57),  (1.0, 1 .0)
//         //curve 7 - Re = 40   (0.3, 475), (1.0, 1 .0)
//         //curve 8 - Re = 30   (0.3, 39),  (1.0, 1 .0)
//         //curve 9 - Re = 20   (0.3, 0.3), (1.0, 1 .0)
//         if (Re < 20) {
//            Re = 20;
//         }
//         return ChartUtil.GetInterpolatedValue(intermediateReCorrectionCurves, Jr, Re);
//      }
//
//      private double CalculateTubeBankFrictionFactor(double Re, TubeLayout layout) {
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-15a
//         //curve 1: (1, 70), (90, 0.8),  (200, 0.365), (500, 0.3),  (1.0e5, 0.12)
//         //curve 2: (1, 54), (125, 0.5), (200, 0.34),  (400, 0.22), (800, 0.196), (3000, 0.154), (1.0e5, 0.09)
//         //curve 3: (1, 49), (50, 1.0),  (125, 0.5),   (300, 0.3),  (500, 0.25),  (1000, 0.22),  (1.0e5, 0.12)
//         //curve 4: (1, 40), (70, 0.6),  (200, 0.295), (400, 0.205), (700, 0.17), (1000, 0.168), (3000, 0.154), (1.0e5, 0.09)
//         
//         //Perry's Chemical Engineers Handbook, Page 11-10, Fig 11-15b
//         //curve 1: (1, 68),    (100, 0.55), (200, 0.33), (400, 0.21),  (700, 0.155), (1000, 0.17)  (1300, 0.155), (2000, 0.165), (3000, 0.199), (6000, 0.2), (1.0e5, 0.18)
//         //curve 2: (1, 50.5), (100, 0.45),  (200, 0.28), (400, 0.185), (700, 0.14),  (1000, 0.16), (1300, 0.14),  (2000, 0.18),  (3000, 0.175), (6000, 0.17),(1.0e5, 0.175)
//
//         //double tempValue;
//         double jFactor = 0.004;
//         Re = Math.Log10(Re);
//         if (layout == TubeLayout.Triangular) {
//            jFactor = ChartUtil.GetInterpolateValue(fkFactorCurve1, Re);
//         }
//         else if (layout == TubeLayout.RotatedSquare) { 
//            jFactor = ChartUtil.GetInterpolateValue(fkFactorCurve2, Re);
//         }
//         else if (layout == TubeLayout.InlineSquare) { 
//            jFactor = ChartUtil.GetInterpolateValue(fkFactorCurve3, Re);
//         }
//         return Math.Pow(10, jFactor);
//      }
//
//      private double CalculateBaffleLeakageDpFactor(double Sm, double Ssb, double Stb) {
//         //Perry's Chemical Engineers Handbook, Page 11-11, Fig 11-16
//         //curve 1 - 0:    (0.0, 1.0), (0.05, 0.8),   (0.1, 0.72),  (0.15, 0.675), (0.2, 0.62),  (0.3, 0.47),  (0.75, 0.3)
//         //curve 2 - 0.25: (0.0, 1.0), (0.05, 0.75),  (0.1, 0.65),  (0.15, 0.58),  (0.2, 0.525), (0.3, 0.4650, (0.75, 0.135)
//         //curve 3 - 0.50: (0.0, 1.0), (0.05, 0.70),  (0.1, 0.575), (0.15, 0.485), (0.2, 0.425), (0.3, 0.345), (0.58, 0.10)
//         //curve 4 - 0.75: (0.0, 1.0), (0.05, 0.67),  (0.1, 0.52),  (0.15, 0.415), (0.2, 0.35),  (0.3, 0.225). (0.4, 0.135)
//         //curve 5 - 1.0:  (0.0, 1.0), (0.05, 0.585), (0.1, 0.435), (0.15, 0.32),  (0.2, 0.245), (0.31, 0.1),  (0.9, 0.025)
//         double x = (Ssb+Stb)/Sm;
//         double y = Ssb/(Ssb+Stb);
//         return ChartUtil.GetInterpolatedValue(baffleLeakageDpCurves, x, y);
//      }
//
//      private double CalculateBundleBypassDpFactor(double Re, double Fbp, int Nss, double Nc) {
//         //Perry's Chemical Engineers Handbook, Page 11-11, Fig 11-17
//         //curve 1a - Nss/Nc = 0.3:   (0, 1.0), (0.7, 0.65)
//         //curve 1b:                  (0, 1.0), (0.7, 0.60)
//         //curve 2a - Nss/Nc = 0.167: (0, 1.0), (0.7, 0.44)
//         //curve 2b:                  (0, 1.0), (0.7, 0.398)
//         //curve 3a - Nss/Nc = 0.1:   (0, 1.0), (0.7, 0.336)
//         //curve 3b:                  (0, 1.0), (0.7, 0.27)
//         //curve 4a - Nss/Nc = 0.05:  (0, 1.0), (0.7, 0.24)
//         //curve 4b:                  (0, 1.0), (0.7, 0.183)
//         //curve 5a  - Nss/Nc = 0.0:  (0, 1.0), (0.51, 0.1)
//         //curve 5b:                  (0, 1.0), (0.605, 0.1)
//         double y = (double)Nss/(double)Nc;
//         double retValue = 1.0;
//         if (y < 0.5) {
//            if (Re >= 100) {
//               retValue = ChartUtil.GetInterpolatedValue(baffleBypassDpCurvesRe1, Fbp, y);
//            }
//            else {
//               retValue = ChartUtil.GetInterpolatedValue(baffleBypassDpCurvesRe2, Fbp, y);
//            }
//         }
//         return Math.Pow(10, retValue);
//      }
//
//      private void InitializeGeometryParams() 
//      {
//         CalculateTubePitches();
//         CalculateFractionOfTotalTubesInCrossFlow();  
//         CalculateShellToBaffleLeakageArea();
//      }
//
//      internal override void PrepareGeometry() 
//      {
//         CalculateTubeDiameters();
//         CalculateBaffleSpacing();
//
//         if (owner.BeingSpecifiedProcessVar is ProcessVarDouble) 
//         {
//            ProcessVarDouble pv = owner.BeingSpecifiedProcessVar as ProcessVarDouble; 
//            if (pv == ratingModel.TubeInnerDiameter) 
//            {
//               if (ratingModel.TubeWallThickness.Value != Constants.NO_VALUE && ratingModel.TubeWallThickness.IsSpecified && pv.Value != Constants.NO_VALUE) 
//               {
//                  TubeOuterDiameterChanged();
//               }
//            }
//            else if (pv == ratingModel.TubeOuterDiameter) 
//            {
//               TubeOuterDiameterChanged();
//            }
//            else if (pv == ratingModel.TubeWallThickness) 
//            {
//               if (ratingModel.TubeInnerDiameter.Value != Constants.NO_VALUE && ratingModel.TubeInnerDiameter.IsSpecified && pv.Value != Constants.NO_VALUE) 
//               {
//                  TubeOuterDiameterChanged();
//               }
//            }
//
//            else if (pv == ratingModel.TubePitch) 
//            {
//               CalculateTubePitches();
//               //above method includes the flowing calls
//               //CalculateTubeRowsInOneCrossFlowSetion();
//               //CalculateCrossFlowRowsInEachWindow();
//               //CalculateCrossFlowArea();
//            }
//            else if (pv == ratingModel.ShellInnerDiameter) 
//            {
//               CalculateTubeRowsInOneCrossFlowSetion();
//               //the following methods includs
//               //CalculateTubeToBaffleLeakageArea(); and
//               //CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
//               CalculateFractionOfTotalTubesInCrossFlow();  
//               CalculateCrossFlowArea();
//               CalculateShellToBaffleLeakageArea();
//            }
//            else if (pv == ratingModel.ShellOuterTubeLimit) 
//            {
//               CalculateFractionOfTotalTubesInCrossFlow();
//               CalculateCrossFlowArea();
//            }
//            else if (pv == ratingModel.BaffleCut) 
//            {
//               CalculateTubeRowsInOneCrossFlowSetion();
//               CalculateFractionOfTotalTubesInCrossFlow();
//               CalculateCrossFlowRowsInEachWindow();
//               CalculateShellToBaffleLeakageArea();
//            }
//            else if (pv == ratingModel.DiametralShellToBaffleClearance) 
//            {
//               CalculateShellToBaffleLeakageArea();
//            }
//         }
//         else if (owner.BeingSpecifiedProcessVar is ProcessVarInt) 
//         {
//            ProcessVarInt pv = owner.BeingSpecifiedProcessVar as ProcessVarInt; 
//            if (pv == ratingModel.TubesPerTubePass || pv == ratingModel.TubePassesPerShellPass) 
//            {
//               CalculateTubeToBaffleLeakageArea();
//               CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
//            }
//         }
//
//         CalculateHeatTransferArea();
//      }
//
//      internal override void TubeLayoutChanged() 
//      {
//         CalculateTubePitches();
//      }
//      
//      private void TubeOuterDiameterChanged() 
//      {
//         CalculateCrossFlowArea();
//         CalculateTubeToBaffleLeakageArea();
//         CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
//      }
//
//      private void CalculateTubePitches() 
//      {
//         if (ratingModel.TubeLayout == TubeLayout.InlineSquare) 
//         {
//            tubePitchParalellToFlow = ratingModel.TubePitch.Value;
//            tubePitchNormalToFlow = ratingModel.TubePitch.Value;
//         }
//         else if (ratingModel.TubeLayout == TubeLayout.RotatedSquare) 
//         {
//            tubePitchParalellToFlow = 0.707 * ratingModel.TubePitch.Value;
//            tubePitchNormalToFlow = 0.707 * ratingModel.TubePitch.Value;
//         }
//         else if (ratingModel.TubeLayout == TubeLayout.Triangular) 
//         {
//            tubePitchParalellToFlow = 0.866 * ratingModel.TubePitch.Value;
//            tubePitchNormalToFlow = 0.5 * ratingModel.TubePitch.Value;
//         }
//         CalculateTubeRowsInOneCrossFlowSetion();
//         CalculateCrossFlowRowsInEachWindow();
//         CalculateCrossFlowArea();
//      }
//
////      private void CalculateHeatTransferArea() 
////      {
////         double htArea = Math.PI * tubeOuterDiameter.Value * tubesPerTubePass.Value*tubePassesPerShellPass.Value*shellPasses.Value * tubeLengthBetweenTubeSheets.Value;
////         owner.Calculate(totalHeatTransferArea, htArea);
////      }
////
//      //Number of tube rows in one cross flow setion
//      private void CalculateTubeRowsInOneCrossFlowSetion() 
//      {
//         double shellDiam = ratingModel.ShellInnerDiameter.Value;
//         baffleCutValue = ratingModel.BaffleCut.Value*shellDiam;
//         tubeRowsInOneCrossFlowSetion = (int) (shellDiam*(1.0-2.0*baffleCutValue*shellDiam)/tubePitchParalellToFlow);
//      }
//
//      private void CalculateFractionOfTotalTubesInCrossFlow() 
//      {
//         double tempValue = (ratingModel.ShellInnerDiameter.Value-2.0*baffleCutValue)/ratingModel.ShellOuterTubeLimit.Value;
//         double acosValue = Math.Acos(tempValue);
//         fractionOfTotalTubesInCrossFlow = (Math.PI+2.0*tempValue*Math.Sin(acosValue) - 2.0*acosValue)/Math.PI;
//         CalculateTubeToBaffleLeakageArea();
//         CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
//      }
//
//      private void CalculateCrossFlowRowsInEachWindow() 
//      {
//         crossFlowRowsInEachWindow = (int) (0.8*baffleCutValue/tubePitchParalellToFlow);
//      }
//
//      private void CalculateCrossFlowArea() 
//      {
//         if (ratingModel.TubeLayout == TubeLayout.InlineSquare || ratingModel.TubeLayout == TubeLayout.RotatedSquare) 
//         {
//            crossFlowArea = ratingModel.BaffleSpacing.Value*(ratingModel.ShellInnerDiameter.Value-ratingModel.ShellOuterTubeLimit.Value+(ratingModel.ShellOuterTubeLimit.Value-ratingModel.TubeOuterDiameter.Value)/tubePitchNormalToFlow*(ratingModel.TubePitch.Value-ratingModel.TubeOuterDiameter.Value));
//         }
//         else if (ratingModel.TubeLayout == TubeLayout.Triangular) 
//         {
//            crossFlowArea = ratingModel.BaffleSpacing.Value*(ratingModel.ShellInnerDiameter.Value-ratingModel.ShellOuterTubeLimit.Value+(ratingModel.ShellOuterTubeLimit.Value-ratingModel.TubeOuterDiameter.Value)/ratingModel.TubePitch.Value*(ratingModel.TubePitch.Value-ratingModel.TubeOuterDiameter.Value));
//         }
//
//         CalculateFractionOfCrossFlowAreaForBypass();
//      }
//
//      private void CalculateFractionOfCrossFlowAreaForBypass() 
//      {
//         fractionOfCrossFlowAreaForBypass = (ratingModel.ShellInnerDiameter.Value - ratingModel.ShellOuterTubeLimit.Value) * ratingModel.BaffleSpacing.Value/crossFlowArea;
//      }
//
//      private void CalculateTubeToBaffleLeakageArea() 
//      {
//         tubeToBaffleLeakageArea = 6.223e-4*ratingModel.TubeOuterDiameter.Value*ratingModel.TubesPerTubePass.Value*ratingModel.TubePassesPerShellPass.Value*(1.0+fractionOfTotalTubesInCrossFlow);
//      }
//
//      private void CalculateShellToBaffleLeakageArea() 
//      {
//         shellToBaffleLeakageArea = 0.5*ratingModel.ShellInnerDiameter.Value*ratingModel.DiametralShellToBaffleClearance.Value*(Math.PI-Math.Acos(1.0-2.0*baffleCutValue/ratingModel.ShellInnerDiameter.Value));
//      }
//
//      private void CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow() 
//      {
//         double tempValue = 1.0-2.0*baffleCutValue/ratingModel.ShellInnerDiameter.Value;
//         double Swg = 0.25*ratingModel.ShellInnerDiameter.Value*ratingModel.ShellInnerDiameter.Value*(Math.Acos(tempValue)-tempValue*Math.Sqrt(1.0-tempValue*tempValue));
//         double Swt = 0.125*ratingModel.TubesPerTubePass.Value*ratingModel.TubePassesPerShellPass.Value*(1.0-fractionOfTotalTubesInCrossFlow)*Math.PI*ratingModel.TubeOuterDiameter.Value*ratingModel.TubeOuterDiameter.Value;
//         areaForFlowThroughWindow = Swg - Swt;
//
//         tempValue = 2.0*Math.Acos(tempValue);
//         equivalentDiameterOfWindow = 4.0*areaForFlowThroughWindow/(0.5*Math.PI*ratingModel.TubesPerTubePass.Value*ratingModel.TubePassesPerShellPass.Value*(1.0-fractionOfTotalTubesInCrossFlow)*ratingModel.TubeOuterDiameter.Value*tempValue);
//      }
//
//      private void CalculateBaffleSpacing() 
//      {
//         double bs = (ratingModel.TubeLengthBetweenTubeSheets.Value - 2.0*ratingModel.EntranceExitBaffleSpacing.Value)/(ratingModel.NumberOfBaffles.Value -1);
//         owner.Calculate(ratingModel.BaffleSpacing, bs);
//         CalculateCrossFlowArea();
//      }
//
//      protected ShellHTCAndDPCalculatorDetailed (SerializationInfo info, StreamingContext context) : base(info, context) 
//      {
//      }
//
//      public override void SetObjectData() 
//      {
//         base.SetObjectData();
////         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionShellHTCAndDPCalculatorDetailed", typeof(int));
////         if (persistedClassVersion == 1) 
////         {
////            this.baffleCutValue = (double) info.GetValue("BaffleCutValue", typeof(double));
////            this.tubePitchParalellToFlow = (double) info.GetValue("TubePitchParalellToFlow", typeof(double));
////            this.tubePitchNormalToFlow = (double) info.GetValue("TubePitchNormalToFlow", typeof(double));
////            this.fractionOfTotalTubesInCrossFlow = (double) info.GetValue("FractionOfTotalTubesInCrossFlow", typeof(double));
////            this.crossFlowRowsInEachWindow = (int) info.GetValue("CrossFlowRowsInEachWindow", typeof(int));
////            this.tubeRowsInOneCrossFlowSetion = (int) info.GetValue("TubeRowsInOneCrossFlowSetion", typeof(int));
////            this.crossFlowArea = (double) info.GetValue("CrossFlowArea", typeof(double));
////            this.tubeToBaffleLeakageArea = (double) info.GetValue("TubeToBaffleLeakageArea", typeof(double));
////            this.shellToBaffleLeakageArea = (double) info.GetValue("ShellToBaffleLeakageArea", typeof(double));
////            this.fractionOfCrossFlowAreaForBypass = (double) info.GetValue("FractionOfCrossFlowAreaForBypass", typeof(double));
////            this.areaForFlowThroughWindow = (double) info.GetValue("AreaForFlowThroughWindow", typeof(double));
////            this.equivalentDiameterOfWindow = (double) info.GetValue("EquivalentDiameterOfWindow", typeof(double));
////         }
//         InitializeGeometryParams();
//      }
//
//      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
//      public override void GetObjectData(SerializationInfo info, StreamingContext context) 
//      {
//         base.GetObjectData(info, context);        
////         info.AddValue("ClassPersistenceVersionShellHTCAndDPCalculatorDetailed", CLASS_PERSISTENCE_VERSION, typeof(int));
////         info.AddValue("BaffleCutValue", this.baffleCutValue, typeof(double));
////         info.AddValue("TubePitchParalellToFlow", this.tubePitchParalellToFlow, typeof(double));
////         info.AddValue("TubePitchNormalToFlow", this.tubePitchNormalToFlow, typeof(double));
////         info.AddValue("FractionOfTotalTubesInCrossFlow", this.fractionOfTotalTubesInCrossFlow, typeof(double));
////         info.AddValue("CrossFlowRowsInEachWindow", this.crossFlowRowsInEachWindow, typeof(int));
////         info.AddValue("TubeRowsInOneCrossFlowSetion", this.tubeRowsInOneCrossFlowSetion, typeof(int));
////         info.AddValue("CrossFlowArea", this.crossFlowArea, typeof(double));
////         info.AddValue("TubeToBaffleLeakageArea", this.tubeToBaffleLeakageArea, typeof(double));
////         info.AddValue("ShellToBaffleLeakageArea", this.shellToBaffleLeakageArea, typeof(double));
////         info.AddValue("FractionOfCrossFlowAreaForBypass", this.fractionOfCrossFlowAreaForBypass, typeof(double));
////         info.AddValue("AreaForFlowThroughWindow", this.areaForFlowThroughWindow, typeof(double));
////         info.AddValue("EquivalentDiameterOfWindow", this.equivalentDiameterOfWindow, typeof(double)); 
//      }
//   }
}
