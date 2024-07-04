using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trackio.Model;
using Trackio.ViewModel;

namespace Trackio.View
{
    public partial class PageProjectProperties : Page
    {
        private int iID;
        private ViewModelProjectMain viewModelFileManager;
        private ViewModelProjectProperties viewModelProjectProperties;
        private ModelProjectProperties modelProjectProperties;

        public PageProjectProperties(int iID)
        {
            //initializers
            this.iID = iID;
            InitializeComponent();
            viewModelProjectProperties = new ViewModelProjectProperties();
            viewModelFileManager = new ViewModelProjectMain();
            //

            //invoke general methods from FileManager to get/set desired values
            viewModelFileManager.createLogFolder();
            viewModelFileManager.readMainLogFile();
            viewModelFileManager.getLastID();
            //

            // if ID is 0 it means the new project option has been choosen
            if (iID == 0)
            {
                //obtain first unsed ID and set it to iID
                iID = viewModelFileManager.iLastID;
                //project is new -> get current date; in new project last updated and current date is the same
                textBoxCreationDate.Text = viewModelProjectProperties.dateCreationDate.ToString();
                textBoxLastUpdated.Text = textBoxCreationDate.Text;

                //getting Project's status variables from Model Class (by default it's mentioned for new Project)
                comboBoxCurrentStatus.ItemsSource = viewModelProjectProperties.listOfProjectsStatus;
                comboBoxCurrentStatus.SelectedIndex = 0;

            }
            else
            {
                //get Project's details from LOG file by ID
                modelProjectProperties = viewModelFileManager.getProjectPropertiesFromFileByID(iID);
                //fill fields with data from LOG
                textBoxName.Text = modelProjectProperties.sNameOfProject.TrimStart().TrimEnd();
                textBoxCreationDate.Text = modelProjectProperties.dateCreationDate.ToString().TrimStart().TrimEnd();
                textBoxLastUpdated.Text = modelProjectProperties.dateLastUppdated.ToString().TrimStart().TrimEnd();
                comboBoxCurrentStatus.ItemsSource = viewModelProjectProperties.listOfProjectsStatus;
                comboBoxCurrentStatus.SelectedIndex = viewModelProjectProperties.listOfProjectsStatus.FindIndex(x => x.StartsWith(modelProjectProperties.sCurrentStatus.ToString().TrimStart().TrimEnd()));
            }

            textBoxID.Text = iID.ToString().TrimStart().TrimEnd();

        }

        private void buttonSaveClick(object sender, RoutedEventArgs e)
        {
            //check if name of project does not have special characters -> it can be an issue during LOG file parsing
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (!regexItem.IsMatch(textBoxName.Text))
            {
                MessageBox.Show("Name of Project can not contain special characters");
                return;
            }

            //setting properties with save button
            viewModelProjectProperties.iID = Int32.Parse(textBoxID.Text);
            viewModelProjectProperties.sNameOfProject = textBoxName.Text;
            viewModelProjectProperties.sCurrentStatus = comboBoxCurrentStatus.SelectedItem.ToString();
            viewModelProjectProperties.dateCreationDate = DateTime.Parse(textBoxCreationDate.Text);
            viewModelProjectProperties.dateLastUppdated = DateTime.Now;
            viewModelFileManager.saveToMainLogFile(viewModelProjectProperties);
            //save current time as last modified date
            textBoxLastUpdated.Text = DateTime.Now.ToString();
        }

        private void buttonCloseClick(object sender, RoutedEventArgs e)
        {
            //there is a need to go backwards 2 steps so instead we're calling main frame
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new PageBlank();
        }
    }
}
