using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Interfaces.Entity
{
    public interface IToken
    {
        string Id { get; set; }
        string UsserId { get; set; }
        UserEntity User { get; set; }
        DateTime ExpirationDateTime { get; set; }
    }
}
