using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
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
    /// Interaction logic for PageProjectProperties.xaml
    /// </summary>
    public partial class PageProjectTracker : Page
    {
        private int iID;
        public PageProjectTracker(int iID)
        {
            this.iID = iID;
            InitializeComponent();
        }
    }
}
