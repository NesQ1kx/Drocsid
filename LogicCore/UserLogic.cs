using DalContracts;
using Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                IsBaned = false,
                Role = "user",
                Email = email,
                RegDate = DateTime.Now
            };

            _userDao.Add(user);

        }

        public bool CheckUser(string username, string password)
        {
            User user = _userDao.GetUserByLogin(username);
            if (_userDao.UserExist(username, password) && user.ConfirmedEmail) return true;
            return false;
        }


        public bool CheckUserReg(string username)
        {
            var users = _userDao.GetUsers();
            foreach(var u in users)
            {
                if (username == u.Username) return true;
            }
            return false;
        }
        
        public User GetUserByLogin(string userName)
        {
            return _userDao.GetUserByLogin(userName);
        }

        public List<Comment> GetUserComments(string userName)
        {
            return _userDao.GetUserComments(userName);
        }

        public void ConfirmEmail(string userName)
        {
            _userDao.ConfirmEmail(userName);
        }

        public string GenerateSalt(int length)
        {
            var rng = new RNGCryptoServiceProvider();
            var salt = new byte[length];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public bool UserExist(int id)
        {
            var users = _userDao.GetUsers();
            foreach(var u in users)
            {
                if (id == u.Id) return true;
            }

            return false;
        }

        public void EditPassword(int id, string password)
        {
            _userDao.EditPassword(id, password);
        }

        public void EditName(int id, string username)
        {
            _userDao.EditName(id, username);
        }

        public User GetUser(int id)
        {
            return _userDao.GetUser(id);
        }

        public void UnbanUser(int id)
        {
            _userDao.UnbanUser(id);
        }

        public void BanUser(int id)
        {
            _userDao.BanUser(id);
        }

        public void Increase(int id)
        {
            _userDao.Increase(id);
        }

        public void Decrease(int id)
        {
            _userDao.Decrease(id);
        }
    }
}
