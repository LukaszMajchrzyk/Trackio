using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace Trackio.View
{
    /// <summary>
    /// Interaction logic for WindowTestRunning.xaml
    /// </summary>
    public partial class WindowTestRunning : Window
    {
        public int iIdOfTest;
        public string sNameOfTest;
        public bool bTestResult;
        public bool bTestAborted = false;
        public WindowTestRunning(string sNameOfTest, int iIdOfTest)
        {
            InitializeComponent();
            this.sNameOfTest = sNameOfTest;
            this.sNameOfTest = sNameOfTest;
            labelHeader.Content = $"[TEST # {iIdOfTest}]. Check if... :";
            labelNameOfTest.Content = sNameOfTest;
        }

        private void buttonAbortTestClick(object sender, RoutedEventArgs e)
        {
            bTestAborted = true;
        }
        private void buttonCorrectClick(object sender, RoutedEventArgs e)
        {
            bTestResult = true;
        }

        private void buttonIncorrectClick(object sender, RoutedEventArgs e)
        {
            bTestResult= false;
        }
    }
}
