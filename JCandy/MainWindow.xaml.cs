using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JCandy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool generateEXE(string path)
        {
            bool result = false;

            using (CSharpCodeProvider code = new CSharpCodeProvider())
            {
                // create compiler parameters
                CompilerParameters compar = new CompilerParameters();

                //Set the icon
                string pathtoicon = "";
                if (!string.IsNullOrEmpty(iconBox.Text) && File.Exists(iconBox.Text))
                    pathtoicon = iconBox.Text;
                else
                    return false;

                compar.CompilerOptions = "/target:winexe" + " " + "/win32icon:" + "\"" + pathtoicon + "\"";
                compar.GenerateExecutable = true;
                compar.IncludeDebugInformation = false;

                if (!string.IsNullOrEmpty(jarBox.Text) && File.Exists(jarBox.Text))
                    compar.EmbeddedResources.Add(jarBox.Text);
                else
                    return false;

                //Add images, music and settings
                /*for (int i = 1; i < filelistbox.Items.Count + 1; i++)
                {
                    compar.EmbeddedResources.Add(Form1.temp + "\\" + i.ToString() + ".jpg");
                }*/

                /*if (this.backmusiccheck.Checked && File.Exists(musicpath.Text))
                {
                    compar.EmbeddedResources.Add(Form1.temp + "\\music.mp3");
                }*/

                //compar.EmbeddedResources.Add(Form1.temp + "\\settings.xml");

                compar.OutputAssembly = path;
                compar.GenerateInMemory = false;

                //Add references
                compar.ReferencedAssemblies.Add("System.dll");
                compar.ReferencedAssemblies.Add("System.Data.dll");
                compar.ReferencedAssemblies.Add("System.Deployment.dll");
                compar.ReferencedAssemblies.Add("System.Drawing.dll");
                compar.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                compar.ReferencedAssemblies.Add("System.Xml.dll");

                compar.TreatWarningsAsErrors = false;

                //Finally compile it
                CompilerResults res = code.CompileAssemblyFromSource(compar,Properties.Resources.Program);

                if (res.Errors.Count > 0)
                    result = false;
                else
                    result = true;
            }

            return result;
        }

    }
}
