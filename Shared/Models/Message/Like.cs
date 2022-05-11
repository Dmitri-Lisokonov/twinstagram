using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Message
{
    public class Like
    {
        public int Id { get; private set; }
        public int MessageId { get; private set; }
        public int UserId { get; private set; }
        public DateTime Date { get; private set; }
    }
}
