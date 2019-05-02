using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySimpleBlog.Data
{
    public class User : IComparable
    {
        public string Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public List<Post> Posts { get; set; }

        public int NumberOfPosts => Posts.Count;

        public int CompareTo(object obj)
        {
            User otherUser = (User)obj;
            return NumberOfPosts.CompareTo(otherUser.NumberOfPosts);
        }
    }
}
