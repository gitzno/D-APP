using D_APP_Core.Entities;
using D_APP_Core.Entities.Program;
using D_APP_Core.Interfaces.IRepositories;
using D_APP_Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public string DecodePassword(string password)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public ServiceResult getAllUser()
        {
            var users = _repository.GetUsers();
            return new ServiceResult()
            {
                devMsg = "Get all Users successfully!",
                userMsg = "Lấy toàn bộ người dùng thành công!",
                statusCode = HttpStatusCode.OK,
                data = users
            };
        }
        public Guid getUserIdByUserName(string username)
        {
            var user = _repository.getUserByUsername(username);
            return user.UserId;
        }
        public ServiceResult getUserByUserName(string userz)
        {
            var user = _repository.getUserByUsername(userz);
            if (user == null)
            {

                return new ServiceResult()
                {
                    devMsg = "Get User Information Failed",
                    userMsg = "Lấy thông tin người dùng thất bại~",
                    statusCode = HttpStatusCode.BadRequest,
                    data = user
                };
            }
            return new ServiceResult()
            {
                devMsg = "Get User Information",
                userMsg = "Lấy thông tin người dùng thành công",
                statusCode = HttpStatusCode.OK,
                data = user
            };
        }

        public ServiceResult LoginService(string username, string password)
        {
            username = username.Trim();
            password = password.Trim();
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return new ServiceResult()
                {
                    devMsg = "Username hoặc password can not empty",
                    userMsg = "Không được để tài khoản hoặc mật khẩu trống!",
                    statusCode = HttpStatusCode.NotFound,
                    data = new { username, password }
                };

            }
            var user = _repository.GetUserByLogin(username, DecodePassword(password));
            if (user == null)
            {
                return new ServiceResult()
                {
                    devMsg = "Username không tồn tại",
                    userMsg = "Tài khoản không tồn tại",
                    statusCode = HttpStatusCode.NotFound,
                    data = new { username, password }
                };
            }
            if (!String.IsNullOrEmpty(user.UserName) && String.IsNullOrEmpty(user.UserPassword))
            {
                return new ServiceResult()
                {
                    devMsg = "Password Wrong",
                    userMsg = "Mật khẩu sai",
                    statusCode = HttpStatusCode.NotFound,
                    data = user
                };
            }
            return new ServiceResult()
            {
                devMsg = "Login successfully",
                userMsg = "Đăng nhập thành công!",
                statusCode = HttpStatusCode.OK,
                data = user
            };
        }

        public ServiceResult Register(User user)
        {
            bool prime = false;

            if (String.IsNullOrEmpty(user.UserFname))
            {
                user.UserFname = "Họ không được để trống!";
                prime = true;
            }
            else if (!Validate.OnlyText(user.UserFname))
            {
                prime = true;
                user.UserFname = "Họ không được có ký tự đặc biệt!";
            }
            if (String.IsNullOrEmpty(user.UserLname))
            {
                prime = true;
                user.UserLname = "Tên không được để trống!";
            }
            else if (!Validate.OnlyText(user.UserLname))
            {
                prime = true;
                user.UserLname = "Tên không được có ký tự đặc biệt!";
            }
            if (String.IsNullOrEmpty(user.UserEmail))
            {
                prime = true;
                user.UserEmail = "Tên không được để trống!";
            }
            else if (!Validate.Email(user.UserEmail))
            {
                user.UserEmail = "Email không đúng định dạng";
                prime = true;
            }
            if (_repository.CheckUserName(user.UserName))
            {
                prime = true;
                user.UserName = "Tài Khoản đã tồn tại!";
            }
            else if (!Validate.UserName(user.UserName))
            {
                prime = true;
                user.UserName = "Tài Khoản có thể có: \n" +
                    "ký tự thường!\n" +
                    "ký tự hoa!\n" +
                    "ký tự đặc biệt!\n" +
                    "Độ dài từ 8 đến 32 ký tự!";
            }
            if (String.IsNullOrEmpty(user.UserPhoneNumber))
            {

            }
            else if (!Validate.Phonenumber(user.UserPhoneNumber))
            {
                prime = true;
                user.UserPhoneNumber = "Hãy nhập đúng số điện thoại!";
            }
            if (!Validate.Password(user.UserPassword))
            {
                prime = true;
                user.UserPassword = "Mật Khẩu phải có: /n" +
                    "Ít nhất 1 ký tự thường! /n" +
                    "Ít nhất 1 ký tự hoa! /n" +
                    "Ít nhất 1 ký tự đặc biệt! /n" +
                    "Độ dài từ 8 đến 32 ký tự!";
            }

            if (!prime)
            {

                user.UserId = Guid.NewGuid();
                user.UserCreatedDate = DateTime.Now;

                user.UserModifiDate = DateTime.Now;

                user.UserRole = 0;
                /*
                 0: user not valid
                 1: user valid 
                 */
                user.UserStatus = 0;
                user.UserPassword = DecodePassword(user.UserPassword);
                var newUser = _repository.CreateUser(user);
                return new ServiceResult()
                {
                    devMsg = "Register successfully!",
                    userMsg = "Đăng ký thành công!",
                    statusCode = HttpStatusCode.OK,
                    data = newUser
                };
            }
            else
            {
                return new ServiceResult()
                {
                    devMsg = "Register Failed!",
                    userMsg = "Đăng ký thất bại!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = user
                };

            }
        }

        public ServiceResult updateProfile(string username, UserInfo userUpdate)
        {
            var userId = getUserIdByUserName(username);
            var res = _repository.updateProfileUser(userId, userUpdate);
            if (res == 0)
            {

                return new ServiceResult()
                {
                    devMsg = "Update Failed!",
                    userMsg = "Cập nhật thất bại!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = userUpdate
                };
            }
            return new ServiceResult()
            {
                devMsg = "Update Profile Successfully!",
                userMsg = "Cập nhật thông tin người dùng thành công!",
                statusCode = HttpStatusCode.OK,
                data = userUpdate
            };
        }

        public ServiceResult ValidateAccount(Guid iDUser)
        {
            var s = _repository.ValidateUserByID(iDUser);
            if (s == 0)
            {
                return new ServiceResult()
                {
                    devMsg = "Validate account faild",
                    userMsg = "Xác thực thất bại!",
                    statusCode = HttpStatusCode.BadRequest,
                    data = s
                };
            }
            return new ServiceResult()
            {
                devMsg = "Validate account successfully",
                userMsg = "Xác thực thành công!",
                statusCode = HttpStatusCode.OK,
                data = s
            };
        }
     
    }
}
