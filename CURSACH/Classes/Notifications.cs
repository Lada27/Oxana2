using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURSACH.Classes
{
    public class Notifications
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
