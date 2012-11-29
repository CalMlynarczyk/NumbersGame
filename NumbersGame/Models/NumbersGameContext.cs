using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NumbersGame.Models
{
    public class NumbersGameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}