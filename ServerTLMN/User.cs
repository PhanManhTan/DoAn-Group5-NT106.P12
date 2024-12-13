using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTLMN
{
    internal class User
    {
        public TcpClient Client { get; set; }
        public string Username { get; set; }

        public string ID { get; set; }
        public StreamReader Reader { get; set; }
        public StreamWriter Writer { get; set; }
        public byte[] Avatar { get; set; }
        public User(TcpClient client)
        {
            this.Client = client;
            this.Username = string.Empty;
            NetworkStream stream = Client.GetStream();
            this.Reader = new StreamReader(stream);
            this.Writer = new StreamWriter(stream);
            this.Avatar = null;
            this.ID = string.Empty;
        }

    }
}
