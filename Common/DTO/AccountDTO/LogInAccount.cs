using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Common.DTO.AccountDTO
{
    public class LogInAccount
    {
        public string Nike { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }

        public LogInAccount()
        {
        }

        public LogInAccount(string nike, string password, UserRole userRole)
        {
            Nike = nike;
            Password = password;
            UserRole = userRole;
        }
    }
}
