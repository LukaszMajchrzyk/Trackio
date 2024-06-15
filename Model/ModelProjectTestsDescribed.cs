using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackio.Model
{
    class ModelProjectTestsDescribed
    {
        //class properties
        public bool bTestSelected {  get; set; }
        public int iID {  get; set; }
        public string sNameOfTest { get; set; }
        public int iRunsCounter { get; set; }
        public List<string> listOfStatuses { get; set; }
        public string sCurrentStatus {  get; set; }

    }
}
