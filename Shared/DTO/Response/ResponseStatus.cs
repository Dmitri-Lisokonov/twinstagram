using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response
{
    public enum ResponseStatus
    {
        Success,
        Error,
        NotFound,
        Unauthorized,
        BadRequest,
        NotModified
    }
}
