using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EczaneOtomasyonu.Models;

public partial class EczaneOtomasyonuContext : DbContext
{
    public EczaneOtomasyonuContext()
    {
    }

    public EczaneOtomasyonuContext(DbContextOptions<EczaneOtomasyonuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hastalar> Hastalars { get; set; }

    public virtual DbSet<Ilaclar> Ilaclars { get; set; }
    public virtual DbSet<Kullanici> Kullanicilar { get; set; }

    public virtual DbSet<Kategoriler> Kategorilers { get; set; }

    public virtual DbSet<Musteriler> Musterilers { get; set; }

    public virtual DbSet<Personeller> Personellers { get; set; }

    public virtual DbSet<SatisDetay> SatisDetays { get; set; }

    public virtual DbSet<Satislar> Satislars { get; set; }

    public virtual DbSet<VwDetayliSatisRaporu> VwDetayliSatisRaporus { get; set; }

    public virtual DbSet<VwKritikStokRaporu> VwKritikStokRaporus { get; set; }

    public virtual DbSet<VwSktyaklasanlar> VwSktyaklasanlars { get; set; }

    public virtual DbSet<VwSonKullanmaTarihiYaklasanlar> VwSonKullanmaTarihiYaklasanlars { get; set; }
    public virtual DbSet<Sepet> Sepet { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EczaneOtomasyonu;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hastalar>(entity =>
        {
            entity.HasKey(e => e.HastaId).HasName("PK__Hastalar__114C5CABBAC0E5D4");

            entity.ToTable("Hastalar");

            entity.HasIndex(e => e.Tc, "IX_Hastalar_TC");

            entity.HasIndex(e => e.Tc, "UQ__Hastalar__3214E409791D9CA2").IsUnique();

            entity.Property(e => e.HastaId).HasColumnName("HastaID");
            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.KayitTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Soyad).HasMaxLength(50);
            entity.Property(e => e.Tc)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TC");
            entity.Property(e => e.Telefon)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ilaclar>(entity =>
        {
            entity.HasKey(e => e.IlacId).HasName("PK__Ilaclar__22000D17EC738B48");

            entity.ToTable("Ilaclar");

            entity.HasIndex(e => e.IlacAdi, "IX_Ilaclar_IlacAdi");

            entity.HasIndex(e => e.BarkodNo, "UQ__Ilaclar__FA8C06C63266CCC1").IsUnique();

            entity.Property(e => e.IlacId).HasColumnName("IlacID");
            entity.Property(e => e.BarkodNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BirimFiyat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IlacAdi).HasMaxLength(150);
            entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

            entity.HasOne(d => d.Kategori).WithMany(p => p.Ilaclars)
                .HasForeignKey(d => d.KategoriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ilac_Kategori");
        });

        modelBuilder.Entity<Kategoriler>(entity =>
        {
            entity.HasKey(e => e.KategoriId).HasName("PK__Kategori__1782CC9259C7D4B6");

            entity.ToTable("Kategoriler");

            entity.HasIndex(e => e.KategoriAdi, "UQ__Kategori__110FF79EFA0F8E01").IsUnique();

            entity.Property(e => e.KategoriId).HasColumnName("KategoriID");
            entity.Property(e => e.KategoriAdi).HasMaxLength(100);
        });

        modelBuilder.Entity<Musteriler>(entity =>
        {
            entity.HasKey(e => e.MusteriId).HasName("PK__Musteril__72624471BC144892");

            entity.ToTable("Musteriler");

            entity.Property(e => e.MusteriId).HasColumnName("MusteriID");
            entity.Property(e => e.Ad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Soyad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefon)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Personeller>(entity =>
        {
            entity.HasKey(e => e.PersonelId).HasName("PK__Personel__0F0C5751A82ABCF3");

            entity.ToTable("Personeller");

            entity.HasIndex(e => e.Tc, "UQ__Personel__3214E409C038F5F9").IsUnique();

            entity.Property(e => e.PersonelId).HasColumnName("PersonelID");
            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.Soyad).HasMaxLength(50);
            entity.Property(e => e.Tc)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TC");
            entity.Property(e => e.Unvan)
                .HasMaxLength(50)
                .HasDefaultValue("Eczacı Kalfası");
        });

        modelBuilder.Entity<SatisDetay>(entity =>
        {
            entity.HasKey(e => e.SatisDetayId).HasName("PK__SatisDet__89F97278AE32627E");

            entity.ToTable("SatisDetay", tb =>
                {
                    tb.HasTrigger("trg_StokDusur");
                    tb.HasTrigger("trg_StokIade");
                });

            entity.Property(e => e.SatisDetayId).HasColumnName("SatisDetayID");
            entity.Property(e => e.BirimFiyat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IlacId).HasColumnName("IlacID");
            entity.Property(e => e.SatisId).HasColumnName("SatisID");

            entity.HasOne(d => d.Ilac).WithMany(p => p.SatisDetays)
                .HasForeignKey(d => d.IlacId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SatisDetay_Ilac");

            entity.HasOne(d => d.Satis).WithMany(p => p.SatisDetays)
                .HasForeignKey(d => d.SatisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SatisDetay_Satis");
        });

        modelBuilder.Entity<Satislar>(entity =>
        {
            entity.HasKey(e => e.SatisId).HasName("PK__Satislar__80CB4CFFF9369338");

            entity.ToTable("Satislar");

            entity.HasIndex(e => e.SatisTarihi, "IX_Satislar_Tarih");

            entity.Property(e => e.SatisId).HasColumnName("SatisID");
            entity.Property(e => e.HastaId).HasColumnName("HastaID");
            entity.Property(e => e.PersonelId).HasColumnName("PersonelID");
            entity.Property(e => e.SatisTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ToplamTutar).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Hasta).WithMany(p => p.Satislars)
                .HasForeignKey(d => d.HastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Satis_Hasta");

            entity.HasOne(d => d.Personel).WithMany(p => p.Satislars)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Satis_Personel");
        });

        modelBuilder.Entity<VwDetayliSatisRaporu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DetayliSatisRaporu");

            entity.Property(e => e.HastaBilgisi).HasMaxLength(101);
            entity.Property(e => e.SatisId).HasColumnName("SatisID");
            entity.Property(e => e.SatisTarihi).HasColumnType("datetime");
            entity.Property(e => e.SatisYapanPersonel).HasMaxLength(101);
            entity.Property(e => e.ToplamTutar).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwKritikStokRaporu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_KritikStokRaporu");

            entity.Property(e => e.BarkodNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BirimFiyat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IlacAdi).HasMaxLength(150);
            entity.Property(e => e.KategoriAdi).HasMaxLength(100);
        });

        modelBuilder.Entity<VwSktyaklasanlar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_SKTYaklasanlar");

            entity.Property(e => e.Barkod)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IlacAd)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwSonKullanmaTarihiYaklasanlar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_SonKullanmaTarihiYaklasanlar");

            entity.Property(e => e.BarkodNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IlacAdi).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
