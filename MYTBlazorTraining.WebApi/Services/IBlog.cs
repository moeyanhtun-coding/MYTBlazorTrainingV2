using MYTBlazorTraining.WebApi.Models.Blog;

namespace MYTBlazorTraining.WebApi.Services
{
    public interface IBlog
    {
        Task<BlogListResponseModel> GetBlogListAsync();
        Task<BlogEditResponseModel> GetBlogByIdAsync(int id);
        Task<BlogCreateResponseModel> CreateBlogAsync(BlogModel blogModel);
        Task<BlogUpdateResponseModel> UpdateBlogAsync(int id, BlogModel blogModel);
        Task<BlogDeleteResponseModel> DeleteBlogAsync(int id);
    }
}
