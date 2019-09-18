namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        ItemName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: false)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        StoreName = c.String(nullable: false),
                        StoreStreet = c.String(nullable: false),
                        StoreCity = c.String(nullable: false),
                        StoreState = c.String(nullable: false),
                        StoreZip = c.String(nullable: false),
                        StorePhoneNumber = c.String(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StoreId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TransactionItem",
                c => new
                    {
                        TransactionItemId = c.Int(nullable: false, identity: true),
                        TransactionId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionItemId)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: false)
                .ForeignKey("dbo.Transaction", t => t.TransactionId, cascadeDelete: false)
                .Index(t => t.TransactionId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        DeliveryStreet = c.String(nullable: false),
                        DeliveryCity = c.String(nullable: false),
                        DeliveryState = c.String(nullable: false),
                        DeliveryZip = c.String(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: false)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.TransactionItem", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.Transaction", "StoreId", "dbo.Store");
            DropForeignKey("dbo.TransactionItem", "ItemId", "dbo.Item");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Item", "StoreId", "dbo.Store");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Transaction", new[] { "StoreId" });
            DropIndex("dbo.TransactionItem", new[] { "ItemId" });
            DropIndex("dbo.TransactionItem", new[] { "TransactionId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Item", new[] { "StoreId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Transaction");
            DropTable("dbo.TransactionItem");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Store");
            DropTable("dbo.Item");
        }
    }
}
