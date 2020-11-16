using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreDAC;
using StoreDAC.DBContext;
using StoreDAC.Repositories;
using StoreServices.Repo;
using StoreServices;
using RetailStoreAPI.Extensions;
using Microsoft.Extensions.Logging;
using RetailStoreAPI.Filters;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;

namespace RetailStoreAPI
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
            services.AddMvcCore(opt =>
            {
                opt.Filters.Add(new ValidatorActionFilter());
            }).AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
                s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

            var connectionString = Configuration["ConnectionStrings:RetailStoreDBConnectionString"];
            services.AddDbContext<RetailStoreDBContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IProdCategoryService, ProdCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBarCodeService, BarCodeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillItemService, BillItemService>();

            services.AddScoped<IProdCategoryRepository, ProdCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBarcodeRepository, BarcodeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillItemRepository, BillItemRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMemoryCache();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                        
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "RetailStore Billing API" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RetailStore Billing API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });          

        }
    }
}
