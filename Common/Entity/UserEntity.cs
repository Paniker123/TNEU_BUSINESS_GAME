using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;
using Common.Interfaces.Entity;

namespace Common.Entity
{
   
    public class UserEntity:IUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Nike { get; set; }
      
        public string Password { get; set; }
    
        public UserRole Role { get; set; }

        public ICollection<PlayerForGameEntity> UserPlayerEntity { get; set; }=new List<PlayerForGameEntity>();


    }
}
