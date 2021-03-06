using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using StoreBL;
using StoreDL;
using StoreMVC.Models;

namespace StoreMVC
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
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
            });
            services.AddDbContext<StoreDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("StoreDB")));
            services.AddScoped<ICustomerRepository, CustomerRepoDB>();
            services.AddScoped<ICustomerBL, CustomerBL>();
            services.AddScoped<IManagerRepository, ManagerRepoDB>();
            services.AddScoped<IManagerBL, ManagerBL>();
            services.AddScoped<ILocationRepository, LocationRepoDB>();
            services.AddScoped<ILocationBL, LocationBL>();
            services.AddScoped<IProductRepository, ProductRepoDB>();
            services.AddScoped<IProductBL, ProductBL>();
            services.AddScoped<IInventoryLineItemRepository, InventoryLineItemRepoDB>();
            services.AddScoped<IInventoryLineItemBL, InventoryLineItemBL>();
            services.AddScoped<ICustomerCartRepository, CustomerCartRepoDB>();
            services.AddScoped<ICustomerCartBL, CustomerCartBL>();
            services.AddScoped<ICustomerOrderHistoryRepository, CustomerOrderHistoryRepoDB>();
            services.AddScoped<ICustomerOrderHistoryBL, CustomerOrderHistoryBL>();
            services.AddScoped<ICustomerOrderLineItemRepository, CustomerOrderLineItemRepoDB>();
            services.AddScoped<ICustomerOrderLineItemBL, CustomerOrderLineItemBL>();
            services.AddScoped<IMapper, Mapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template
                options.MessageTemplate = "Handled {RequestPath}";

                // Emit debug-level events instead of the defaults
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });
            app.UseHttpsRedirection();
            

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
