using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Common.Interfaces.Entity
{
    public interface IUser
    {
        string Id { get; set; }
        string Nike { get; set; }
        string Password { get; set; }
        UserRole Role { get; set; }
    }
}
