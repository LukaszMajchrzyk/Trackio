using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Trackio.Model
{
    class ModelProjectProperties
    {
        //class properties
        public int iID { get; set; }
        public string sNameOfProject { get; set; }
        public string sCurrentStatus { get; set; }
        public List<string> listOfProjectsStatus { get; set; }
        public DateTime dateCurretDateAndTime{ get; set; }
        public DateTime dateLastUppdated{ get; set; }

    }
}
