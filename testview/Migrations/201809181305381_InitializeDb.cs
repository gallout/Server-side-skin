namespace testview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        completed = c.Boolean(nullable: false),
                        Secret = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tasks");
        }
    }
}
