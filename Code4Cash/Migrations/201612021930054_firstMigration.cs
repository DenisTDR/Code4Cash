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
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        MeetingRoomEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingRoomEntity", t => t.MeetingRoomEntity_Id)
                .Index(t => t.MeetingRoomEntity_Id);
            
            CreateTable(
                "dbo.BookingEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false, precision: 0),
                        End = c.DateTime(nullable: false, precision: 0),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        MeetingRoom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingRoomEntity", t => t.MeetingRoom_Id)
                .Index(t => t.MeetingRoom_Id);
            
            CreateTable(
                "dbo.MeetingRoomEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingEntity", "MeetingRoom_Id", "dbo.MeetingRoomEntity");
            DropForeignKey("dbo.AssetEntity", "MeetingRoomEntity_Id", "dbo.MeetingRoomEntity");
            DropIndex("dbo.BookingEntity", new[] { "MeetingRoom_Id" });
            DropIndex("dbo.AssetEntity", new[] { "MeetingRoomEntity_Id" });
            DropTable("dbo.MeetingRoomEntity");
            DropTable("dbo.BookingEntity");
            DropTable("dbo.AssetEntity");
        }
    }
}
