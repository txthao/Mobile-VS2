using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core
{
    public class User
    {
        string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        string hoten;

        public string Hoten
        {
            get { return hoten; }
            set { hoten = value; }
        }
        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
