using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySimpleBlog.Data;

namespace MySimpleBlog
{
    public class Startup
    {
        private static Random gen = new Random();


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddDbContext<APIContext>(opt => opt.UseInMemoryDatabase("RIP"));

            var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<APIContext>
                (options => options.UseSqlServer(connection));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                var context = serviceProvider.GetService<APIContext>();

                AddTempData(context);

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Posts}/{action=Index}/{id?}");
            });
        }

        private static void AddTempData(APIContext context)
        {
            // Generating temp users to fill the db //
            User tempUser = AddTempDataGenerateUser("egyeske", "elsoNeve", "elsoCsaladja", context);
            AddTempDataPost(tempUser, "elsoPostID", "elso comment", context);

            tempUser = AddTempDataGenerateUser("ketteske", "kettesNeve", "kettesCsaladja", context);
            AddTempDataPost(tempUser, "masodikPostID", "masodik comment", context);

            tempUser = AddTempDataGenerateUser("harmaska", "harmasNeve", "harmasCsaladja", context);
            AddTempDataPost(tempUser, "harmadikPostID", "harmadik comment", context);

            tempUser = AddTempDataGenerateUser("harmaska", "harmasNeve", "harmasCsaladja", context);
            AddTempDataPost(tempUser, "negyedikPostID", "negyedik comment", context);
        }

        private static void AddTempDataPost(User user, string postId, string postComment, APIContext context)
        {
            Post testPost = new Post
            {
                Id = postId,
                UserId = user.Id,
                User = user,
                Content = postComment,
                CreationDate = RandomDay()
            };
            
            context.Posts.Add(testPost);
            context.SaveChangesAsync();
        }

        private static User AddTempDataGenerateUser(string userId, string firstName, string lastName, APIContext context)
        {
            User testUser = new User
            {
                Id = userId,
                FirstName = firstName,
                LastName = lastName
            };

            //context.Users.Add(testUser);
            //context.SaveChanges();

            return testUser;
        }

        private static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
