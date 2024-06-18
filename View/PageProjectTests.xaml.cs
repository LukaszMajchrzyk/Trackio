using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class PageProjectTests : Page
    {
        private ViewModelProjectTestsDescribed viewModelProjectTestsDescribed;
        private int iNextFreeNumberOfTest;
        private int iMainProjectID;
        private int iTestID;
        ObservableCollection<ViewModelProjectTestsDescribed> observableCollectionListOfProjectTests;
        public PageProjectTests(int iMainProjectID)
        {
            InitializeComponent();
            //storing main ID of Project
            this.iMainProjectID = iMainProjectID;
            //tests status initializer
            viewModelProjectTestsDescribed = new ViewModelProjectTestsDescribed(iMainProjectID);
            sCurrentStatus.ItemsSource = viewModelProjectTestsDescribed.listOfStatuses;


            viewModelProjectTestsDescribed.createTestsDescribedLogFile();
            


            //initilizer for ObservableCollection
            observableCollectionListOfProjectTests = new ObservableCollection<ViewModelProjectTestsDescribed>();
            observableCollectionListOfProjectTests = viewModelProjectTestsDescribed.readTestDescribedLogFile();
            dgProjectTests.ItemsSource = observableCollectionListOfProjectTests;
            dgProjectTests.AllowDrop = false;


        }

        private void AddNewTest(object sender, RoutedEventArgs e)
        {
            //default starting number of ID is 1
            iNextFreeNumberOfTest = 1;
            if (observableCollectionListOfProjectTests.Count > 0)
            {
                int[] arrayOfIds = observableCollectionListOfProjectTests.Select(Test => Test.iID).ToArray();

                iNextFreeNumberOfTest = Enumerable.Range(1, int.MaxValue).Except(arrayOfIds).FirstOrDefault();
            }
            observableCollectionListOfProjectTests.Add(new ViewModelProjectTestsDescribed(iMainProjectID) { iID = iNextFreeNumberOfTest, iRunsCounter = 0 });

        }

        private void RemoveTest(object sender, RoutedEventArgs e)
        {
            //checking if selected Test has already performed Runs. If so deleting is not allowed
            observableCollectionListOfProjectTests.RemoveAt(iTestID);


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
                if (observableCollectionListOfProjectTests[i].sNameOfTest == null || !regexItem.IsMatch(observableCollectionListOfProjectTests[i].sNameOfTest) )
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
                viewModelProjectTestsDescribed.iID = observableCollectionListOfProjectTests[i].iID;
                viewModelProjectTestsDescribed.sNameOfTest = observableCollectionListOfProjectTests[i].sNameOfTest;
                viewModelProjectTestsDescribed.iRunsCounter = observableCollectionListOfProjectTests[i].iRunsCounter;
                viewModelProjectTestsDescribed.sCurrentStatus = observableCollectionListOfProjectTests[i].sCurrentStatus;
                viewModelProjectTestsDescribed.saveTestDescribedLogFile();
            }

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            iTestID = dgProjectTests.SelectedIndex;
        }




    }
}
