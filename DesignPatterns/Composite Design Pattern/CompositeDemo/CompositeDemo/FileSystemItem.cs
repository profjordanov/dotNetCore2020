using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeDemo
{
    public abstract class FileSystemItem
    {
        public FileSystemItem(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public abstract decimal GetSizeInKB();

        
    }
}
