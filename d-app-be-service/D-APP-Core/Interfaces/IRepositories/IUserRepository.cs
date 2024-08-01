using D_APP_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Check username is valid?
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool CheckUserName(string? userName);

        /// <summary>
        /// Create user with inforamtion
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User CreateUser(User user);

        /// <summary>
        /// Get user by username or email and check password is correct
        /// </summary>
        /// <param name="username">username or email of user</param>
        /// <param name="password">password of user</param>
        /// <returns>
        /// null - if username or email cant match any user
        /// User have only username - if user wrong password
        /// user - username and password correct
        /// </returns>
        public User GetUserByLogin(string username, string password);

        /// <summary>
        /// Get all users 
        /// </summary>
        /// <returns>List of user</returns>
        public IEnumerable<User> GetUsers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDUser"></param>
        /// <returns></returns>
        public int ValidateUserByID(Guid iDUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User? getUserByID(Guid id);

        /// <summary>
        /// get user by username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User? getUserByUsername(string user);
        
        /// <summary>
        /// update profile of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int updateProfileUser(Guid userid, UserInfo user);
    }
}
