namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updateLog2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingRoomPropertyUpdateLog", "OldValue", c => c.String(unicode: false));
            AddColumn("dbo.MeetingRoomPropertyUpdateLog", "NewValue", c => c.String(unicode: false));
        }

        public override void Down()
        {
            DropColumn("dbo.MeetingRoomPropertyUpdateLog", "NewValue");
            DropColumn("dbo.MeetingRoomPropertyUpdateLog", "OldValue");
        }
    }
}
