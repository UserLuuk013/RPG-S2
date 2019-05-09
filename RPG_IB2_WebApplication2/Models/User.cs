using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class User
    {
        public User(int id, string username, string email)
        {
            this.Id = id;
            this.UserName = username;
            this.Email = email;
        }

        public User(int id, string username, string email, string password)
        {
            this.Id = id;
            this.UserName = username;
            this.Email = email;
            this.Password = password;
        }
        public User()
        {

        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string Password { get; set; }
    }
}
