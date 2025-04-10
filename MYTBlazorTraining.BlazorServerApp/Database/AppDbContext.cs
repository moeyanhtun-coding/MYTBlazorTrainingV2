using Microsoft.EntityFrameworkCore;
using MYTBlazorTraining.BlazorServerApp.Models;

namespace MYTBlazorTraining.BlazorServerApp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<BlogModel> Blogs {get; set;}
    }
}
