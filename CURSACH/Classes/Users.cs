using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURSACH
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }  
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; } 
        public byte[] Photo { get; set; }

      
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string Title { get; set; }
    }
}
