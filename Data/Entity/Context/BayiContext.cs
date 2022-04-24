using AppCore.DataAccess.Configs;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity.Context
{
    public class BayiContext : DbContext
    {
        public DbSet<Hesap> Hesaplar { get; set; }
        public DbSet<HesapDetayi> HesapDetaylari { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Urun> Urunler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Sql Bağlantı.

            string connectionString;
            connectionString = ConnectionConfig.ConnectionString;
            if (string.IsNullOrWhiteSpace(connectionString))
            {

                connectionString = @"server=.\sqlexpress;database=Bayi;trusted_connection=true;";
            }
            optionsBuilder.UseSqlServer(connectionString);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentApi tablolar arası ilişkiler.
            modelBuilder.Entity<Urun>()
                .ToTable("BayiUrunler")
                .HasOne(urun => urun.Kategori)
                .WithMany(kategori => kategori.Urunler)
                .HasForeignKey(urun => urun.KategoriId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Hesap>()
                .ToTable("BayiHesaplar")
                .HasOne(hesap => hesap.HesapDetayi)
                .WithOne(hesapDetayi => hesapDetayi.Hesap)
                .HasForeignKey<Hesap>(hesap => hesap.HesapDetayiId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Hesap>()
                .HasOne(hesap => hesap.Rol)
                .WithMany(rol => rol.Hesap)
                .HasForeignKey(hesap => hesap.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Kategori>()
                .ToTable("BayiKategoriler");
            modelBuilder.Entity<HesapDetayi>()
                .ToTable("BayiHesapDetay");
            modelBuilder.Entity<Rol>()
                .ToTable("BayiRoller");

        }

        public void ExecuteStoreCommand(string v)
        {
            throw new NotImplementedException();
        }
    }
}
