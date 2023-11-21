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
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedAdmin(builder);

    }
    private void SeedAdmin(ModelBuilder builder)
    {
        const string adminUserName = "admin@admin.com";
        const string userUserName = "user@user.com";
        const string adminRoleName = "admin";
        const string userRoleName = "user";
        const string password = "Mm12345.";

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

        var appAdmin = new AppUser
        {
            Id = 1,
            Email = adminUserName,
            EmailConfirmed = true,
            UserName = adminUserName,
            NormalizedUserName = adminUserName.ToUpper(),
        };
        var appUser = new AppUser
        {
            Id = 2,
            Email = userUserName,
            EmailConfirmed = true,
            UserName = userUserName,
            NormalizedUserName = userUserName.ToUpper(),
        };


        PasswordHasher<AppUser> ph = new();
        appAdmin.PasswordHash = ph.HashPassword(appAdmin, password);

        appUser.PasswordHash = ph.HashPassword(appUser, password);

        builder.Entity<AppUser>().HasData(appAdmin);
        builder.Entity<AppUser>().HasData(appUser);

        builder.Entity<IdentityUserLogin<int>>().HasKey(x => x.UserId);

        builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
        {
            RoleId = 1,
            UserId = 1,
        });
        builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
        {
            RoleId = 2,
            UserId = 2,
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<BaseEntity>();

        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                _ => DateTime.Now
            };
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
