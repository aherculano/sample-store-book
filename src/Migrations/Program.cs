using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Migrations;
public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<MigrationsDbContext>((options) =>
                {
                    options.UseSqlServer("Server=localhost:1433;Database=SVC_BOOK_STORE;User Id=sa;Password=P4ssw0rd!;");
                });
            })
            .Build();
        
        host.Run();
    }      
}
