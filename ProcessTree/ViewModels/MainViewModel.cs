using ProcessTree.Marshaling;
using ProcessTree.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace ProcessTree.ViewModels
{
    public sealed class MainViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IProcessTreeManager treeManager = new ProcessTreeManager();
        private bool isSelectedSomeProcess;
        private string processName;
        private object selectedProcess;

        public MainViewModel()
        {
            StartNewProcess = new DelegateCommand(() =>
            {
                StartupInfo si = new StartupInfo();
                si.cb = (uint)Marshal.SizeOf(si);

                ProcessInformation pi = new ProcessInformation();

                var pSec = new SecurityAttributes();
                var tSec = new SecurityAttributes();

                NativeMethods.CreateProcess(null, processName, ref pSec, ref tSec, false, 0, IntPtr.Zero, null, ref si, out pi);
            });
            StopProcess = new DelegateCommand(() =>
            {
                int processId = (int)((Models.ProcessTree)SelectedProcess).ProcessId;
                Process currentProcess = Process.GetProcessById(processId);
                currentProcess.Kill();
            });
            Refresh = new DelegateCommand(() => treeManager.RefreshTrees());
        }

        public bool IsSelectedSomeProcess
        {
            get => isSelectedSomeProcess;
            set
            {
                SetProperty(ref isSelectedSomeProcess, value);
            }
        }

        public string ProcessName
        {
            get => processName;
            set
            {
                SetProperty(ref processName, value);
            }
        }

        public IEnumerable<Models.ProcessTree> ProcessTrees => treeManager.GetProcessTrees();
        public ICommand Refresh { get; }

        public object SelectedProcess
        {
            get => selectedProcess;
            set
            {
                SetProperty(ref selectedProcess, value);
            }
        }

        public ICommand StartNewProcess { get; }

        public ICommand StopProcess { get; }
    }
}