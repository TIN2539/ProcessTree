using System.Collections.Generic;

namespace ProcessTree.ViewModels
{
    public interface IProcessTreeManager
    {
        bool AddChildProcessTree(uint parrentId, Models.ProcessTree processTree);

        void AddParrentId(uint parrentId);

        void AddProcessTree(Models.ProcessTree processTree);

        void CreateTree();

        IEnumerable<uint> GetAllParrentIds();

        IEnumerable<Models.ProcessTree> GetProcessTrees();

        void RefreshTrees();
    }
}