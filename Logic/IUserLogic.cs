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
    }
}
