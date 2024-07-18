using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class PageProjectTests : Page
    {
        private ViewModelProjectTestsDescribed viewModelProjectTestsDescribed;
        private int iNextFreeNumberOfTest;
        private int iMainProjectID;
        private int iCounterOfRuns;
        private int iTestID;
        private bool bRunAborted = false;
        private Dictionary<int, bool> dictionaryIdTestAndResult;
        ObservableCollection<ViewModelProjectTestsDescribed> observableCollectionListOfProjectTests;
        ObservableCollection<ViewModelProjectTestsDescribed> observableCollectionListOfProjectTestsEligableForRun;
        ObservableCollection<ViewModelProjectTracker> observableCollectionViewModelProjectTracker;
        ViewModelProjectTracker viewModelProjectTracker;
        public PageProjectTests(int iMainProjectID)
        {
            InitializeComponent();
            viewModelProjectTracker = new ViewModelProjectTracker(iMainProjectID);
            //storing main ID of Project
            this.iMainProjectID = iMainProjectID;
            //tests status initializer
            viewModelProjectTestsDescribed = new ViewModelProjectTestsDescribed(iMainProjectID);
            sCurrentStatus.ItemsSource = viewModelProjectTestsDescribed.listOfStatuses;
            viewModelProjectTestsDescribed.createTestsDescribedLogFile();
            //initilizer for ObservableCollection
            observableCollectionListOfProjectTestsEligableForRun = new ObservableCollection<ViewModelProjectTestsDescribed>();
            observableCollectionListOfProjectTests = new ObservableCollection<ViewModelProjectTestsDescribed>();
            observableCollectionListOfProjectTests = viewModelProjectTestsDescribed.readTestDescribedLogFile();
            dgProjectTests.ItemsSource = observableCollectionListOfProjectTests;
            dgProjectTests.AllowDrop = false;
        }

        private void AddNewTest(object sender, RoutedEventArgs e)
        {
            //default Runs' counter is 0
            iCounterOfRuns = 0;
            //default starting number of ID is 1
            iNextFreeNumberOfTest = 1;
            if (observableCollectionListOfProjectTests.Count > 0)
            {
                int[] arrayOfIds = observableCollectionListOfProjectTests.Select(Test => Test.iID).ToArray();

                iNextFreeNumberOfTest = Enumerable.Range(1, int.MaxValue).Except(arrayOfIds).FirstOrDefault();
            }
            observableCollectionListOfProjectTests.Add(new ViewModelProjectTestsDescribed(iMainProjectID) { iID = iNextFreeNumberOfTest, iRunsCounter = iCounterOfRuns });

        }

        private void RemoveTest(object sender, RoutedEventArgs e)
        {
            //check if test is selected
            if (dgProjectTests.SelectedItem == null)
            {
                WindowsMessageGeneric windowMessageGeneric = new WindowsMessageGeneric("Please select test in table first");
                windowMessageGeneric.Show();
            }
            //checking if selected Test has already performed Runs. If so deleting is not allowed
            else if (observableCollectionListOfProjectTests[dgProjectTests.SelectedIndex].iRunsCounter != 0)
            {
                WindowsMessageGeneric windowMessageGeneric = new WindowsMessageGeneric("You cannot delete Test if it has been run before");
                windowMessageGeneric.Show();
            }
            else
            {
                observableCollectionListOfProjectTests.RemoveAt(iTestID);
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            //sorting obsv collection and refreshing datagrid items' source
            this.observableCollectionListOfProjectTests = new ObservableCollection<ViewModelProjectTestsDescribed>(this.observableCollectionListOfProjectTests.OrderBy(value => value.iID));
            dgProjectTests.ItemsSource = observableCollectionListOfProjectTests;
            //setting fields to object properties
            for (int i = 0; i < observableCollectionListOfProjectTests.Count; i++)
            {
                //check if name of test does not have special characters -> it can be an issue during LOG file parsing
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                if (observableCollectionListOfProjectTests[i].sNameOfTest == null || !regexItem.IsMatch(observableCollectionListOfProjectTests[i].sNameOfTest))
                {
                    MessageBox.Show("Please provid valid name of Test. No empty value or special characters");
                    break;
                }
                //check if selected status is not empty
                if (observableCollectionListOfProjectTests[i].sCurrentStatus == null)
                {
                    MessageBox.Show("Please select status of added test");
                    break;
                }
                //filling object's properties
                viewModelProjectTestsDescribed.iID = observableCollectionListOfProjectTests[i].iID;
                viewModelProjectTestsDescribed.sNameOfTest = observableCollectionListOfProjectTests[i].sNameOfTest;
                viewModelProjectTestsDescribed.iRunsCounter = observableCollectionListOfProjectTests[i].iRunsCounter;
                viewModelProjectTestsDescribed.sCurrentStatus = observableCollectionListOfProjectTests[i].sCurrentStatus;
                viewModelProjectTestsDescribed.sComment = observableCollectionListOfProjectTests[i].sComment;
                viewModelProjectTestsDescribed.saveTestDescribedLogFile();
            }

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            iTestID = dgProjectTests.SelectedIndex;
        }

        private void RunAllTests(object sender, RoutedEventArgs e)
        {
            //get updated data to observable collection to obtain last Runs' ID
            observableCollectionListOfProjectTests = viewModelProjectTestsDescribed.readTestDescribedLogFile();
            dgProjectTests.ItemsSource = observableCollectionListOfProjectTests;

            dictionaryIdTestAndResult = new Dictionary<int, bool>();
            //create observable collection which has only eligeble tests
            for (int i = 0; i < observableCollectionListOfProjectTests.Count; i++)
            {
                if ((observableCollectionListOfProjectTests[i].sCurrentStatus.Equals("In Work") || observableCollectionListOfProjectTests[i].sCurrentStatus.Equals("Created")) && !bRunAborted)
                {
                    observableCollectionListOfProjectTestsEligableForRun.Add(observableCollectionListOfProjectTests[i]);
                }
            }


            for (int i = 0; i < observableCollectionListOfProjectTestsEligableForRun.Count; i++)
            {

                WindowTestRunning windowTestRunning = new WindowTestRunning(observableCollectionListOfProjectTestsEligableForRun[i].sNameOfTest, observableCollectionListOfProjectTestsEligableForRun[i].iID);
                windowTestRunning.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                windowTestRunning.ShowDialog();
                //if button Abort is clicked -> break the loop and do not save Run's result
                if (windowTestRunning.bTestAborted)
                {
                    bRunAborted = true;
                    break;
                }
                //adding values to dictionary from other one
                foreach (var v in windowTestRunning.dictionaryOfIdsAndResult)
                {
                    dictionaryIdTestAndResult.Add(v.Key, v.Value);
                }
            }
            if (dictionaryIdTestAndResult != null)
            {
                
                //saving Runs' Count to Log
                for (int i = 0;i < dictionaryIdTestAndResult.Count; i++)
                {
                    iCounterOfRuns = viewModelProjectTestsDescribed.iID = observableCollectionListOfProjectTestsEligableForRun[i].iRunsCounter;
                    iCounterOfRuns += 1;
                    viewModelProjectTestsDescribed.iID = observableCollectionListOfProjectTestsEligableForRun[i].iID;
                    viewModelProjectTestsDescribed.sNameOfTest = observableCollectionListOfProjectTestsEligableForRun[i].sNameOfTest;
                    viewModelProjectTestsDescribed.iRunsCounter = iCounterOfRuns;
                    viewModelProjectTestsDescribed.sCurrentStatus = observableCollectionListOfProjectTestsEligableForRun[i].sCurrentStatus;
                    viewModelProjectTestsDescribed.sComment = observableCollectionListOfProjectTestsEligableForRun[i].sComment;
                    viewModelProjectTestsDescribed.saveTestDescribedLogFile();
                }
                //saving result to log for each test and id //projecttracker
                viewModelProjectTracker.saveTrackerToLog(observableCollectionListOfProjectTestsEligableForRun, dictionaryIdTestAndResult,iCounterOfRuns);
            }
            //get updated data to observable collection
            observableCollectionListOfProjectTests = viewModelProjectTestsDescribed.readTestDescribedLogFile();
            dgProjectTests.ItemsSource = observableCollectionListOfProjectTests;
        }

        private void ButtonCloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new PageBlank();
        }
    }
}
