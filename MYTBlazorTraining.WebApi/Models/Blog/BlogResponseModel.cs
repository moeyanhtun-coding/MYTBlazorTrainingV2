using MYTBlazorTraining.WebApi.Db;

namespace MYTBlazorTraining.WebApi.Models.Blog
{
    public record BlogListResponseModel(bool Success, List<TblBlog> Blogs);
    public record BlogEditResponseModel(bool Success, TblBlog Blog, string Message = null);
    public record BlogCreateResponseModel(bool Success, string Message = null);
    public record BlogUpdateResponseModel(bool Success, string Message = null);
    public record BlogDeleteResponseModel(bool Success, string Message = null);
}
