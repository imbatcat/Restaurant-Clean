using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Users.Dtos
{
    public class UserDto
    {
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string UserName { get; set; }
    }
}
