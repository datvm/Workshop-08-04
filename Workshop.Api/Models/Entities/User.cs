using System;
using System.Collections.Generic;

namespace Workshop.Api.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Tweet = new HashSet<Tweet>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string GoogleId { get; set; }

        public ICollection<Tweet> Tweet { get; set; }
    }
}
