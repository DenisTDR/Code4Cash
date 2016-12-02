namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_selector : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssetEntity", "Selector", c => c.String(maxLength: 20, storeType: "nvarchar"));
            AddColumn("dbo.BookingEntity", "Selector", c => c.String(maxLength: 20, storeType: "nvarchar"));
            AddColumn("dbo.MeetingRoomEntity", "Selector", c => c.String(maxLength: 20, storeType: "nvarchar"));
            CreateIndex("dbo.AssetEntity", "Id");
            CreateIndex("dbo.AssetEntity", "Selector");
            CreateIndex("dbo.BookingEntity", "Id");
            CreateIndex("dbo.BookingEntity", "Selector");
            CreateIndex("dbo.MeetingRoomEntity", "Id");
            CreateIndex("dbo.MeetingRoomEntity", "Selector");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MeetingRoomEntity", new[] { "Selector" });
            DropIndex("dbo.MeetingRoomEntity", new[] { "Id" });
            DropIndex("dbo.BookingEntity", new[] { "Selector" });
            DropIndex("dbo.BookingEntity", new[] { "Id" });
            DropIndex("dbo.AssetEntity", new[] { "Selector" });
            DropIndex("dbo.AssetEntity", new[] { "Id" });
            DropColumn("dbo.MeetingRoomEntity", "Selector");
            DropColumn("dbo.BookingEntity", "Selector");
            DropColumn("dbo.AssetEntity", "Selector");
        }
    }
}
