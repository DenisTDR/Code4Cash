namespace Code4Cash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_entites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileEntity",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountEntity", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Selector);
            
            CreateTable(
                "dbo.AccountEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleEntity", t => t.Role_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.RoleEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Power = c.Int(nullable: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Selector);
            
            CreateTable(
                "dbo.SessionEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Expiration = c.DateTime(nullable: false, precision: 0),
                        Token = c.String(unicode: false),
                        Selector = c.String(maxLength: 36, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Updated = c.DateTime(nullable: false, precision: 0),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountEntity", t => t.Account_Id)
                .Index(t => t.Id)
                .Index(t => t.Selector)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileEntity", "Id", "dbo.AccountEntity");
            DropForeignKey("dbo.SessionEntity", "Account_Id", "dbo.AccountEntity");
            DropForeignKey("dbo.AccountEntity", "Role_Id", "dbo.RoleEntity");
            DropIndex("dbo.SessionEntity", new[] { "Account_Id" });
            DropIndex("dbo.SessionEntity", new[] { "Selector" });
            DropIndex("dbo.SessionEntity", new[] { "Id" });
            DropIndex("dbo.RoleEntity", new[] { "Selector" });
            DropIndex("dbo.RoleEntity", new[] { "Id" });
            DropIndex("dbo.AccountEntity", new[] { "Role_Id" });
            DropIndex("dbo.AccountEntity", new[] { "Selector" });
            DropIndex("dbo.AccountEntity", new[] { "Id" });
            DropIndex("dbo.ProfileEntity", new[] { "Selector" });
            DropIndex("dbo.ProfileEntity", new[] { "Id" });
            DropTable("dbo.SessionEntity");
            DropTable("dbo.RoleEntity");
            DropTable("dbo.AccountEntity");
            DropTable("dbo.ProfileEntity");
        }
    }
}
