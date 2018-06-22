namespace Samoloty.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Samoloty.Models.AircraftDBCtxt>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Samoloty.Models.AircraftDBCtxt";
        }

        protected override void Seed(Samoloty.Models.AircraftDBCtxt context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
