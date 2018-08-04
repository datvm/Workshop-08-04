using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Api.Models.ViewModels
{

    public class GoogleAuthResponseViewModel
    {
        public string Url { get; set; }
    }

    public class GoogleLoginResponseViewModel
    {
        public string Token { get; set; }
    }

}
