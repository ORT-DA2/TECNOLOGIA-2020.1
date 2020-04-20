using System;
using System.Collections.Generic;
using System.Linq;

namespace ej_filters.auth
{
    public class Auth
    {
        private List<User> Users { get; set; }
        public Auth()
        {
            Users = new List<User>();
            Users.Add(new User() { Nickname = "nicof", Password = "uru", Token = "asd.nico.qwe" });
            Users.Add(new User() { Nickname = "aler", Password = "esp", Token = "asd.ale.qwe" });
        }
        public string Login(User user)
        {
            User u = Users.Where(x => x.Nickname == user.Nickname && x.Password == user.Password).FirstOrDefault();
            if (u == null) return "";
            else return u.Token;
        }
        public bool IsLogued(string token)
        {
            return Users.Exists(x => x.Token == token);
        }
    }
}
