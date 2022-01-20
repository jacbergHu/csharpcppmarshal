using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCDll {
    public class CSUnmanageWrap {
        #region P/Invoke

        [DllImport("FunctionLib", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateClass();

        [DllImport("FunctionLib", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisposeClass(IntPtr pobj);
        // must be specified to cdecl,its means caller clean stack
        [DllImport("FunctionLib", EntryPoint = "CallGetAge", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CallGetAge(IntPtr pobj);

        [DllImport("FunctionLib", EntryPoint = "CallGetVolumn", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int CallGetVolumn(IntPtr pobj);

        [DllImport("FunctionLib", EntryPoint = "CallReName", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [
            return :MarshalAs(UnmanagedType.LPStr)
        ]
        public static extern string CallReName(IntPtr pobj, string oldName);

        #endregion

        #region Members
        private IntPtr _pNativeObject; // Variable to hold the C++ class's this pointer
        #endregion Members

        public CSUnmanageWrap() {
            // We have to Create an instance of this class through an exported function
            this._pNativeObject = CreateClass();
        }

        public void Dispose() {
            Dispose(true);
        }

        protected virtual void Dispose(bool bDisposing) {
            if (this._pNativeObject != IntPtr.Zero) {
                // Call the DLL Export to dispose this class
                DisposeClass(this._pNativeObject);
                this._pNativeObject = IntPtr.Zero;
            }

            if (bDisposing) {
                // No need to call the finalizer since we've now cleaned
                // up the unmanaged memory
                GC.SuppressFinalize(this);
            }
        }

        // This finalizer is called when Garbage collection occurs, but only if
        // the IDisposable.Dispose method wasn't already called.
        ~CSUnmanageWrap() {
            Dispose(false);
        }

        #region Wrapper methods
        public int GetAge() {
            int age = CallGetAge(this._pNativeObject);
            return age;
        }

        public int GetVolumn() {
            int volumn = CallGetVolumn(this._pNativeObject);
            return volumn;
        }

        public string ReName(string oldName) {
            string result = CallReName(this._pNativeObject, oldName);
            return result;
        }
        #endregion Wrapper methods
    }
}
