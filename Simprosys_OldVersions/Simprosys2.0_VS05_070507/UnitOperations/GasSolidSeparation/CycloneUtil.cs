using System;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.Plots;

namespace Prosimo.UnitOperations.GasSolidSeparation 
{
   /// <summary>
   /// Summary description for CycloneUtil.
   /// </summary>
   public class CycloneUtil {
      
      //Perry's Chemical Engineers Handbook, Page 17-28, Table 17-3
      //Flow contraction pressure drop factor
      //Curve 1: (0.0, 0.5), (0.1, 0.47), (0.2, 0.43), (0.3, 0.395), (0.4, 0.35), (1.0, 0.0)
      private static readonly PointF[] dpContractionFactorCurve = {new PointF(0.05f, 0.5f), new PointF(0.1f, 0.47f), new PointF(0.2f, 0.43f),
                                                           new PointF(0.3f, 0.395f), new PointF(0.4f, 0.35f), new PointF(1.0f, 0.0f)
                                                          };
      //Perry's Chemical Engineers Handbook, Page 17-28, Fig 17-39
      //collection efficiency correction curve for Tangential Inlet
      //Curve 1: (0.17, 0.02), (0.6, 0.30), (1.0, 0.5), (2.0, 0.72), (4.0, 0.85), (5.0, 0.9), (6.0, 0.93), (7.0, 0.95), (8.0, 0.97), (9.0, 0.99), (10, 0.999), (20, 1.0)
      private static readonly PointF[] ceDiamCorrectionCurve1 = {new PointF((float)Math.Log10(0.17), 0.02f), new PointF((float)Math.Log10(0.6), 0.30f),
                                         new PointF((float)Math.Log10(1.0), 0.50f), new PointF((float)Math.Log10(2.0), 0.72f), 
                                         new PointF((float)Math.Log10(4.0), 0.85f), new PointF((float)Math.Log10(5.0), 0.90f),
                                         new PointF((float)Math.Log10(6.0), 0.93f), new PointF((float)Math.Log10(7.0), 0.95f),
                                         new PointF((float)Math.Log10(8.0), 0.97f), new PointF((float)Math.Log10(9.0), 0.99f),
                                         new PointF((float)Math.Log10(10.0), 0.999f), new PointF((float)Math.Log10(20.0), 1.0f)
                                        };  
      //collection efficiency correction curve for Scroll or Volute Inlet
      //Curve 2: (0.4, 0.02),  (0.8, 0.14), (1.2, 0.3), (2.0, 0.50)  (3.2, 0.70), (6.0, 0.9),  (7.0, 0.95), (8.0, 0.97), (9.0, 0.99), (10, 0.999), (20, 1.0)
      private static readonly PointF[] ceDiamCorrectionCurve2 = {new PointF((float)Math.Log10(0.4), 0.02f), new PointF((float)Math.Log10(0.8), 0.14f),
                                          new PointF((float)Math.Log10(1.2), 0.30f), new PointF((float)Math.Log10(2.0), 0.5f), 
                                          new PointF((float)Math.Log10(3.2), 0.70f), new PointF((float)Math.Log10(6.0), 0.90f),
                                          new PointF((float)Math.Log10(7.0), 0.95f), new PointF((float)Math.Log10(8.0), 0.97f),
                                          new PointF((float)Math.Log10(9.0), 0.99f), new PointF((float)Math.Log10(10.0), 0.999f),
                                          new PointF((float)Math.Log10(20.0), 1.0f)
                                        };    

      //Perry's Chemical Engineers Handbook, Page 17-29, Fig 17-40
      //collection efficiency correction curve family for solids loading for Geldart-type solids A and C 
      private static readonly CurveF[] ceLoadingCorrectionCurvesAC = new CurveF[7]; 
      //Perry's Chemical Engineers Handbook, Page 17-29, Fig 17-41
      //collection efficiency correction curve family for solids loading for Geldart-type solids B and D 
      private static readonly CurveF[] ceLoadingCorrectionCurvesBD = new CurveF[9]; 
      
      //Perry's Chemical Engineers Handbook, Page 17-30, Fig 17-42
      //pressure drop correction curve for solids loading 
      //Curve: (0.049, 2.5), (0.12, 2.0), (0.245, 1.78), (0.49, 1.62), (1.0, 1.5), (2.45, 1.37), (4.9, 1.28), (10, 1.19), (24.5, 1.1), (49, 1.06), (98, 1.04)
      private static readonly PointF[] dpLoadingCorrectionCurve = {new PointF((float)Math.Log10(0.049), 2.5f), new PointF((float)Math.Log10(0.12), 2.0f),
                                           new PointF((float)Math.Log10(0.245), 1.78f), new PointF((float)Math.Log10(0.49), 1.62f), 
                                           new PointF((float)Math.Log10(1.0), 1.5f), new PointF((float)Math.Log10(2.45), 1.37f),
                                           new PointF((float)Math.Log10(4.9), 1.28f), new PointF((float)Math.Log10(10.0), 1.19f),
                                           new PointF((float)Math.Log10(24.5), 1.1f), new PointF((float)Math.Log10(49), 1.06f),
                                           new PointF((float)Math.Log10(98.0), 1.04f)
                                          };  


      static CycloneUtil() {
         //Perry's Chemical Engineers Handbook, Page 17-29, Fig 17-40
         //curve 1 - 0.35: (0.0023, 0.35), (0.005, 0.38), (0.0115, 0.425), (0.023, 0.48),  (0.063, 0.61),  (0.115, 0.765), (0.23, 0.915), (0.5, 0.988), (1.15, 0.9979), (2.1, 0.99935), (22.3, 0.99935)  
         //curve 2 - 0.55: (0.0023, 0.50), (0.005, 0.59), (0.0115, 0.680), (0.023, 0.765), (0.063, 0.885), (0.115, 0.930), (0.23, 0.978), (0.5, 0.996), (1.15, 0.9989), (2.0, 0.99955), (22.3, 0.99955)
         //curve 2 - 0.65: (0.0023, 0.65), (0.005, 0.695), (0.0115, 0.77), (0.023, 0.85), (0.063, 0.928), (0.115, 0.965), (0.23, 0.99), (0.5, 0.9975), (1.15, 0.9997), (1.9, 0.99965), (22.3, 0.99965)
         //curve 4 - 0.85: (0.0023, 0.85), (0.005, 0.88), (0.0115, 0.914), (0.023, 0.93), (0.063, 0.971), (0.115, 0.985), (0.23, 0.9962), (0.5, 0.999), (1.15, 0.9997), (1.8, 0.99985), (22.3, 0.99985)
         //curve 5 - 0.94: (0.0023, 0.94), (0.005, 0.95), (0.0115, 0.96), (0.023, 0.97), (0.063, 0.988), (0.115, 0.9938), (0.23, 0.998), (0.5, 0.9995), (1.15, 0.99986), (2.2, 0.99994), (22.3, 0.99994)
         //curve 6 - 0.97: (0.0023, 0.97), (0.005, 0.972), (0.0115, 0.978), (0.023, 0.983), (0.063, 0.994), (0.115, 0.997), (0.23, 0.9991), (0.5, 0.99975), (1.15, 0.99997), (2.23, 0.99997), (22.3, 0.99997)
         //curve 7 - 0.99: (0.0023, 0.99), (0.005, 0.991), (0.0115, 0.9928), (0.023, 0.995), (0.063, 0.997), (0.115, 0.9987), (0.23, 0.99962), (0.5, 0.99992), (1.15, 0.99998), (1.66, 0.99999), (22.3, 0.99999)
         PointF[] pa1 = {new PointF((float) Math.Log10(0.0023), 0.35f), new PointF((float) Math.Log10(0.005), 0.38f),  new PointF((float) Math.Log10(0.0115), 0.425f), new PointF((float) Math.Log10(0.023), 0.48f), 
                         new PointF((float) Math.Log10(0.063), 0.61f), new PointF((float) Math.Log10(0.115), 0.765f), new PointF((float) Math.Log10(0.23), 0.915f), new PointF((float) Math.Log10(0.5), 0.988f), 
                         new PointF((float) Math.Log10(1.15), 0.9979f), new PointF((float) Math.Log10(2.1), 0.99935f), new PointF((float) Math.Log10(22.3), 0.99935f)};
         PointF[] pa2 = {new PointF((float) Math.Log10(0.0023), 0.5f), new PointF((float) Math.Log10(0.005), 0.59f),  new PointF((float) Math.Log10(0.0115), 0.68f), new PointF((float) Math.Log10(0.023), 0.765f), 
                         new PointF((float) Math.Log10(0.063), 0.885f), new PointF((float) Math.Log10(0.115), 0.930f), new PointF((float) Math.Log10(0.23), 0.978f), new PointF((float) Math.Log10(0.5), 0.996f), 
                         new PointF((float) Math.Log10(1.15), 0.9989f), new PointF((float) Math.Log10(2.0), 0.99955f), new PointF((float) Math.Log10(22.3), 0.99955f)};
         PointF[] pa3 = {new PointF((float) Math.Log10(0.0023), 0.65f), new PointF((float) Math.Log10(0.005), 0.695f),  new PointF((float) Math.Log10(0.0115), 0.77f), new PointF((float) Math.Log10(0.023), 0.85f), 
                         new PointF((float) Math.Log10(0.063), 0.928f), new PointF((float) Math.Log10(0.115), 0.965f), new PointF((float) Math.Log10(0.23), 0.99f), new PointF((float) Math.Log10(0.5), 0.9975f), 
                         new PointF((float) Math.Log10(1.15), 0.9997f), new PointF((float) Math.Log10(1.9), 0.99965f), new PointF((float) Math.Log10(22.3), 0.99965f)};
         PointF[] pa4 = {new PointF((float) Math.Log10(0.0023), 0.85f), new PointF((float) Math.Log10(0.005), 0.88f),  new PointF((float) Math.Log10(0.0115), 0.914f), new PointF((float) Math.Log10(0.023), 0.93f), 
                         new PointF((float) Math.Log10(0.063), 0.971f), new PointF((float) Math.Log10(0.115), 0.985f), new PointF((float) Math.Log10(0.23), 0.9962f), new PointF((float) Math.Log10(0.5), 0.999f), 
                         new PointF((float) Math.Log10(1.15), 0.9997f), new PointF((float) Math.Log10(1.8), 0.99985f), new PointF((float) Math.Log10(22.3), 0.99985f)};
         PointF[] pa5 = {new PointF((float) Math.Log10(0.0023), 0.94f), new PointF((float) Math.Log10(0.005), 0.95f),  new PointF((float) Math.Log10(0.0115), 0.96f), new PointF((float) Math.Log10(0.023), 0.97f), 
                         new PointF((float) Math.Log10(0.063), 0.988f), new PointF((float) Math.Log10(0.115), 0.9938f), new PointF((float) Math.Log10(0.23), 0.998f), new PointF((float) Math.Log10(0.5), 0.9995f), 
                         new PointF((float) Math.Log10(1.15), 0.99986f), new PointF((float) Math.Log10(2.2), 0.99994f), new PointF((float) Math.Log10(22.3), 0.99994f)};
         PointF[] pa6 = {new PointF((float) Math.Log10(0.0023), 0.97f), new PointF((float) Math.Log10(0.005), 0.972f),  new PointF((float) Math.Log10(0.0115), 0.978f), new PointF((float) Math.Log10(0.023), 0.983f), 
                         new PointF((float) Math.Log10(0.063), 0.994f), new PointF((float) Math.Log10(0.115), 0.997f), new PointF((float) Math.Log10(0.23), 0.9991f), new PointF((float) Math.Log10(0.5), 0.99975f), 
                         new PointF((float) Math.Log10(1.15), 0.99997f), new PointF((float) Math.Log10(2.2), 0.99997f), new PointF((float) Math.Log10(22.3), 0.99997f)};
         PointF[] pa7 = {new PointF((float) Math.Log10(0.0023), 0.99f), new PointF((float) Math.Log10(0.005), 0.991f),  new PointF((float) Math.Log10(0.0115), 0.99928f), new PointF((float) Math.Log10(0.023), 0.995f), 
                         new PointF((float) Math.Log10(0.063), 0.997f), new PointF((float) Math.Log10(0.115), 0.9987f), new PointF((float) Math.Log10(0.23), 0.99962f), new PointF((float) Math.Log10(0.5), 0.99992f), 
                         new PointF((float) Math.Log10(1.15), 0.99998f), new PointF((float) Math.Log10(2.2), 0.99999f), new PointF((float) Math.Log10(22.3), 0.99999f)};
         ceLoadingCorrectionCurvesAC[0] = new CurveF(0.35f, pa1);
         ceLoadingCorrectionCurvesAC[1] = new CurveF(0.55f, pa2);
         ceLoadingCorrectionCurvesAC[2] = new CurveF(0.65f, pa3);
         ceLoadingCorrectionCurvesAC[3] = new CurveF(0.85f, pa4);
         ceLoadingCorrectionCurvesAC[4] = new CurveF(0.94f, pa5);
         ceLoadingCorrectionCurvesAC[5] = new CurveF(0.97f, pa6);
         ceLoadingCorrectionCurvesAC[6] = new CurveF(0.99f, pa7);
         
         //Perry's Chemical Engineers Handbook, Page 17-29, Fig 17-41
         //curve 1 - 0.10: (0.001, 0.1), (0.005, 0.118), (0.01, 0.138),  (0.02, 0.17), (0.05, 0.214), (0.1, 0.278), (0.2, 0.32), (0.5, 0.43), (1.0, 0.505), (2.1, 0.56)
         //curve 2 - 0.20: (0.001, 0.2), (0.005, 0.24), (0.01, 0.265),  (0.02, 0.302), (0.05, 0.36), (0.1, 0.458), (0.2, 0.53), (0.5, 0.65), (1.0, 0.715), (2.1, 0.76)
         //curve 3 - 0.35: (0.001, 0.35), (0.005, 0.40), (0.01, 0.45),  (0.02, 0.48), (0.05, 0.58), (0.1, 0.65), (0.2, 0.732), (0.5, 0.85), (1.0, 0.90), (2.1, 0.92)
         //curve 4 - 0.55: (0.001, 0.55), (0.005, 0.60), (0.01, 0.67),  (0.02, 0.75), (0.05, 0.87), (0.1, 0.902), (0.2, 0.94), (0.5, 0.955), (1.0, 0.966), (2.1, 0.968)
         //curve 5 - 0.65: (0.001, 0.65), (0.005, 0.70), (0.01, 0.78),  (0.02, 0.86), (0.05, 0.918), (0.1, 0.948), (0.2, 0.969), (0.5, 0.977), (1.0, 0.979), (2.1, 0.98)
         //curve 6 - 0.85: (0.001, 0.85), (0.005, 0.87), (0.01, 0.902),  (0.02, 0.93), (0.05, 0.961), (0.1, 0.973), (0.2, 0.981), (0.5, 0.984), (1.0, 0.988), (2.1, 0.99)
         //curve 7 - 0.94: (0.001, 0.94), (0.005, 0.95), (0.01, 0.96),  (0.02, 0.972), (0.05, 0.982), (0.1, 0.985), (0.2, 0.9895), (0.5, 0.9912), (1.0, 0.9916), (2.1, 0.9917)
         //curve 8 - 0.97: (0.001, 0.97), (0.005, 0.975), (0.01, 0.981),  (0.02, 0.983), (0.05, 0.988), (0.1, 0.992), (0.2, 0.9932), (0.5, 0.9935), (1.0, 0.9937), (2.1, 0.9938)
         //curve 9 - 0.99: (0.001, 0.99), (0.005, 0.992), (0.01, 0.9938),  (0.02, 0.9945), (0.05, 0.9958), (0.1, 0.9961), (0.2, 0.997), (0.5, 0.9971), (1.0, 0.9973), (2.1, 0.9973)
         PointF[] pc1 = {new PointF((float) Math.Log10(0.001), 0.1f), new PointF((float) Math.Log10(0.005), 0.118f),  new PointF((float) Math.Log10(0.01), 0.138f), new PointF((float) Math.Log10(0.02), 0.17f), 
                         new PointF((float) Math.Log10(0.05), 0.214f), new PointF((float) Math.Log10(0.1), 0.278f), new PointF((float) Math.Log10(0.2), 0.32f), new PointF((float) Math.Log10(0.5), 0.43f), 
                         new PointF((float) Math.Log10(1.0), 0.505f), new PointF((float) Math.Log10(2.1), 0.56f)};
         PointF[] pc2 = {new PointF((float) Math.Log10(0.001), 0.2f), new PointF((float) Math.Log10(0.005), 0.24f),  new PointF((float) Math.Log10(0.01), 0.265f), new PointF((float) Math.Log10(0.02), 0.302f), 
                         new PointF((float) Math.Log10(0.05), 0.36f), new PointF((float) Math.Log10(0.1), 0.458f), new PointF((float) Math.Log10(0.2), 0.53f), new PointF((float) Math.Log10(0.5), 0.65f), 
                         new PointF((float) Math.Log10(1.0), 0.715f), new PointF((float) Math.Log10(2.1), 0.76f)};
         PointF[] pc3 = {new PointF((float) Math.Log10(0.001), 0.35f), new PointF((float) Math.Log10(0.005), 0.4f),  new PointF((float) Math.Log10(0.01), 0.45f), new PointF((float) Math.Log10(0.02), 0.48f), 
                         new PointF((float) Math.Log10(0.05), 0.58f), new PointF((float) Math.Log10(0.1), 0.65f), new PointF((float) Math.Log10(0.2), 0.732f), new PointF((float) Math.Log10(0.5), 0.85f), 
                         new PointF((float) Math.Log10(1.0), 0.90f), new PointF((float) Math.Log10(2.1), 0.92f)};
         PointF[] pc4 = {new PointF((float) Math.Log10(0.001), 0.55f), new PointF((float) Math.Log10(0.005), 0.60f),  new PointF((float) Math.Log10(0.01), 0.67f), new PointF((float) Math.Log10(0.02), 0.75f), 
                         new PointF((float) Math.Log10(0.05), 0.87f), new PointF((float) Math.Log10(0.1), 0.902f), new PointF((float) Math.Log10(0.2), 0.94f), new PointF((float) Math.Log10(0.5), 0.955f), 
                         new PointF((float) Math.Log10(1.0), 0.966f), new PointF((float) Math.Log10(2.1), 0.968f)};
         PointF[] pc5 = {new PointF((float) Math.Log10(0.001), 0.65f), new PointF((float) Math.Log10(0.005), 0.70f),  new PointF((float) Math.Log10(0.01), 0.78f), new PointF((float) Math.Log10(0.02), 0.86f), 
                         new PointF((float) Math.Log10(0.05), 0.918f), new PointF((float) Math.Log10(0.1), 0.948f), new PointF((float) Math.Log10(0.2), 0.969f), new PointF((float) Math.Log10(0.5), 0.977f), 
                         new PointF((float) Math.Log10(1.0), 0.979f), new PointF((float) Math.Log10(2.1), 0.98f)};
         PointF[] pc6 = {new PointF((float) Math.Log10(0.001), 0.85f), new PointF((float) Math.Log10(0.005), 0.87f),  new PointF((float) Math.Log10(0.01), 0.902f), new PointF((float) Math.Log10(0.02), 0.93f), 
                         new PointF((float) Math.Log10(0.05), 0.961f), new PointF((float) Math.Log10(0.1), 0.973f), new PointF((float) Math.Log10(0.2), 0.981f), new PointF((float) Math.Log10(0.5), 0.984f), 
                         new PointF((float) Math.Log10(1.0), 0.988f), new PointF((float) Math.Log10(2.1), 0.99f)};
         PointF[] pc7 = {new PointF((float) Math.Log10(0.001), 0.94f), new PointF((float) Math.Log10(0.005), 0.95f),  new PointF((float) Math.Log10(0.01), 0.96f), new PointF((float) Math.Log10(0.02), 0.972f), 
                         new PointF((float) Math.Log10(0.05), 0.982f), new PointF((float) Math.Log10(0.1), 0.985f), new PointF((float) Math.Log10(0.2), 0.9895f), new PointF((float) Math.Log10(0.5), 0.9912f), 
                         new PointF((float) Math.Log10(1.0), 0.9916f), new PointF((float) Math.Log10(2.1), 0.9917f)};
         PointF[] pc8 = {new PointF((float) Math.Log10(0.001), 0.97f), new PointF((float) Math.Log10(0.005), 0.975f),  new PointF((float) Math.Log10(0.01), 0.981f), new PointF((float) Math.Log10(0.02), 0.983f), 
                         new PointF((float) Math.Log10(0.05), 0.988f), new PointF((float) Math.Log10(0.1), 0.992f), new PointF((float) Math.Log10(0.2), 0.9932f), new PointF((float) Math.Log10(0.5), 0.9935f), 
                         new PointF((float) Math.Log10(1.0), 0.9937f), new PointF((float) Math.Log10(2.1), 0.9938f)};
         PointF[] pc9 = {new PointF((float) Math.Log10(0.001), 0.99f), new PointF((float) Math.Log10(0.005), 0.992f),  new PointF((float) Math.Log10(0.01), 0.9938f), new PointF((float) Math.Log10(0.02), 0.9945f), 
                         new PointF((float) Math.Log10(0.05), 0.9958f), new PointF((float) Math.Log10(0.1), 0.9961f), new PointF((float) Math.Log10(0.2), 0.9970f), new PointF((float) Math.Log10(0.5), 0.9971f), 
                         new PointF((float) Math.Log10(1.0), 0.9973f), new PointF((float) Math.Log10(2.1), 0.9973f)};

         ceLoadingCorrectionCurvesBD[0] = new CurveF(0.1f, pc1);
         ceLoadingCorrectionCurvesBD[1] = new CurveF(0.2f, pc2);
         ceLoadingCorrectionCurvesBD[2] = new CurveF(0.35f, pc3);
         ceLoadingCorrectionCurvesBD[3] = new CurveF(0.55f, pc4);
         ceLoadingCorrectionCurvesBD[4] = new CurveF(0.65f, pc5);
         ceLoadingCorrectionCurvesBD[5] = new CurveF(0.85f, pc6);
         ceLoadingCorrectionCurvesBD[6] = new CurveF(0.94f, pc7);
         ceLoadingCorrectionCurvesBD[7] = new CurveF(0.97f, pc8);
         ceLoadingCorrectionCurvesBD[8] = new CurveF(0.99f, pc9);
      }
         
      internal static double CalculateCollectionEfficiency(double dpiDpth, CycloneInletConfiguration inletConfiguration) {
         if (inletConfiguration == CycloneInletConfiguration.Tangential) {
            return ChartUtil.GetInterpolateValue(ceDiamCorrectionCurve1, Math.Log10(dpiDpth));
         }
         else {
            return ChartUtil.GetInterpolateValue(ceDiamCorrectionCurve2, Math.Log10(dpiDpth));
         }
      }
      
      internal static double CorrectedCollectionEfficiency(double e0, ParticleTypeGroup particleType, double loading) {
         if (particleType == ParticleTypeGroup.A || particleType == ParticleTypeGroup.C) {
            return ChartUtil.GetInterpolatedValue(ceLoadingCorrectionCurvesAC, e0, Math.Log10(loading));
         }
         else {
            return ChartUtil.GetInterpolatedValue(ceLoadingCorrectionCurvesBD, e0, Math.Log10(loading));
         }
      }

      internal static double CalculateDPContractionFactor(double areaRatio) {
         return ChartUtil.GetInterpolateValue(dpContractionFactorCurve, areaRatio);
      }

      internal static double CalculateDPLoadingCorrectionFactor(double loading) {
         return ChartUtil.GetInterpolateValue(dpLoadingCorrectionCurve, Math.Log10(loading));
      }
   }
}
