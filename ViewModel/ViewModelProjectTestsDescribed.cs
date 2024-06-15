using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackio.Model;

namespace Trackio.ViewModel
{
    class ViewModelProjectTestsDescribed
    {
        //class fields
        string sDirectoryLogFiles = System.IO.Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Trackio/LOG/";
        string sProjectDescribedLogFile;
        bool bProjectDescribedLogFileExists;
        int iIDofMainProject;


        private ModelProjectTestsDescribed modelProjectTestsDescribed;
        public bool bTestSelected
        {
            get { return modelProjectTestsDescribed.bTestSelected; }
            set { modelProjectTestsDescribed.bTestSelected = value; }
        }
        public int iID
        {
            get { return modelProjectTestsDescribed.iID; }
            set { modelProjectTestsDescribed.iID = value; }
        }
        public int iRunsCounter
        {
            get { return modelProjectTestsDescribed.iRunsCounter; }
            set { modelProjectTestsDescribed.iRunsCounter = value; }
        }
        public string sNameOfTest
        {
            get { return modelProjectTestsDescribed.sNameOfTest; }
            set { modelProjectTestsDescribed.sNameOfTest = value; }
        }

        public List <string> listOfStatuses
        {
            get { return modelProjectTestsDescribed.listOfStatuses; }
            set { modelProjectTestsDescribed.listOfStatuses = value; }
        }

        public string sCurrentStatus
        {
            get { return modelProjectTestsDescribed.sCurrentStatus; }
            set { modelProjectTestsDescribed.sCurrentStatus = value; }
        }

        public ViewModelProjectTestsDescribed(int iIDofMainProject)
        {
            this.iIDofMainProject = iIDofMainProject;
            modelProjectTestsDescribed = new ModelProjectTestsDescribed();
            modelProjectTestsDescribed.listOfStatuses = ["Created", "In Work", "Done", "Failed", "Obsolete"];

        }
        public ViewModelProjectTestsDescribed()
        {
        }

        public void createTestsDescribedLogFile ()
        {
            projectDescribedLogFileExists();
            //create a file only if it does not exist
            if (!bProjectDescribedLogFileExists)
            {
                var vProjectFile = System.IO.File.Create(sProjectDescribedLogFile);
                vProjectFile.Close();
            }
        }

        public void readTestDescribedLogFile ()
        {
            //reading LOG file for tests described exists
            string[] arrayOfLinesTestDecribed = File.ReadAllLines(sProjectDescribedLogFile);

        }

        public void saveTestDescribedLogFile(int iIDofMainProject)
        {
            projectDescribedLogFileExists();
            File.SetAttributes(sProjectDescribedLogFile, FileAttributes.Normal);
            //creating whole section to write to file
            string[] sArrayOfStringsToWrite = new string[4];
            //sArrayOfStringsToWrite[0] = "[Test_" + viewmodelProjectProperties.iID + "]";











        }


        public void projectDescribedLogFileExists()
        {
            //checking if LOG file for tests described exists
            sProjectDescribedLogFile = sDirectoryLogFiles + $"Trackio_Project_No_{iIDofMainProject}.LOG";
            bProjectDescribedLogFileExists =  System.IO.File.Exists(sProjectDescribedLogFile);
        }
    }
}
