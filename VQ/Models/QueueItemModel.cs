using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VQ.Models
{
    public class QueueItemModel
    {
        public int BusinessId { get; set; }
        public int UserId { get; set;}
        public string JoinTimestamp { get; set;}
        public string ExitTimestamp { get; set;} 

    }
}
