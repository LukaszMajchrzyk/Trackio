using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trackio.Model;
using Trackio.View;

namespace Trackio
{
    public partial class MainWindow : Window
    {
        //title bar color fields
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        const int DWWMA_CAPTION_COLOR = 35;
        //
        public MainWindow()
        {
            //applying color settings to title bar
            IntPtr hWnd = new WindowInteropHelper(this).EnsureHandle();
            int[] colorstr = new int[] { 0x444444 };
            DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, colorstr, 4);
            //initialize main window
            InitializeComponent();
            //load blank page as a welcome page
            MainFrame.Content = new PageBlank();
        }

        private void menuCreateNewClick(object sender, RoutedEventArgs e)
        {
            //in case of new project we will use ID = 0;
            int iID = 0;
            MainFrame.Content = new PageProject(iID);

        }

        private void menuOpenClick(object sender, RoutedEventArgs e)
        {
            //implement -> getting id of project now it's hardcoded
            int iID = 1;
            MainFrame.Content = new PageProject(iID);



        }
        private void menuSaveClick(object sender, RoutedEventArgs e)
        {

        }
        private void menuCloseProjectClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PageBlank();
        }

        private void menuQuitClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void menuAboutClick(object sender, RoutedEventArgs e)
        {
            WindowAbout windowAbout = new WindowAbout();
            windowAbout.Show();
        }

    }
}