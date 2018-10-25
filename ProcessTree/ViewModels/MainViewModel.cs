using ProcessTree.Marshaling;
using ProcessTree.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace ProcessTree.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        private readonly Command refreshCommand;
        private readonly Command startProcessCommand;
        private readonly Command stopProcessCommand;
        private readonly IProcessTreeManager treeManager;
        private string processName = string.Empty;
        private Models.ProcessTree selectedProcess;

        public MainViewModel(IProcessTreeManager treeManager)
        {
            this.treeManager = treeManager;
            startProcessCommand = new DelegateCommand(StartProcess, () => CanStartProcess);
            stopProcessCommand = new DelegateCommand(StopProcess, () => CanStopProcess);
            refreshCommand = new DelegateCommand(RefreshList);
        }

        [DependsUponProperty(nameof(ProcessName))]
        public bool CanStartProcess => processName != string.Empty;

        [DependsUponProperty(nameof(SelectedProcess))]
        public bool CanStopProcess => selectedProcess != null;

        public IEnumerable<Models.ProcessTree> ProcessesTree => treeManager.GetProcessTree();

        public string ProcessName
        {
            get => processName;
            set => SetProperty(ref processName, value);
        }

        public ICommand RefreshCommand => refreshCommand;

        public Models.ProcessTree SelectedProcess
        {
            get => selectedProcess;
            set => SetProperty(ref selectedProcess, value);
        }

        [RaiseCanExecuteDependsUpon(nameof(CanStartProcess))]
        public ICommand StartProcessCommand => startProcessCommand;

        [RaiseCanExecuteDependsUpon(nameof(CanStopProcess))]
        public ICommand StopProcessCommand => stopProcessCommand;

        private void RefreshList()
        {
            treeManager.RefreshTree();
        }

        private void StartProcess()
        {
            StartupInfo si = new StartupInfo();
            si.cb = (uint)Marshal.SizeOf(si);

            ProcessInformation pi = new ProcessInformation();

            var pSec = new SecurityAttributes();
            var tSec = new SecurityAttributes();

            NativeMethods.CreateProcess(null, processName, ref pSec, ref tSec, false, 0, IntPtr.Zero, null, ref si, out pi);

            ProcessName = string.Empty;
        }

        private void StopProcess()
        {
            treeManager.CloseProcess(selectedProcess);
        }
    }
}