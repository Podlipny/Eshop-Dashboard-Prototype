using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Dashboard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Eshop.Dashboard.API
{
  /// <summary>
  /// Resoves problem with migration
  /// An error occurred while calling method 'BuildWebHost' on class 'Program'. Continuing without the application service provider. Error: Invalid object name 'Categories'.
  /// Unable to create an object of type 'EshopDbContext'. Add an implementation of 'IDesignTimeDbContextFactory<EshopDbContext>' to the project, or see https://go.microsoft.com/fwlink/?linkid=851728 for additional patterns supported at design time.
  /// </summary>
  public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EshopDbContext>
  {
    public EshopDbContext CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var builder = new DbContextOptionsBuilder<EshopDbContext>();

      var connectionString = configuration["ConnectionStrings:eshopDasboardDBConnectionString"];

      builder.UseSqlServer(connectionString);

      return new EshopDbContext(builder.Options);
    }
  }
}
