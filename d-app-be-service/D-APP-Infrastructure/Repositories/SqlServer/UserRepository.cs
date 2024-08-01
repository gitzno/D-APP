using D_APP_Core.Entities;
using D_APP_Core.Interfaces.IRepositories;
using D_APP_Infrastructure.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DAppContext _context;

        public UserRepository(DAppContext context)
        {
            _context = context;
        }

        public bool CheckUserName(string? userName)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserName == userName);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            var res = _context.SaveChanges();
            if (res < 0)
            {
                return null;
            }
            return user;
        }

        public User GetUserByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByLogin(string username, string password)
        {
            //get user with email or user name
            var user = _context.Users.FirstOrDefault(user => user.UserName == username || user.UserEmail == username);

            // if cant not find user return null
            if (user == null)
            {
                return null;
            }

            // if password wrong return user have only username
            if (user.UserPassword != password)
            {
                return new User()
                {
                    UserName = username,
                };
            }

            // if username or email correct and password is correct return user
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public int ValidateUserByID(Guid iDUser)
        {
            var user = getUserByID(iDUser);
            if (user == null)
            {
                return 0;
            }
            user.UserStatus = 1;
            return _context.SaveChanges();
        }


        public User? getUserByID(Guid id)
        {
            return _context.Users.FirstOrDefault(user => user.UserId == id);
        }

        public User? getUserByUsername(string userz)
        {
            return _context.Users.FirstOrDefault(user => user.UserName == userz);
        }

        public int updateProfileUser(Guid userid, UserInfo user)
        {
            DateTime dateTime = user.UserDateOfBirth.ToDateTime(TimeOnly.MinValue);

            // You can also specify the time and DateTimeKind
            TimeOnly specificTime = TimeOnly.Parse("10:30:00");
            DateTimeKind dateTimeKind = DateTimeKind.Utc;  // Or DateTimeKind.Local

            dateTime = user.UserDateOfBirth.ToDateTime(specificTime, dateTimeKind);

            var userNew = _context.Users.FirstOrDefault(user => user.UserId == userid);
            if (userNew != null)
            {
                userNew.UserFname = user.UserFname;
                userNew.UserLname = user.UserLname;
                userNew.UserEmail = user.UserEmail;
                userNew.UserName = user.UserName;
                userNew.UserDateOfBirth = dateTime;
                userNew.UserModifiDate = DateTime.Now;
                userNew.UserPhoneNumber = user.UserPhoneNumber;
            }
            return _context.SaveChanges();

        }
    }
}
