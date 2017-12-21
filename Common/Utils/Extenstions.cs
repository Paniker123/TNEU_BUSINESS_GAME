using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class Extenstions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.Identity.Name;
        }
    }
}
