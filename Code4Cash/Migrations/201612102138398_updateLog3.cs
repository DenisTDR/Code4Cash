namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateLog3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingRoomPropertyUpdateLog", "BookingEntity_Id", c => c.Int());
            CreateIndex("dbo.MeetingRoomPropertyUpdateLog", "BookingEntity_Id");
            AddForeignKey("dbo.MeetingRoomPropertyUpdateLog", "BookingEntity_Id", "dbo.Booking", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingRoomPropertyUpdateLog", "BookingEntity_Id", "dbo.Booking");
            DropIndex("dbo.MeetingRoomPropertyUpdateLog", new[] { "BookingEntity_Id" });
            DropColumn("dbo.MeetingRoomPropertyUpdateLog", "BookingEntity_Id");
        }
    }
}
