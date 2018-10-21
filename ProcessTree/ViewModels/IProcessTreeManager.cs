using System.Collections.Generic;

namespace ProcessTree.ViewModels
{
    public interface IProcessTreeManager
    {
        void CloseProcess(int id);

        void CreateTree();

        IEnumerable<Models.ProcessTree> GetProcessTree();

        void RefreshTree();

        void StartProcess(string name);
    }
}