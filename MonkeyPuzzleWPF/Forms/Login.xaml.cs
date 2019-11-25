using MonkeyPuzzleWPF.ViewModels;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        UserVM user;
        public Login()
        {
            InitializeComponent();
            user = new UserVM(this);
            DataContext = user;
        }

        private void btn_signIn_Click(object sender, RoutedEventArgs e)
        {
            //LecturerMain lec = new LecturerMain();
            //this.Hide();
        }
    }
}
