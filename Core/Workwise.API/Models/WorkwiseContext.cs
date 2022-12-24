using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Workwise.API.Models;

public partial class WorkwiseDbContext : DbContext
{
    public WorkwiseDbContext()
    {
    }

    public WorkwiseDbContext(DbContextOptions<WorkwiseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<FriendMapping> FriendMappings { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<MapPostImage> MapPostImages { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<OnlineUser> OnlineUsers { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserImage> UserImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=workwise;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.ChatMessageId).HasName("PK_dbo.ChatMessages");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.ViewedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Comments");

            entity.Property(e => e.CommentedByUserId)
                .HasMaxLength(128)
                .HasColumnName("CommentedBy_UserId");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");

            entity.HasOne(d => d.CommentedByUser).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentedByUserId)
                .HasConstraintName("FK_dbo.Comments_dbo.UserProfiles_CommentedBy_UserId");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_dbo.Comments_dbo.Posts_Post_Id");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorLogId).HasName("PK__ErrorLog__D65247C2FFCA6689");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage).IsUnicode(false);
            entity.Property(e => e.ErrorProcedure)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FriendMapping>(entity =>
        {
            entity.HasKey(e => e.FriendMappingId).HasName("PK_dbo.FriendMappings");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.UserImages");

            entity.ToTable("Image");
        });

        modelBuilder.Entity<MapPostImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ImageModels");

            entity.ToTable("MapPostImage");

            entity.HasOne(d => d.Image).WithMany(p => p.MapPostImages)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_Image_Id_MapPostImage_ImageId");

            entity.HasOne(d => d.Post).WithMany(p => p.MapPostImages)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Post_Id_MapPostImage_PostId");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<OnlineUser>(entity =>
        {
            entity.HasKey(e => e.OnlineUserId).HasName("PK_dbo.OnlineUsers");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Posts");

            entity.Property(e => e.PostedOn).HasColumnType("datetime");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Tags");

            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.Tag1).HasColumnName("Tag");

            entity.HasOne(d => d.Post).WithMany(p => p.Tags)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_dbo.Tags_dbo.Posts_Post_Id");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK_dbo.UserNotifications");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_dbo.UserProfiles");

            entity.Property(e => e.UserId).HasMaxLength(128);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.PostId1).HasColumnName("Post_Id1");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.UserProfilePosts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_dbo.UserProfiles_dbo.Posts_Post_Id");

            entity.HasOne(d => d.PostId1Navigation).WithMany(p => p.UserProfilePostId1Navigations)
                .HasForeignKey(d => d.PostId1)
                .HasConstraintName("FK_dbo.UserProfiles_dbo.Posts_Post_Id1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
