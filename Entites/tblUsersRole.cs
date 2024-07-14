using System;
using System.Collections.Generic;
using System.Text;

namespace Entites
{
    public class tblUsersRoles
    {
        public int ID { get; set; }
        public string UserRole { get; set; }
        public ICollection<tblUsers> tblUsers { get; set; }
    }
}
