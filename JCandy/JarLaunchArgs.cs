using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCandy
{
    [Serializable]
    class JarLaunchArgs
    {
        String jarName;
        String javaParams;

        public String JarName
        {
            get { return jarName; }
            set { jarName = value; }
        }

        public String JavaParams
        {
            get { return javaParams; }
            set { javaParams = value; }
        }

    }
}