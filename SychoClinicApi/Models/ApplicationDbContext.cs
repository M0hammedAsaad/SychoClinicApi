using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SychoClinicApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CategoriesArticle> CategoriesArticles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MainArticle> MainArticles { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<ContectUs> ContectUs { get; set; }

    }
}
