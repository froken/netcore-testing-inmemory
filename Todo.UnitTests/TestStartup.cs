using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Database.Contexts;
using Todo.Server;

namespace Todo.UnitTests
{
    public class TestStartup : Startup
    {
        public static SqliteConnection Connection { get; set; }

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(options => options.UseSqlite(Connection));
        }
    }
}
