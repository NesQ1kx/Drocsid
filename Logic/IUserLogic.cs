using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IUserLogic
    {
        void AddUser(string username, string password, string email);
        bool CheckUserReg(string username);
        bool CheckUser(string username, string password);
        User GetUserByLogin(string userName);
        List<Comment> GetUserComments(string userName);
        void ConfirmEmail(string userName);
        string GenerateSalt(int length);
        bool UserExist(int id);
        void EditPassword(int id, string password);
        void EditName(int id, string username);
        User GetUser(int id);
        void UnbanUser(int id);
        void BanUser(int id);
        void Increase(int id);
        void Decrease(int id);
    }
}
