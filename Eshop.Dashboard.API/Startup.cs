using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Dashboard.API.ViewModels.Users;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Eshop.Dashboard.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      var connectionString = Configuration["connectionStrings:eshopDasboardDBConnectionString"];
      services.AddDbContext<EshopDbContext>(o => o.UseSqlServer(connectionString));

      services.AddScoped<IUsersRepository, UsersRepository>();

      services.AddMemoryCache();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, EshopDbContext dbContext)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        dbContext.EnsureSeedDataForContext();
      }
      else
      {
        app.UseExceptionHandler(appBuilder =>
        {
          appBuilder.Run(async context =>
          {
            // global exception handling
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerFeature != null)
            {
              var logger = loggerFactory.CreateLogger("Global exception logger");
              logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);

              // in case we have exceptionHandlerFeature we are able to send exception message
              context.Response.StatusCode = 500;
              await context.Response.WriteAsync(exceptionHandlerFeature.Error.Message);
            }
            else
            {
              // for all non handled exceptions send status code 500
              context.Response.StatusCode = 500;
              await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            }
          });
        });
      }

      AutoMapper.Mapper.Initialize(cfg =>
      {
        cfg.CreateMap<RegisterViewModel, User>();

      });

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseMvc();
    }
  }
}
