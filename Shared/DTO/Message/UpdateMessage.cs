using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Message
{
    internal class UpdateMessage
    {
        public Guid? Description { get; set; }

        public string? Image { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
