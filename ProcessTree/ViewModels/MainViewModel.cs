﻿using ProcessTree.Marshaling;
using ProcessTree.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace ProcessTree.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        private readonly IProcessTreeManager treeManager;
        private string processName = string.Empty;
        private Models.ProcessTree selectedProcess;
        private readonly Command refreshCommand;
        private readonly Command startProcessCommand;
        private readonly Command stopProcessCommand;

        public MainViewModel(IProcessTreeManager treeManager)
        {
            this.treeManager = treeManager;
            startProcessCommand = new DelegateCommand(StartProcess, () => CanStartProcess);
            stopProcessCommand = new DelegateCommand(StopProcess, () => CanStopProcess);
            refreshCommand = new DelegateCommand(RefreshList);
        }

        private void RefreshList()
        {
            treeManager.RefreshTree();
        }

        private void StopProcess()
        {
            treeManager.CloseProcess(selectedProcess.ProcessId);
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

        [DependsUponProperty(nameof(ProcessName))]
        public bool CanStartProcess => processName != string.Empty;

        [DependsUponProperty(nameof(SelectedProcess))]
        public bool CanStopProcess => selectedProcess != null;

        public string ProcessName
        {
            get => processName;
            set => SetProperty(ref processName, value);
        }

        public IEnumerable<Models.ProcessTree> ProcessesTree => treeManager.GetProcessTree();

        public Models.ProcessTree SelectedProcess
        {
            get => selectedProcess;
            set => SetProperty(ref selectedProcess, value);
        }

        public ICommand RefreshCommand => refreshCommand;

        [RaiseCanExecuteDependsUpon(nameof(CanStartProcess))]
        public ICommand StartProcessCommand => startProcessCommand;

        [RaiseCanExecuteDependsUpon(nameof(CanStopProcess))]
        public ICommand StopProcessCommand => stopProcessCommand;
    }
}