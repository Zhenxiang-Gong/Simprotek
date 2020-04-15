// AppStartup.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "atlstr.h"

#import <mscorlib.tlb> rename("ReportEvent", "ReportEventX") raw_interfaces_only high_property_prefixes("_get","_put","_putref")
using namespace mscorlib;

//#import <ProtectTestUI.tlb> raw_interfaces_only high_property_prefixes("_get","_put","_putref")
//#import "C:\\radu\\AMSCOTECH\\ProtectTest\\ProtectTestSolution\\ProtectTestUI\\bin\\Debug\\ProtectTestUI.tlb" raw_interfaces_only high_property_prefixes("_get","_put","_putref")
//using namespace ProtectTestUI;


int _tmain(int argc, _TCHAR* argv[])
{
   ICorRuntimeHost *pHost = NULL;

   HRESULT hr = CorBindToRuntimeEx(
							 NULL,	//Retrieve latest version by default
							 L"wks",	//Request a WorkStation build of the CLR
                      STARTUP_LOADER_OPTIMIZATION_SINGLE_DOMAIN, 
							 CLSID_CorRuntimeHost,
							 IID_ICorRuntimeHost,
                      (void **)&pHost
                      );

	if (!SUCCEEDED(hr))
	{
		printf("CorBindToRuntimeEx failed\n");
		return 1;
	}

	pHost->Start();

	// Get a pointer to the default domain in the process.  
	_AppDomain* pDefaultDomain = NULL;
	IUnknown* pAppDomainPunk = NULL;

	hr = pHost->GetDefaultDomain(&pAppDomainPunk);
//	assert(pAppDomainPunk);
	if (!SUCCEEDED(hr))
	{
		printf("GetDefaultDomain failed\n");
		return 1;
	}


	hr = pAppDomainPunk->QueryInterface(__uuidof(_AppDomain), (void**) &pDefaultDomain);
//	assert(pDefaultDomain);
	if (!SUCCEEDED(hr))
	{
		printf("QueryInterface failed\n");
		return 1;
	}



   char *orig = "C:\\radu\\AMSCOTECH\\ProtectTest_old\\ProtectTestSolution\\AppStartup\\Debug\\ProtectTestUI.exe";
//   char *orig = "ProtectTestUI.exe";
   CComBSTR ccombstr(orig);
   _Assembly* pAssembly = NULL;
   hr = pDefaultDomain->Load_2(ccombstr, &pAssembly);
//   assert(pAssembly);
	if (!SUCCEEDED(hr))
	{
		printf("Load_2 failed\n");
		return 1;
	}

   _MethodInfo* pMethodInfo = NULL;
   hr = pAssembly->get_EntryPoint(&pMethodInfo);
   assert(pMethodInfo);
	if (!SUCCEEDED(hr))
	{
		printf("get_EntryPoint failed\n");
		return 1;
	}

   pMethodInfo->Invoke_3(_variant_t(), NULL, NULL);





	pAppDomainPunk->Release();
	pDefaultDomain->Release();

   pHost->Stop();

   return 0;
}

