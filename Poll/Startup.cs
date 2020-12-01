using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using PollProject.Repository;
using PollProject.Infra.Database;
using Microsoft.OpenApi.Models;

namespace PollProject
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Poll API", Version = "v1" });
            });

            services.AddScoped<IPollRepository, PollRepository>();
            services.AddScoped<IPollStatsRepository, PollStatsRepository>();
            services.AddScoped<IPollOptionsRepository, PollOptionsRepository>();

            var mongoClient = new MongoClient(Configuration.GetSection("MongoConnection:ConnectionString").Value);
            var database = mongoClient.GetDatabase(Configuration.GetSection("MongoConnection:Database").Value);
            services.AddScoped(_ => database);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Poll API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MongoDbPersistence.Configure();
        }
    }
}
