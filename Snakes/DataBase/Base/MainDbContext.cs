using Microsoft.EntityFrameworkCore;

namespace Snakes.DataBase.Base
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        { }
        public DbSet<Models.WorldBehaviourModel> WorldBehaviours { get; set; }
    }
}
