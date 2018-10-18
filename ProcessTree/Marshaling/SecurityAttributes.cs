using System;
using System.Runtime.InteropServices;

namespace ProcessTree.Marshaling
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SecurityAttributes
    {
        public int nLength;
        public IntPtr lpSecurityDescriptor;
        public int bInheritHandle;
    }
}