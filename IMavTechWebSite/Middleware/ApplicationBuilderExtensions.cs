using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures and adds static file middleware to serve files directly from the node_modules
        /// folder in this project
        /// </summary>
        /// <param name="app">The IApplicationBuilder object</param>
        /// <param name="environment">The IHostingEnvironment object</param>
        /// <param name="maxAge">If specified, the max time that a static file can be cached</param>
        /// <returns>The IApplicationBuilder object</returns>
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app,
                                                         IHostingEnvironment environment,
                                                         TimeSpan? maxAge = null,
                                                         string requestPath = "/node_modules")
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            AddMiddleware(app, environment, maxAge, requestPath);

            return app;
        }

        private static void AddMiddleware(IApplicationBuilder app, IHostingEnvironment environment, TimeSpan? maxAge, string requestPath)
        {
            var path = Path.Combine(environment.ContentRootPath, "node_modules");
            var provider = new PhysicalFileProvider(path);

            var options = new FileServerOptions { RequestPath = requestPath };
            options.StaticFileOptions.FileProvider = provider;

            if (maxAge != null)
            {
                options.StaticFileOptions.OnPrepareResponse = context =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue {MaxAge = maxAge, Public = true};
                };
            }

            options.EnableDirectoryBrowsing = false;
            app.UseFileServer(options);
        }
    }
}
