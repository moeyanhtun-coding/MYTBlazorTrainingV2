using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYTBlazorTraining.WebApi.Db;
using MYTBlazorTraining.WebApi.Models.Blog;
using MYTBlazorTraining.WebApi.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MYTBlazorTraining.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BlogController : ControllerBase
    {
        private readonly IBlog _blog;

        public BlogController(IBlog iblog)
        {
            _blog = iblog;
        }

        [HttpGet("list")]

        public async Task<ActionResult<BlogListResponseModel>> GetBlogList()
        {
            var result = await _blog.GetBlogListAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEditResponseModel>> GetBlog(int id)
        {
            var result = await _blog.GetBlogByIdAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult<BlogCreateResponseModel>> CreateBlog(BlogModel blogModel)
        {
            var result = await _blog.CreateBlogAsync(blogModel);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<BlogUpdateResponseModel>> UpdateBlog(int id, BlogModel blogModel)
        {
            var result = await _blog.UpdateBlogAsync(id, blogModel);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogDeleteResponseModel>> DeleteBlog(int id)
        {
            var result = await _blog.DeleteBlogAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }


        //private readonly AppDbContext _db;

        //[HttpGet]
        //public async Task<IActionResult> Read()
        //{
        //    var blogs =await _db.Blogs.OrderByDescending(x=>x.BlogId).ToListAsync();
        //    return Ok(blogs);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> BlogEdit(int id)
        //{
        //    var blog =  await _db.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
        //    if (blog is null)
        //        return NotFound("Blog Not Found");
        //    return Ok(blog);
        //}

        //[HttpPost]
        //public async Task<IActionResult> BlogCreate(TblBlog blogModel)
        //{
        //   await _db.Blogs.AddAsync(blogModel);
        //    int result = await _db.SaveChangesAsync();
        //    string message = result > 0 ? "Blog Created" : "Blog Not Created";
        //    return Ok(message);
        //}

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> BlogUpdate(int id, TblBlog blogModel)
        //{
        //    var blog = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        //    if (blog is null)
        //        return NotFound("Blog Not Found");
        //    blog.BlogTitle = blogModel.BlogTitle;
        //    blog.BlogAuthor = blogModel.BlogAuthor;
        //    blog.BlogContent = blogModel.BlogContent;
        //    _db.Blogs.Entry(blog).State = EntityState.Modified;
        //    await _db.SaveChangesAsync();
        //    return Ok("Blog Updated");
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> BlogDelete(int id)
        //{
        //    var blog = await _db.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
        //    if (blog is null)
        //        return NotFound("Blog Not Found");
        //    _db.Remove(blog);
        //    _db.Entry(blog).State = EntityState.Deleted;
        //    int result = await _db.SaveChangesAsync();
        //    string message = result > 0 ? "Blog Delete Successful" : "Blog Delete Failed";
        //    return Ok(message);
        //}
    }
}