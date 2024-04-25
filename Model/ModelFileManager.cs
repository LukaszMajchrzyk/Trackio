using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Trackio.Model
{
    internal class ModelFileManager
    {
        //class properties
        public int iID { get; set; }
        public int iLastID { get; set; }
        public List<int> iListOfTestsPerformed { get; set; }
        public List<string> sListOfFilesWithExtension { get; set; }
        public List<string> sListOfProjectNames { get; set; }
        public Dictionary<int,string> dictionaryIDsAndProjectNames { get; set; }


    }
}
