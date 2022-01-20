using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCDll {
    public delegate int TestDelegate2Cpp([MarshalAs(UnmanagedType.LPArray, SizeConst = 10)] int[] data, int n);
    class Program {
        static void Main(string[] args) {
            Random rand = new Random();

            #region general marshal

            double sum = FunctionLib.Add(3.2, 2.4);
            Console.WriteLine(sum);
            double c = 0;
            FunctionLib.Minus(3.2, 1.9, out c);
            Console.WriteLine(c);

            double[] angles = new double[10] { 355, 322, 0, 2, 368, 359.8, 355.4, 355.1, 348.9, 350 };
            double[] data = new double[10];
            FunctionLib.GetArray(angles, angles.Length, data);

            #endregion

            #region delegate marshal
            TestDelegate2Cpp del = new TestDelegate2Cpp((int[] dataIn, int n) => {
                int ret = 0;
                for (int i = 0; i < n; i++) {
                    ret += dataIn[i];
                }
                return ret;
            });

            int resultII = 0;
            FunctionLib.DelegateCall(del, ref resultII);
            Console.WriteLine(resultII);
            #endregion

            #region struct array marshal

            int size = Marshal.SizeOf(typeof(Class)) * 50;
            byte[] bytes = new byte[size];
            IntPtr pbuff = Marshal.AllocHGlobal(size);
            Class[] pclass = new Class[50];
            FunctionLib.GetClass(pbuff);

            for (int i = 0; i < 50; i++) {
                IntPtr ppointer = new IntPtr(pbuff.ToInt64() + Marshal.SizeOf(typeof(Class)) * i);
                pclass[i] = (Class) Marshal.PtrToStructure(ppointer, typeof(Class));
            }
            Marshal.FreeHGlobal(pbuff);

            Versions version = new Versions();
            version.Mainversion = 10;
            version.Subversion = 1;
            version.Szversion = "hello world...";
            version.Hight = 1.67;
            version.Osversion = 0;
            GetVersion(ref version);

            size = Marshal.SizeOf(typeof(Versions));
            IntPtr pversion = Marshal.AllocHGlobal(size);
            Marshal.WriteInt32(pversion, size);
            FunctionLib.GetVersion(pversion);
            version.Osversion = Marshal.ReadInt32(pversion, 0);
            version.Subversion = Marshal.ReadInt32(pversion, 4);
            version.Mainversion = Marshal.ReadInt32(pversion, 8);
            version.Hight = Marshal.ReadInt64(pversion, 12);
            version.Szversion = Marshal.PtrToStringAnsi((IntPtr) (pversion + 2148), 128);
            Marshal.FreeHGlobal(pversion);
            Console.WriteLine(version);

            IntPtr ptr = FunctionLib.GetStruct();
            version = (Versions) Marshal.PtrToStructure(ptr, typeof(Versions));
            Marshal.FreeCoTaskMem(ptr);
            // 非托管内存分配方法使用CoTaskMemAlloc()因此可以直接使用Marshal的静态方法进行内存释放
            //FunctionLib.FreeStruct(ptr);
            Console.WriteLine("\n非托管代码传出的结构体");
            Console.WriteLine(version);

            #endregion

            #region c# use c++ class with IntPtr

            CSUnmanageWrap cs = new CSUnmanageWrap();
            int agec = cs.GetAge();
            int volumnc = cs.GetVolumn();
            string resultc = cs.ReName("lucy");
            cs.Dispose();
            #endregion

            Console.Read();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Student {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string name;
            public int age;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public double[] scores;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Class {
            public int number;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 126)]
            public Student[] students;
        }

        [DllImport("FunctionLib", EntryPoint = "GetXVersion", CallingConvention = CallingConvention.Cdecl)]
        private static extern void GetVersion(ref Versions pversion);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct Versions {
            public int Osversion;
            public int Subversion;
            public int Mainversion;
            public double Hight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128 * 2)]
            public double[] Points;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public int[] Index;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Szversion;

            public override string ToString() {
                return string.Format("{0}\n{1}\n{2}\n{3}\n{4}", Osversion, Mainversion, Subversion, Hight, Szversion);
            }
        }
    }

    public static class FunctionLib {
        [DllImport("FunctionLib", EntryPoint = "Add", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Add(double a, double b);

        [DllImport("FunctionLib", EntryPoint = "Minus", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Minus(double a, double b, out double c);

        [DllImport("FunctionLib", EntryPoint = "GetArray", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetArray(double[] data, int n, double[] squre);

        [DllImport("FunctionLib", EntryPoint = "GetClass", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetClass(IntPtr pclass);

        [DllImport("FunctionLib", EntryPoint = "GetXVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetVersion(IntPtr pversion);

        [DllImport("FunctionLib", EntryPoint = "CalcStddev", CallingConvention = CallingConvention.Cdecl)]
        public static extern double CalcStddev(double[] angles, int n);

        [DllImport("FunctionLib", EntryPoint = "GetStruct", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetStruct();

        [DllImport("FunctionLib", EntryPoint = "FreeStruct", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeStruct(IntPtr ptr);

        [DllImport("FunctionLib", EntryPoint = "DelegateCall", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DelegateCall(TestDelegate2Cpp ptr, ref int result);
    }
}
