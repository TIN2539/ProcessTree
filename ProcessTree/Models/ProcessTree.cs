using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProcessTree.Models
{
    public class ProcessTree
    {
        public ProcessTree(string processName, uint processId, uint parrentId)
        {
            ProcessName = processName;
            ProcessId = processId;
            ParrentId = parrentId;
        }

        public uint ParrentId { get; }

        public ICollection<ProcessTree> ChildTree { get; } = new ObservableCollection<ProcessTree>();

        public string ProcessName { get; }

        public uint ProcessId { get; }
    }
}