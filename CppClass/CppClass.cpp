
#include "CppClass.h"

using namespace TestCppClass;

TestCppClass::MShape::MShape() {
	sp = new Shape();
}

TestCppClass::MShape::~MShape() {
	if (sp) {
		delete sp;
		sp = nullptr;
	}
}

int TestCppClass::MShape::GetAge() {
	return sp->GetAge();
}
int TestCppClass::MShape::GetVolumn() {
	return sp->GetVolumn();
}

String^ TestCppClass::MShape::ReName(String^ oldName)
{
	IntPtr pString = Marshal::StringToHGlobalAnsi(oldName);
	char* ret = nullptr;
	try
	{
		char* pchString = static_cast<char*>(pString.ToPointer());
		ret = sp->ReName(pchString);
	}
	finally
	{
		Marshal::FreeHGlobal(pString);
	}

	String^ strValue = Marshal::PtrToStringAnsi(static_cast<IntPtr>(ret));
	return strValue;
}
