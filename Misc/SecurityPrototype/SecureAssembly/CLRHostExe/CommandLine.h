#ifndef __COMMANDLINE_H__9BDC769B_DD72_4801_A419_40326C078D38__
#define __COMMANDLINE_H__9BDC769B_DD72_4801_A419_40326C078D38__

#include <Shellapi.h>
#pragma comment(lib, "Shell32.lib")

class CCommandLine
{
	LPTSTR	m_lpszCmdLine;
	LPWSTR*	m_pArgv;
	int		m_nArgc;
public:
	CCommandLine(void)
	{
		m_lpszCmdLine = GetCommandLine();
		m_pArgv = CommandLineToArgvW(bstr_t(m_lpszCmdLine), &m_nArgc);
	}

	~CCommandLine(void)
	{
	}

	inline int GetArgumentCount() const {	return m_nArgc;	}
	inline LPWSTR* GetArguments() const	{	return m_pArgv;	}

	inline LPWSTR operator[](int nIndex) const
	{
		if(nIndex >= m_nArgc || nIndex < 0) return NULL;
		return m_pArgv[nIndex];
	}
};

#endif