using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeDemo
{
    public class DirectoryItem : FileSystemItem
    {
        public DirectoryItem(string name) : base(name)
        {
        }

        public List<FileSystemItem> Items { get; } = new List<FileSystemItem>();

        public void Add(FileSystemItem component)
        {
            this.Items.Add(component);
        }

        public void Remove(FileSystemItem component)
        {
            this.Items.Remove(component);
        }

        public override decimal GetSizeInKB()
        {
            return this.Items.Sum(x => x.GetSizeInKB());
        }
    }
}
