namespace PosCookents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cookents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoughtItemAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoughtItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        BoughtItemAdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BoughtItemAdmins", t => t.BoughtItemAdminId, cascadeDelete: true)
                .Index(t => t.BoughtItemAdminId);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAddresses", "BoughtItemAdminId", "dbo.BoughtItemAdmins");
            DropForeignKey("dbo.Stocks", "ItemId", "dbo.Items");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BoughtItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BoughtItemAdmins", "ItemId", "dbo.Items");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserAddresses", new[] { "BoughtItemAdminId" });
            DropIndex("dbo.Stocks", new[] { "ItemId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BoughtItems", new[] { "ItemId" });
            DropIndex("dbo.BoughtItemAdmins", new[] { "ItemId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserModels");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.Stocks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.BoughtItems");
            DropTable("dbo.Items");
            DropTable("dbo.BoughtItemAdmins");
            DropTable("dbo.AdminModels");
        }
    }
}
