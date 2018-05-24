using DalContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class UserDao : IUserDao
    {
        public void Add(User user)
        {
           using (var db = new SampleContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        
        public List<User> GetUsers()
        {
            using (var db = new SampleContext())
            {
                return db.Users.ToList();
            }
        }

        public bool UserExist(string username, string password)
        {
            using (var db = new SampleContext())
            {
                var users = db.Users.Select(c => new
                {
                    login = c.Username,
                    pas = c.Password
                });

                foreach(var u in users)
                {
                    if (u.login == username && u.pas == password) return true;
                }
            }
            return false;
        }
    }
}
