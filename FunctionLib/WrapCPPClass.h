#pragma once
#ifndef __WRAPCPPCLASS_H__
#define __WRAPCPPCLASS_H__
#include <string.h>
#include "Shape.h" // needed for EXAMPLEUNMANAGEDDLL_API

#ifdef __cplusplus
extern "C"
{
#endif

	extern Shape *CreateClass();
	extern void DisposeClass(Shape *pObject);
	extern int CallGetAge(Shape *pObject);
	extern int CallGetVolumn(Shape *pObject);
	extern char *CallReName(Shape *pObject, char *oldName);

#ifdef __cplusplus
}
#endif

#endif // __TestClassCallers_h__
