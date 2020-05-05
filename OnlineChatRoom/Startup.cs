using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OnlineChatRoom.DataAccess.Models;
using OnlineChatRoom.Hub;

namespace OnlineChatRoom
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConnectionString = Configuration["DatabaseConnection"];
            services.AddSignalR().AddAzureSignalR(Configuration["SignalRConnection"]);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddMvc().AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Home/Index", "");

                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddMvcOptions(options => options.EnableEndpointRouting = false);

            services.AddDbContext<ChatContext>(options =>
                options.UseSqlServer(ConnectionString));
            services.AddScoped(provider => new ChatContext(ConnectionString));
            services.AddScoped(provider =>
                new BlobServiceClient(Configuration["BlobStorageConnection"]));

            services.AddIdentity<AspNetUsers, AspNetRoles>(options =>
                {
                    options.User.RequireUniqueEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<ChatContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllers()
                .AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAzureAppConfiguration();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseFileServer();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{area=Home}/{page=Index}");


                routes.MapRoute(
                    name: "defaultControllers",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<Chat>("/signalRChat");

                endpoints.MapControllerRoute(name: "chat",
                    pattern: "chat/{action}",
                    defaults: new { controller = "Chat", action = "index" });
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
