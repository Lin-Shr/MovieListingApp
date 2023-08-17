using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Areas.Identity.Data;
using MovieApp.Models;
using System.Reflection.Emit;

namespace MovieApp.Data;

public class MovieAppContext : IdentityDbContext<MovieAppUser>
{
    //public MovieAppContext()
    //{
    //}

    public MovieAppContext(DbContextOptions<MovieAppContext> options)
        : base(options)
    {
    }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<GenreMovie> GenreMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<GenreMovie>()
          .HasKey(m => new { m.MovieId, m.GenreId });
    }


}
