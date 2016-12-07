namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role_functions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleEntity", "Functions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoleEntity", "Functions");
        }
    }
}
