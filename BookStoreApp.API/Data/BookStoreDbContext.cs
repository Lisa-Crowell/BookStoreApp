using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : DbContext
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

            entity.Property(e => e.Image).HasMaxLength(55);

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
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}