namespace Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kek : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        AuthorId = c.Int(nullable: false),
                        Author = c.String(),
                        TopicId = c.Int(nullable: false),
                        TopicName = c.String(),
                        Pubdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                        SectionId = c.Int(nullable: false),
                        Text = c.String(),
                        AuthorId = c.Int(nullable: false),
                        Author = c.String(),
                        Email = c.String(),
                        Pubdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        ConfirmedEmail = c.Boolean(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        Messages = c.Int(nullable: false),
                        Role = c.String(),
                        Status = c.Int(nullable: false),
                        IsBaned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Topics");
            DropTable("dbo.Sections");
            DropTable("dbo.Comments");
        }
    }
}
