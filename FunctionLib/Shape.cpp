#include "Shape.h"
Shape::Shape()
{
	age = 0;
	height = 0;
	width = 0;
}

Shape::~Shape()
{
	std::cout << "deconstructor" << std::endl;
}
int Shape::GetAge()
{
	return age;
}
int Shape::GetVolumn()
{
	return age * height * width;
}

char *Shape::ReName(char *oldName)
{
	std::cout << "success." << std::endl;
	return nullptr;
}
