using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Packet
    {
        

        public string Code { get; set; }
        public string Username { get; set; }
        public string RoomID { get; set; }
        public string ID {  get; set; }
        public List<string> cards { get; set; } = new List<string>();
        public byte[] ArrayByte { get; set; }

    }
}
