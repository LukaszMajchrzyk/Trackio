using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Cryptography;
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
using Trackio.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Trackio.View
{
    public partial class PageOpenProject : Page
    {
        private int iIdOfSelectedProject;
        private ViewModelFileManager viewModelFileManager;
        public PageOpenProject()
        {
            InitializeComponent();
            //get existing IDs and Project Names from main LOG file
            viewModelFileManager = new ViewModelFileManager();
            viewModelFileManager.readMainLogFile();
            //setting values to data grid
            datagridIDsAndProjectsNames.ItemsSource = viewModelFileManager.dictionaryIDsAndProjectNames;
        }


        //method to set names in Data Grid; binding from xml
        private void dataGridHeaderNames(object sender, EventArgs e)
        {
            datagridIDsAndProjectsNames.Columns[0].Header = "ID";
            datagridIDsAndProjectsNames.Columns[1].Header = "Project Name";
        }
        private void buttonOpenClick(object sender, RoutedEventArgs e)
        {
            frameOpenProject.Content = new PageProjectProperties(iIdOfSelectedProject);
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            frameOpenProject.Navigate(new System.Uri("/View/PageBlank.xaml",UriKind.RelativeOrAbsolute));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyValuePair<int, string> kvpParsedLine = (KeyValuePair<int,string>)datagridIDsAndProjectsNames.CurrentItem;
            iIdOfSelectedProject = kvpParsedLine.Key;
        }
    }
}
