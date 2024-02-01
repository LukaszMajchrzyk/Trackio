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
    /// <summary>
    /// Interaction logic for PageProjectProperties.xaml
    /// </summary>
    public partial class PageNewProjectProperties : Page
    {
        private ModelProjectProperties modelProjectProperties;
        public PageNewProjectProperties()
        {
            InitializeComponent();
            //Creating objects of Model Class
            ViewModelProjectProperties viewModelProjectProperties = new ViewModelProjectProperties();
            ViewModelFileManager viewModelFileManager = new ViewModelFileManager();
            //get current date; in new project last updated and current date is the same
            textBoxCreationDate.Text = viewModelProjectProperties.dateCurretDateAndTime.ToString();
            textBoxLastUpdate.Text = textBoxCreationDate.Text;
            //getting Project's status variables from Model Class (by default it's mentioned for new Project)
            comboBoxCurrentStatus.ItemsSource = viewModelProjectProperties.listOfProjectsStatus;
            comboBoxCurrentStatus.SelectedIndex = 0 ;
            textBoxID.Text = viewModelFileManager.iLastID.ToString();
        }

    }
}
