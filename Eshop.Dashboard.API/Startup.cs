using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Dashboard.API.ViewModels.Products;
using Eshop.Dashboard.API.ViewModels.Users;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
<<<<<<< HEAD
using Eshop.Dashboard.Services.Helpers;
=======
using Eshop.Dashboard.Services.Dto;
>>>>>>> 44f4c4b5e7a98efcafd045e0864b15d8cfbc88b3
using Eshop.Dashboard.Services.Repositories;
using Eshop.Dashboard.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

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
      // allows CORS
      services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Tokens:Issuer"],
            ValidAudience = Configuration["Tokens:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
          };
        });

      services.AddMvc().AddJsonOptions(options =>
        {
          options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });

      // Register the Swagger generator, defining one or more Swagger documents  
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "EShop API", Version = "v1" });
      });

      var connectionString = Configuration["connectionStrings:eshopDasboardDBConnectionString"];
      services.AddDbContext<EshopDbContext>(o => o.UseSqlServer(connectionString));

      services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
      services.AddScoped<IUrlHelper>(implementationFactory =>
      {
        var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
        return new UrlHelper(actionContext);
      });
      services.AddTransient<IPropertyMappingService>(implementationFactory =>
      {
        // TODO: move this into separate file
        IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();

        // TODO: use nameof from interface
        Dictionary<string, PropertyMappingValue> productPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
          {"Id", new PropertyMappingValue(new List<string>() {"Id"})},
          {"Name", new PropertyMappingValue(new List<string>() {"Name"})},
          {"Description", new PropertyMappingValue(new List<string>() {"Description"})},
          {"Price", new PropertyMappingValue(new List<string>() {"Price"})},
          {"Category", new PropertyMappingValue(new List<string>() {"Category.Name"})}
        };

        propertyMappings.Add(new PropertyMapping<Services.Repositories.ProductDtoViewModel, Product>(productPropertyMapping));
        return new PropertyMappingService(propertyMappings);
      });

      services.AddScoped<IUsersRepository, UsersRepository>();
      services.AddScoped<IProductsRepository, ProductsRepository>();

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

      // Enable middleware to serve generated Swagger as a JSON endpoint.  
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.  
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eshop API V1");
      });

      //TODO: move this into separate automapper config file
      AutoMapper.Mapper.Initialize(cfg =>
      {
        cfg.CreateMap<RegisterViewModel, User>();
        cfg.CreateMap<User, UserDtoViewModel>();
        cfg.CreateMap<Product, ViewModels.Products.ProductDtoViewModel>()
          .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
      });

      app.UseCors("AllowAll");

      app.UseAuthentication();

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseMvc();
    }
  }
}
