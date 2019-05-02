using System.Collections.Generic;

namespace MySimpleBlog.Data
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
