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
        public bool bTestAborted = false;
        public Dictionary<int, bool> dictionaryOfIdsAndResult;
        public WindowTestRunning(string sNameOfTest, int iIdOfTest)
        {
            InitializeComponent();
            this.sNameOfTest = sNameOfTest;
            labelHeader.Content = $"[TEST # {iIdOfTest}]. Check if... :";
            labelNameOfTest.Content = sNameOfTest;
            dictionaryOfIdsAndResult = new Dictionary<int, bool>();
        }

        private void buttonAbortTestClick(object sender, RoutedEventArgs e)
        {
            bTestAborted = true;
            dictionaryOfIdsAndResult.Clear();
            this.Close();
        }
        private void buttonCorrectClick(object sender, RoutedEventArgs e)
        {
            dictionaryOfIdsAndResult.Add(iIdOfTest, true);
            this.Close();
        }

        private void buttonIncorrectClick(object sender, RoutedEventArgs e)
        {
            dictionaryOfIdsAndResult.Add(iIdOfTest, false);
            this.Close();
        }
    }
}
