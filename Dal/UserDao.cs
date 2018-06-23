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

        public User GetUserByLogin(string userName)
        {
            User user;
            using (var db = new SampleContext())
            {
                return user = db.Users.Where(u => u.Username == userName).FirstOrDefault();
            }
        }

        public List<Comment> GetUserComments(string userName)
        {
            List<Comment> comments;
            using (var db = new SampleContext())
            {
                return comments = db.Comments.Where(c => c.Author == userName).ToList();
            }
        }

        public void ConfirmEmail(string userName)
        {
            using (var db = new SampleContext())
            {
                var user = db.Users.Where(u => u.Username == userName).FirstOrDefault();
                user.ConfirmedEmail = true;
                db.SaveChanges();
            }
        }

        public void EditPassword(int id, string password)
        {
            using (var db = new SampleContext())
            {
                var user = db.Users.Find(id);
                user.Password = password;
                db.SaveChanges();
            }
        }

        public void EditName(int id, string username)
        {
            using (var db = new SampleContext())
            {
                var user = db.Users.Find(id);
                var comments = db.Comments.Where(c => c.AuthorId == id).ToList();
                var topics = db.Topics.Where(t => t.AuthorId == id).ToList();
                if (comments != null)
                {
                    foreach(var c in comments)
                    {
                        c.Author = username;
                    }
                }
                if(topics != null)
                {
                    foreach(var t in topics)
                    {
                        t.Author = username;
                    }
                }
                user.Username = username;

                db.SaveChanges();
            }
        }

        public User GetUser(int id)
        {
            User user;
            using (var db = new SampleContext())
            {
                return user = db.Users.Find(id);
            }
        }

        public void UnbanUser(int id)
        {
            using (var db = new SampleContext())
            {
                var user = db.Users.Find(id);
                user.IsBaned = false;
                db.SaveChanges();
            }
        }

        public void BanUser(int id)
        {
            using (var db = new SampleContext())
            {
                var user = db.Users.Find(id);
                user.IsBaned = true;
                db.SaveChanges();
            }
        }

        public void Increase(int id)
        {
            using (var db = new SampleContext())
            {
                var user = db.Users.Find(id);
                user.Role = "admin";
                db.SaveChanges();
            }
        }

        public void Decrease(int id)
        {
            using (var db = new SampleContext())
            {
                var user = db.Users.Find(id);
                user.Role = "user";
                db.SaveChanges();
            }
        }
    }
}
