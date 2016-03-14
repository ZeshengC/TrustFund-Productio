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
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\PC\Documents\GitHub\TrustFund-Production\TrustFund.Data\Migrations");
        }

        public DbSet<Account> AccountSet { get; set; }
        public DbSet<CustomerFile> CustomerFilesSet { get; set; }
        public DbSet<Investment> InvestmentSet { get; set; }
        public DbSet<PNLInvestmentNetPerformance> PNLInvestmentNetPerformanceSet { get; set; }
        public DbSet<Fund> FundSet { get; set; }
        public DbSet<FundPerformance> FundPerformanceSet { get; set; }
        public DbSet<FundManager> FundManagerSet { get; set; }
        public DbSet<PNLInvestor> PNLInvestorSet { get; set; }
        public DbSet<Investor> InvestorSet { get; set; }
        public DbSet<PNLInvestment> PNLInvestmentSet { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();
            modelBuilder.Entity<Account>().HasKey<int>(e => e.AccountId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<CustomerFile>().HasKey<int>(e => e.FileId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<Investment>().HasKey<int>(e => e.InvestmentId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<PNLInvestmentNetPerformance>().HasKey<int>(e => e.PerformanceId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<Fund>().HasKey<int>(e => e.FundId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<FundPerformance>().HasKey<int>(e => e.FundPerformanceId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<FundManager>().HasKey<int>(e => e.ManagerId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<PNLInvestor>().HasKey<int>(e => e.PNLInvestorId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<Investor>().HasKey<int>(e => e.InvestorId).Ignore<int>(e => e.EntityId);
            modelBuilder.Entity<PNLInvestment>().HasKey<int>(e => e.PNLInvestmentId).Ignore<int>(e => e.EntityId);
        }
    }
}
