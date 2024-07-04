using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackio.Model
{
    internal class ModelProjectTracker
    {
        //class properties
        public int iIdOfProject {  get; set; }
        public int iIdOfTest { get; set; }
        public int iIdOfRun { get; set; }
        public int iLastIdOfRun { get; set; }
        public string sNameOfTest { get; set; }
        public bool bResult { get; set; }
    }
}
