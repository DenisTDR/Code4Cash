namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_name_to_meeting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingRoomEntity", "Name", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeetingRoomEntity", "Name");
        }
    }
}
