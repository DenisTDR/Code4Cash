namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_asset_value : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssetEntity", "Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssetEntity", "Value");
        }
    }
}
