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
    /// Interaction logic for MarkSheet.xaml
    /// </summary>
    public partial class MarkSheet : Window
    {
        public MarkSheet()
        {
            InitializeComponent();
            DataContext = new MarkVM();
            cbo_tests.SelectedIndex = 0;
        }
    }
}
