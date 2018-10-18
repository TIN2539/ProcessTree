using ProcessTree.Marshaling;
using System;
using System.Runtime.InteropServices;

namespace ProcessTree
{
    public static class NativeMethods
    {
        [DllImport("Kernel32.dll")]
        public extern static bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CreateProcess(
              string lpApplicationName,
              string lpCommandLine,
              ref SecurityAttributes lpProcessAttributes,
              ref SecurityAttributes lpThreadAttributes,
              bool bInheritHandles,
              uint dwCreationFlags,
              IntPtr lpEnvironment,
              string lpCurrentDirectory,
              [In] ref StartupInfo lpStartupInfo,
              out ProcessInformation lpProcessInformation);

        [DllImport("Kernel32.dll")]
        public extern static IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);

        [DllImport("Kernel32.dll")]
        public extern static bool Process32First(IntPtr handle, ref ProcessEntry32 th32processid);

        [DllImport("Kernel32.dll")]
        public extern static bool Process32Next(IntPtr handle, ref ProcessEntry32 th32processid);
    }
}