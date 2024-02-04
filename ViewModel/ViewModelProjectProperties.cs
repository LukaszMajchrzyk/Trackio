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
        private ViewModelFileManager viewModelFileManager;

        public int iID 
        {
            get { return modelProjectProperties.iID; }
            set { modelProjectProperties.iID = value; }
        }

        public string sNameOfProject
        {
            get { return modelProjectProperties.sNameOfProject; }
            set { modelProjectProperties.sNameOfProject = value; }
        }

        public List<string> listOfProjectsStatus
        {
            get { return modelProjectProperties.listOfProjectsStatus; }
            set { modelProjectProperties.listOfProjectsStatus = value; }
        }

        public string sCurrentStatus
        {
            get { return modelProjectProperties.sCurrentStatus; }
            set { modelProjectProperties.sCurrentStatus = value; }
        }

        public DateTime dateCreationDate
        {
            get { return modelProjectProperties.dateCreationDate; }
            set { modelProjectProperties.dateCreationDate = value; }
        }

        public DateTime dateLastUppdated
        {
            get { return modelProjectProperties.dateLastUppdated; }
            set { modelProjectProperties.dateLastUppdated = value; }
        }

        public ViewModelProjectProperties()
        {
            //object of model
            modelProjectProperties = new ModelProjectProperties();
            //object for file management
            viewModelFileManager = new ViewModelFileManager();

            //initialize values
            modelProjectProperties.listOfProjectsStatus = ["Created", "In Work", "Done", "Failed", "Obsolete"];
            modelProjectProperties.dateCreationDate = DateTime.Now;

            //invoke methods to obtain data

        }



    }
}
