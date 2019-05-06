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

        public int GetNumberOfPosts()
        {
            return this.Posts.Count;
        }

        [Display(Name = "Number of posts")]
        public int NumberOfPosts
        {
            get
            {
                return this.Posts.Count;
            }
        }

        public int CompareTo(object obj)
        {
            User otherUser = (User)obj;


            return GetNumberOfPosts().CompareTo(otherUser.GetNumberOfPosts());
        }
    }
}
