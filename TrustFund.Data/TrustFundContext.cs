using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Business.Entities;

namespace TrustFund.Data
{
    public class TrustFundContext : DbContext
    {
        public TrustFundContext()
            : base("name = TrustFund")
        {
            Database.SetInitializer<TrustFundContext>(null);
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\PC\Documents\GitHub\TrustFund\TrustFund.Data\Migrations");
        }

        public DbSet<Account> AccountSet { get; set; }
        public DbSet<CustomerFile> CustomerFilesSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();
            modelBuilder.Entity<Account>().HasKey<int>(e => e.AccountId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<CustomerFile>().HasKey<int>(e => e.FileId).Ignore<int>(e => e.EntityId);
        }
    }
}
