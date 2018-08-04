using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Api.Models
{

    public class ApiSettings
    {

        public GoogleOAuth2Settings GoogleOAuth2 { get; set; }
        public JwtSettings Jwt { get; set; }

        public class GoogleOAuth2Settings
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string RedirectUrl { get; set; }
        }

        public class JwtSettings
        {
            public string Audience { get; set; }
            public string Issuer { get; set; }
            public string SecurityKey { get; set; }
        }

    }

}
