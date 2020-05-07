using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestAssembly
{
    public class MyComputer : IComputer
    {
        private Dictionary<string, string> users = new Dictionary<string, string>();
        public IEnumerable<string> Users => users.Select(x => x.Key);

        public string MyName;
        private bool Working = true;

        public List<FileInfo> GetFiles()
        {
            return new List<FileInfo>()
            {
                new FileInfo("a"), new FileInfo("b"), new FileInfo("c")
            };
        }

        public bool LogIn(string user, string password)
        {
            if(users.ContainsKey(user))
            {
                var actualPwd = users[user];
                return actualPwd == password;
            }

            return false;
        }

        public void Shutdown()
        {
            Working = false;
        }
    }
}
