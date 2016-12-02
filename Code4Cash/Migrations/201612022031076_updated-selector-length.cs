namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedselectorlength : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AssetEntity", new[] { "Selector" });
            DropIndex("dbo.BookingEntity", new[] { "Selector" });
            DropIndex("dbo.MeetingRoomEntity", new[] { "Selector" });
            AlterColumn("dbo.AssetEntity", "Selector", c => c.String(maxLength: 36, storeType: "nvarchar"));
            AlterColumn("dbo.BookingEntity", "Selector", c => c.String(maxLength: 36, storeType: "nvarchar"));
            AlterColumn("dbo.MeetingRoomEntity", "Selector", c => c.String(maxLength: 36, storeType: "nvarchar"));
            CreateIndex("dbo.AssetEntity", "Selector");
            CreateIndex("dbo.BookingEntity", "Selector");
            CreateIndex("dbo.MeetingRoomEntity", "Selector");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MeetingRoomEntity", new[] { "Selector" });
            DropIndex("dbo.BookingEntity", new[] { "Selector" });
            DropIndex("dbo.AssetEntity", new[] { "Selector" });
            AlterColumn("dbo.MeetingRoomEntity", "Selector", c => c.String(maxLength: 20, storeType: "nvarchar"));
            AlterColumn("dbo.BookingEntity", "Selector", c => c.String(maxLength: 20, storeType: "nvarchar"));
            AlterColumn("dbo.AssetEntity", "Selector", c => c.String(maxLength: 20, storeType: "nvarchar"));
            CreateIndex("dbo.MeetingRoomEntity", "Selector");
            CreateIndex("dbo.BookingEntity", "Selector");
            CreateIndex("dbo.AssetEntity", "Selector");
        }
    }
}
