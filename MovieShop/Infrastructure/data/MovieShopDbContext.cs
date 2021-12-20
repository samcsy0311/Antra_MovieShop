using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.data
{
     public class MovieShopDbContext:DbContext
     {
          public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options):base (options)
          {
               
          }

          public DbSet<Genre> Genres { get; set; }
          public DbSet<Movie> Movies { get; set; }
          public DbSet<Trailer> Trailers { get; set; }
          public DbSet<Role> Roles { get; set; }
          public DbSet<User> Users { get; set; }
          public DbSet<Favorite> Favorites { get; set; }
          public DbSet<Purchase> Purchases { get; set; }
          public DbSet<Review> Reviews { get; set; }
          public DbSet<MovieGenre> MovieGenres { get; set; }
          public DbSet<UserRole> UserRoles { get; set; }
          public DbSet<Cast> Casts { get; set; }
          public DbSet<MovieCast> MoviesCasts { get; set;}
          public DbSet<Crew> Crews { get; set; }
          public DbSet<MovieCrew> MovieCrews { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               // you can do fluent API configurations here
               modelBuilder.Entity<Movie>(ConfigureMovie);
               modelBuilder.Entity<Trailer>(ConfigureTrailer);
               modelBuilder.Entity<Role>(ConfigureRole);
               modelBuilder.Entity<User>(ConfigureUser);
               modelBuilder.Entity<Favorite>(ConfigureFavorite);
               modelBuilder.Entity<Purchase>(ConfigurePurchase);
               modelBuilder.Entity<Review>(ConfigureReview);
               modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
               modelBuilder.Entity<UserRole>(ConfigureUserRole);
               modelBuilder.Entity<Cast>(ConfigureCast);
               modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
               modelBuilder.Entity<Crew>(ConfigureCrew);
               modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
          }

          private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
          {
               builder.ToTable("MovieCrew");
               builder.HasKey(mc => new { mc.MovieId, mc.CrewID, mc.Department, mc.Job });
               builder.Property(mc => mc.Department).HasMaxLength(128);
               builder.Property(mc => mc.Job).HasMaxLength(128);
          }

          private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
          {
               builder.ToTable("Crew");
               builder.HasKey(c => c.Id);
               builder.Property(c => c.Name).HasMaxLength(128);
               builder.Property(c=>c.ProfilePath).HasMaxLength(2084);
          }

          private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
          {
               builder.ToTable("MovieCast");
               builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });
               builder.Property(mc => mc.Character).HasMaxLength(450);
          }

          private void ConfigureCast(EntityTypeBuilder<Cast> builder)
          {
               builder.ToTable("Cast");
               builder.HasKey(c => c.Id);
               builder.Property(c => c.Name).HasMaxLength(128);
               builder.Property(c => c.ProfilePath).HasMaxLength(2084);
          }

          private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
          {
               builder.ToTable("UserRole");
               builder.HasKey(ur => new { ur.UserId, ur.RoleId });
          }

          private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
          {
               builder.ToTable("MovieGenre");
               builder.HasKey(mg => new { mg.MovieID, mg.GenreID });
          }

          private void ConfigureReview(EntityTypeBuilder<Review> builder)
          {
               builder.ToTable("Review");
               builder.HasKey(r => new { r.MovieId, r.UserId });
               builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
          }

          private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
          {
               builder.ToTable("Purchase");
               builder.HasKey(p => p.Id);
               builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)");
          }

          private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
          {
               builder.ToTable("Favorite");
               builder.HasKey(x => x.Id);
          }

          private void ConfigureUser(EntityTypeBuilder<User> builder)
          {
               builder.ToTable("User");
               builder.HasKey(t => t.Id);
               builder.HasIndex(u => u.Email).IsUnique();
               builder.Property(t => t.FirstName).HasMaxLength(128);
               builder.Property(t => t.LastName).HasMaxLength(128);
               builder.Property(t => t.Email).HasMaxLength(256);
               builder.Property(t => t.HashedPassword).HasMaxLength(1024);
               builder.Property(t => t.Salt).HasMaxLength(1024);
               builder.Property(t => t.PhoneNumber).HasMaxLength(16);
               builder.Property(t => t.FirstName).HasMaxLength(128);
          }

          private void ConfigureRole(EntityTypeBuilder<Role> builder)
          {
               builder.ToTable("Role");
               builder.HasKey(t => t.Id);
               builder.Property(t => t.Name).HasMaxLength(20);
          }

          private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
          {
               builder.ToTable("Trailer");
               builder.HasKey(t => t.Id);
               builder.Property(t => t.Name).HasMaxLength(2084);
               builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
          }

          private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
          {
               // here you can establish your Fluent API way of modeling the table

               builder.ToTable("Movie");
               builder.HasKey(m => m.Id);
               builder.Property(m => m.Title).IsRequired().HasMaxLength(256);
               builder.Property(m => m.Overview).HasMaxLength(4096);
               builder.Property(m => m.Tagline).HasMaxLength(512);
               builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
               builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
               builder.Property(m => m.PosterUrl).HasMaxLength(2084);
               builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
               builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
               builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
               builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
               builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
               builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
          }
     }
}
