using FA.JustBlog.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

public class Post
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255)]
    public string Title { get; set; }

    [StringLength(255)]
    public string ShortDescription { get; set; }
    [StringLength(1045)]
    public string PostContent { get; set; }

    [StringLength(255)]
    public string UrlSlug { get; set; }
    public bool Published { get; set; }
    public DateTime PostedOn { get; set; }
    public DateTime Modified { get; set; }
    public int CategoryId { get; set; }
    public int ViewCount { get; set; }
    public int RateCount { get; set; }
    public double TotalRate { get; set; }
    public decimal Rate  
    {
        get
        {
            if (RateCount == 0)
            {
                return 0;
            } else
            {
                return (decimal)TotalRate / RateCount;
            }
        }
        set { }
    }

    public Category Category { get; set; }

    public IList<PostTagMap> PostTagMaps { get; set; }

    // Constructor to ensure ShortDescription is provided
    public Post()
    {
        ShortDescription = ""; // or any default value you prefer
        PostContent = "";
        Title = "";
        UrlSlug = "";
    }
}
