namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class description_to_asset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssetEntity", "Description", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssetEntity", "Description");
        }
    }
}
