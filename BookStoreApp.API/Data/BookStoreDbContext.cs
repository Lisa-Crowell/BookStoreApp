using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; } = null!;
    public virtual DbSet<Author> Authors { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(55);
            entity.Property(e => e.LastName).HasMaxLength(55);
            entity.Property(e => e.Bio).HasMaxLength(255);
        });
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA09FAB742")
                .IsUnique();

            entity.Property(e => e.Image).HasMaxLength(250);

            entity.Property(e => e.Isbn)
                .HasMaxLength(55)
                .HasColumnName("ISBN");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.Property(e => e.Summary).HasMaxLength(255);

            entity.Property(e => e.Title).HasMaxLength(55);

            entity.HasOne(d => d.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        modelBuilder.Entity<IdentityRole>().HasData
        (
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "8344074e-8623-4e1a-b0c1-84fb8678x8fc"
            },
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "8344074e-8623-4e1a-b0c1-84fb8678x8gd"
            });
        var hasher = new PasswordHasher<ApiUser>();
        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser
            {
                Id = "8344074e-8623-4e1a-b0c1-84fb8678x8he",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@bookstore.com",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "Admin",
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
            },
            new ApiUser
            {
                Id = "8344074e-8623-4e1a-b0c1-84fb8678x8if",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@bookstore.com",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "8344074e-8623-4e1a-b0c1-84fb8678x8fc",
                UserId = "8344074e-8623-4e1a-b0c1-84fb8678x8he"
            },
            new IdentityUserRole<string>
            {
                RoleId = "8344074e-8623-4e1a-b0c1-84fb8678x8gd",
                UserId = "8344074e-8623-4e1a-b0c1-84fb8678x8if"
            }
        );
            
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}