using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static System.Console;
using static System.Environment;
using static System.IO.Directory;

namespace DevOpsRenamer
{
    internal class Program
    {
        private const string SlnExtension = ".sln";
        private const string SlnSearchPattern = "*" + SlnExtension;
        private const string ProjectNamePattern = "HeidelbergCement.Platform.";
        public static void Main(string[] args)
        {
            WriteLine("DF Service Template - DevOps Renamer v1.0");
            WriteLine("Working in: " + CurrentDirectory);


            var serviceName = GetServiceNameBySlnFile();
            WriteLine("Service Name: " + serviceName);

            WriteLine("Processing valid port number...");
            var tcpPortHelper = new TcpPortHelper();
            var port = tcpPortHelper.GetFreeTcpPort();
            WriteLine("New Port number: " + port);

            WriteLine("Processing Dockerfile...");
            var dockerFileProcessor = new DockerFileProcessor();
            //dockerFileProcessor.ProcessByServiceNameAndPort(serviceName, port);
            WriteLine("Dockerfile Done.");

            var helm = new HelmDirectoryProcessor();
        }

        private static string GetServiceNameBySlnFile()
        {
            var slnFileName = GetFiles(CurrentDirectory, SlnSearchPattern).FirstOrDefault();

            if (slnFileName == null)
            {
                throw new Exception("Could not find a file with .sln extension.");
            }

            var nameContainsPattern = slnFileName.Contains(ProjectNamePattern);

            if (nameContainsPattern)
            {
                return slnFileName.GetStringBetween(ProjectNamePattern, SlnExtension);
            }

            BackgroundColor = ConsoleColor.DarkRed;
            WriteLine($"YOUR PROJECT DOES NOT FOLLOWS THE '{ProjectNamePattern}' PATTERN!" +
                      "PLEASE, CONSIDER RENAMING!");
            ResetColor();

            Thread.Sleep(1000);

            return default(string); //TODO
        }
    }
}
