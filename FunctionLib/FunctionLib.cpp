#include "FunctionLib.h"

double Add(double a, double b)
{
	return a + b;
}

void Minus(double a, double b, double *c)
{
	*c = a - b;
}

void GetArray(double *origin, int n, double *data)
{
	for (int i = 0; i < n; i++)
	{
		data[i] = origin[i] * origin[i];
	}
}

PSimpleStruct GetStruct(void)
{
	PSimpleStruct simpleStruct = (PSimpleStruct)malloc(sizeof(SimpleStruct));
	simpleStruct->Mainversion = 19;
	simpleStruct->Osversion = 2;
	simpleStruct->Subversion = 1;
	simpleStruct->Hight = 2.3;
	memset(simpleStruct->Index, 0, 20 * sizeof(int));
	for (int i = 0; i < 128; i++)
		for (int j = 0; j < 2; j++)
		{
			if (j == 0)
			{
				simpleStruct->Points[i][j] = 103.455;
			}
			else
			{
				simpleStruct->Points[i][j] = 35.892329;
			}
		}

	strcpy(simpleStruct->Szversion, "this is test version.");
	return simpleStruct;
}

void FreeStruct(PSimpleStruct ps)
{
	if (ps != nullptr)
	{
		delete ps;
		ps = nullptr;
	}
}

int GetClass(Class pclass[50])
{
	for (int i = 0; i < 50; i++)
	{
		pclass[i].number = i;
		for (int j = 0; j < 126; j++)
		{
			memset(pclass[i].students[j].name, 0, 20);
			sprintf(pclass[i].students[j].name, "name_%d_%d", i, j);
			pclass[i].students[j].age = j % 2 == 0 ? 15 : 20;
		}
	}
	return 0;
}

void GetXVersion(PSimpleStruct version)
{
	version->Osversion++;
	version->Subversion = 0;
	version->Mainversion++;
	version->Hight = 1.89;
	memset(version->Szversion, 0, sizeof(version->Szversion));
	strcpy(version->Szversion, "hello world this is version infos...");
}

void DelegateCall(DelegateFunc callBack, int *result)
{
	int *data = new int[10];
	for (int i = 0; i < 10; i++)
		data[i] = i * i;
	int ret = 0;
	if (callBack)
	{
		ret = callBack(data, 10);
	}
	*result = 100 - ret;
	delete[] data;
	data = nullptr;
}
