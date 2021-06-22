using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class ArpaMediaContext : DbContext
    {
        public ArpaMediaContext()
        {
        }

        public ArpaMediaContext(DbContextOptions<ArpaMediaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<BannerType> BannerTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryPost> CategoryPosts { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Subscribed> Subscribeds { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost; Database=ArpaMedia;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banner");

                entity.Property(e => e.ImageUrl).HasMaxLength(2048);

                entity.Property(e => e.SmallText).HasMaxLength(60);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.BannerType)
                    .WithMany(p => p.Banners)
                    .HasForeignKey(d => d.BannerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK_Banner_BannerType");
            });

            modelBuilder.Entity<BannerType>(entity =>
            {
                entity.ToTable("BannerType");

                entity.HasIndex(e => e.Title, "UQ__BannerTy__2CB664DC992DF19A")
                    .IsUnique();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.BannerTypes)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK_BannerType_Language");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => new { e.ParentCategoryId, e.Name }, "UNQ_ParentCategory_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK_Category_Language");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FRK_Category_Category");
            });

            modelBuilder.Entity<CategoryPost>(entity =>
            {
                entity.ToTable("CategoryPost");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryPosts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK_CategoryPost_Category");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.CategoryPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK_CategoryPost_Post");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.AudioFile).HasMaxLength(2048);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.PostImage).HasMaxLength(2048);

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.VideoFile).HasMaxLength(2048);
            });

            modelBuilder.Entity<Subscribed>(entity =>
            {
                entity.ToTable("Subscribed");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UNQ_tbUser_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordKey)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
