using System;
using System.Linq;

namespace DevOpsRenamer
{
    public class TcpPortHelper
    {
        private const int MinPortNumber = 5000;
        private const int MaxPortNumber = 7000;

        public string GetFreeTcpPort()
        {
            var random = new Random();
            var newPort =  random.Next(MinPortNumber, MaxPortNumber).ToString();

            var existingPorts = new string[]{ "5175", "5005", "6522" };

            while (existingPorts.Contains(newPort))
            {
                newPort = random.Next(MinPortNumber, MaxPortNumber).ToString();
            }

            return newPort;
        }
    }
}