namespace Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "TopicName", c => c.String());
            DropColumn("dbo.Comments", "TopicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "TopicId", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "TopicName");
        }
    }
}
