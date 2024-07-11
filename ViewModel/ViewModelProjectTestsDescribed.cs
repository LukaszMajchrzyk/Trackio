using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<ViewModelProjectTestsDescribed> observableCollectionOfViewModelProjectTestDescribed;
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

        public string sComment
        {
            get { return modelProjectTestsDescribed.sComment; }
            set { modelProjectTestsDescribed.sComment = value; }

        }


        public ViewModelProjectTestsDescribed(int iIDofMainProject)
        {
            this.iIDofMainProject = iIDofMainProject;
            modelProjectTestsDescribed = new ModelProjectTestsDescribed();
            modelProjectTestsDescribed.listOfStatuses = ["Created", "In Work", "Done", "Failed", "Obsolete"];
        }




        public ViewModelProjectTestsDescribed(int iID, string sNameOfTest, int iRunsCounter, string sCurrentStatus, string sComment)
        {
            modelProjectTestsDescribed = new ModelProjectTestsDescribed();
            modelProjectTestsDescribed.iID = iID;
            modelProjectTestsDescribed.sNameOfTest = sNameOfTest;
            modelProjectTestsDescribed.iRunsCounter = iRunsCounter;
            modelProjectTestsDescribed.sCurrentStatus = sCurrentStatus;
            modelProjectTestsDescribed.sComment= sComment;
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

        public ObservableCollection<ViewModelProjectTestsDescribed> readTestDescribedLogFile()
        {
            projectDescribedLogFileExists();
            if (bProjectDescribedLogFileExists)
            {
                dictionaryOfExistingTestIDs = new Dictionary<int, int>();
                int iLineCounter = 0;
                //reading LOG file for tests described exists
                string[] arrayOfLinesTestDecribed = File.ReadAllLines(sProjectDescribedLogFile);
                observableCollectionOfViewModelProjectTestDescribed = new ObservableCollection<ViewModelProjectTestsDescribed>();
                for (int i = 0; i < arrayOfLinesTestDecribed.Length; i++)
                {
                    if (arrayOfLinesTestDecribed[i].Contains("[Test_"))
                    {
                        iID = Int32.Parse(arrayOfLinesTestDecribed[i].Substring(6, arrayOfLinesTestDecribed[i].Length - 7));
                        sNameOfTest = arrayOfLinesTestDecribed[i + 1].Substring(arrayOfLinesTestDecribed[i + 1].LastIndexOf(':') + 1);
                        iRunsCounter = Int32.Parse(arrayOfLinesTestDecribed[i + 2].Substring(arrayOfLinesTestDecribed[i + 2].LastIndexOf(':') + 1));
                        sCurrentStatus = arrayOfLinesTestDecribed[i + 3].Substring(arrayOfLinesTestDecribed[i + 3].LastIndexOf(':') + 1);
                        sComment = arrayOfLinesTestDecribed[i + 4].Substring(arrayOfLinesTestDecribed[i + 4].LastIndexOf(':') + 1);
                        //dictionary will keep test ID and number of line where section for this test begins
                        dictionaryOfExistingTestIDs.Add(iID, iLineCounter);
                        observableCollectionOfViewModelProjectTestDescribed.Add(new ViewModelProjectTestsDescribed(iID, sNameOfTest, iRunsCounter, sCurrentStatus, sComment) );
                    }
                    iLineCounter += 1;
                }
                return observableCollectionOfViewModelProjectTestDescribed;
            }
            return new ObservableCollection<ViewModelProjectTestsDescribed>();
        }

        public void saveTestDescribedLogFile()
        {
            File.SetAttributes(sProjectDescribedLogFile, FileAttributes.Normal);
            int iLineNumberToBeUpdated = 0;
            string[] sArrayOfStringsToWrite = new string[5];
            //checking if test already exists in LOG file; if it does update section will be called 
            if (dictionaryOfExistingTestIDs.ContainsKey(iID))
            {
                //read LOG file again for update purpose
                string[] arrayOfLinesTestDecribed = File.ReadAllLines(sProjectDescribedLogFile);
                //get value from Dictionary by Key; it's our line's number which we want to update
                iLineNumberToBeUpdated = dictionaryOfExistingTestIDs[iID];
                arrayOfLinesTestDecribed[iLineNumberToBeUpdated] = $"[Test_{iID}]";
                arrayOfLinesTestDecribed[iLineNumberToBeUpdated + 1] = $"Name :{sNameOfTest}";
                arrayOfLinesTestDecribed[iLineNumberToBeUpdated + 2] = $"Runs' Count :{iRunsCounter}";
                arrayOfLinesTestDecribed[iLineNumberToBeUpdated + 3] = $"Current Status :{sCurrentStatus}";
                arrayOfLinesTestDecribed[iLineNumberToBeUpdated + 4] = $"Comments :{sComment}";
                File.WriteAllLines(sProjectDescribedLogFile, arrayOfLinesTestDecribed);
            }
            else
            {
                //creating whole section to write to file
                sArrayOfStringsToWrite[0] = $"[Test_{iID}]";
                sArrayOfStringsToWrite[1] = $"Name :{sNameOfTest}";
                sArrayOfStringsToWrite[2] = $"Runs' Count :{iRunsCounter}";
                sArrayOfStringsToWrite[3] = $"Current Status :{sCurrentStatus}";
                sArrayOfStringsToWrite[4] = $"Comments :{sComment}";
                File.AppendAllLines(sProjectDescribedLogFile, sArrayOfStringsToWrite);
            }
            //call read function to update object's fields (most important is dictionary) 
            readTestDescribedLogFile();
        }

        public void projectDescribedLogFileExists()
        {
            //checking if LOG file for tests described exists
            sProjectDescribedLogFile = sDirectoryLogFiles + $"Trackio_Project_No_{iIDofMainProject}.LOG";
            bProjectDescribedLogFileExists = System.IO.File.Exists(sProjectDescribedLogFile);
        }
    }
}
