#include "StdAfx.h"
#include "cryptdecrypt.h"

CCryptDecrypt::CCryptDecrypt(void)
{
	m_hHeap	= HeapCreate(HEAP_NO_SERIALIZE, 4096, 0);
}

CCryptDecrypt::~CCryptDecrypt(void)
{
	FreeBuffers();

	if(m_hHeap)
	{
		HeapDestroy(m_hHeap);
		m_hHeap = NULL;
	}
}

LPCVOID CCryptDecrypt::Encrypt(LPCVOID lpData, DWORD dwDataLen, const _bstr_t& bstrPassword, DWORD* pdwBufLen)
{
	if(m_hHeap == NULL)
	{
		m_strErrMsg = _T("Heap Not Created Properly");
		return NULL;
	}
	if(lpData == NULL)
	{
		m_strErrMsg = _T("NULL Pointer Data for Encryption");
		return NULL;
	}
	if(dwDataLen <= 0)
	{
		m_strErrMsg = _T("Zero sized Data for Encryption");
		return NULL;
	}

	LPVOID	pBuf = NULL;

	bool bSuccess = InternalCryptDecrypt(lpData, dwDataLen, bstrPassword, &pBuf, pdwBufLen, true);
		
	m_vecBuf.push_back(pBuf);			//It is possible that memory is allocted but Encryption failed !!

	if(bSuccess == false)
	{
		pBuf = NULL;					//We already Added pBuf to the vector so we can do this

		*pdwBufLen = NULL;
	}

	return pBuf;
}


LPCVOID CCryptDecrypt::Decrypt(LPCVOID lpData, DWORD dwDataLen, const bstr_t& bstrPassword, DWORD* pdwBufLen)
{
	if(m_hHeap == NULL)
	{
		m_strErrMsg = _T("Heap Not Created Properly");
		return NULL;
	}
	if(lpData == NULL)
	{
		m_strErrMsg = _T("NULL Pointer Data for Decryption");
		return NULL;
	}
	if(dwDataLen <= 0)
	{
		m_strErrMsg = _T("Zero sized Data for Decryption");
		return NULL;
	}

	LPVOID	pBuf = NULL;

	bool bSuccess = InternalCryptDecrypt(lpData, dwDataLen, bstrPassword, &pBuf, pdwBufLen, false);

	m_vecBuf.push_back(pBuf);			//It is possible that memory is allocted but Decryption failed !!

	if(bSuccess == false)
	{
		pBuf = NULL;					//We already Added pBuf to the vector so we can do this

		*pdwBufLen = NULL;
	}

	return pBuf;
}


bool CCryptDecrypt::InternalCryptDecrypt(LPCVOID lpData, DWORD dwDataLen, const bstr_t& bstrPassword, LPVOID* ppRetBuf, DWORD* pdwBufLen, bool bEncrypt /*= true*/)
{
	bool		bSuccess	= false;
	HCRYPTPROV	hCryptProv	= NULL;
	HCRYPTHASH	hHash		= NULL;
	HCRYPTKEY	hKey		= NULL;
	
	*pdwBufLen	= dwDataLen;

	do
	{
		try
		{
			if(CryptAcquireContext(&hCryptProv, NULL, MS_ENHANCED_PROV, PROV_RSA_FULL, 0) == false)
			{
				if(CryptAcquireContext(&hCryptProv, NULL, MS_ENHANCED_PROV, PROV_RSA_FULL, CRYPT_NEWKEYSET) == false)
				{
					m_strErrMsg = _T("Unable to Acquire Crypt Provider");
					break;
				}
			}
			if(CryptCreateHash(hCryptProv, CALG_MD5, 0, 0, &hHash) == false)
			{
				m_strErrMsg = _T("Unable to Create Hash");
				break;
			}
			if(CryptHashData(hHash, (BYTE*)(char*) bstrPassword, bstrPassword.length(), 0) == false)
			{
				m_strErrMsg = _T("Unable to Hash the Password");
				break;
			}
			if(CryptDeriveKey(hCryptProv, ENCRYPT_ALGORITHM, hHash, KEYLENGTH, &hKey) == false)
			{
				m_strErrMsg = _T("Unable to Derive Key");
				break;
			}
			if(CryptDestroyHash(hHash) == false)
			{
				m_strErrMsg = _T("Unable to Destroy Hash");
				break;
			}
			hHash = NULL;
			if(bEncrypt)
			{
				if(CryptEncrypt(hKey, 0, true, 0, NULL, pdwBufLen, 0) == false)
				{
					m_strErrMsg = _T("Unable to Determine the Size of Encrypted Buffer");
					break;
				}
			}
			if(NULL == (*ppRetBuf = HeapAlloc(m_hHeap, HEAP_ZERO_MEMORY, *pdwBufLen)))
			{
				m_strErrMsg = _T("Unable to Allocate Memory");
				break;
			}
			if(bEncrypt)
			{
				if(CryptEncrypt(hKey, 0, true, 0, (BYTE*) memcpy(*ppRetBuf, lpData, dwDataLen), &dwDataLen, *pdwBufLen) == false)
				{
					m_strErrMsg = _T("Unable to Encrypt the Data");
					break;
				}
			}
			else
			{
				if(CryptDecrypt(hKey, 0, true, 0, (BYTE*) memcpy(*ppRetBuf, lpData, dwDataLen), pdwBufLen) == false)
				{
					m_strErrMsg = _T("Unable to Decrypt the Data");
					break;
				}
			}

			bSuccess = true;
		}
		catch(_com_error e)
		{
			m_strErrMsg = e.ErrorMessage();
		}
	}while(false);

	if(hKey)
	{
		CryptDestroyKey(hKey);
		hKey = NULL;
	}
	if(hHash)	
	{
		CryptDestroyHash(hHash);
		hHash = NULL;
	}
	if(hCryptProv)
	{
		CryptReleaseContext(hCryptProv, 0);
		hCryptProv = NULL;
	}

	return bSuccess;
}

void CCryptDecrypt::FreeBuffers()
{
	if(m_hHeap == NULL)	return;

	LPVOIDVECTOR::iterator iter = m_vecBuf.begin();
	LPVOIDVECTOR::iterator iterEnd = m_vecBuf.end();

	for( ; iter != iterEnd; ++iter)
	{
		if(*iter)
			HeapFree(m_hHeap, 0, *iter);
	}

	m_vecBuf.clear();
}
