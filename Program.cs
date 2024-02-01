
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// namespace YourNamespace
// {
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        // Equivalent of Startup.ConfigureServices
                        services.AddSingleton<CardService>();
                        services.AddSingleton<FeesService>();

                        services.AddControllers();  // 

                        // Add more services here
                    })
                    .Configure(app =>
                    {

                        // registers middleware for basic authentication
                        //app.UseMiddleware<BasicAuthMiddleware>();

                        // registers middleware for secure authentication
                        app.UseMiddleware<SecureAuthMiddleware>();

                        // Equivalent of Startup.Configure
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();     // Use the developer exception page in Development environment for detailed error information
                        }

                        app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
                        app.UseRouting(); // Enables routing functionality in the application
                        app.UseAuthorization();  // Enables authorization middleware. 
                        // Map the endpoints of the application
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers(); // Use attribute routing for controllers
                        });
                        //app.Run();  // 
                    });
                });
    }
// }
