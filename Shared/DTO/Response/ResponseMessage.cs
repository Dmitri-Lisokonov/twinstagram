using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response
{
    public class ResponseMessage<T> where T : class
    {
        public T Data { get; private set; }
        public string Status { get; private set; }
        
        public ResponseMessage(T message, string status)
        {
            Data = message;
            Status = status;
        }
    }
}
