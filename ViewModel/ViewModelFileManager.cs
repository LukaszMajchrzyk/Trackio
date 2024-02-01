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
        public List<int> listOfIDs
        {
            get { return modelFileManager.listOfIDs; }
            set { modelFileManager.listOfIDs = value; }
        }

        public List<string> listOfFilesWithExtension
        {
            get { return modelFileManager.listOfFilesWithExtension; }
            set { modelFileManager.listOfFilesWithExtension = value; }
        }


        public ViewModelFileManager()
        {
            //creating object of model
            modelFileManager = new ModelFileManager();
            //main logic from methods
            createLogFolder();
            getFileListFromLogFiles();
            getLastID();

        }


        //methods section
        void createLogFolder()
        {
            //create LOG folder if does not exist
            bool bLogFileExists = System.IO.Directory.Exists(sDirectoryLogFiles);
            if (!bLogFileExists) System.IO.Directory.CreateDirectory(sDirectoryLogFiles);
        }

        void getFileListFromLogFiles()
        {
            // get Project Files from Directory (seatch for *.log extension); list of IDs is list of files without extension
            listOfFilesWithExtension = System.IO.Directory.GetFiles(sDirectoryLogFiles, "*.log").ToList();
            if (listOfFilesWithExtension.Count > 0)
            {
                listOfIDs = listOfFilesWithExtension.Select(System.IO.Path.GetFileNameWithoutExtension).Select(int.Parse).ToList();
                listOfIDs.Sort();
            }
        }

        void getLastID()
        {
            //lastId may not be last item in list (in case of non sequnce list with missing ids somehow)
            //so we will always start checking from 1 and keep checking for gap in sequence or add +1 to last item if no gaps are present
            iLastID = 1;
            if (listOfIDs != null)
            {
                for (int i = 0; i < listOfIDs.Count; i++)
                {
                    if (listOfIDs[i] != iLastID)
                    {
                        listOfIDs.Add(iLastID);
                        break;
                    }
                    else iLastID++;
                }
            }
            else listOfIDs = new List<int> { iLastID };
        }

        void readProjectFile()
        {

        }
    }
}
