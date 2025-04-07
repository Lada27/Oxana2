using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CURSACH
{
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string Email { get; set; }
        public static string UserName { get; set; }
        public static string Phone { get; set; }

        public static string Role { get; set; }
        public static byte[] Photo { get; set; }
        public static string Password { get; set; }

        public static void ClearUser()
        {
            UserId = 0;
            Email = null;
            UserName = null;
            Role = null;
            Photo = null;
            Password = null;

        }
    }
}

