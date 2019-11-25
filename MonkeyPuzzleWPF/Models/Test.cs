using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyPuzzleWPF.Models
{
    public class Test
    {
        public int TestID { get; set; }

        public String TestName { get; set; }

        public int NumberOfQuestions { get; set; }

        public int UserID { get; set; }

        public override string ToString()
        {
            return TestName;
        }
    }
}
