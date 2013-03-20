using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class ModelContext : DbContext
    {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelContext()
            : base(ConfigurationManager.ConnectionStrings["ModelContext"].ConnectionString)
        {
            Database.SetInitializer(new ModelInitializer());
            Database.Initialize(false);
        }

        #endregion

        #region Public entity sets

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Address> Addresses { get; set; }

        #endregion
        #region Overrided methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>().Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Bank>().Property(b => b.Name).IsRequired();

            modelBuilder.Entity<Address>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Address>().Property(a => a.Street).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Number).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Number).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.ZipCode).IsOptional();
            modelBuilder.Entity<Address>().Property(a => a.Country).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.City).IsOptional();
            modelBuilder.Entity<Address>().Property(a => a.Latitude).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Longitude).IsRequired();

            modelBuilder.Entity<Branch>().Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Branch>().Property(b => b.Name).IsRequired();
            modelBuilder.Entity<Branch>().Property(b => b.Email).IsOptional();
            modelBuilder.Entity<Branch>().Property(b => b.PhoneNumber).IsOptional();
            modelBuilder.Entity<Branch>().Property(b => b.StoreHours).IsOptional();
            modelBuilder.Entity<Branch>().HasRequired(b => b.Bank).WithMany().Map(m => m.MapKey("BankId"));
            modelBuilder.Entity<Branch>().HasRequired(b => b.Address).WithMany().Map(m => m.MapKey("AddressId"));


        }

        #endregion

    }
}