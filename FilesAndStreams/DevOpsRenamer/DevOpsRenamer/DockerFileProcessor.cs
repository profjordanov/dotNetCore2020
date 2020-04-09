using System;
using System.IO;
using System.Linq;
using System.Text;
using static System.Environment;
using static System.IO.Directory;

namespace DevOpsRenamer
{
    public class DockerFileProcessor
    {
        private const string ApiNamePlaceholder = "$$API_NAME$$";
        private const string PortPlaceholder = "$$PORT$$";
        private readonly string _dockerFilePath;

        public DockerFileProcessor()
        {
            var dockerfile = GetFiles(CurrentDirectory, "Dockerfile").FirstOrDefault();

            _dockerFilePath = dockerfile ?? throw new Exception("Could not find a Dockerfile.");
        }

        public void ProcessByServiceNameAndPort(string serviceName, string port)
        {
            var contents = File.ReadAllText(_dockerFilePath);

            contents = contents.Replace(PortPlaceholder, port);
            contents = contents.Replace(ApiNamePlaceholder, serviceName);

            File.WriteAllText(_dockerFilePath, contents, Encoding.UTF8);
        }
    }
}