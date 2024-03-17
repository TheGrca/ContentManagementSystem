using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content_Management_System.Class
{
    public enum UserRole {Visitor, Admin}
    internal class User
    {
        private string username {  get; set; }
        private string password { get; set; }
        private UserRole role { get; set; } 
        
    }
}
