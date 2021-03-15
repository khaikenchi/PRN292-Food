using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagerApp
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool role { get; set; }

        public bool status { get; set; }

        public User() { }

        public User(string username, string password, string fullname, string address, string email, string phone, bool role, bool status)
        {
            this.username = username;
            this.password = password;
            this.fullname = fullname;
            this.address = address;
            this.email = email;
            this.phone = phone;
            this.role = role;
            this.status = status;
        }
    }
}
