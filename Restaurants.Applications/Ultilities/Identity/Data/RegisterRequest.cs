using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Ultilities.Identity.Data
{
    public sealed class RegisterRequest
    {
        public required string Email { get; set; } = default!;
        public required string Username { get; set; } = default!;
        public required string Password { get; set; } = default!;
        public required string Birthdate { get; set; } = default!;
        public required string PhoneNumber { get; set; } = default!;
    }
}
