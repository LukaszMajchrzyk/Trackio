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

namespace Trackio.View
{
    /// <summary>
    /// Interaction logic for PageProject.xaml
    /// </summary>
    public partial class PageProject : Page
    {
        public PageProject()
        {
            InitializeComponent();
        }

        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabProjectProperties.IsSelected) pageTab.Content = new PageNewProjectProperties();
            if (tabTests.IsSelected) pageTab.Content = new PageProjectTests();
            if (tabTracker.IsSelected) pageTab.Content = new PageProjectTracker();
        }


    }
}
