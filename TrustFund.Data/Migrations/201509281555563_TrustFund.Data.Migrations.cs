namespace TrustFund.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrustFundDataMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        LoginEmail = c.String(),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.CustomerFile",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Type = c.Int(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Directory = c.String(),
                    })
                .PrimaryKey(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerFile");
            DropTable("dbo.Account");
        }
    }
}
