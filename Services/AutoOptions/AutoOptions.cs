using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Services.AutoOptions
{
   public class AutoOptions
   {
       public const string ISSUER = "fdf_production_web_ip_server";
       public const string AUDIENS = "fdf_production_web_ip_funats";
       const string KEY = "Say_who_is_your_friend_I_say_who_is_you";
       public const int LIFETIME = 60 * 24;

       public static SymmetricSecurityKey GetSymmetricSecurityKey()
       {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
       }
   }
}
