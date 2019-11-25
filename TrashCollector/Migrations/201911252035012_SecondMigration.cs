namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ApplicationId", c => c.String(maxLength: 128));
            AddColumn("dbo.Employees", "ApplicationId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "ApplicationId");
            CreateIndex("dbo.Employees", "ApplicationId");
            AddForeignKey("dbo.Customers", "ApplicationId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employees", "ApplicationId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "ApplicationId", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "ApplicationId" });
            DropIndex("dbo.Customers", new[] { "ApplicationId" });
            DropColumn("dbo.Employees", "ApplicationId");
            DropColumn("dbo.Customers", "ApplicationId");
        }
    }
}
