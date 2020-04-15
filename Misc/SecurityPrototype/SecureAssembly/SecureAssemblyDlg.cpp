// SecureAssemblyDlg.cpp : implementation file
//

#include "stdafx.h"
#include "SecureAssembly.h"
#include "SecureAssemblyDlg.h"
#include "CryptDecrypt.h"
#include "ResourceManipulator.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CSecureAssemblyDlg dialog



CSecureAssemblyDlg::CSecureAssemblyDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSecureAssemblyDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	m_bValidSourcePath = false;
	m_bValidDestinationPath = false;
	m_nEmbeddedExeID = IDR_CLRHOST_EXE;

	CoInitialize(NULL);
}

CSecureAssemblyDlg::~CSecureAssemblyDlg()
{
	CoUninitialize();
}

void CSecureAssemblyDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_SOURCEPATH_EDIT, m_ctrlSourcePathEdit);
	DDX_Control(pDX, IDC_DESTINATIONPATH_EDIT, m_ctrlDestinationPathEdit);
	DDX_Control(pDX, IDC_PASSWORD_EDIT, m_ctrlPasswordEdit);
	DDX_Control(pDX, IDC_GO_SECURE_BUTTON2, m_ctrlGoButton);
}

BEGIN_MESSAGE_MAP(CSecureAssemblyDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(ID_SOURCE_BROWSE, OnBnClickedSourceBrowse)
	ON_BN_CLICKED(ID_DESTINATION_BROWSE, OnBnClickedDestinationBrowse)
	ON_BN_CLICKED(IDC_GO_SECURE_BUTTON2, OnBnClickedGoSecureButton2)
	ON_BN_CLICKED(IDCANCEL, OnBnClickedCancel)
	ON_BN_CLICKED(IDC_CLRHOST_RADIO, OnBnClickedClrhostRadio)
	ON_BN_CLICKED(IDC_BOOTSTRAP_RADIO, OnBnClickedBootstrapRadio)
END_MESSAGE_MAP()


// CSecureAssemblyDlg message handlers

BOOL CSecureAssemblyDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	CheckDlgButton(IDC_CLRHOST_RADIO, BST_CHECKED);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CSecureAssemblyDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CSecureAssemblyDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CSecureAssemblyDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CSecureAssemblyDlg::OnBnClickedSourceBrowse()
{
	CFileDialog FileDlg(true,NULL,NULL,OFN_HIDEREADONLY|OFN_FILEMUSTEXIST,_T("Managed Assemblies (*.Exe)|*.Exe||"),this);
	if(FileDlg.DoModal() != IDOK)	return;
	this->m_ctrlSourcePathEdit.SetWindowText(FileDlg.GetPathName());
	m_bValidSourcePath = true;
	this->m_ctrlGoButton.EnableWindow(m_bValidDestinationPath);
}

void CSecureAssemblyDlg::OnBnClickedDestinationBrowse()
{
	CFileDialog FileDlg(false,_T("Exe"),_T("SecuredExe"),OFN_OVERWRITEPROMPT,_T("Native Executables (*.Exe)|*.Exe||"),this);
	if(FileDlg.DoModal() != IDOK)	return;
	this->m_ctrlDestinationPathEdit.SetWindowText(FileDlg.GetPathName());
	m_bValidDestinationPath = true;
	this->m_ctrlGoButton.EnableWindow(m_bValidSourcePath);
}

void CSecureAssemblyDlg::OnBnClickedGoSecureButton2()
{
	CString strSourcePath,strDestinationPath,strPassword;
	this->m_ctrlSourcePathEdit.GetWindowText(strSourcePath);
	this->m_ctrlDestinationPathEdit.GetWindowText(strDestinationPath);
	this->m_ctrlPasswordEdit.GetWindowText(strPassword);
	if(strSourcePath.CompareNoCase(strDestinationPath) == 0)	
	{
		MessageBox(_T("Source and Destination Paths Should not be Same !!"),_T("Error"),MB_OK);
		return;
	}
	try
	{
		CCryptDecrypt CryDec;

		CFile SourceFile(strSourcePath, CFile::modeRead | CFile::typeBinary | CFile::shareDenyWrite);

		LPVOID pSource = new BYTE[SourceFile.GetLength()+1];
		if(pSource)
		{
			SourceFile.Read(pSource, SourceFile.GetLength());

			DWORD dwEncLen = 0;
			LPCVOID pEnc = CryDec.Encrypt(pSource, SourceFile.GetLength(), (LPCTSTR)strPassword, &dwEncLen);

			if(!CResourceManipulator::DumpResource(MAKEINTRESOURCE(m_nEmbeddedExeID), _T("RT_EMBEDDED_EXE"),strDestinationPath))
				MessageBox(*CResourceManipulator::GetErrorMessage());
			if(!CResourceManipulator::UpdateResource(strDestinationPath,MAKEINTRESOURCE(IDR_EMBEDDED_ASSEMBLY),_T("RT_EMBEDDED_ASSEMBLY"),MAKELANGID(LANG_NEUTRAL, SUBLANG_NEUTRAL),(LPVOID)pEnc,dwEncLen))
				MessageBox(*CResourceManipulator::GetErrorMessage());
			
			delete(pSource);

			CString strMsg = strDestinationPath + _T(" was generated successfully.\n\nYour managed assembly is now protected from dissembly.\n\nYou could invoke your assembly any time by supplying the same password you entered here (leave it blank if none).");

            MessageBox(strMsg,_T("Successful"),MB_OK | MB_ICONINFORMATION);			
		}
		else
			MessageBox(_T("Unable to Allocate Memory"), _T("Error"),MB_OK|MB_ICONERROR);

	}
	catch(CFileException* pEx)
	{
		pEx->ReportError();
		pEx->Delete();
	}
}

void CSecureAssemblyDlg::OnBnClickedCancel()
{
	OnCancel();
}

void CSecureAssemblyDlg::OnBnClickedClrhostRadio()
{
	m_nEmbeddedExeID = IDR_CLRHOST_EXE;
}

void CSecureAssemblyDlg::OnBnClickedBootstrapRadio()
{
	m_nEmbeddedExeID = IDR_BOOTSTRAP_EXE;
}
