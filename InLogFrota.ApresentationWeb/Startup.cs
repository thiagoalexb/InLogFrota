using AutoMapper;
using InLogFrota.ApresentationWeb.Extensions;
using InLogFrota.ApresentationWeb.Mapper;
using InLogFrota.Core.Notifications;
using InLogFrota.Core.Repositories;
using InLogFrota.Core.Services;
using InLogFrota.Core.UnitOfWork;
using InLogFrota.Data.Context;
using InLogFrota.Impl.Notifications;
using InLogFrota.Impl.Repositories;
using InLogFrota.Impl.Services;
using InLogFrota.Impl.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InLogFrota.ApresentationWeb
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

            services.AddAutoMapper(typeof(Startup));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToViewModelProfile());
                cfg.AddProfile(new ViewModelToEntityProfile());
            });

            services.AddMediatR(typeof(Startup));

            services.AddSingleton(mapperConfig);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, InLogFrotaContext>();

            services.AddScoped<IVeiculoService, VeiculoService>();

            services.AddScoped<IVeiculoRepository, VeiculoRepository>();

            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<Notification>, NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseErrorHandlingMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Veiculo}/{action=Index}/{id?}");
            });
        }
    }
}
