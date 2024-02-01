using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackio.Model;

namespace Trackio.ViewModel
{
    class ViewModelProjectProperties
    {
        private ModelProjectProperties modelProjectProperties;

        public List<string> listOfProjectsStatus
        {
            get { return modelProjectProperties.listOfProjectsStatus; }
            set { modelProjectProperties.listOfProjectsStatus = value; }
        }

        public DateTime dateCurretDateAndTime
        {
            get { return modelProjectProperties.dateCurretDateAndTime; }
            set { modelProjectProperties.dateCurretDateAndTime = value; }
        }

        public ViewModelProjectProperties()
        {
            modelProjectProperties = new ModelProjectProperties();
            modelProjectProperties.listOfProjectsStatus = ["Created", "In Work", "Done", "Failed", "Obsolete"];
            modelProjectProperties.dateCurretDateAndTime = DateTime.Now;
        }



    }
}
