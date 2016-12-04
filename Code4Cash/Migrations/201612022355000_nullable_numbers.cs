namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable_numbers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeetingRoomEntity", "Capacity", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeetingRoomEntity", "Capacity", c => c.Int(nullable: false));
        }
    }
}
