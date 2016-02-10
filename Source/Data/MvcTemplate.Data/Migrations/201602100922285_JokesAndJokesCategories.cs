namespace MvcTemplate.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class JokesAndJokesCategories : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Jokes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JokeCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.JokeCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.Jokes", "CategoryId", "dbo.JokeCategories");
            this.DropIndex("dbo.JokeCategories", new[] { "IsDeleted" });
            this.DropIndex("dbo.Jokes", new[] { "IsDeleted" });
            this.DropIndex("dbo.Jokes", new[] { "CategoryId" });
            this.DropTable("dbo.JokeCategories");
            this.DropTable("dbo.Jokes");
        }
    }
}
