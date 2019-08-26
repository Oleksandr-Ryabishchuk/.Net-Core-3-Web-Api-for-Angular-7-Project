using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
