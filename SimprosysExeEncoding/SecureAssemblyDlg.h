// SecureAssemblyDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CSecureAssemblyDlg dialog
class CSecureAssemblyDlg : public CDialog
{
// Construction
public:
	CSecureAssemblyDlg(CWnd* pParent = NULL);	// standard constructor
	~CSecureAssemblyDlg();
// Dialog Data
	enum { IDD = IDD_SECUREASSEMBLY_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

	bool	m_bValidSourcePath;
	bool	m_bValidDestinationPath;

	USHORT	m_nEmbeddedExeID;

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
private:
	CEdit m_ctrlSourcePathEdit;
	CEdit m_ctrlDestinationPathEdit;
	CButton m_ctrlGoButton;
public:
	afx_msg void OnBnClickedCancel();
	afx_msg void OnBnClickedSourceBrowse();
	afx_msg void OnBnClickedDestinationBrowse();
	afx_msg void OnBnClickedGoSecureButton2();
};
