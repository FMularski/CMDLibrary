using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary
{
    class User
    {
        public enum RoleType
        {
            Regular,
            Administrator
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public List<Book> RentedBooks { get; set; }

    }
}
