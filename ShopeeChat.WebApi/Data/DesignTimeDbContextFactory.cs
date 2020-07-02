using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ShopeeChat.Infrastructure.Utility;
using System;
using System.IO;

namespace ShopeeChat.WebApi.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShopeeChatDbContext>
    {
        public ShopeeChatDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ShopeeChatDbContext>();
            var connectionString = AppSettings.Get<string>("Databases:SqlServer:ConnectionStrings:DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new ShopeeChatDbContext(builder.Options);
        }
    }
}