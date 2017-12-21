using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.TokenDTO
{
    public class TokenDTO
    {
        public string TokenString { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}
