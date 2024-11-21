using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienLenDoAn
{
    internal class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string RoomID { get; set; }
        public User() { }
        public User(string name, string iD, string roomID)
        {
            Name = name;
            ID = iD;
            RoomID = roomID;
        }
    }
}
