using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Trackio.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Trackio.View
{
    public partial class PageOpenProject : Page
    {
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

        private void buttonOpenClick(object sender, RoutedEventArgs e)
        {
            int iID = 1;
        }
        //method to set names in Data Grid; binding from xaml
        private void dataGridHeaderNames(object sender, EventArgs e)
        {
            datagridIDsAndProjectsNames.Columns[0].Header = "ID";
            datagridIDsAndProjectsNames.Columns[1].Header = "Project Name";
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            frameOpenProject.Navigate(new System.Uri("/View/PageBlank.xaml",UriKind.RelativeOrAbsolute));
        }
    }
}
