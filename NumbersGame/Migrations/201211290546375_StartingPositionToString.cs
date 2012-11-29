namespace NumbersGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartingPositionToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game", "StartingPosition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "StartingPosition");
        }
    }
}
