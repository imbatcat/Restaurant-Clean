using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Ultilities.Identity.Authentication
{
    public class AuthenticationResponse
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
