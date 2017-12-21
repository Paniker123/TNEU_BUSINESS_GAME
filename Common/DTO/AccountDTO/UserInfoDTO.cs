using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Common.DTO.AccountDTO
{
    public class UserInfoDTO
    {
        public string Nike { get; set; }
        public UserRole UserRole { get; set; }

        public UserInfoDTO(string nike, UserRole userRole)
        {
            Nike = nike;
            UserRole = userRole;
        }
    }
}
