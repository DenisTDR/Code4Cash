namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationsReset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Value = c.Int(nullable: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingRoom", t => t.Room_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.MeetingRoom",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Capacity = c.Int(),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        Floor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Floor", t => t.Floor_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.Floor_Id);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        Active = c.Boolean(nullable: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Selector);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Power = c.Int(nullable: false),
                        Functions = c.Int(nullable: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Selector);
            
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Expiration = c.DateTime(nullable: false, precision: 0),
                        Token = c.String(unicode: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Account_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false, precision: 0),
                        End = c.DateTime(nullable: false, precision: 0),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        MeetingRoom_Id = c.Int(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingRoom", t => t.MeetingRoom_Id, cascadeDelete: true)
                .ForeignKey("dbo.Account", t => t.Owner_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.MeetingRoom_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Floor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Level = c.Int(nullable: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Selector);
            
            CreateTable(
                "dbo.MeetingRoomAccountRellation",
                c => new
                    {
                        MeetingRoomEntity_Id = c.Int(nullable: false),
                        AccountEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MeetingRoomEntity_Id, t.AccountEntity_Id })
                .ForeignKey("dbo.MeetingRoom", t => t.MeetingRoomEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.Account", t => t.AccountEntity_Id, cascadeDelete: true)
                .Index(t => t.MeetingRoomEntity_Id)
                .Index(t => t.AccountEntity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingRoom", "Floor_Id", "dbo.Floor");
            DropForeignKey("dbo.Booking", "Owner_Id", "dbo.Account");
            DropForeignKey("dbo.Booking", "MeetingRoom_Id", "dbo.MeetingRoom");
            DropForeignKey("dbo.Asset", "Room_Id", "dbo.MeetingRoom");
            DropForeignKey("dbo.MeetingRoomAccountRellation", "AccountEntity_Id", "dbo.Account");
            DropForeignKey("dbo.MeetingRoomAccountRellation", "MeetingRoomEntity_Id", "dbo.MeetingRoom");
            DropForeignKey("dbo.Session", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.Account", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Profile", "Id", "dbo.Account");
            DropIndex("dbo.MeetingRoomAccountRellation", new[] { "AccountEntity_Id" });
            DropIndex("dbo.MeetingRoomAccountRellation", new[] { "MeetingRoomEntity_Id" });
            DropIndex("dbo.Floor", new[] { "Selector" });
            DropIndex("dbo.Floor", new[] { "Id" });
            DropIndex("dbo.Booking", new[] { "Owner_Id" });
            DropIndex("dbo.Booking", new[] { "MeetingRoom_Id" });
            DropIndex("dbo.Booking", new[] { "Selector" });
            DropIndex("dbo.Booking", new[] { "Id" });
            DropIndex("dbo.Session", new[] { "Account_Id" });
            DropIndex("dbo.Session", new[] { "Selector" });
            DropIndex("dbo.Session", new[] { "Id" });
            DropIndex("dbo.Role", new[] { "Selector" });
            DropIndex("dbo.Role", new[] { "Id" });
            DropIndex("dbo.Profile", new[] { "Selector" });
            DropIndex("dbo.Profile", new[] { "Id" });
            DropIndex("dbo.Account", new[] { "Role_Id" });
            DropIndex("dbo.Account", new[] { "Selector" });
            DropIndex("dbo.Account", new[] { "Id" });
            DropIndex("dbo.MeetingRoom", new[] { "Floor_Id" });
            DropIndex("dbo.MeetingRoom", new[] { "Selector" });
            DropIndex("dbo.MeetingRoom", new[] { "Id" });
            DropIndex("dbo.Asset", new[] { "Room_Id" });
            DropIndex("dbo.Asset", new[] { "Selector" });
            DropIndex("dbo.Asset", new[] { "Id" });
            DropTable("dbo.MeetingRoomAccountRellation");
            DropTable("dbo.Floor");
            DropTable("dbo.Booking");
            DropTable("dbo.Session");
            DropTable("dbo.Role");
            DropTable("dbo.Profile");
            DropTable("dbo.Account");
            DropTable("dbo.MeetingRoom");
            DropTable("dbo.Asset");
        }
    }
}
