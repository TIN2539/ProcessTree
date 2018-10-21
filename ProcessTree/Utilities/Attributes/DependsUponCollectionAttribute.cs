using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTree.Utilities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DependsUponCollectionAttribute : Attribute
    {
        private readonly string collectionName;

        public DependsUponCollectionAttribute(string collectionName)
        {
            this.collectionName = collectionName;
        }

        public string CollectionName => collectionName;
    }
}
