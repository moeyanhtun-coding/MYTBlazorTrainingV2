using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYTBlazorTraining.WebApi.Db;
using MYTBlazorTraining.WebApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MYTBlazorTraining.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db = new();

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var blogs =await _db.Blogs.OrderByDescending(x=>x.BlogId).ToListAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var blog =  await _db.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog is null)
                return NotFound("Blog Not Found");
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> BlogCreate(BlogModel blogModel)
        {
           await _db.Blogs.AddAsync(blogModel);
            int result = await _db.SaveChangesAsync();
            string message = result > 0 ? "Blog Created" : "Blog Not Created";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> BlogUpdate(int id, BlogModel blogModel)
        {
            var blog = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
                return NotFound("Blog Not Found");
            blog.BlogTitle = blogModel.BlogTitle;
            blog.BlogAuthor = blogModel.BlogAuthor;
            blog.BlogContent = blogModel.BlogContent;
            _db.Blogs.Entry(blog).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok("Blog Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            var blog = await _db.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog is null)
                return NotFound("Blog Not Found");
            _db.Remove(blog);
            _db.Entry(blog).State = EntityState.Deleted;
            int result = await _db.SaveChangesAsync();
            string message = result > 0 ? "Blog Delete Successful" : "Blog Delete Failed";
            return Ok(message);
        }
    }
}