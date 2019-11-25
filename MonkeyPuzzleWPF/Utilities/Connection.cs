using MonkeyPuzzleWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonkeyPuzzleWPF.Utilities
{
    class Connection
    {
        static readonly string dbPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\MonkeyPuzzleWPF\\AppData\\monkeypuzzle.mdf"));
        static string CONNECTION_STRING
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=" + dbPath + "; Trusted_Connection=Yes; database=monkeypuzzle;";
            }
        }

        public static User GetUser(String userID, String passWord)
        {
            User user = new User();

            string sql = "Select * From Users where [userPassword] = @pWord and [UserID] = @uID;";
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@uID", userID);
                        command.Parameters.AddWithValue("@pWord", passWord);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            user.UserID = reader["UserID"].ToString();
                            user.Password = reader["userPassword"].ToString();
                            user.AccessGroup = Convert.ToInt32(reader["AccessGroup"]);
                            user.Name = reader["Firstname"].ToString();
                            user.Surname = reader["Surname"].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return user;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();

            string sql = "Select * From Users";
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserID = reader["UserID"].ToString(),
                                Password = reader["userPassword"].ToString(),
                                AccessGroup = Convert.ToInt32(reader["AccessGroup"]),
                                Name = reader["Firstname"].ToString(),
                                Surname = reader["Surname"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return users;
        }

        public static int GetTestID()
        {
            int id;
            string sql = "Select MAX([testID]) From Tests;";
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    
                    connection.Open();
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return id;
        }

        public static int GetQuestID()
        {
            int id;
            string sql = "Select MAX([questionID]) From Questions;";
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    connection.Open();
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return id;
        }

        public static ObservableCollection<Question> GetQuestions(int testID)
        {
            ObservableCollection<Question> questions = new ObservableCollection<Question>();
            string sql = "Select * From Questions where [testID] = @id;";
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", testID);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            ansA = reader["AnswerA"].ToString(),
                            ansB = reader["AnswerB"].ToString(),
                            ansC = reader["AnswerC"].ToString(),
                            ansD = reader["AnswerD"].ToString(),
                            correctAns = reader["CorrectAnswer"].ToString(),
                            Quest = reader["Question"].ToString(),
                            QuestionID = Convert.ToInt32(reader["questionID"].ToString()),
                            TestID = Convert.ToInt32(reader["testID"].ToString())
                        });
                    }
                }
            }

            return questions;
        }

        public static List<Mark> GetMarks(int testID)
        {
            List<Mark> marks = new List<Mark>();

            string sql = "Select * From Marks;";// where [testID] = @id;";
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //command.Parameters.AddWithValue("@id", testID);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        marks.Add(new Mark
                        {
                            MarkID = Convert.ToInt32(reader["markID"].ToString()),
                            UserID = reader["userID"].ToString(),
                            TestID = Convert.ToInt32(reader["testID"].ToString()),
                            MarkInt = Convert.ToInt32(reader["Mark"].ToString()),
                            MarkPercent = Convert.ToInt32(reader["Percentage"].ToString())
                        });
                    }
                }
            }

            return marks;
        }

        public static void DeleteFromDB(String table, String column, int key)
        {
            string sql = "Delete From " + table + " Where " + column + " = " + key;
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The application is unable to connect to the database. Please contact your System Admin.\n\nError Message:\n\n" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static List<Test> GetTests()
        {
            List<Test> tests = new List<Test>();
            string sql = "Select * From Tests";
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        tests.Add(new Test
                        {
                            TestID = Convert.ToInt32(reader["testID"].ToString()),
                            NumberOfQuestions = Convert.ToInt32(reader["NumOfQuestions"].ToString()),
                            TestName = reader["TestName"].ToString(),
                            UserID = Convert.ToInt32(reader["userID"].ToString())
                        });
                    }
                }
            }

            return tests;
        }

        public static void InsertToDB(Test test)
        {
            string sql = @"IF EXISTS(SELECT * FROM Tests WHERE testID = @testID)
                        UPDATE Tests 
                        SET testID = @testID, TestName = @TestName, NumOfQuestions = @NumOfQuestions, userID = @userID
                        WHERE testID = @testID
                    ELSE
                        INSERT INTO Tests(testID, TestName, NumOfQuestions, userID) VALUES(@testID, @TestName, @NumOfQuestions, @userID); ";
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@testID", test.TestID);
                        command.Parameters.AddWithValue("@TestName", test.TestName);
                        command.Parameters.AddWithValue("@NumOfQuestions", test.NumberOfQuestions);
                        command.Parameters.AddWithValue("@userID", test.UserID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The application is unable to connect to the database. Please contact your System Admin.\n\nError Message:\n\n" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void InsertToDB(Question question)
        {
            string sql = @"IF EXISTS(SELECT * FROM Questions WHERE questionID = @questionID)
                        UPDATE Questions 
                        SET questionID = @questionID, Question = @Question, testID= @testID, AnswerA = @AnswerA, AnswerB = @AnswerB, AnswerC = @AnswerC, AnswerD = @AnswerD, CorrectAnswer = @CorrectAnswer
                        WHERE questionID = @questionID
                    ELSE
                        INSERT INTO Questions(questionID, Question, testID, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer) VALUES(@questionID, @Question, @testID, @AnswerA, @AnswerB, @AnswerC, @AnswerD, @CorrectAnswer); ";
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@questionID", question.QuestionID);
                        command.Parameters.AddWithValue("@Question", question.Quest);
                        command.Parameters.AddWithValue("@testID", question.TestID);
                        command.Parameters.AddWithValue("@AnswerA", question.ansA);
                        command.Parameters.AddWithValue("@AnswerB", question.ansB);
                        command.Parameters.AddWithValue("@AnswerC", question.ansC);
                        command.Parameters.AddWithValue("@AnswerD", question.ansD);
                        command.Parameters.AddWithValue("@CorrectAnswer", question.correctAns);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The application is unable to connect to the database. Please contact your System Admin.\n\nError Message:\n\n" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
