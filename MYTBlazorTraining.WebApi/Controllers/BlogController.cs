using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYTBlazorTraining.WebApi.Db;
using MYTBlazorTraining.WebApi.Models;

namespace MYTBlazorTraining.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db = new();

        [HttpGet]
        public IActionResult Read()
        {
            var blogs = _db.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult BlogEdit(int id)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
                return NotFound("Blog Not Found");
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult BlogCreate([FromBody] BlogModel blogModel)
        {
            _db.Add(blogModel);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Blog Created" : "Blog Not Created";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult BlogUpdate(int id, [FromBody] BlogModel blogModel)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
                return NotFound("Blog Not Found");
            blog.BlogTitle = blogModel.BlogTitle;
            blog.BlogAuthor = blogModel.BlogAuthor;
            blog.BlogContent = blogModel.BlogContent;
            _db.SaveChanges();
            return Ok("Blog Updated");
        }
        
        [HttpDelete("{id}")]
        public IActionResult BlogDelete(int id)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
                return NotFound("Blog Not Found");
            _db.Remove(blog);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Blog Delete Successful" : "Blog Delete Failed";
            return Ok(message);
        }
    }
}
