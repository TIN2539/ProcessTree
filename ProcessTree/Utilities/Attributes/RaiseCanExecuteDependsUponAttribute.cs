using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTree.Utilities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RaiseCanExecuteDependsUponAttribute : Attribute
    {
        private readonly string propertyName;

        public RaiseCanExecuteDependsUponAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string PropertyName => propertyName;
    }
}
