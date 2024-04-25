using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string CommentHeader { get; set; }
        [StringLength(1024)]
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
        public int PostId { get; set; }

        public Post Post { get; set; }

    }
}
