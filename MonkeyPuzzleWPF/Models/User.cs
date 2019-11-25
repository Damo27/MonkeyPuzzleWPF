using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using MonkeyPuzzleWPF.Utilities;

namespace MonkeyPuzzleWPF.Models
{
    public class User
    {
        private string userID;
        private string password;
        private string _name;
        private string _surname;
        private int _accessGroup;

        [Required]
        public String UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        public String Name { get => _name; set => _name = value; }
        public String Surname { get => _surname; set => _surname = value; }

        [Required]
        public String Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public int AccessGroup { get => _accessGroup; set => _accessGroup = value; }


        #region Operator override
        //------------------Override the comparison operators to compare users----------------------------
        public static bool operator ==(User userOne, User userTwo)
        {
            if (System.Object.ReferenceEquals(userOne, null) || System.Object.ReferenceEquals(userTwo, null))
            {
                return false;
            }
            else
            {
                if (userOne.UserID.Equals(userTwo.UserID) && userOne.Password.Equals(userTwo.Password))
                {
                    return true;
                }
                return false; 
            }

        }

        public static bool operator !=(User userOne, User userTwo)
        {
            return !(userOne == userTwo);
        }
        #endregion Operator override

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Surname);
        }
    }
}
