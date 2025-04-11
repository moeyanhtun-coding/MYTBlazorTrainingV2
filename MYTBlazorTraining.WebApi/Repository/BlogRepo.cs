using Microsoft.EntityFrameworkCore;
using MYTBlazorTraining.WebApi.Db;
using MYTBlazorTraining.WebApi.Models.Blog;
using MYTBlazorTraining.WebApi.Services;
using System.Reflection.Metadata.Ecma335;

namespace MYTBlazorTraining.WebApi.Repository
{
    public class BlogRepo : IBlog
    {
        private readonly AppDbContext _context;

        public BlogRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BlogCreateResponseModel> CreateBlogAsync(BlogModel blogModel)
        {
            var blog = new TblBlog
            {
                BlogTitle = blogModel.BlogTitle,
                BlogAuthor = blogModel.BlogAuthor,
                BlogContent = blogModel.BlogContent
            };

            await _context.Blogs.AddAsync(blog);
            var result = await _context.SaveChangesAsync();

            if (result is 0)
                return new BlogCreateResponseModel(false, "Blog Not Created");
            return new BlogCreateResponseModel(true, "Blog Created Successfully");
        }

        public async Task<BlogDeleteResponseModel> DeleteBlogAsync(int id)
        {
            var blog = await FindBlogByIdAsync(id);

            if (blog is null) return new BlogDeleteResponseModel(false, "Blog Not Found");
            _context.Blogs.Remove(blog);
            _context.Entry(blog).State = EntityState.Deleted;
            var result = await _context.SaveChangesAsync();

            if (result is 0) return new BlogDeleteResponseModel(false, "Blog Not Deleted");
            return new BlogDeleteResponseModel(true, "Blog Deleted Successfully");
        }

        public async Task<BlogEditResponseModel> GetBlogByIdAsync(int id)
        {
            var blog = await FindBlogByIdAsync(id);
            if (blog is null)
                return new BlogEditResponseModel(false, null!, "Blog Not Found");

            var blogModel = new TblBlog
            {
                BlogId = blog.BlogId,
                BlogTitle = blog.BlogTitle,
                BlogAuthor = blog.BlogAuthor,
                BlogContent = blog.BlogContent
            };

            return new BlogEditResponseModel(true, blogModel, "Blog Found");
        }

        public async Task<BlogListResponseModel> GetBlogListAsync()
        {
            var blogList = await _context.Blogs.ToListAsync();
            if (blogList is null) return new BlogListResponseModel(false, new List<TblBlog>());
            return new BlogListResponseModel(true, blogList);
        }

        public async Task<BlogUpdateResponseModel> UpdateBlogAsync(int id, BlogModel blogModel)
        {
            var blog = await FindBlogByIdAsync(id);
            if (blog is null) return new BlogUpdateResponseModel(false, "Blog Not Found");

            blog.BlogTitle = blogModel.BlogTitle;
            blog.BlogAuthor = blogModel.BlogAuthor;
            blog.BlogContent = blogModel.BlogContent;
            _context.Entry(blog).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            if (result is 0) return new BlogUpdateResponseModel(false, "Blog Updated Fail");
            return new BlogUpdateResponseModel(true, "Blog Updated Successfully");
        }
        private async Task<TblBlog> FindBlogByIdAsync(int id)
        {
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id)!;
            if (blog is null) return null!;
            return blog;
        }
    }
}
