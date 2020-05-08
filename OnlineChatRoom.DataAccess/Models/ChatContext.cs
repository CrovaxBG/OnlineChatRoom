using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineChatRoom.DataAccess.Models
{
    public partial class ChatContext : IdentityDbContext<AspNetUsers, AspNetRoles, string, AspNetUserClaims,
        AspNetUserRoles, AspNetUserLogins, AspNetRoleClaims, AspNetUserTokens>
    {
        public virtual DbSet<ChatConnections> ChatConnections { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        public ChatContext()
        {
        }

        public ChatContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Message).HasMaxLength(512);

                entity.Property(e => e.StackTrace).HasMaxLength(1024);
            });
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.Property(e => e.UserId).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.UserId).HasMaxLength(100);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(100);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasDefaultValueSql("(N'https://chatavatarstorageaccount.blob.core.windows.net/useravatars/00000000-0000-0000-0000-000000000000?sv=2019-10-10&ss=b&srt=co&sp=rwdlacx&se=2020-06-04T21:13:42Z&st=2020-05-05T13:13:42Z&spr=https&sig=g7ljMk1rnyyMH5GyhmioBM2sf2Rj5ELbh1sIkhxBR%2F0%3D')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUsersSession>(entity =>
            {
                entity.Property(e => e.LoginDate).HasColumnType("datetime");

                entity.Property(e => e.LogoutDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUsersSession)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUsersSession_AspNetUsers");
            });

            modelBuilder.Entity<ChatConnections>(entity =>
            {
                entity.HasKey(e => e.ConnectionId);

                entity.Property(e => e.ConnectionId).ValueGeneratedNever();

                entity.Property(e => e.RoomName).HasMaxLength(128);

                entity.Property(e => e.UserAgent).HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.RoomNameNavigation)
                    .WithMany(p => p.ChatConnections)
                    .HasForeignKey(d => d.RoomName)
                    .HasConstraintName("FK_ChatConnections_Rooms");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatConnections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatConnections_AspNetUsers");
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.RoomName);

                entity.Property(e => e.RoomName).HasMaxLength(128);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
