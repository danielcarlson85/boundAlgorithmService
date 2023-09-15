using AutoMapper;
using Bound.AlgorithmService.EventBus;
using Bound.AlgorithmService.Abstractions.Constants;
using Bound.AlgorithmService.Abstractions.Interfaces;
using Bound.AlgorithmService.Abstractions.Models.Settings;
using Bound.AlgorithmService.Managers.Authorization;
using Bound.AlgorithmService.Managers.Mapping;
using Bound.AlgorithmService.Managers.Services;
using Bound.AlgorithmService.Runtime.ExtensionMethods;
using Bound.Nugets.AzureADB2C.Local;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using WorkoutData.Managers;
using WorkoutData.Abstractions.Interfaces;

namespace Bound.AlgorithmService.Runtime
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerConfig();

            services.AddAzureADB2CAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                   policy.RequireClaim("extension_Role", "Admin"));
            });

            services.AddControllers();

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IADTokenService, ADTokenService>();
            services.AddTransient<IRestClient,  RestClient>();
            services.AddScoped<JwtSecurityTokenHandler>();

            services.Configure<EventBusSettings>(options => _configuration.GetSection("EventBus").Bind(options));
            string blobStorageConnectionString = this._configuration.GetConnectionString("StorageAccountStorageConnectionString");

            services.AddScoped<IBlobContainerManager>(_ => new BlobContainerManager(blobStorageConnectionString));
            services.AddScoped<IBlobManager>(_ => new BlobsManager(blobStorageConnectionString));

            var eventBusConnectionString = _configuration["EventBus:ConnectionString"];
            services.AddUserEventBus(eventBusConnectionString);
            services.AddGraphUserAPI(_configuration);

            services.AddApplicationInsightsTelemetry(_configuration["APPINSIGHTS_CONNECTIONSTRING"]);
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserEventBusHandler userEventBusHandler)
        {
            await userEventBusHandler.StartRecieveMessageAsync();

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(SwaggerConstants.SwaggerEndpoint, $"{SwaggerConstants.Title} {SwaggerConstants.Version}"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}