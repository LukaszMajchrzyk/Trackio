using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Trackio.Model;

namespace Trackio.ViewModel
{
    class ViewModelFileManager
    {
        //class fields
        private ModelFileManager modelFileManager;
        string sDirectoryLogFiles = Directory.GetCurrentDirectory() + "/LOG/";
        string sMainLogFile = Directory.GetCurrentDirectory() + "/LOG/" + "Trackio.PB";
        string isDictionaryOfIDsAndProjectNames;
        //class properties
        public int iLastID
        {
            get { return modelFileManager.iLastID; }
            set { modelFileManager.iLastID = value; }
        }

        public int iID
        {
            get { return modelFileManager.iID; }
            set { modelFileManager.iID = value; }
        }
        public List<int> iListOfIDs
        {
            get { return modelFileManager.iListOfIDs; }
            set { modelFileManager.iListOfIDs = value; }
        }

        public List<string> sListOfFilesWithExtension
        {
            get { return modelFileManager.sListOfFilesWithExtension; }
            set { modelFileManager.sListOfFilesWithExtension = value; }
        }

        public List<string> sListOfProjectNames
        {
            get { return modelFileManager.sListOfProjectNames; }
            set { modelFileManager.sListOfProjectNames = value; }
        }


        public ViewModelFileManager()
        {
            //creating object of model
            modelFileManager = new ModelFileManager();
        }


        //methods section
        public void createLogFolder()
        {
            //create LOG folder if does not exist
            bool bLogFolderExists = System.IO.Directory.Exists(sDirectoryLogFiles);
            if (!bLogFolderExists) System.IO.Directory.CreateDirectory(sDirectoryLogFiles);
        }

        public void getFileListFromLogFiles()
        {
            // get Project Files from Directory (seatch for *.log extension); list of IDs is list of files without extension
            sListOfFilesWithExtension = System.IO.Directory.GetFiles(sDirectoryLogFiles, "*.log").ToList();
            if (sListOfFilesWithExtension.Count > 0)
            {
                iListOfIDs = sListOfFilesWithExtension.Select(System.IO.Path.GetFileNameWithoutExtension).Select(int.Parse).ToList();
                iListOfIDs.Sort();
            }
        }

        public void getLastID()
        {
            //lastId may not be last item in list (in case of non sequnce list with missing ids somehow)
            //so we will always start checking from 1 and keep checking for gap in sequence or add +1 to last item if no gaps are present
            iLastID = 1;
            if (iListOfIDs != null)
            {
                for (int i = 0; i < iListOfIDs.Count; i++)
                {
                    if (iListOfIDs[i] != iLastID)
                    {
                        iListOfIDs.Add(iLastID);
                        break;
                    }
                    else iLastID++;
                }
            }
            else iListOfIDs = new List<int> { iLastID};
        }

        public void readProjectLogFile()
        {
        }

        public void readMainLogFile()
        {
            //create main Log file if it does not exist

            bool bMainLogFileExists = System.IO.File.Exists(sMainLogFile);
            if (!bMainLogFileExists) System.IO.File.Create(sMainLogFile);

            if (File.Exists(sMainLogFile))
            {
                // Store each line in array of strings 
                string[] sLines = File.ReadAllLines(sMainLogFile);


            }
        }


    }
}
