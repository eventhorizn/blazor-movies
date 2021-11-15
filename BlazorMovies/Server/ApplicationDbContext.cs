using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorMovies.Server;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieActors>().HasKey(x => new { x.MovieId, x.PersonId });
        modelBuilder.Entity<MoviesGenres>().HasKey(x => new { x.MovieId, x.GenreId });

        var userAdminId = "a729e84c-d741-4c50-99d8-f2f4e4ae38d2";
        var hasher = new PasswordHasher<IdentityUser>();
        var userAdmin = new IdentityUser()
        {
            Id = userAdminId,
            Email = "devgary@gmail.com",
            UserName = "devgary@gmail.com",
            NormalizedEmail = "devgary@gmail.com",
            NormalizedUserName = "devgary@gmail.com",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Aa123456!")
        };

        modelBuilder.Entity<IdentityUser>().HasData(userAdmin);
        modelBuilder.Entity<IdentityUserClaim<string>>()
            .HasData(new IdentityUserClaim<string>()
            {
                Id = 1,
                ClaimType = ClaimTypes.Role,
                ClaimValue = "Admin",
                UserId = userAdminId
            });

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
        modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

        modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<MoviesGenres> MoviesGenres { get; set; }
    public DbSet<MovieActors> MovieActors { get; set; }
    public DbSet<MovieRating> MovieRatings { get; set; }
}