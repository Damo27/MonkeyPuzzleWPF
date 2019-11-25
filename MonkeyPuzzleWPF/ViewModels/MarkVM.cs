using MonkeyPuzzleWPF.Models;
using MonkeyPuzzleWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyPuzzleWPF.ViewModels
{
    class MarkVM : INotifyPropertyChanged
    {

        public MarkVM()
        {
            AllMarks = Connection.GetMarks(0);
            Tests = Connection.GetTests();
            Users = Connection.GetUsers();
        }
        private Test _chosenTest;
        private List<Mark> _marks;
        private ObservableCollection<Question> _questionsInTest;
        private User _testCreator;
        private int _numberQuestions;

        public List<Mark> AllMarks { get; set; }

        public List<Test> Tests { get; set; }

        public List<User> Users { get; set; }

        public int NumberQuestions { get => _numberQuestions; set { _numberQuestions = value; OnPropertyChanged("NumberQuestions"); } }

        public User TestCreator { get => _testCreator; set { _testCreator = value; OnPropertyChanged("TestCreator"); } }

        public ObservableCollection<Question> QuestionsInTest { get => _questionsInTest; set { _questionsInTest = value; OnPropertyChanged("QuestionsInTest"); } }

        public Test ChosenTest { get => _chosenTest; set { _chosenTest = value; OnPropertyChanged("Test"); Marks = AllMarks.Where(x => x.TestID == _chosenTest.TestID).ToList(); QuestionsInTest = Connection.GetQuestions(_chosenTest.TestID); TestCreator = Users.Where(x => x.UserID == _chosenTest.UserID.ToString()).First(); NumberQuestions = _chosenTest.NumberOfQuestions; } }

        public List<Mark> Marks { get => _marks; set { _marks = value; foreach (Mark m in _marks) { m.UserName = Users.Where(x => x.UserID == m.UserID).FirstOrDefault().ToString(); OnPropertyChanged("Marks"); }; } }



        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
