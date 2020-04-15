// CLRHostExe.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "CLRHostExe.h"
#include "Parameters.h"
#include "..\CryptDecrypt.h"
#include "CommandLine.h"

#define	 VERBOSE_ERR	1	//Make this 0 to suppress the MsgBox() and to use OutputDebugString() instead;
#include "..\Error.h"

#include <MSCoreE.h>
#pragma comment(lib, "mscoree.lib")

#import "MSCorLib.tlb"
using namespace mscorlib;

#include <AtlBase.h>

CString	gstrPassword;

Err::CErrMsg gErrMsg;

LRESULT CALLBACK PasswordDlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam);

bool InvokeAssemblyResource(LPTSTR lpCmdLine);

SAFEARRAY* GetDecryptedResource();


int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	InitCommonControls();

	if(IDOK != DialogBox(hInstance, (LPCTSTR)IDD_PASSWORD_DIALOG, GetDesktopWindow(), (DLGPROC)PasswordDlgProc))
		return 0;

	CoInitialize(NULL);

	if(false == InvokeAssemblyResource(lpCmdLine))
		Err::ErrorMessage(gErrMsg.GetErrorMessage());

	CoUninitialize();

	return 0;
}


LRESULT CALLBACK PasswordDlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch(message)
	{
	case WM_INITDIALOG:
		{
			RECT rect;
			int nScreenX = GetSystemMetrics(SM_CXSCREEN);
			int nScreenY = GetSystemMetrics(SM_CYSCREEN);
			GetClientRect(hDlg,&rect);
			SetWindowPos(hDlg,HWND_TOP,(nScreenX-rect.right)/2,(nScreenY-rect.bottom)/2,0,0,SWP_NOSIZE);
			return true;
		}
	case WM_COMMAND:
		{
			if(LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
			{
				GetDlgItemText(hDlg, IDC_PASSWORD_EDIT, gstrPassword.GetBuffer(MAX_PATH), MAX_PATH);
				gstrPassword.ReleaseBuffer();
				EndDialog(hDlg, LOWORD(wParam));
				return true;
			}
			break;
		}
	}
	return false;
}

//
// GetDecryptedResource: Loads the Encrypted Assembly from the Resources and Decrypts it into a SafeArray;
// Caller Should SafeArrayDestroy() the returned value;
//
SAFEARRAY* GetDecryptedResource()
{
	HRSRC hRC = FindResource(NULL,MAKEINTRESOURCE(IDR_EMBEDDED_ASSEMBLY),"RT_EMBEDDED_ASSEMBLY");
	if(hRC == NULL)
	{
		gErrMsg.CollectErrorDetails();	return NULL;
	}

	HGLOBAL hRes = LoadResource(NULL,hRC);
	if(hRes == NULL)
	{
		gErrMsg.CollectErrorDetails(); return NULL;
	}

	DWORD dwSize = SizeofResource(NULL,hRC);
	if(dwSize == NULL)
	{
		gErrMsg.CollectErrorDetails();	return NULL;
	}

	LPVOID hEncAsm = LockResource(hRes);
	if(hEncAsm == NULL)
	{
		gErrMsg = _T("Unable to Lock Assembly for Decryption");	return NULL;
	}

	CCryptDecrypt CryDec;
	DWORD dwAsmLen = 0;

    LPCVOID lpAsm = CryDec.Decrypt(hEncAsm,dwSize,(LPCTSTR)gstrPassword,&dwAsmLen);

	UnlockResource(hRes);

	if(lpAsm == NULL)
	{
		gErrMsg = *CryDec.GetErrorMessage();	return NULL;
	}

	SAFEARRAY* pSA = NULL;

	if(NULL !=(pSA = SafeArrayCreateVector(VT_UI1, 0, dwSize)))
	{
		LPVOID pBuf = NULL;

		if(FAILED(SafeArrayAccessData(pSA,&pBuf)))
		{
			gErrMsg = _T("Unable to Access SafeArray Data");
			return NULL;
		}
		else
		{
			memcpy(pBuf, lpAsm, dwAsmLen);
			
			SafeArrayUnaccessData(pSA);

			return pSA;
		}
	}
	else
	{
		gErrMsg = _T("Unable to Allocate Memory while Loading the Assembly");
	}

	return NULL;
}

//
// InvokeAssemblyResource: Initializes the CLR and Invokes the Decrypted Assembly; ShutsDown the CLR at the end;
//
bool InvokeAssemblyResource(LPTSTR lpCmdLine)
{		
	CComPtr<ICorRuntimeHost>	spRuntimeHost;
	CComPtr<_AppDomain>			spAppDomain;
	CComPtr<IUnknown>			spUnk;

	bool bSuccess = false;

	if(FAILED(CorBindToRuntimeEx( NULL,								// Latest Version by Default
						L"wks",										// Workstation build
						STARTUP_LOADER_OPTIMIZATION_SINGLE_DOMAIN,
						CLSID_CorRuntimeHost ,
						IID_ICorRuntimeHost ,
						(void**)&spRuntimeHost)))
	{
		gErrMsg = _T("Unable to Bind CLR");
		return false;
	}
	if(FAILED(spRuntimeHost->Start()))
	{
		gErrMsg = _T("Unable to Start CLR");
		return false;
	}
	do
	{
		if(FAILED(spRuntimeHost->GetDefaultDomain(&spUnk)))
		{
			gErrMsg = _T("Unable to GetDefaultDomain");
			break;
		}
		if(FAILED(spUnk->QueryInterface(&spAppDomain.p)))
		{
			gErrMsg = _T("Unable to Query AppDomain Interface");
			break;
		}

		SAFEARRAY*	pSA = GetDecryptedResource();
		if(pSA)
		{
			int nNumArgs = 0;
			CCommandLine CmdLine;
			LPTSTR*	pStr = new LPSTR[CmdLine.GetArgumentCount()];
			for(int i=1; i < CmdLine.GetArgumentCount(); ++i)
			{
				LPWSTR pArg = CmdLine[i];
				pStr[i-1] = new TCHAR[wcslen(pArg)+1];
				_tcscpy(pStr[i-1], bstr_t(pArg));
			}

			CParameters param(1);
			param.AddParameter(pStr, CmdLine.GetArgumentCount()-1);

			try
			{	
				spAppDomain->Load_3(pSA)->EntryPoint->Invoke_3(_variant_t(), NULL);	// Invoke the Entry Point with No Arguments

				/***
					To Invoke the Method with CommandLine Arguments, call should be shown below.
				***/

				// spAppDomain->Load_3(pSA)->EntryPoint->Invoke_3(_variant_t(), param);
				
				bSuccess = true;	// Everything Went Fine !!
			}
			catch(_com_error ex)
			{
				gErrMsg = ex.ErrorMessage();
			}

			for(int i=1; i < CmdLine.GetArgumentCount(); ++i)
			{				
				delete pStr[i-1];
				pStr[i-1] = NULL;
			}
			delete[] pStr;

			SafeArrayDestroy(pSA);
			pSA = NULL;	
		}
	}while(false);

	if(FAILED(spRuntimeHost->Stop()))
	{
		gErrMsg = _T("Unable to Stop CLR");
		return false;
	}

	return bSuccess;
}