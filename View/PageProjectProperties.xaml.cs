using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private ViewModelFileManager viewModelFileManager;
        private ViewModelProjectProperties viewModelProjectProperties;
        private ModelProjectProperties modelProjectProperties;

        public PageProjectProperties(int iID)
        {
            //initializers
            this.iID = iID;
            InitializeComponent();
            viewModelProjectProperties = new ViewModelProjectProperties();
            viewModelFileManager = new ViewModelFileManager();
            //

            //invoke general methods from FileManager to get/set desired values
            viewModelFileManager.createLogFolder();
            viewModelFileManager.readMainLogFile();
            viewModelFileManager.getProjectsTestsPerformedListFromLogFiles();
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
                modelProjectProperties = viewModelFileManager.getProjectProfpertiesFromFileByID(iID);
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

        private void buttonCancelClick(object sender, RoutedEventArgs e)
        {
            framePageProjectProperties.Navigate(new System.Uri("/View/PageBlank.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
