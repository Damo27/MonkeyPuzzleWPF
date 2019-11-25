using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyPuzzleWPF.Models
{
    class Question
    {
        public int QuestionID { get; set; }

        public int TestID { get; set; }

        public string Quest { get; set; }

        public string ansA { get; set; }

        public string ansB { get; set; }

        public string ansC { get; set; }

        public string ansD { get; set; }

        public string correctAns { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:\n A) {1}\n B) {2}\n C) {3}\nD) {4}\nCorrect: {5}",Quest, ansA, ansB, ansC, ansD, correctAns);
        }
    }
}
