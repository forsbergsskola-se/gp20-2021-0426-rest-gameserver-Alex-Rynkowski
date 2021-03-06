using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MMORPG.Exceptions;
using MMORPG.Repositories;

namespace MMORPG.Api{
    public class Startup{
        public Startup(IConfiguration configuration){
            this.Configuration = configuration;
        }

        public IConfiguration Configuration{ get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){
            
            services.AddSingleton<IRepository, MongoDbRepository>();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo{Title = "MMORPG", Version = "v1"}); });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MMORPG v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}