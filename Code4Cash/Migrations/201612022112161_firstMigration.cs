namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        MeetingRoomEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingRoomEntity", t => t.MeetingRoomEntity_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.MeetingRoomEntity_Id);
            
            CreateTable(
                "dbo.BookingEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false, precision: 0),
                        End = c.DateTime(nullable: false, precision: 0),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        MeetingRoom_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingRoomEntity", t => t.MeetingRoom_Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.MeetingRoom_Id);
            
            CreateTable(
                "dbo.MeetingRoomEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Capacity = c.Int(nullable: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Selector);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingEntity", "MeetingRoom_Id", "dbo.MeetingRoomEntity");
            DropForeignKey("dbo.AssetEntity", "MeetingRoomEntity_Id", "dbo.MeetingRoomEntity");
            DropIndex("dbo.MeetingRoomEntity", new[] { "Selector" });
            DropIndex("dbo.MeetingRoomEntity", new[] { "Id" });
            DropIndex("dbo.BookingEntity", new[] { "MeetingRoom_Id" });
            DropIndex("dbo.BookingEntity", new[] { "Selector" });
            DropIndex("dbo.BookingEntity", new[] { "Id" });
            DropIndex("dbo.AssetEntity", new[] { "MeetingRoomEntity_Id" });
            DropIndex("dbo.AssetEntity", new[] { "Selector" });
            DropIndex("dbo.AssetEntity", new[] { "Id" });
            DropTable("dbo.MeetingRoomEntity");
            DropTable("dbo.BookingEntity");
            DropTable("dbo.AssetEntity");
        }
    }
}
