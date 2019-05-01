using System;
using System.ComponentModel.DataAnnotations;

namespace MySimpleBlog.Data
{
    public class Post
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Display(Name = "Comment")]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created on ")]
        public DateTime CreationDate { get; set; }

    }
}