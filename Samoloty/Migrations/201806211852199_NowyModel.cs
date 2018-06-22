namespace Samoloty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NowyModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        dep = c.String(),
                        dest = c.String(),
                        AircraftID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aircraft", t => t.AircraftID, cascadeDelete: true)
                .Index(t => t.AircraftID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "AircraftID", "dbo.Aircraft");
            DropIndex("dbo.Flights", new[] { "AircraftID" });
            DropTable("dbo.Flights");
        }
    }
}
