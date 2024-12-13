using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTLMN
{
    internal class Room
    {
        public int roomID;
        public List<User> userList = new List<User>();

        public string GetUsernameListInString()
        {
            List<string> usernames = new List<string>();
            foreach (User user in userList)
            {
                usernames.Add(user.Username);
            }
            string[] s = usernames.ToArray();
            string res = string.Join(",", s);

            return res;
        }
    }
}

