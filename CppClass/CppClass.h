#pragma once
#include "../FunctionLib/Shape.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Collections::Generic;
using namespace System::Collections;

//#pragma comment(lib,"FunctionLib.lib")
#pragma managed
namespace TestCppClass {
	public ref class MShape
	{
	public:
		MShape();
		~MShape();
	public:
		int GetAge();
		int GetVolumn();
		String^ ReName(String^ oldName);
	private:
		Shape* sp = nullptr;
	};
}
