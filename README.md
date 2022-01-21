# csharpcppmarshal
this project is a test example for marshaling data or structor from c# to c++ or verse visa.i
t mainly contains base data type,e.g. int,int*,and function pointer with delegate in c#,or structor array,
class marshal etc.hope much more talking with me.

you can reference this project to your solution,e.g. c# calls algorithms which is mostly written by c++.
cli/clr is not implemented well in linux,so if you use this case is linux,you call c++ class in c# with IntPtr parameter,
your c# class can hold this handle,and transfer it to unmanage code in c++ of void* or other class object,
and if you deconstructor the obj,you can make it to IntPtr.Zero,it stands for nullptr of c++.
## os instructions
you can run thia example in linux and windwos,many possible tech will be used in windows of c++ marshal with c#,but 
Marshal.sizeof() api will be some error in linux,you'd better count the byte size of a data object.
stable and high performance clr/cli will be hoped in future..netcore can still develop some server app in terminal.
