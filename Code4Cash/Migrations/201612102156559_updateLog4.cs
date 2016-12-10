namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateLog4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "MeetingRoomPropertyUpdateLog", newName: "BookingPropertyUpdateLog");
        }
        
        public override void Down()
        {
            RenameTable(name: "BookingPropertyUpdateLog", newName: "MeetingRoomPropertyUpdateLog");
        }
    }
}
