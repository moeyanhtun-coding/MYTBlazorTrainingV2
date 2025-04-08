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
        private readonly AppDbContext _db;
        public BlogController()
        {
            _db = new AppDbContext();
        }

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
        public IActionResult BlogCreate(BlogModel blogModel)
        {
            _db.Add(blogModel);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Blog Created" : "Blog Not Created";
            return Ok(message);
        }
        
        [HttpDelete("{id}")]
        public IActionResult BlogCreate(int BlogId)
        {
            var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == BlogId);
            if (blog is null)
                return NotFound("Blog Not Found");
            _db.Remove(blog);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Blog Delete Successful" : "Blog Delete Failed";
            return Ok(message);
        }
    }
}
