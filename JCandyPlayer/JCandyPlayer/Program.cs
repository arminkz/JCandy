using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCandyPlayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            String javaParam = ""; //TODO : Read from Setting.xml
            String jarName = "PacMan.jar";
            String tempJar = Path.Combine(Path.GetTempPath(), jarName);
            MessageBox.Show(tempJar);

            String jrePath;
            String javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment";

            //Write Jar to Temp Location
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(jarName))
            {
                using (var file = new FileStream(tempJar, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
            
            //Get Java Home Path
            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(javaKey))
            {
                String currentVersion = baseKey.GetValue("CurrentVersion").ToString();
                using (var homeKey = baseKey.OpenSubKey(currentVersion))
                    jrePath = homeKey.GetValue("JavaHome").ToString();
            }

            MessageBox.Show("JRE Path : " + jrePath);

            var processInfo = new ProcessStartInfo(jrePath, "-jar " + jarName)
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };


            //processInfo.WorkingDirectory = myPath + "\\bin\\";
            Process proc;
            if ((proc = Process.Start(processInfo)) == null)
            {
                throw new InvalidOperationException("");
            }
            proc.PriorityClass = ProcessPriorityClass.High;
            //proc.PriorityBoostEnabled = true;
            proc.WaitForExit();
            int exitCode = proc.ExitCode;
            MessageBox.Show("Program Exited with Code : " + exitCode);
            proc.Close();
        }
    }
}
