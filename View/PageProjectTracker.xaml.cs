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
using Trackio.ViewModel;

namespace Trackio.View
{
    public partial class PageProjectTracker : Page
    {
        private int iID;
        private ViewModelProjectTracker viewModelProjectTracker;
        public PageProjectTracker(int iID)
        {
            this.iID = iID;
            InitializeComponent();
            viewModelProjectTracker = new ViewModelProjectTracker(iID);
            viewModelProjectTracker.readProjectLogFile();


        }
    }
}
