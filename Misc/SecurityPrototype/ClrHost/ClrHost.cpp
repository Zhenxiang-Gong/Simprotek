// ClrHost.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#import <mscorlib.tlb> raw_interfaces_only high_property_prefixes("_get","_put","_putref")
using namespace ComRuntimeLibrary;

//
// import the type library for our managed hosting code.  This typelib  
// was built using the tlbimp tlbexp SDK tool
//
#import <MgdHost.tlb> raw_interfaces_only high_property_prefixes("_get","_put","_putref")
using namespace mgdhost;

void ReportError(HRESULT hr)
{
	LPVOID lpMsgBuf;
	DWORD bOk = FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER | 
		FORMAT_MESSAGE_FROM_SYSTEM | 
		FORMAT_MESSAGE_IGNORE_INSERTS,
		NULL,
		hr,
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
		(LPTSTR) &lpMsgBuf,
		0,
		NULL); 

	if (bOk)
	{
		_ftprintf(stderr, _T("\t%s\n"), (LPTSTR)((LPTSTR)lpMsgBuf));
		LocalFree( lpMsgBuf );
	}
	else
	{
		_ftprintf(stderr, _T("Unknown Error\n"));
	}

}

int main(int argc, char* argv[])
{
	printf("Unmanaged hosting code started...\n");
	//
	// Declare variables passed to CorBindToRuntime.  This sample is hard coded
	// to load the Beta1 version of the CLR, workstation build.
	//
	LPWSTR pszVer = L"v1.0.2204";
	LPWSTR pszFlavor = L"wks";
	ICorRuntimeHost *pHost = NULL;

	//
	// CorBindToRuntime is the primary api hosts use to load the CLR into a process.
	// In addition to version and "svr vs wks" - we also select concurrent gc, and "no
	// domain neutral code ("single domain host")
	// 
	HRESULT hr = CorBindToRuntimeEx(
                      //version
                      pszVer,       
                      // svr or wks                        
                      pszFlavor,    
                      //domain-neutral"ness" and gc settings - see below.
                      STARTUP_LOADER_OPTIMIZATION_SINGLE_DOMAIN | STARTUP_CONCURRENT_GC, 
                      CLSID_CorRuntimeHost, 
                      IID_ICorRuntimeHost, 
                      (void **)&pHost);
	if (!SUCCEEDED(hr))
	{
		printf("CorBindToRuntime failed\n");
		ReportError(hr);
		return 1;
	}

	printf("Version 1.0.2204 of the CLR loaded...\n");
	//
	// Start the CLR.
	//
	pHost->Start();

	//
	// Get a pointer to the default domain in the process.  
	//
	_AppDomain *pDefaultDomain = NULL;
	IUnknown   *pAppDomainPunk = NULL;

	hr = pHost->GetDefaultDomain(&pAppDomainPunk);
	assert(pAppDomainPunk); 

	hr = pAppDomainPunk->QueryInterface(__uuidof(_AppDomain), 
                                        (void**) &pDefaultDomain);
	assert(pDefaultDomain);

	//
	// Load the managed portion of our host into the default domain.
	//
	_HostProcessRequest *pMgdHost = NULL;
	_ObjectHandle *pObjHandle = NULL;
	
	hr = pDefaultDomain->CreateInstance(
			_bstr_t("MgdHost"), 
			_bstr_t("ClrHost.MgdHost.HostProcessRequest"),
			&pObjHandle); 
	printf("Managed hosting code successfully created...\n");

	assert(pObjHandle);

	VARIANT v;
	VariantInit(&v);
	hr = pObjHandle->Unwrap(&v);

	assert(v.pdispVal);
	hr = v.pdispVal->QueryInterface(__uuidof(_HostProcessRequest), 
                                (void**) &pMgdHost);
	assert(pMgdHost);

	pMgdHost->RunUserCode(_bstr_t("MyDomain"));

	//
	// Release stuff
	//
	pAppDomainPunk->Release();
	pDefaultDomain->Release();
	pMgdHost->Release();

	//
	// Stop the CLR.  Once the CLR has been unloaded from a process, you
	// cannot load it again.
	//
	pHost->Stop();
	return 0;
}

