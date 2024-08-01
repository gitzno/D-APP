using D_APP_Core.Entities;
using D_APP_Core.Entities.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Interfaces.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// Login service
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>
        /// a account for user
        /// </returns>
        public ServiceResult LoginService(string username, string password);

        /// <summary>
        /// Decode pasword with MD5
        /// </summary>
        /// <param name="password"></param>
        /// <returns>
        /// a string is password decode
        /// </returns>
        public string DecodePassword(string password);

        /// <summary>
        /// get all a user
        /// </summary>
        /// <returns></returns>
        public ServiceResult getAllUser();

        /// <summary>
        /// register for user
        /// </summary>
        /// <param name="user">
        /// information of user
        /// </param>
        public ServiceResult Register(User user);

        /// <summary>
        /// Validate account with email
        /// </summary>
        /// <param name="iDUser"></param>
        /// <returns></returns>
        public ServiceResult ValidateAccount(Guid iDUser);

        /// <summary>
        /// get user by username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ServiceResult getUserByUserName(string user);

        /// <summary>
        /// update profile for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ServiceResult updateProfile(string username, UserInfo userUpdate);
       
    }
}
