namespace Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Price", c => c.Single(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Item", "Price");
        }
    }
}
