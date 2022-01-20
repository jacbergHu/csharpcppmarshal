using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCDll
{
    public class DataElement
    {
    }

    public class Util
    {
        /// <summary> 
        ///  将结构转换为字节数组
        /// </summary> 
        /// <param name="obj"> 结构对象 </param> 
        /// <returns> 字节数组 </returns>  
        public static byte[] StructToBytes(object obj)
        {
            // 得到结构体的大小 
            int size = Marshal.SizeOf(obj);
            // 创建byte数组 
            byte[] bytes = new byte[size];
            // 分配结构体大小的内存空间 
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            // 将结构体拷到分配好的内存空间 
            Marshal.StructureToPtr(obj, structPtr, false);
            // 从内存空间拷到byte数组 
            Marshal.Copy(structPtr, bytes, 0, size);
            // 释放内存空间 
            Marshal.FreeHGlobal(structPtr);
            // 返回byte数组 
            return bytes;
        }

        /// <summary> 
        ///  byte数组转结构
        /// </summary> 
        /// <param name="bytes"> byte数组 </param> 
        /// <param name="type"> 结构类型 </param> 
        /// <returns> 转换后的结构 </returns>  
        public static object BytesToStruct(byte[] bytes, Type type)
        {
            // 得到结构的大小 
            int size = Marshal.SizeOf(type);
            // byte数组长度小于结构的大小 
            if (size > bytes.Length)
            {
                // 返回空 
                return null;
            }
            // 分配结构大小的内存空间 
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            // 将byte数组拷到分配好的内存空间 
            Marshal.Copy(bytes, 0, structPtr, size);
            // 将内存空间转换为目标结构 
            object obj = Marshal.PtrToStructure(structPtr, type);
            // 释放内存空间 
            Marshal.FreeHGlobal(structPtr);
            // 返回结构 
            return obj;
        }
    }
}
