using ExamProject.API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Choices { get; set; }
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        string adminUserName = "admin@admin.com";
        string adminRoleName = "admin";
        string password = "Mm12345.";
        string userRoleName = "user";

        builder.Entity<AppRole>().HasData(new AppRole
        {
            Id = 1,
            Name = adminRoleName,
            NormalizedName = adminRoleName.ToUpper()
        });
        builder.Entity<AppRole>().HasData(new AppRole
        {
            Id = 2,
            Name = userRoleName,
            NormalizedName = userRoleName.ToUpper()
        });

        var appUser = new AppUser
        {
            Id = 1,
            Email = adminUserName,
            EmailConfirmed = true,
            UserName = adminUserName,
            NormalizedUserName = adminUserName.ToUpper(),
        };

        PasswordHasher<AppUser> ph = new();
        appUser.PasswordHash = ph.HashPassword(appUser, password);

        builder.Entity<AppUser>().HasData(appUser);

        builder.Entity<IdentityUserLogin<int>>().HasKey(x => x.UserId);

        builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
        {
            RoleId = 1,
            UserId = 1,
        });
    }
}
