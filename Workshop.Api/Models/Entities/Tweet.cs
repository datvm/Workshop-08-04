using System;
using System.Collections.Generic;

namespace Workshop.Api.Models.Entities
{
    public partial class Tweet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedTime { get; set; }

        public User User { get; set; }
    }
}
