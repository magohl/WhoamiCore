using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WhoamiCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await WriteStuff(context);
                });
            });
        }

        private static async Task WriteStuff(HttpContext context)
        {
            await context.Response.WriteAsync($"Hostname: {System.Net.Dns.GetHostName()}{Environment.NewLine}");
            await context.Response.WriteAsync($"Method: {context.Request.Method}{Environment.NewLine}");
            await context.Response.WriteAsync($"Path: {context.Request.Path}{Environment.NewLine}");
            await context.Response.WriteAsync($"Remote-Ip:port: {context.Connection.RemoteIpAddress.MapToIPv4().ToString() + ":" + context.Connection.RemotePort.ToString()}{Environment.NewLine}");
            await context.Response.WriteAsync($"Local-Ip:port: {context.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + context.Connection.LocalPort.ToString()}{Environment.NewLine}");
            await context.Response.WriteAsync($"OS Architecture: {System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString()}{Environment.NewLine}");
            await context.Response.WriteAsync($"OS Description: {System.Runtime.InteropServices.RuntimeInformation.OSDescription}{Environment.NewLine}");
            await context.Response.WriteAsync($"Process Architecture: {System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString()}{Environment.NewLine}");
            await context.Response.WriteAsync($"Framework: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}{Environment.NewLine}");
            await context.Response.WriteAsync($"System Version: {System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion()}{Environment.NewLine}");

            foreach (var header in context.Request.Headers)
            {
                await context.Response.WriteAsync($"Request-Header {header.Key}: {header.Value}{Environment.NewLine}");
            }
        }
    }
}
