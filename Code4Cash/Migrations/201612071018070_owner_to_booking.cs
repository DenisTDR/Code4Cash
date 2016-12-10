namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class owner_to_booking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingEntity", "Owner_Id", c => c.Int());
            CreateIndex("dbo.BookingEntity", "Owner_Id");
            AddForeignKey("dbo.BookingEntity", "Owner_Id", "dbo.AccountEntity", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingEntity", "Owner_Id", "dbo.AccountEntity");
            DropIndex("dbo.BookingEntity", new[] { "Owner_Id" });
            DropColumn("dbo.BookingEntity", "Owner_Id");
        }
    }
}
