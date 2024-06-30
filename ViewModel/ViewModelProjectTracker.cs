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
        private ModelProjectTracker modelProjectTracker;
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

        public ViewModelProjectTracker(int iIdOfProject, int iIdOfTest, string sNameOfTest, bool bResult)
        {
            modelProjectTracker = new ModelProjectTracker();
            modelProjectTracker.iIdOfProject = iIdOfProject;
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
                string[] arrayOfLinesTestTracker = File.ReadAllLines(sProjectLogFile);
                observableCollectionViewModelProjectTracker = new ObservableCollection<ViewModelProjectTracker>();
                for (int i = 0; i < arrayOfLinesTestTracker.Length; i++)
                {
                    //checking for 2 "_" characters; this is how we identify tracker and not a project log (single "_")
                    if (arrayOfLinesTestTracker[i].IndexOf("_") != arrayOfLinesTestTracker[i].LastIndexOf("_"))
                    {
                        iIdOfProject = Int32.Parse(arrayOfLinesTestTracker[i].Substring(9, arrayOfLinesTestTracker[i].Length - 12));
                        iIdOfTest = Int32.Parse(arrayOfLinesTestTracker[i].Substring(11, arrayOfLinesTestTracker[i].Length - 12));
                        sNameOfTest = arrayOfLinesTestTracker[i + 3].Substring(arrayOfLinesTestTracker[i + 3].LastIndexOf(':') + 1);
                        if (arrayOfLinesTestTracker[i + 4].Substring(arrayOfLinesTestTracker[i + 4].LastIndexOf(':') + 1) == "True") bResult = true;
                        else bResult = false;
                        observableCollectionViewModelProjectTracker.Add(new ViewModelProjectTracker(iIdOfProject, iIdOfTest, sNameOfTest, bResult));
                    }
                }
            }
            return observableCollectionViewModelProjectTracker;
        }


        public void projectLogFileExists()
        {
            //checking if LOG file for tests described exists
            sProjectLogFile = sDirectoryLogFiles + $"Trackio_Project_No_{iIDofMainProject}.LOG";
            bProjectLogFileExists = System.IO.File.Exists(sProjectLogFile);
        }

    }

}
