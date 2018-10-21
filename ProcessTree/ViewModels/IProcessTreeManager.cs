using System.Collections.Generic;

namespace ProcessTree.ViewModels
{
    public interface IProcessTreeManager
    {
        void StartProcess(string name);

        IEnumerable<Models.ProcessTree> GetProcessTree();

        void CloseProcess(int Id);

        void RefreshTree();

        void CreateTree();
    }
}