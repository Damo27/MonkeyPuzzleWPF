using MonkeyPuzzleWPF.Models;
using MonkeyPuzzleWPF.Utilities;
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
using System.Windows.Shapes;

namespace MonkeyPuzzleWPF.Forms
{
    /// <summary>
    /// Interaction logic for DeleteTest.xaml
    /// </summary>
    public partial class DeleteTest : Window
    {
        List<Test> tests;
        public DeleteTest()
        {
            InitializeComponent();
            tests = Connection.GetTests();
            cbo_tests.ItemsSource = tests;
            cbo_tests.DisplayMemberPath = "TestName";
            cbo_tests.SelectedIndex = 0;
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            Test test = (Test)cbo_tests.SelectedItem;
            MessageBoxResult delete;
            delete = MessageBox.Show("Are you sure you want to permanently delete " + test.TestName + "? All questions and marks assosiated with " + test.TestName + " will also be removed.", "Delete?", MessageBoxButton.YesNo);

            if (delete == MessageBoxResult.Yes)
            {
                Connection.DeleteFromDB("Marks", "testID", test.TestID);
                Connection.DeleteFromDB("Questions", "testID", test.TestID);
                Connection.DeleteFromDB("Tests", "testID", test.TestID);
            }
        }
    }
}
