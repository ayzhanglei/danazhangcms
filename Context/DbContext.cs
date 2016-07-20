using Microsoft.EntityFrameworkCore;
using System.IO;
using DanaZhangCms.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DanaZhangCms.Domain.DbModels;

namespace DanaZhangCms
{
    // public class MyContext : DbContext
    //{
    //    public MyContext(DbContextOptions<MyContext> options)
    //        : base(options)
    //    {

    //    }

    //    public DbSet<User> manager { get; set; }
    //}

    public class MyContext : IdentityDbContext<User, Role, long>
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<ArticleCategory> ArticleCategorys { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.ToTable("UserClaim");
            });

            modelBuilder.Entity<IdentityRoleClaim<long>>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("RoleClaim");
            });

            modelBuilder.Entity<IdentityUserRole<long>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("UserRole");
            });

            modelBuilder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.ToTable("UserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<long>>(b =>
            {
                b.ToTable("UserToken");
            });
            modelBuilder.Entity<Channel>().ToTable("Channel");
            modelBuilder.Entity<ArticleCategory>().ToTable("ArticleCategory");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Banner>().ToTable("Banner");
        }
    }

}