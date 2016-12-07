namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_active_property_account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountEntity", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountEntity", "Active");
        }
    }
}
