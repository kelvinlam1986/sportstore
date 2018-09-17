using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SpyStore.DAL;
using SpyStore.DAL.Initializers;
using SpyStore.DAL.Repos;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Service.Filters;

namespace SpyStore.Service
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore(config => config.Filters.Add(new SpyStoreExceptionFilter()))
				.AddJsonFormatters(j => {
					j.ContractResolver = new DefaultContractResolver();
					j.Formatting = Newtonsoft.Json.Formatting.Indented;
				});

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", builder =>
				{
					builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
				});
			});

			var conn = Configuration["connectionStrings:SpyStore"];
			services.AddDbContext<StoreContext>(options => options.UseSqlServer(conn));
			services.AddScoped<ICategoryRepo, CategoryRepo>();
			services.AddScoped<IProductRepo, ProductRepo>();
			services.AddScoped<ICustomerRepo, CustomerRepo>();
			services.AddScoped<IShoppingCartRepo, ShoppingCartRepo>();
			services.AddScoped<IOrderRepo, OrderRepo>();
			services.AddScoped<IOrderDetailRepo, OrdeDetailRepo>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				using (var serviceScope = app
					.ApplicationServices
					.GetRequiredService<IServiceScopeFactory>()
					.CreateScope()) {
					StoreDataInitializer.InitializeData(serviceScope.ServiceProvider);
				}
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseMvc();
		}
	}
}
