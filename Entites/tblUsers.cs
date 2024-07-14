using System;

namespace Entites
{
    public class tblUsers
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
        public tblUsersRoles tblUsersRoles { get; set; }
    }
}
