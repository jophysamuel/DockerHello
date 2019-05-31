using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DockerHelloSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var webProxy = new WebProxy(
                new Uri(configuration["ProxyUri"]),
                BypassOnLocal: false);

            var proxyHttpClientHandler = new HttpClientHandler
            {
                Proxy = webProxy,
                UseProxy = true,
            };

            var httpClient = new HttpClient(proxyHttpClientHandler)
            {
                BaseAddress = new Uri(configuration["RestServiceUri"])
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}


//<add key = "http_proxy" value="http://your_proxy_url:8080" />

//https://stackoverflow.com/questions/41185443/nuget-connection-attempt-failed-unable-to-load-the-service-index-for-source/51456498#51456498