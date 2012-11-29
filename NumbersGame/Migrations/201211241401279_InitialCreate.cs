namespace NumbersGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.Move",
                c => new
                    {
                        MoveId = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MoveId)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Move", new[] { "GameId" });
            DropForeignKey("dbo.Move", "GameId", "dbo.Game");
            DropTable("dbo.Move");
            DropTable("dbo.Game");
        }
    }
}
