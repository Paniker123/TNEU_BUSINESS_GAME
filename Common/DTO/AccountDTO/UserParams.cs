using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Common.DTO.AccountDTO
{
    public class UserParams
    {
        public OrderBy Asc { get; set; } = OrderBy.Descending;
        
        public UserRole SortesType { get; set; } = UserRole.User;

        public string Nike { get; set; } = string.Empty;

        public int Skip { get; set; } = 0;

        [Range(1, int.MaxValue)]
        public int Range { get; set; } = 10;
    }
}
