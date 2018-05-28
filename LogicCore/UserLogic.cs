using DalContracts;
using Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCore
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;
        public UserLogic(IUserDao userDao)
        {
            _userDao = userDao;
        }
        public void AddUser(string username, string password, string email)
        {
            User user = new User()
            {
                Username = username,
                Password = password,
                Email = email
            };

            _userDao.Add(user);

        }

        public bool CheckUser(string username, string password) => _userDao.UserExist(username, password);
        

        public bool CheckUserReg(string username)
        {
            var users = _userDao.GetUsers();
            foreach(var u in users)
            {
                if (username == u.Username) return true;
            }
            return false;
        }
        
        
    }
}
