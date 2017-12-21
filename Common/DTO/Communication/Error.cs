using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Communication
{
    public class Error
    {
        public int ErrorCode { get; private set; }
        public string ErrorDescriprion { get; set; }

        public Error() {
        }

        public Error(string description)
        {
            ErrorDescriprion = description;
            ErrorCode = 500;
        }
        public Error(int errorCode, string description)
        {
            ErrorDescriprion = description;
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"Status code:{ErrorCode}, \nDescription:{ErrorDescriprion}";
        }
    }
}
