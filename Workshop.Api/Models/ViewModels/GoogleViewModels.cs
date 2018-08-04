using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Api.Models.ViewModels
{
    
    public class GoogleUserInfoViewModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("sub")]
        public string GoogleId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
