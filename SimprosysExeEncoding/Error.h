#ifndef _ERROR_H__FC8EA04F_57D6_4002_81BE_F37E1FA54F47_
#define _ERROR_H__FC8EA04F_57D6_4002_81BE_F37E1FA54F47_

#include <TChar.h>

#ifndef VERBOSE_ERR
#define VERBOSE_ERR	1	//Make this 0 to suppress the MsgBox()
#endif

namespace Err
{
	inline void GetErrorDetails(LPTSTR lpszBuf, DWORD dwBufLen)
	{
		FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
			NULL, GetLastError(), MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), lpszBuf, dwBufLen, NULL );
	}

	inline void ErrorMessage(LPCTSTR lpszErrMsg)
	{
#if VERBOSE_ERR
		MessageBox(NULL, lpszErrMsg, _T("Error"), MB_OK | MB_ICONERROR);
#else
		OutputDebugString(lpszErrMsg);
#endif
	}

	class CErrMsg
	{
		enum {ErrMsgBufSize = 1023};

		TCHAR m_szErrBuf[ ErrMsgBufSize + 1 ];

	public:
		CErrMsg()
		{
			m_szErrBuf[0] = _T('\0');
		}

		CErrMsg(LPCTSTR lpszErrMsg)
		{
			SetErrorMessage(lpszErrMsg);
		}

		inline void CollectErrorDetails()
		{
			GetErrorDetails(m_szErrBuf, ErrMsgBufSize);
		}

		inline void ReportError()
		{
			CollectErrorDetails();
			ErrorMessage(m_szErrBuf);
		}

		inline LPCTSTR GetErrorMessage() const
		{
			return m_szErrBuf;
		}

		inline operator LPCTSTR() const
		{
			return GetErrorMessage();
		}

		inline void ReportError(LPCTSTR lpszErrMsg)
		{
			SetErrorMessage(lpszErrMsg);
			ErrorMessage(m_szErrBuf);
		}

		inline void Clear()
		{
			ZeroMemory(m_szErrBuf, ErrMsgBufSize);
		}

		inline void SetErrorMessage(LPCTSTR lpszErrMsg)
		{
			_tcsncpy(m_szErrBuf, lpszErrMsg, ErrMsgBufSize);
			m_szErrBuf[ErrMsgBufSize] = _T('\0');
		}

		inline CErrMsg& operator=(LPCTSTR lpszErrMsg)
		{
			SetErrorMessage(lpszErrMsg);
			return *this;
		}
	};
}
#endif
