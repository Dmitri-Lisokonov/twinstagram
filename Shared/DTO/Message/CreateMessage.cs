using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Message
{
    public class CreateMessage
    {
        public Guid? UserId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
