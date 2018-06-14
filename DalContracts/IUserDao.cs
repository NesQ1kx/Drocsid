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
        User GetUserByLogin(string userName);
        List<Comment> GetUserComments(string userName);
        void ConfirmEmail(string userName);
        void EditPassword(int id, string password);
        void EditName(int id, string username);
        User GetUser(int id);
        void UnbanUser(int id);
        void BanUser(int id);
    }

}
