using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackio.Model;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Trackio.ViewModel
{
    internal class ViewModelProjectTracker
    {
        string sProjectLogFile;
        bool bProjectLogFileExists;
        int iIDofMainProject;
        string sDirectoryLogFiles = System.IO.Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Trackio/LOG/";
        ObservableCollection<ViewModelProjectTracker> observableCollectionViewModelProjectTracker;
        SortedSet<int> sortedSetOfRunIds;
        Dictionary<int, int> dictionaryOfTestIdsAndRuns;
        private ModelProjectTracker modelProjectTracker;
        private ViewModelProjectTestsDescribed viewModelProjectTestsDescribed;
        public int iIdOfProject
        {
            get { return modelProjectTracker.iIdOfProject; }
            set { modelProjectTracker.iIdOfProject = value; }
        }
        public int iIdOfTest
        {
            get { return modelProjectTracker.iIdOfTest; }
            set { modelProjectTracker.iIdOfTest = value; }
        }
        public int iIdOfRun
        {
            get { return modelProjectTracker.iIdOfRun; }
            set { modelProjectTracker.iIdOfRun = value; }
        }
        public int iLastIdOfRun
        {
            get { return modelProjectTracker.iLastIdOfRun; }
            set { modelProjectTracker.iLastIdOfRun = value; }
        }
        public string sNameOfTest
        {
            get { return modelProjectTracker.sNameOfTest; }
            set { modelProjectTracker.sNameOfTest = value; }
        }
        public bool bResult
        {
            get { return modelProjectTracker.bResult; }
            set { modelProjectTracker.bResult = value; }
        }

        public ViewModelProjectTracker(int iIdOfProject, int iIdOfRun, int iIdOfTest, string sNameOfTest, bool bResult)
        {
            modelProjectTracker = new ModelProjectTracker();
            modelProjectTracker.iIdOfProject = iIdOfProject;
            modelProjectTracker.iIdOfRun = iIdOfRun;
            modelProjectTracker.iIdOfTest = iIdOfTest;
            modelProjectTracker.sNameOfTest = sNameOfTest;
            modelProjectTracker.bResult = bResult;
        }

        public ViewModelProjectTracker(int iIDofMainProject)
        {
            modelProjectTracker = new ModelProjectTracker();
            this.iIDofMainProject = iIDofMainProject;
        }

        public ObservableCollection<ViewModelProjectTracker> readProjectLogFile()
        {
            projectLogFileExists();
            if (bProjectLogFileExists)
            {
                sortedSetOfRunIds = new SortedSet<int>();
                dictionaryOfTestIdsAndRuns = new Dictionary<int, int>();
                string[] arrayOfLinesTestTracker = File.ReadAllLines(sProjectLogFile);
                observableCollectionViewModelProjectTracker = new ObservableCollection<ViewModelProjectTracker>();
                for (int i = 0; i < arrayOfLinesTestTracker.Length; i++)
                {
                    //checking for 2 "_" characters; this is how we identify tracker and not a project log (single "_")
                    if (arrayOfLinesTestTracker[i].IndexOf("_") != arrayOfLinesTestTracker[i].LastIndexOf("_"))
                    {
                        iIdOfRun = Int32.Parse(arrayOfLinesTestTracker[i].Substring(9, arrayOfLinesTestTracker[i].Length - 12));
                        iIdOfTest = Int32.Parse(arrayOfLinesTestTracker[i].Substring(11, arrayOfLinesTestTracker[i].Length - 12));
                        sNameOfTest = arrayOfLinesTestTracker[i + 1].Substring(arrayOfLinesTestTracker[i + 1].LastIndexOf(':') + 1);
                        if (bool.Parse(arrayOfLinesTestTracker[i + 2].Substring(arrayOfLinesTestTracker[i + 2].LastIndexOf(':') + 1))) bResult = true;
                        else bResult = false;
                        sortedSetOfRunIds.Add(iIdOfRun);
                        observableCollectionViewModelProjectTracker.Add(new ViewModelProjectTracker(iIdOfProject, iIdOfRun, iIdOfTest, sNameOfTest, bResult));
                        //dictionary needed for main section of LOG update
                        dictionaryOfTestIdsAndRuns.Add(iIdOfTest, iIdOfRun);
                    }
                }
                //finding last Id of Run to set next non occupied ID;
                for (int i = 1; i <= sortedSetOfRunIds.Count + 1; i++)
                {
                    if (sortedSetOfRunIds.Contains(i)) iLastIdOfRun = i + 1;
                    else iLastIdOfRun = i;
                }

            }
            return observableCollectionViewModelProjectTracker;
        }

        public void saveTrackerToLog(ObservableCollection<ViewModelProjectTestsDescribed> observableCollectionviewModelProjectTestDecribed, Dictionary<int, bool> dictionaryOfIdsAndTestResult)
        {
            projectLogFileExists();
            if (bProjectLogFileExists)
            {
                string[] arrayOfCurrentFile = File.ReadAllLines(sProjectLogFile);
                List <string> listOfLinestoBeSaved = new List <string>();
                listOfLinestoBeSaved = arrayOfCurrentFile.ToList();
                for (int i = 0; i < dictionaryOfIdsAndTestResult.Count; i++) 
                {
                    string[] arrayOfLinesToBeAdded = new string[4];
                    arrayOfLinesToBeAdded[0] = "";
                    arrayOfLinesToBeAdded[1] = $"[Tracker_{9}_{dictionaryOfIdsAndTestResult.ElementAt(i).Key}]";
                    arrayOfLinesToBeAdded[2] = $"Name: {observableCollectionviewModelProjectTestDecribed[i].sNameOfTest}";
                    arrayOfLinesToBeAdded[3] = $"Result:{dictionaryOfIdsAndTestResult.ElementAt(i).Value}";
                    listOfLinestoBeSaved.AddRange(arrayOfLinesToBeAdded.ToList());
                }
                File.WriteAllLines(sProjectLogFile, listOfLinestoBeSaved.ToArray());
            }
        }


        public void projectLogFileExists()
        {
            //checking if LOG file for tests described exists
            sProjectLogFile = sDirectoryLogFiles + $"Trackio_Project_No_{iIDofMainProject}.LOG";
            bProjectLogFileExists = System.IO.File.Exists(sProjectLogFile);
        }
    }
}
