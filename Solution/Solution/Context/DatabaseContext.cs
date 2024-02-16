using Microsoft.EntityFrameworkCore;
using Solution.Models;

namespace Solution.Contexts
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _configuration = configuration;
        }

        public DbSet<MasterUserModel> MasterUsers { get; set; }
        public DbSet<TransactionDocumentModel> TransactionDocuments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            var connectionString = _configuration.GetConnectionString("PostgreSQLConnection");
            options.UseNpgsql(connectionString);

        }
    }
}
