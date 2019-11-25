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
    /// Interaction logic for SelectTest.xaml
    /// </summary>
    public partial class SelectTest : Window
    {
        List<Test> tests;
        User user;
        public SelectTest(User user)
        {
            InitializeComponent();
            tests = Connection.GetTests();
            this.user = user;
            cbo_tests.ItemsSource = tests;
            cbo_tests.DisplayMemberPath = "TestName";
            cbo_tests.SelectedIndex = 0;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            Test test = (Test)cbo_tests.SelectedItem;
            this.Hide();
            NewTest newTest = new NewTest(user, test);
            newTest.ShowDialog();
            newTest = null;
            this.Close();
        }
    }
}
