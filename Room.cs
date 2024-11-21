using System.Collections.Generic;

namespace Server
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
        