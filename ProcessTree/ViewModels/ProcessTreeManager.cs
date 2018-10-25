using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace ProcessTree.ViewModels
{
    public class ProcessTreeManager : IProcessTreeManager
    {
        private ICollection<Models.ProcessTree> allProcesses = new List<Models.ProcessTree>();
        private ICollection<Models.ProcessTree> processTrees = new ObservableCollection<Models.ProcessTree>();

        public void CloseProcess(Models.ProcessTree selectedProcess)
        {
            Process currentProcess = Process.GetProcessById(selectedProcess.ProcessId);
            currentProcess.Kill();
            RemoveSelectedItem(selectedProcess);
        }

        public void CreateTree()
        {
            GetProcessesList();
            var orderedProcessesByParrentId = allProcesses.OrderBy(node => node.ParrentId).ToArray();
            for (int i = 0; i < orderedProcessesByParrentId.Length; ++i)
            {
                Models.ProcessTree parent = FindInTree(orderedProcessesByParrentId[i].ParrentId);
                if ((orderedProcessesByParrentId[i].ParrentId == 0 && orderedProcessesByParrentId[i].ProcessId == 0) || parent == null)
                {
                    processTrees.Add(orderedProcessesByParrentId[i]);
                }
                else
                {
                    parent.ChildTree.Add(orderedProcessesByParrentId[i]);
                }
            }
        }

        public IEnumerable<Models.ProcessTree> GetProcessTree()
        {
            CreateTree();
            return processTrees;
        }

        public void RefreshTree()
        {
            processTrees.Clear();
            allProcesses.Clear();
            CreateTree();
        }

        public void StartProcess(string name)
        {
            Process.Start(name);
        }

        private void FindInChild(Models.ProcessTree selectedProcess, ICollection<Models.ProcessTree> child)
        {
            foreach (Models.ProcessTree process in child)
            {
                if (process == selectedProcess)
                {
                    child.Remove(process);
                    break;
                }
                else if (process.ChildTree.Count > 0)
                {
                    FindInChild(selectedProcess, process.ChildTree);
                }
            }
        }

        private Models.ProcessTree FindInNode(Models.ProcessTree node, int id)
        {
            Models.ProcessTree finded = null;
            foreach (Models.ProcessTree child in node.ChildTree)
            {
                if (child.ProcessId.Equals(id))
                {
                    finded = child;
                    break;
                }
                else if (child.ChildTree.Count != 0)
                {
                    finded = FindInNode(child, id);
                    if (finded != null)
                    {
                        break;
                    }
                }
            }
            return finded;
        }

        private Models.ProcessTree FindInTree(int id)
        {
            Models.ProcessTree finded = null;
            foreach (Models.ProcessTree node in allProcesses)
            {
                if (node.ProcessId.Equals(id))
                {
                    finded = node;
                    break;
                }
                else if (node.ChildTree.Count != 0)
                {
                    finded = FindInNode(node, id);
                    if (finded != null)
                    {
                        break;
                    }
                }
            }
            return finded;
        }

        private void GetProcessesList()
        {
            var proc = NativeMethods.CreateToolhelp32Snapshot(SnapshotFlags.Process, 0);

            var entry = new ProcessEntry32();
            entry.dwSize = (uint)Marshal.SizeOf(entry);

            if (NativeMethods.Process32First(proc, ref entry))
            {
                do
                {
                    allProcesses.Add(new Models.ProcessTree(entry));
                } while (NativeMethods.Process32Next(proc, ref entry));
            }
            NativeMethods.CloseHandle(proc);
        }

        private void RemoveSelectedItem(Models.ProcessTree selectedProcess)
        {
            var deleted = false;
            foreach (Models.ProcessTree process in processTrees)
            {
                if (process == selectedProcess)
                {
                    processTrees.Remove(process);
                    deleted = true;
                    break;
                }
            }
            if (!deleted)
            {
                foreach (Models.ProcessTree process in processTrees)
                {
                    if (process.ChildTree.Count > 0)
                    {
                        FindInChild(selectedProcess, process.ChildTree);
                    }
                }
            }
        }
    }
}