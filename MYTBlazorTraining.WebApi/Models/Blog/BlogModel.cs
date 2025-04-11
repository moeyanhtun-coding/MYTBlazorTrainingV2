using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYTBlazorTraining.WebApi.Models.Blog
{
    public class BlogModel
    {
        [Required]
        public string? BlogTitle { get; set; }
        [Required]
        public string? BlogAuthor { get; set; }
        [Required]
        public string? BlogContent { get; set; }
    }
}
