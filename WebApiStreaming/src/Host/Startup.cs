using System;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Host
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.Use(async (context, next) =>
            {
                if (!IsMultipartContentType(context.Request.ContentType))
                {
                    await next();
                    return;
                }

                var boundary = GetBoundary(context.Request.ContentType);
                var reader = new MultipartReader(boundary, context.Request.Body);
                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    // process each image
                    const int chunkSize = 1024;
                    var buffer = new byte[chunkSize];
                    var bytesRead = 0;
                    var fileName = GetFileName(section.ContentDisposition);

                    using (var stream = new FileStream(fileName, FileMode.Append))
                    {
                        do
                        {
                            bytesRead = await section.Body.ReadAsync(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, bytesRead);

                        } while (bytesRead > 0);
                    }

                    section = await reader.ReadNextSectionAsync();
                }

                //context.Response.Body.WriteAsync("Done.");
            });

            app.UseStaticFiles();

            app.UseMvc();
        }

        private static bool IsMultipartContentType(string contentType)
        {
            return
                !string.IsNullOrEmpty(contentType) &&
                contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string GetBoundary(string contentType)
        {
            var elements = contentType.Split(' ');
            var element = elements.Where(entry => entry.StartsWith("boundary=")).First();
            var boundary = element.Substring("boundary=".Length);
            // Remove quotes
            if (boundary.Length >= 2 && boundary[0] == '"' &&
                boundary[boundary.Length - 1] == '"')
            {
                boundary = boundary.Substring(1, boundary.Length - 2);
            }
            return boundary;
        }

        private string GetFileName(string contentDisposition)
        {
            return ContentDispositionHeaderValue.Parse(contentDisposition).FileName;
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
