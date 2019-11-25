using MonkeyPuzzleWPF.Forms;
using MonkeyPuzzleWPF.Models;
using MonkeyPuzzleWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MonkeyPuzzleWPF.ViewModels
{
    class TestVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private Test _test;
        private string _testName;
        private Question _question;
        private string _quest;
        private int _questID;
        private int _testID;
        private string _ansA;
        private string _ansB;
        private string _ansC;
        private string _ansD;
        private string _correctAns;
        private int _index;
        private NewTest newTest;

        public Test Test { get => _test; set => _test = value; }

        public Question Question {
            get => _question; set
            {
                _question = value;
                if (_question != null)
                {
                    Quest = _question.Quest;
                    QuestID = _question.QuestionID;
                    AnsA = _question.ansA;
                    AnsB = _question.ansB;
                    AnsC = _question.ansC;
                    AnsD = _question.ansD;
                    CorrectAns = changeCorrectAnswer(_question.correctAns);
                }
            }
        }

        public int Index { get => _index; set { _index = value; OnPropertyChanged("Index"); } }

        [Required]
        public String TestName { get => _testName; set { _testName = value; OnPropertyChanged("TestName"); Test.TestName = value; } }

        [Required]
        public string Quest { get => _quest; set { _quest = value; OnPropertyChanged("Quest"); } }

        public int QuestID { get => _questID; set { _questID = value; } }

        public int TestID { get => _testID; set { _testID = value; } }

        [Required]
        public string AnsA { get => _ansA; set { _ansA = value; OnPropertyChanged("AnsA"); } }

        [Required]
        public string AnsB { get => _ansB; set { _ansB = value; OnPropertyChanged("AnsB"); } }

        [Required]
        public string AnsC { get => _ansC; set { _ansC = value; OnPropertyChanged("AnsC"); } }

        [Required]
        public string AnsD { get => _ansD; set { _ansD = value; OnPropertyChanged("AnsD"); } }

        [Required]
        public string CorrectAns { get => _correctAns; set { _correctAns = value; OnPropertyChanged("CorrectAns"); } }

        public ObservableCollection<Question> QuestionsInTest { get; set; }

        public ICommand SaveQuestion { get; private set; }
        public ICommand DeleteQuestion { get; private set; }
        public ICommand SaveTest { get; private set; }

        public TestVM(NewTest newTest, Test test, ObservableCollection<Question> questions)
        {
            SaveQuestion = new SaveQuestionCommand(this);
            DeleteQuestion = new DeleteQuestionCommand(this);
            SaveTest = new SaveTestCommand(this);

            this.newTest = newTest;
            this.Test = test;
            QuestionsInTest = questions;
            TestID = test.TestID;
            TestName = test.TestName;
            Index = -1;
        }

        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults)
                    )
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        class SaveQuestionCommand : ICommand
        {
            TestVM localTestVM;

            public SaveQuestionCommand(TestVM testVM)
            {
                this.localTestVM = testVM;
                localTestVM.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return !string.IsNullOrEmpty(localTestVM.Quest) && !string.IsNullOrEmpty(localTestVM.AnsA) && !string.IsNullOrEmpty(localTestVM.AnsB) && !string.IsNullOrEmpty(localTestVM.AnsC) && !string.IsNullOrEmpty(localTestVM.AnsD) && !string.IsNullOrEmpty(localTestVM.CorrectAns);
            }

            public void Execute(object parameter)
            {
                if (localTestVM.Index >= 0)
                {
                    localTestVM.CorrectAns = changeCorrectAnswer(localTestVM.CorrectAns);
                    localTestVM.QuestionsInTest.RemoveAt(localTestVM.Index);
                    localTestVM.QuestionsInTest.Add(new Question() { ansA = localTestVM.AnsA, ansB = localTestVM.AnsB, ansC = localTestVM.AnsC, ansD = localTestVM.AnsD, correctAns = localTestVM.CorrectAns, Quest = localTestVM.Quest, TestID = localTestVM.TestID, QuestionID = localTestVM.QuestID });
                    localTestVM.Question = new Question();
                    localTestVM.Index = -1;
                    localTestVM.QuestID++;
                }
                else
                {
                    localTestVM.CorrectAns = changeCorrectAnswer(localTestVM.CorrectAns);
                    localTestVM.QuestionsInTest.Add(new Question() { ansA = localTestVM.AnsA, ansB = localTestVM.AnsB, ansC = localTestVM.AnsC, ansD = localTestVM.AnsD, correctAns = localTestVM.CorrectAns, Quest = localTestVM.Quest, TestID = localTestVM.TestID, QuestionID = localTestVM.QuestID });
                    localTestVM.Question = new Question();
                    localTestVM.QuestID++;
                }
            }
        }

        class DeleteQuestionCommand : ICommand
        {
            TestVM localTestVM;

            public DeleteQuestionCommand(TestVM testVM)
            {
                this.localTestVM = testVM;
                localTestVM.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                if (localTestVM.Index >= 0 && localTestVM.QuestionsInTest.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public void Execute(object parameter)
            {
                localTestVM.QuestionsInTest.RemoveAt(localTestVM.Index);
                localTestVM.Question = new Question();
                localTestVM.Index = -1;
            }
        }

        class SaveTestCommand : ICommand
        {
            TestVM localTestVM;

            public SaveTestCommand(TestVM testVM)
            {
                this.localTestVM = testVM;
                localTestVM.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                if (!string.IsNullOrEmpty(localTestVM.TestName) && localTestVM.QuestionsInTest.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public void Execute(object parameter)
            {
                MessageBoxResult exit;
                exit = MessageBox.Show("Are you sure you want to Save " + localTestVM.TestName +" and exit?", "Save and Exit?", MessageBoxButton.YesNo);

                if (exit == MessageBoxResult.Yes)
                {
                    localTestVM.Test.NumberOfQuestions = localTestVM.QuestionsInTest.Count;
                    Connection.InsertToDB(localTestVM.Test);
                    foreach (Question q in localTestVM.QuestionsInTest)
                    {
                        Connection.InsertToDB(q);
                    }
                    localTestVM.newTest.Close();
                }
            }
        }

        public static string changeCorrectAnswer(string oldCorrectAns)
        {
            string newCorrectAns = string.Empty;
            switch (oldCorrectAns)
            {
                case "0":
                    {
                        newCorrectAns = "A";
                        break;
                    }
                case "1":
                    {
                        newCorrectAns = "B";
                        break;
                    }
                case "2":
                    {
                        newCorrectAns = "C";
                        break;
                    }
                case "3":
                    {
                        newCorrectAns = "D";
                        break;
                    }
                case "A":
                    {
                        newCorrectAns = "0";
                        break;
                    }
                case "B":
                    {
                        newCorrectAns = "1";
                        break;
                    }
                case "C":
                    {
                        newCorrectAns = "2";
                        break;
                    }
                case "D":
                    {
                        newCorrectAns = "3";
                        break;
                    }
            }
            return newCorrectAns;
        }
    }
}
