using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace MYTBlazorTraining.WebApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<TblBlog> Blogs { get; set; }
        public DbSet<TblUser> Users { get; set; }
    }
}
