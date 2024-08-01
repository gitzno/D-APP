using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_APP_Core.Entities
{
    public class UserInfo
    {
        public string UserFname { get; set; }
        public string UserLname { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public DateOnly UserDateOfBirth { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
