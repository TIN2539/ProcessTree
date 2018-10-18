using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace ProcessTree.ViewModels
{
    public class ProcessTreeManager : IProcessTreeManager
    {
        private ICollection<uint> parrentIds = new List<uint>();
        private ICollection<Models.ProcessTree> processTrees = new ObservableCollection<Models.ProcessTree>();

        public ProcessTreeManager()
        {
            CreateTree();
        }

        public bool AddChildProcessTree(uint parrentId, Models.ProcessTree processTree)
        {
            foreach (var tree in processTrees)
            {
                if (tree.ParrentId == parrentId)
                {
                    tree.ChildTree.Add(processTree);
                    return true;
                }
            }
            return false;
        }

        public void AddParrentId(uint parrentId)
        {
            parrentIds.Add(parrentId);
        }

        public void AddProcessTree(Models.ProcessTree processTree)
        {
            processTrees.Add(processTree);
        }

        public void CreateTree()
        {
            var proc = NativeMethods.CreateToolhelp32Snapshot(SnapshotFlags.Process, 0);

            var entry = new ProcessEntry32();
            entry.dwSize = (uint)Marshal.SizeOf(entry);

            if (NativeMethods.Process32First(proc, ref entry))
            {
                do
                {
                    if (!GetAllParrentIds().Contains(entry.th32ParentProcessID))
                    {
                        AddParrentId(entry.th32ParentProcessID);
                        AddProcessTree(new Models.ProcessTree(entry.szExeFile, entry.th32ProcessID, entry.th32ParentProcessID));
                    }
                    else
                    {
                        AddChildProcessTree(entry.th32ParentProcessID, new Models.ProcessTree(entry.szExeFile, entry.th32ProcessID, entry.th32ParentProcessID));
                    }
                } while (NativeMethods.Process32Next(proc, ref entry));
            }

            NativeMethods.CloseHandle(proc);
        }

        public IEnumerable<uint> GetAllParrentIds()
        {
            return parrentIds;
        }

        public IEnumerable<Models.ProcessTree> GetProcessTrees()
        {
            return processTrees;
        }

        public void RefreshTrees()
        {
            processTrees.Clear();
            parrentIds.Clear();
            CreateTree();
        }
    }
}