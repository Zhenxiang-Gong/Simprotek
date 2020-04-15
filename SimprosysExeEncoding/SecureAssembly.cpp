// SecureAssembly.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "SecureAssembly.h"
#include "SecureAssemblyDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CSecureAssemblyApp

BEGIN_MESSAGE_MAP(CSecureAssemblyApp, CWinApp)
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()


// CSecureAssemblyApp construction

CSecureAssemblyApp::CSecureAssemblyApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CSecureAssemblyApp object

CSecureAssemblyApp theApp;


// CSecureAssemblyApp initialization

BOOL CSecureAssemblyApp::InitInstance()
{
	// InitCommonControls() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	InitCommonControls();

	CWinApp::InitInstance();

	AfxEnableControlContainer();


	CSecureAssemblyDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}
