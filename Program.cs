using System.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

/* write request pipeline time */
app.Use(async (context, next) =>
{
    await RequestStopWatch(context, next);
});

app.UseForwardedHeaders();

/* main endpoint */
app.MapGet("{*url}", async context =>
{
    context.Response.Headers.Add("Cache-Control", "no-cache");
    await WriteRequestInfo(context);
});

app.Run();

/* helper methods */
static async Task WriteRequestInfo(HttpContext context)
{
    //Write connection, request and system information
    await context.Response.WriteAsync(Logo());
    await context.Response.WriteAsync($"Hostname: {System.Net.Dns.GetHostName()}{Environment.NewLine}");
    await context.Response.WriteAsync($"Method: {context.Request.Method}{Environment.NewLine}");
    await context.Response.WriteAsync($"Path: {context.Request.Path}{Environment.NewLine}");
    await context.Response.WriteAsync($"Scheme: {context.Request.Scheme}{Environment.NewLine}");
    await context.Response.WriteAsync($"Host header: {context.Request.Host}{Environment.NewLine}");
    await context.Response.WriteAsync($"Remote-Ip:port: {context.Connection.RemoteIpAddress?.MapToIPv4().ToString()}:{context.Connection.RemotePort.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync($"Local-Ip:port: {context.Connection.LocalIpAddress?.MapToIPv4().ToString()}:{context.Connection.LocalPort.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync($"OS Architecture: {System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync($"OS Description: {System.Runtime.InteropServices.RuntimeInformation.OSDescription}{Environment.NewLine}");
    await context.Response.WriteAsync($"Runtime identifier: {System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier}{Environment.NewLine}");
    await context.Response.WriteAsync($"Framework: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}{Environment.NewLine}");
    await context.Response.WriteAsync($"CLR Version: {System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion()}{Environment.NewLine}");
    await context.Response.WriteAsync($"Processor count: {System.Environment.ProcessorCount}{Environment.NewLine}");
    await context.Response.WriteAsync($"Process architecture: {System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync($"Process start time: {System.Diagnostics.Process.GetCurrentProcess().StartTime}{Environment.NewLine}");

    await context.Response.WriteAsync($"Request Http Headers{Environment.NewLine}");

    foreach (var header in context.Request.Headers)
    {
        await context.Response.WriteAsync($" | {header.Key}: {header.Value}{Environment.NewLine}");
    }

}

static async Task RequestStopWatch(HttpContext context, Func<Task> next)
{
    var sw = new Stopwatch();
    sw.Start();
    await next.Invoke();
    sw.Stop();
    await context.Response.WriteAsync($"Request pipeline roundtrip: {sw.ElapsedMilliseconds}ms{Environment.NewLine}");
}

static string Logo() => $"""" 
___________________________________________________________
           __                          _                    
 _      __/ /_  ____  ____ _____ ___  (_)________  ________ 
| | /| / / __ \/ __ \/ __ `/ __ `__ \/ / ___/ __ \/ ___/ _ \
| |/ |/ / / / / /_/ / /_/ / / / / / / / /__/ /_/ / /  /  __/
|__/|__/_/ /_/\____/\__,_/_/ /_/ /_/_/\___/\____/_/   \___/ 
___________________________________________________________{Environment.NewLine}{Environment.NewLine}
"""";
