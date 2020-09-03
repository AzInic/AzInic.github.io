using Hotel_Automatization.Classes.DataBaseViews;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Automatization.Classes
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("dbConnectStr") { }

        //Коллекции сущностей для работы с бд
        public DbSet<CityView> Cities { get; set; }
        public DbSet<RegionView> Regions { get; set; }
        public DbSet<CountryView> Countries { get; set; }
       
        
        //Настройка таблиц бд
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //чтобы не было проблем с датами на стороне сервера
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            //Настройка обычных полей
            modelBuilder.Entity<CityView>().Property(u => u.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(30);

            modelBuilder.Entity<RegionView>().Property(u => u.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(30);

            modelBuilder.Entity<CountryView>().Property(u => u.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(90);

            modelBuilder.Entity<CountryView>().Property(u => u.PeopleAmount)
               .IsRequired();

            modelBuilder.Entity<CountryView>().Property(u => u.Square)
              .IsRequired();

            modelBuilder.Entity<CountryView>().Property(u => u.Code)
             .IsRequired();

           
            //Настройка Внешних ключей.
            modelBuilder.Entity<CountryView>()
              .HasRequired(prof => prof.Region).WithMany(x => x.Countries);

            modelBuilder.Entity<CountryView>()
           .HasRequired(prof => prof.Capital).WithRequiredDependent(x => x.Country);

            //Унаследованная обработка
            base.OnModelCreating(modelBuilder);
        }
    }
}
