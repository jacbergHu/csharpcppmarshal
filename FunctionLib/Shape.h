#pragma once
#include <iostream>
using namespace std;

extern "C" class Shape
{
public:
	Shape();
	~Shape();

public:
	int GetAge();
	int GetVolumn();
	char *ReName(char *oldName);

private:
	int age;
	int height;
	int width;
};
