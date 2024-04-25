using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class PostTagMap
    {
        // Define model post tag map in here

        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }


    }
}
