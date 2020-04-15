#ifndef __PARAMETERS_H_A1B7ECCA_1691_43d0_ACDD_EBE1208005F9
#define __PARAMETERS_H_A1B7ECCA_1691_43d0_ACDD_EBE1208005F9

#ifdef _DEBUG
#define NO_ERROR_CHECKING	FALSE		//Use Error Checking for Debug Versions (throws Exception from Constructor !!)
#else
#define NO_ERROR_CHECKING	TRUE		//Turn-off Error Checking for Release Versions (Faster but Dangerous !!)
#endif

#ifndef _MFC_VER						//Check if this is NON-MFC application
	typedef const SAFEARRAY* LPCSAFEARRAY;	
#endif

#include <comDef.h>

class CParameters
{
	int		m_nParamCount;
	int		m_nCurParam;
	SAFEARRAY*	m_pSA;

private:
	inline bool IsInvalid()
	{
        return m_nCurParam >= m_nParamCount || m_pSA == NULL;
	}
public:

	CParameters(int nCount)
	{
		m_nCurParam = 0;
		m_nParamCount = nCount;

		SAFEARRAYBOUND bounds[] = {{nCount,0}};			//Array Contains 'nCount' Elements starting from Index '0'
		m_pSA = SafeArrayCreate(VT_VARIANT,1,bounds);	//Create a one-dimensional SafeArray of variants

#if NO_ERROR_CHECKING == FALSE
		if(m_pSA == NULL)	throw "Exception: Unable to Create Safe Array";
#endif
	}

	~CParameters(void)
	{
		if(m_pSA)
		{
			SafeArrayDestroy(m_pSA);
			m_pSA = NULL;
		}
	}

	inline operator LPSAFEARRAY() const		{	return m_pSA;			}
	inline operator LPCSAFEARRAY() const	{	return m_pSA;			}
	inline int	GetCount() const			{	return m_nParamCount;	}

	inline HRESULT AddParameter(const bool& bParamVal)
	{
#if NO_ERROR_CHECKING == FALSE
		if(IsInvalid())	return E_FAIL;
#endif
		long lIndex[] = { m_nCurParam++ };
		VARIANT		var;
		var.vt		= VT_BOOL;
		var.boolVal = bParamVal;
		return SafeArrayPutElement(m_pSA, lIndex, &var);
	}

	inline HRESULT AddParameter(const long& lParamVal)
	{
#if NO_ERROR_CHECKING == FALSE
		if(IsInvalid())	return E_FAIL;
#endif
		long lIndex[] = { m_nCurParam++ };
		VARIANT var;
		var.vt	= VT_I4;
		var.lVal= lParamVal;
		return SafeArrayPutElement(m_pSA, lIndex, &var);
	}

	inline HRESULT AddParameter(LPCTSTR lpszParamVal)
	{
		try
		{
#if NO_ERROR_CHECKING == FALSE
		if(IsInvalid())	return E_FAIL;
#endif
			long lIndex[] = { m_nCurParam++ };
			VARIANT		var;
			var.vt		= VT_BSTR;
			var.bstrVal = SysAllocString(bstr_t(lpszParamVal));
			return SafeArrayPutElement(m_pSA, lIndex, &var);
		}
		catch(_com_error& e)
		{
			return e.Error();
		}
	}

	inline HRESULT AddParameter(const LPCTSTR lpszArray[], int nCount)
	{
		try
		{
#if NO_ERROR_CHECKING == FALSE
		if(IsInvalid())	return E_FAIL;
#endif
			VARIANT var;
			SAFEARRAYBOUND bounds[] = {{nCount,0}};			//Array Contains 'nCount' Elements starting from Index '0'
			var.vt		= VT_ARRAY | VT_BSTR;
			var.parray	= SafeArrayCreate(VT_BSTR,1,bounds);	//Create a one-dimensional SafeArray of variants
			if(var.parray == NULL)
				return E_OUTOFMEMORY;

			for(long i=0; i < nCount; ++i)
			{		
				HRESULT hr = SafeArrayPutElement(var.parray, &i, SysAllocString(bstr_t(lpszArray[i])));
				if(FAILED(hr)) return hr;
			}

			long lIndex[] = { m_nCurParam++ };
			return SafeArrayPutElement(m_pSA, lIndex, &var);
		}
		catch(_com_error& e)
		{
			return e.Error();
		}
	}
};

#endif