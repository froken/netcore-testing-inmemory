using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Database.Contexts;
using Todo.Database.Repositories;

namespace Todo.Server
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
            Mapper.Initialize(cfg => { cfg.AddProfile(new TodoMapperProfile()); });

            services.AddAutoMapper();

            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddMvc();

            ConfigureDbContext(services);
        }

        public virtual void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("todo"),
                                    x => x.MigrationsAssembly("Todo.Database")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}
