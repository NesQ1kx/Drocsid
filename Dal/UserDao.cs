using DalContracts;
using Entities;
using System;
using System.Collections.Generic;
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
    }
}
