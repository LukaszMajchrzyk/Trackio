using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;
using Trackio.Model;
using Trackio.View;

namespace Trackio.ViewModel
{
    class ViewModelFileManager
    {
        //class fields
        private ModelProjectProperties modelProjectProperties;
        private ModelFileManager modelFileManager;
        string sDirectoryLogFiles = System.IO.Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Trackio/LOG/";
        string sMainLogFile = System.IO.Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Trackio/LOG/" + "Trackio.PB";
        bool bProjectAlreadyExistsinMainLogFile = false;
        int iLineNumberOfUpdatedProject = 0;
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

        public List<string> sListOfProjectNames
        {
            get { return modelFileManager.sListOfProjectNames; }
            set { modelFileManager.sListOfProjectNames = value; }
        }

        public Dictionary<int, string> dictionaryIDsAndProjectNames
        {
            get { return modelFileManager.dictionaryIDsAndProjectNames; }
            set { modelFileManager.dictionaryIDsAndProjectNames = value; }
        }


        public ViewModelFileManager()
        {
            //creating objects
            modelFileManager = new ModelFileManager();
            modelProjectProperties = new ModelProjectProperties();
        }


        //methods section
        public void createLogFolder()
        {
            //create LOG folder if does not exist
            bool bLogFolderExists = System.IO.Directory.Exists(sDirectoryLogFiles);
            if (!bLogFolderExists) System.IO.Directory.CreateDirectory(sDirectoryLogFiles);
        }

        public ModelProjectProperties getProjectProfpertiesFromFileByID(int iID)
        {
            if (File.Exists(sMainLogFile))
            {
                //reading main LOG file
                string[] arrayOfLines = File.ReadAllLines(sMainLogFile);
                for (int i = 0; i < arrayOfLines.Length; i++)
                {
                    if (arrayOfLines[i].Contains("Trackio_" + iID))
                    {
                        iLineNumberOfUpdatedProject = i;
                        bProjectAlreadyExistsinMainLogFile = true;
                        modelProjectProperties.iID = iID;
                        modelProjectProperties.sNameOfProject = arrayOfLines[i + 1].Substring(arrayOfLines[i + 1].LastIndexOf(':') + 1);
                        modelProjectProperties.dateCreationDate = DateTime.Parse(arrayOfLines[i + 2].Split(new[] { ':' }, 2)[1]);
                        modelProjectProperties.dateLastUppdated = DateTime.Parse(arrayOfLines[i + 3].Split(new[] { ':' }, 2)[1]);
                        modelProjectProperties.sCurrentStatus = arrayOfLines[i + 4].Substring(arrayOfLines[i + 4].LastIndexOf(':') + 1);
                    }
                }
            }
            return modelProjectProperties;
        }

        public void getLastID()
        {
            //lastId may not be last item in list (in case of non sequnce list with missing ids somehow)
            //so we will always start checking from 1 and keep checking for gap in sequence or add +1 to last item if no gaps are present
            iLastID = 1;
            if (dictionaryIDsAndProjectNames != null)
            {
                for (int i = 0; i < dictionaryIDsAndProjectNames.Count; i++)
                {
                    if (dictionaryIDsAndProjectNames.ElementAt(i).Key != iLastID)
                    {
                        break;
                    }
                    else iLastID++;
                }
            }
        }

        public void readMainLogFile()
        {
            mainLogFileExists();
            //main logic
            dictionaryIDsAndProjectNames = new Dictionary<int, string>();
            if (File.Exists(sMainLogFile))
            {
                //reading main LOG file
                string[] arrayOfLines = File.ReadAllLines(sMainLogFile);

                for (int i = 0; i < arrayOfLines.Length; i++)
                {
                    //search for ID (key)[i] and name (value)[i+1] which is next line in parsed file; get only value without ":" by substring
                    if (arrayOfLines[i].Contains("Trackio"))
                    {
                        int iIDFromLOG = Int32.Parse(arrayOfLines[i].Substring(9, arrayOfLines[i].Length - 10));
                        string sNameFromLog = arrayOfLines[i + 1];
                        sNameFromLog = sNameFromLog.Substring(arrayOfLines[i + 1].LastIndexOf(':') + 1);
                        sNameFromLog = String.Concat(sNameFromLog.Where(c => !Char.IsWhiteSpace(c)));
                        dictionaryIDsAndProjectNames.Add(iIDFromLOG, sNameFromLog);
                    }
                }
            }
        }

        public void saveToMainLogFile(ViewModelProjectProperties viewmodelProjectProperties)
        {
            mainLogFileExists();
            File.SetAttributes(sMainLogFile, FileAttributes.Normal);
            //creating whole section to write to file
            string[] sArrayOfStringsToWrite = new string[5];
            sArrayOfStringsToWrite[0] = "[Trackio_" + viewmodelProjectProperties.iID + "]";
            sArrayOfStringsToWrite[1] = "Name : " + viewmodelProjectProperties.sNameOfProject;
            sArrayOfStringsToWrite[2] = "Creation Date : " + viewmodelProjectProperties.dateCreationDate;
            sArrayOfStringsToWrite[3] = "Last Update : " + viewmodelProjectProperties.dateLastUppdated;
            sArrayOfStringsToWrite[4] = "Current Status : " + viewmodelProjectProperties.sCurrentStatus;
            //write data to file
            //check if ID of project already exists; if it does call the update method; else write section from scratch
            if (bProjectAlreadyExistsinMainLogFile)
            {
                string[] arrayOfUpdatedLines = File.ReadAllLines(sMainLogFile);
                arrayOfUpdatedLines[iLineNumberOfUpdatedProject] = sArrayOfStringsToWrite[0];
                arrayOfUpdatedLines[iLineNumberOfUpdatedProject + 1] = sArrayOfStringsToWrite[1];
                arrayOfUpdatedLines[iLineNumberOfUpdatedProject + 2] = sArrayOfStringsToWrite[2];
                arrayOfUpdatedLines[iLineNumberOfUpdatedProject + 3] = sArrayOfStringsToWrite[3];
                arrayOfUpdatedLines[iLineNumberOfUpdatedProject + 4] = sArrayOfStringsToWrite[4];
                File.WriteAllLines(sMainLogFile, arrayOfUpdatedLines);
            }
            else File.AppendAllLines(sMainLogFile, sArrayOfStringsToWrite);
        }


        public void mainLogFileExists()
        {
            //create main Log file if it does not exist
            bool bMainLogFileExists = System.IO.File.Exists(sMainLogFile);
            if (!bMainLogFileExists)
            {
                createLogFolder();
                var vLogFile = System.IO.File.Create(sMainLogFile);
                vLogFile.Close();
            }
        }

    }
}
