using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts
{
    public interface IUserDao
    {
        void Add(User user);
        List<User> GetUsers();
        bool UserExist(string username, string password);
    }

}
