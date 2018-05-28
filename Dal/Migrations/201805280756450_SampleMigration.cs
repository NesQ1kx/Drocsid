namespace Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "Email", c => c.String());
            AddColumn("dbo.Users", "Messages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Messages");
            DropColumn("dbo.Topics", "Email");
        }
    }
}
