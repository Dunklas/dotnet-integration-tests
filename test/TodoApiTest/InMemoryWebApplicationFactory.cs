using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models;

namespace TodoApiTest
{
    public class InMemoryWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<TodoContext>));
                services.Remove(descriptor);
                services.AddDbContext<TodoContext>(opt =>
                                                   opt.UseInMemoryDatabase("InMemoryDbForTesting"));
            });
        }   
    }
}
