namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsomePropsToMovieModelforce : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "ReleseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "ReleseDate", c => c.DateTime(nullable: true));
        }
    }
}
