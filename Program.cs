using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace node_install_windows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Downloading NodeJS...");
            Console.WriteLine();
            
            getNode("0.8.16");
        }
        
        private static void getNode(string version, string architecture="")
        {
            architecture = (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))) ? "64" : "86";

            WebClient client = new WebClient();

            string url = String.Format("http://nodejs.org/dist/v{0}/x{1}/node-v{0}-x{1}.msi", version, architecture);

            string savePath = String.Format("node-v{0}-x{1}.msi", version, architecture);

            try
            {
                client.DownloadFile(url, savePath);
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
        }
    }
}