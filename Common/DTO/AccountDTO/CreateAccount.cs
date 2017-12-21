using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Common.DTO.AccountDTO
{
  public class CreateAccount
    {
        public string Nike { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
