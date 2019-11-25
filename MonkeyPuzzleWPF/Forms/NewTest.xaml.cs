using MonkeyPuzzleWPF.Models;
using MonkeyPuzzleWPF.Utilities;
using MonkeyPuzzleWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NewTest.xaml
    /// </summary>
    public partial class NewTest : Window
    {
        Test test;

        public NewTest(User user)
        {
            InitializeComponent();
            this.Title += user.Name + " " + user.Surname;
            ObservableCollection<Question> questions = new ObservableCollection<Question>();
            test = new Test() { TestID = Connection.GetTestID() + 1, UserID = Convert.ToInt32(user.UserID)};
            DataContext = new TestVM(this, test, questions);
        }

        public NewTest(User user, Test test)
        {
            InitializeComponent();
            this.Title += user.Name + " " + user.Surname;
            ObservableCollection<Question> questions = Connection.GetQuestions(test.TestID); 
            this.test = test;
            DataContext = new TestVM(this, test, questions);
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_saveQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult exit;
            exit = MessageBox.Show("Are you sure you want to exit without saving?", "Exit?", MessageBoxButton.YesNo);

            if (exit == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

            base.OnClosing(e);
        }
    }
}
