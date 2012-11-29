using NumbersGame.Models;
using System.Collections.Generic;

namespace NumbersGame.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NumbersGame.Models.NumbersGameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NumbersGame.Models.NumbersGameContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Games.AddOrUpdate(
                g => g.Name,
                new Game
                {
                    Name = "Joey75",
                    StartingPosition = "1,2,3,4,5,6,7,0,8",
                    Moves = new List<Move> {
                        new Move { Sequence = 0,
                                   Row = 2,
                                   Column = 2
                        }
                    }
                },
                new Game
                {
                    Name = "FatSonKid",
                    StartingPosition = "1,2,3,4,5,6,0,7,8",
                    Moves = new List<Move> {
                        new Move { Sequence = 0,
                                   Row = 2,
                                   Column = 1
                        },
                        new Move { Sequence = 1,
                                   Row = 2,
                                   Column = 2
                        }
                    }
                }
            );

            context.SaveChanges();
        }
    }
}
