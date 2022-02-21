using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NTSTraining.Models.Entities
{
    public partial class NTSTrainingContext : DbContext
    {
        public NTSTrainingContext()
        {
        }

        public NTSTrainingContext(DbContextOptions<NTSTrainingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiKhachHang> LoaiKhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=14.248.84.128,9198;Database=NTSTraining;User Id=dev;Password=nts?v73NTQvf)#+;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("KhachHang");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(300);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GhiChu).IsRequired();

                entity.Property(e => e.LoaiKhachHangId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhachHang)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TenKhachHang)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.LoaiKhachHang)
                    .WithMany(p => p.KhachHangs)
                    .HasForeignKey(d => d.LoaiKhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KhachHang_LoaiKhachHang");
            });

            modelBuilder.Entity<LoaiKhachHang>(entity =>
            {
                entity.ToTable("LoaiKhachHang");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.TenLoaiKhachHang)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.ToTable("NhanVien");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Cmnd)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CMND");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HoVaTen)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhongBanId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.PhongBan)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.PhongBanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NhanVien_PhongBan");
            });

            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.ToTable("PhongBan");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.TenPhongBan)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
