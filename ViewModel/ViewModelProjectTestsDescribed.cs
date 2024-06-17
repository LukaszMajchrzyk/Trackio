using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        Dictionary<int, int> dictionaryOfExistingTestIDs;
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

        public List<string> listOfStatuses
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

        public void createTestsDescribedLogFile()
        {
            projectDescribedLogFileExists();
            //create a file only if it does not exist
            if (!bProjectDescribedLogFileExists)
            {
                var vProjectFile = System.IO.File.Create(sProjectDescribedLogFile);
                vProjectFile.Close();
            }
        }

        public void readTestDescribedLogFile()
        {
            dictionaryOfExistingTestIDs = new Dictionary<int, int>();
            int iLineCounter = 0;
            //reading LOG file for tests described exists
            string[] arrayOfLinesTestDecribed = File.ReadAllLines(sProjectDescribedLogFile);
            foreach (string s in arrayOfLinesTestDecribed)
            {
                iLineCounter += 1;
                //dictionary will keep test ID and number of line where section for this test begins

                if (s.Contains("[Test_"))
                {
                    var a = s.Substring(6, 1);
                    dictionaryOfExistingTestIDs.Add(Int32.Parse(s.Substring(6, s.Length -7 )), iLineCounter);
                }
            }





        }

        public void saveTestDescribedLogFile()
        {
            projectDescribedLogFileExists();
            File.SetAttributes(sProjectDescribedLogFile, FileAttributes.Normal);
            //creating whole section to write to file
            string[] sArrayOfStringsToWrite = new string[4];
            sArrayOfStringsToWrite[0] = $"[Test_{iID}]";
            sArrayOfStringsToWrite[1] = $"Name : {sNameOfTest}";
            sArrayOfStringsToWrite[2] = $"Runs' Count : {iRunsCounter}";
            sArrayOfStringsToWrite[3] = $"Current Status : {sCurrentStatus}";



            //checking if test already exists in LOF file; if it does update section will be called 
            //update
            //File.WriteAllLines(sProjectDescribedLogFile, sArrayOfStringsToWrite);

            //create
            File.AppendAllLines(sProjectDescribedLogFile, sArrayOfStringsToWrite);

        }


        public void projectDescribedLogFileExists()
        {
            //checking if LOG file for tests described exists
            sProjectDescribedLogFile = sDirectoryLogFiles + $"Trackio_Project_No_{iIDofMainProject}.LOG";
            bProjectDescribedLogFileExists = System.IO.File.Exists(sProjectDescribedLogFile);
        }
    }
}
