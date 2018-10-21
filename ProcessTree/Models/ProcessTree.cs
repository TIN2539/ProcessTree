using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProcessTree.Models
{
    public class ProcessTree
    {
        private readonly int parrentId;
        private readonly int processId;
        private readonly string processName;
        private readonly ICollection<ProcessTree> childTree = new ObservableCollection<ProcessTree>();

        public ProcessTree(ProcessEntry32 process)
        {
            processName = process.szExeFile;
            processId = (int)process.th32ProcessID;
            parrentId = (int)process.th32ParentProcessID;
        }
        public ICollection<ProcessTree> ChildTree => childTree;

        public int ParrentId => parrentId;

        public int ProcessId => processId;

        public string ProcessName => processName;
    }
}