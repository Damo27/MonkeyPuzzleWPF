using MonkeyPuzzleWPF.Models;
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
    /// Interaction logic for LecturerMain.xaml
    /// </summary>
    public partial class LecturerMain : Window
    {
        User user;
        public LecturerMain(User user)
        {
            InitializeComponent();
            this.user = user;
            this.Title += user.Name + " " + user.Surname;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult exit;
            exit = MessageBox.Show("Are you sure you want to exit?", "Exit?", MessageBoxButton.YesNo);

            if (exit == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }

            base.OnClosing(e);
        }

        private void btn_createTest_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NewTest newTest = new NewTest(user);
            newTest.ShowDialog();
            newTest = null;
            this.Show();
        }

        private void btn_editTest_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SelectTest selectTest = new SelectTest(user);
            selectTest.ShowDialog();
            selectTest = null;
            this.Show();
        }

        private void btn_deleteTest_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DeleteTest delTest = new DeleteTest();
            delTest.ShowDialog();
            delTest = null;
            this.Show();
        }

        private void btn_viewMarks_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MarkSheet markSheet = new MarkSheet();
            markSheet.ShowDialog();
            markSheet = null;
            this.Show();
        }
    }


}
