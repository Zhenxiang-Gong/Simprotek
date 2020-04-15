using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;

[assembly:AssemblyVersion("1.0.0.0")]

namespace ClrHost.MgdHost
{
   public class HostProcessRequest
   {
     private static byte[] s_somePublicKey = 
     { 
            0, 36, 0, 0, 4, 128, 0, 0, 148, 0, 0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
            82, 83, 65, 49, 0, 4, 0, 0, 3, 0, 0, 0, 207, 203, 50, 145, 170,
            113, 95, 233, 157, 64, 212, 144, 64, 51, 111, 144, 86, 215, 136,
            111, 237, 70, 119, 91, 199, 187, 84, 48, 186, 68, 68, 254, 248, 52,
            142, 189, 6, 249, 98, 243, 151, 118, 174, 77, 195, 183, 176, 74,
            127, 230, 244, 159, 37, 247, 64, 66, 62, 191, 44, 11, 137, 105, 141,
            141, 8, 172, 72, 214, 156, 237, 15, 200, 248, 59, 70, 94, 8, 7, 172,
            17, 236, 29, 204, 125, 5, 78, 128, 122, 67, 51, 109, 222, 64, 138,
            83, 147, 164, 133, 86, 18, 50, 114, 206, 238, 231, 47, 22, 96, 183,
            25, 39, 211, 133, 97, 170, 191, 92, 172, 29, 241, 115, 70, 51, 198,
            2, 248, 242, 213 
      };

	  private PolicyLevel DefineHostPolicy()
	  {
		    // Create the domain level policy
			PolicyLevel    pl   = PolicyLevel.CreateAppDomainLevel();

			//
			// include a code group that gives permissions only to assemblies that 
			// that are strong named with s_somePublicKey
			//
			UnionCodeGroup snCG = 
				new UnionCodeGroup( 
					 new StrongNameMembershipCondition(new                                      
				            StrongNamePublicKeyBlob( s_somePublicKey ), null, null ), 
				new PolicyStatement(new PermissionSet(PermissionState.Unrestricted)));

			pl.RootCodeGroup.AddChild(snCG);
			Console.WriteLine("Domain security policy successfully created...");

			return pl;

	  }

      public void RunUserCode(String domainName)
      {
			//
			// Setup AppBase and Configuration file
			//
			IDictionary properties = new Hashtable();

			properties.Add(AppDomainFlags.ApplicationBase, "c:\\temp");
			properties.Add(AppDomainFlags.ConfigurationFile, "c:\\temp\\myapp.config");
			Console.WriteLine("Domain properties successfully initialized...");

			//
			// Create sample evidence - this example sets the domain level evidence
			// to the url of a web site
			//
			Evidence  e      = new Evidence();
			e.AddHost(new Url("http://www.somesite.com"));
			Console.WriteLine("Domain level evidence successfully created...");

			//
			// Create the domain. 
			//
			AppDomain ad = AppDomain.CreateDomain(domainName,
				                         e,
										 null,
										 properties);
			//
			// Define a sample domain specific code access security policy
			//
			ad.SetAppDomainPolicy(DefineHostPolicy());

			Console.WriteLine("Domain " + domainName + " successfully created...");
			
			//
			// unload the domain
			//
			Console.WriteLine("Domain " + domainName + " unloaded...");
			Console.WriteLine("Done.");
			AppDomain.Unload(ad);
      }
   }

}