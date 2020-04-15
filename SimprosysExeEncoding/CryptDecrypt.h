#ifndef __CRYPT_DECRPYT_H_5B050AC3_6AF8_45da_9FEA_C85F1508F12D
#define __CRYPT_DECRPYT_H_5B050AC3_6AF8_45da_9FEA_C85F1508F12D

#include <WinCrypt.h>
#include <comutil.h>
#include <comdef.h>
#include <vector>

typedef std::vector<LPVOID> LPVOIDVECTOR;

#define	ENCRYPT_ALGORITHM	CALG_RC4
#define ENCRYPT_BLOCKSIZE	8
#define KEYLENGTH			0x00800000

//
// Supports Encryption and Decryption of Data (Auto-Manages Buffers)
//
class CCryptDecrypt
{
	CString	m_strErrMsg;

	HANDLE	m_hHeap;

	LPVOIDVECTOR m_vecBuf;			// Keeps track of the List of Buffers Created while Encrypting and Decrypting

	bool InternalCryptDecrypt(LPCVOID lpData, DWORD dwDataLen, const bstr_t& bstrPassword, LPVOID* ppRetBuf, DWORD* pdwBufLen, bool bEncrypt = true);

public:
	CCryptDecrypt(void);
	~CCryptDecrypt(void);

	LPCVOID Encrypt(LPCVOID lpData, DWORD dwDataLen, const bstr_t& bstrPassword, DWORD* pdwBufLen);

	LPCVOID Decrypt(LPCVOID lpData, DWORD dwDataLen, const bstr_t& bstrPassword, DWORD* pdwBufLen);

	inline const CString* GetErrorMessage() const	{	return &m_strErrMsg;	}

	void FreeBuffers();
};

#endif