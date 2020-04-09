using System;
using System.IO;
using System.Linq;
using static System.Environment;

namespace DevOpsRenamer
{
    public class HelmDirectoryProcessor
    {
        private readonly string _helmDirectoryName;

        public HelmDirectoryProcessor()
        {
            var helmDirectory = Directory.GetDirectories(CurrentDirectory, "helm").FirstOrDefault();

            _helmDirectoryName = helmDirectory ?? throw new Exception();
        }
    }
}