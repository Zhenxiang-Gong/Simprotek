// BootstrapExe.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "BootstrapExe.h"
#include "..\CryptDecrypt.h"

#define	 VERBOSE_ERR	1	//Make this 0 to suppress the MsgBox() and to use OutputDebugString() instead;
#include "..\Error.h"

#import "Release\InvokeAssembly.tlb" no_namespace named_guids

CString	gstrPassword;

Err::CErrMsg gErrMsg;

LRESULT CALLBACK PasswordDlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam);

bool InvokeAssemblyResource();

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

	if(false == InvokeAssemblyResource())
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
bool InvokeAssemblyResource()
{		
	IInvokeAssemblyPtr pInvoker;

	if(FAILED(pInvoker.CreateInstance(CLSID_CInvokeAssembly)))
	{
		gErrMsg = _T("Unable to Create InvokeAssembly Object !!");
		return false;
	}

	SAFEARRAY*	pSA = GetDecryptedResource();

	if(pSA)
	{
		pInvoker->LoadAndExecute(pSA);

		SafeArrayDestroy(pSA);

		pSA = NULL;	

		return true;
	}

	return false;
}
