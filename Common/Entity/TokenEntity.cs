using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Interfaces.Entity;

namespace Common.Entity
{
   
    public class TokenEntity:IToken
    {

        [Key]
        public string Id { get; set; }
 
        public UserEntity User { get; set; }

        public string UsserId { get; set; }

        public DateTime ExpirationDateTime { get; set; }



    }
}
