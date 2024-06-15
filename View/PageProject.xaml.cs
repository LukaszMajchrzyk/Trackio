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
    public partial class PageProject : Page
    {
        private int iID;
        public PageProject(int iID)
        {
            this.iID = iID;
            InitializeComponent();
        }

        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabProjectProperties.IsSelected) pageTab.Content = new PageProjectProperties(iID);
            if (tabTests.IsSelected) pageTab.Content = new PageProjectTests(iID);
            if (tabTracker.IsSelected) pageTab.Content = new PageProjectTracker(iID);
        }

    }
}
