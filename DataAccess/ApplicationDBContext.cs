using Microsoft.EntityFrameworkCore;
using static Food_Enforcement.Models.EF_Models;


namespace Food_Enforcement.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Results> Results { get; set; }
        public DbSet<Meta> Meta { get; set; }
        public DbSet<Openfda> Openfda { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<RootObject> RootObject { get; set; }
    }
}
