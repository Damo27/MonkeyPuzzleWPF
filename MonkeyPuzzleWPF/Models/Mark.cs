using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyPuzzleWPF.Models
{
    class Mark
    {
        public int MarkID { get; set; }

        public String UserID { get; set; }

        public String UserName { get; set; }

        public int TestID { get; set; }

        public int MarkInt { get; set; }

        public int MarkPercent { get; set; }

        public override string ToString()
        {
            int space = 43-(UserID + UserName + MarkPercent.ToString()).Length;
            string spaceStr = "";
            for (int i = 0; i < space; i++)
            {
                spaceStr += " ";
            }
            return string.Format("{0}\t {1}{3}{2}%", UserID, UserName, MarkPercent, spaceStr);
        }
    }
}
