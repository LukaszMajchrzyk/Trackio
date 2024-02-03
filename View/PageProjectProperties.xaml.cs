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
            viewModelFileManager.getFileListFromLogFiles();
            viewModelFileManager.getLastID();
            //


            // if ID is 0 it means the new project option has been choosen
            if (iID == 0)
            {
                //project is new -> get current date; in new project last updated and current date is the same
                textBoxCreationDate.Text = viewModelProjectProperties.dateCurretDateAndTime.ToString();
                textBoxLastUpdate.Text = textBoxCreationDate.Text;

                //getting Project's status variables from Model Class (by default it's mentioned for new Project)
                comboBoxCurrentStatus.ItemsSource = viewModelProjectProperties.listOfProjectsStatus;
                comboBoxCurrentStatus.SelectedIndex = 0;
                textBoxID.Text = viewModelFileManager.iLastID.ToString();

            }
            //if ID is != 0 it means open project option has been choosen
            //implement -> loading project details
            else
            {
                textBoxID.Text = iID.ToString();
            }

        }

        private void buttonSaveClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
