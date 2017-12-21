using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Socket.Enum;

namespace Common.Socket.Communication
{
   public class Notification<T>
    {
        public NotificationActions Actions { get; set; }
        public T Data { get; set; }
    }
}
