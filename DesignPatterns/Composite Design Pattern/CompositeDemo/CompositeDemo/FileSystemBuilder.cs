using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeDemo
{
    public class FileSystemBuilder
    {
        private DirectoryItem currentDirectory;

        public FileSystemBuilder(string rootDirectory)
        {
            this.Root = new DirectoryItem(rootDirectory);
            this.currentDirectory = this.Root;
        }

        public DirectoryItem Root { get; }

        public DirectoryItem AddDirectory(string name)
        {
            var dir = new DirectoryItem(name);
            this.currentDirectory.Add(dir);
            this.currentDirectory = dir;
            return dir;
        }

        public FileItem AddFile(string name, long fileByes)
        {
            var file = new FileItem(name, fileByes);
            this.currentDirectory.Add(new FileItem(name, fileByes));
            return file;
        }

        public DirectoryItem SetCurrentDirectory(string directoryName)
        {
            var dirStack = new Stack<DirectoryItem>();
            dirStack.Push(this.Root);
            while (dirStack.Any())
            {
                var current = dirStack.Pop();
                if (current.Name == directoryName)
                {
                    this.currentDirectory = current;
                    return current;
                }
                foreach (var item in current.Items.OfType<DirectoryItem>())
                {
                    dirStack.Push(item);
                }
            }
            throw new InvalidOperationException($"Directory name '{directoryName}' not found!");
        }

    }
}
