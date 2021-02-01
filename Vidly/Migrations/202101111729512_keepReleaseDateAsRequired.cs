namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keepReleaseDateAsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "ReleseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "ReleseDate", c => c.DateTime());
        }
    }
}
