#include "WrapCPPClass.h"

extern "C" Shape *CreateClass()
{
	return new Shape();
}

extern "C" void DisposeClass(Shape *pObject)
{
	if (pObject != nullptr)
	{
		delete pObject;
		pObject = nullptr;
	}
}

extern "C" int CallGetAge(Shape *pObject)
{
	if (pObject != nullptr)
	{
		return pObject->GetAge();
	}
	return -1;
}

extern "C" int CallGetVolumn(Shape *pObject)
{
	if (pObject != nullptr)
	{
		return pObject->GetVolumn();
	}
	return -1;
}

extern "C" char *CallReName(Shape *pObject, char *oldName)
{
	// marshal char* or string to c#
	// reference https://social.msdn.microsoft.com/Forums/vstudio/en-US/ccdc1c5c-8324-4ac8-9273-564205181a66/return-char-arrays-from-c-to-c#:~:text=%20Return%20char%20arrays%20from%20C%2B%2B%20to%20C%23,%3A%0Aextern%20%22C%22%20RETURNCHAR_API%20char%204%20...%20More%20
	if (pObject != nullptr)
	{
		//char* result = pObject->ReName(oldName);
		char test1[11] = "successed.";
		char *pszReturn = nullptr;
		size_t stSize = strlen(test1) + sizeof(char);

		pszReturn = (char *)::malloc(stSize);
		// Copy the contents of test1
		// to the memory pointed to by pszReturn.
		strcpy(pszReturn, test1);
		return pszReturn;
	}
	return nullptr;
}
