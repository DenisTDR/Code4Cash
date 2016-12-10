namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingRoomPropertyUpdateLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(unicode: false),
                        UpdateId = c.Int(nullable: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        MeetingRoom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingRoom", t => t.MeetingRoom_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.MeetingRoom_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingRoomPropertyUpdateLog", "MeetingRoom_Id", "dbo.MeetingRoom");
            DropIndex("dbo.MeetingRoomPropertyUpdateLog", new[] { "MeetingRoom_Id" });
            DropIndex("dbo.MeetingRoomPropertyUpdateLog", new[] { "Selector" });
            DropIndex("dbo.MeetingRoomPropertyUpdateLog", new[] { "Id" });
            DropTable("dbo.MeetingRoomPropertyUpdateLog");
        }
    }
}
