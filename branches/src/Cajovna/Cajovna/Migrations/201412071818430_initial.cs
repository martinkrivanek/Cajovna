namespace Cajovna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PolozkaMenus",
                c => new
                    {
                        polozkaMenuID = c.Int(nullable: false, identity: true),
                        price_sell = c.Double(nullable: false),
                        date_added = c.DateTime(nullable: false),
                        avalible = c.Boolean(nullable: false),
                        name = c.String(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.polozkaMenuID);
            
            CreateTable(
                "dbo.Slozenis",
                c => new
                    {
                        slozeniID = c.Int(nullable: false, identity: true),
                        quantity = c.Double(nullable: false),
                        surovinaID = c.Int(nullable: false),
                        polozkaMenuID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.slozeniID)
                .ForeignKey("dbo.Surovinas", t => t.surovinaID, cascadeDelete: true)
                .ForeignKey("dbo.PolozkaMenus", t => t.polozkaMenuID, cascadeDelete: true)
                .Index(t => t.surovinaID)
                .Index(t => t.polozkaMenuID);
            
            CreateTable(
                "dbo.Surovinas",
                c => new
                    {
                        surovinaID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        desription = c.String(),
                        date_added = c.DateTime(nullable: false),
                        unit = c.Int(nullable: false),
                        number_of_units = c.Double(nullable: false),
                        price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.surovinaID);
            
            CreateTable(
                "dbo.PolozkaUctus",
                c => new
                    {
                        polozkaUctuID = c.Int(nullable: false, identity: true),
                        date_ordered = c.DateTime(nullable: false),
                        date_paid = c.DateTime(),
                        polozkaMenuID = c.Int(nullable: false),
                        ucetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.polozkaUctuID)
                .ForeignKey("dbo.PolozkaMenus", t => t.polozkaMenuID, cascadeDelete: true)
                .ForeignKey("dbo.Ucets", t => t.ucetID, cascadeDelete: true)
                .Index(t => t.polozkaMenuID)
                .Index(t => t.ucetID);
            
            CreateTable(
                "dbo.Ucets",
                c => new
                    {
                        ucetID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        date_added = c.DateTime(nullable: false),
                        date_closed = c.DateTime(),
                        stulID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ucetID)
                .ForeignKey("dbo.Stuls", t => t.stulID, cascadeDelete: true)
                .Index(t => t.stulID);
            
            CreateTable(
                "dbo.Stuls",
                c => new
                    {
                        stulID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.stulID);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ucets", "stulID", "dbo.Stuls");
            DropForeignKey("dbo.PolozkaUctus", "ucetID", "dbo.Ucets");
            DropForeignKey("dbo.PolozkaUctus", "polozkaMenuID", "dbo.PolozkaMenus");
            DropForeignKey("dbo.Slozenis", "polozkaMenuID", "dbo.PolozkaMenus");
            DropForeignKey("dbo.Slozenis", "surovinaID", "dbo.Surovinas");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ucets", new[] { "stulID" });
            DropIndex("dbo.PolozkaUctus", new[] { "ucetID" });
            DropIndex("dbo.PolozkaUctus", new[] { "polozkaMenuID" });
            DropIndex("dbo.Slozenis", new[] { "polozkaMenuID" });
            DropIndex("dbo.Slozenis", new[] { "surovinaID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Stuls");
            DropTable("dbo.Ucets");
            DropTable("dbo.PolozkaUctus");
            DropTable("dbo.Surovinas");
            DropTable("dbo.Slozenis");
            DropTable("dbo.PolozkaMenus");
        }
    }
}
