#include <iostream>
#include <complex>
#include <string.h>

using namespace std;
const double PI = 3.1415926535898;
const double COMPENSATEFACTOR = 1e-9;
const double REARTH = 6378137;
const double EPS = 1E-8;

typedef int (*DelegateFunc)(int *, int);
typedef complex<double> Complex;
#pragma region test

#pragma pack(1)
typedef struct Student
{
	char name[20];
	int age;
	double scores[32];
} Student;
#pragma pack()

#pragma pack(1)
typedef struct Class
{
	int number;
	Student students[126];
} Class;
#pragma pack()

#pragma pack(1)
typedef struct _SimpleStruct
{
	int Osversion;
	int Subversion;
	int Mainversion;
	double Hight;
	double Points[128][2];
	int Index[20];
	char Szversion[128];
} SimpleStruct, *PSimpleStruct;
#pragma pack()
#pragma endregion

extern "C"
{
	PSimpleStruct GetStruct(void);

	void FreeStruct(PSimpleStruct ps);

	int GetClass(Class pclass[50]);

	double Add(double a, double b);

	void GetArray(double *origin, int n, double *data);

	void Minus(double a, double b, double *c);

	void GetXVersion(PSimpleStruct version);

	void DelegateCall(DelegateFunc callBack, int *result);
}
