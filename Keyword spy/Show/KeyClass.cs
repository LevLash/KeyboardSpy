using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Show
{
    public class KeyClass
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Time { get; set; }
        public string UpDown { get; set; }
        public string PC { get; set; }
        public string Status { get; set; }


        public KeyClass(int id, string key, string time, string pc, string upDown)
        {
            Id = id;
            Key = key;
            Time = time;
            UpDown = upDown;
            PC = pc;
        }

        public KeyClass(int id, string pc, string status)
        {
            Id = id;
            PC = pc;
            Status = status;
        }
    }
}
