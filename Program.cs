﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace node_install_windows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Downloading NodeJS...");       
            
            /* Download NodeJS and get the file name */
            string fname = getNode();

            Console.WriteLine("Installing...");
            
            /* Invoke the installer and wait for it to finish */
            Process nodeInstallPr = new Process();

            nodeInstallPr.StartInfo.FileName = fname;
            nodeInstallPr.Start();
            nodeInstallPr.WaitForInputIdle();
            nodeInstallPr.WaitForExit();
            nodeInstallPr.Close();

            Console.WriteLine("Node installed!");
        }
        
        /* Downloads nodeJS returns the file name downloaded */
        private static string getNode(string version=null, string architecture=null)
        {
            if(architecture == null)
                architecture = (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))) ? "64" : "86";
            
            if (version == null)
                version = findLatestNodeVersion();

            WebClient client = new WebClient();

            string url = String.Format("http://nodejs.org/dist/v{0}/x{1}/node-v{0}-x{1}.msi", version, architecture);

            string savePath = String.Format("node-v{0}-x{1}.msi", version, architecture);

            try
            {
                client.DownloadFile(url, savePath);
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }

            return savePath;
        }

        /* Scrapes http://nodejs.org/dist/latest/ and identifies the version number of the latest stable release */
        private static string findLatestNodeVersion()
        {
            WebRequest req = HttpWebRequest.Create("http://nodejs.org/dist/latest/");
            req.Method = "GET";
            string src = "";

            StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream());
            src = reader.ReadToEnd();

            Regex exp = new Regex(@"node-v(?<version>\d+.\d+.\d+)");

            string result = exp.Match(src).Groups["version"].Value;
            
            return result;
        }
    }
}