var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("{*url}", async context =>
{
    context.Response.Headers.Add("Cache-Control", "no-cache");
    await WriteRequestInfo(context);
});

app.Run();

static async Task WriteRequestInfo(HttpContext context)
{
    //Write connection, request and system information
    await context.Response.WriteAsync($"Hostname: {System.Net.Dns.GetHostName()}{Environment.NewLine}");
    await context.Response.WriteAsync($"Method: {context.Request.Method}{Environment.NewLine}");
    await context.Response.WriteAsync($"Path: {context.Request.Path}{Environment.NewLine}");
    await context.Response.WriteAsync($"Scheme: {context.Request.Scheme}{Environment.NewLine}");
    await context.Response.WriteAsync($"Protocol: {context.Request.Protocol}{Environment.NewLine}");
    await context.Response.WriteAsync($"Host header: {context.Request.Host}{Environment.NewLine}");
    await context.Response.WriteAsync(Environment.NewLine);

    await context.Response.WriteAsync($"Remote-Ip:port: {context.Connection.RemoteIpAddress.MapToIPv4().ToString()}:{context.Connection.RemotePort.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync($"Local-Ip:port: {context.Connection.LocalIpAddress.MapToIPv4().ToString()}:{context.Connection.LocalPort.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync(Environment.NewLine);

    await context.Response.WriteAsync($"OS Architecture: {System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync($"OS Description: {System.Runtime.InteropServices.RuntimeInformation.OSDescription}{Environment.NewLine}");
    await context.Response.WriteAsync($"Runtime identifier: {System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier}{Environment.NewLine}");
    await context.Response.WriteAsync($"Framework: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}{Environment.NewLine}");
    await context.Response.WriteAsync($"Process Architecture: {System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString()}{Environment.NewLine}");
    await context.Response.WriteAsync($"Processor count: {System.Environment.ProcessorCount}{Environment.NewLine}");
    await context.Response.WriteAsync($"System Version: {System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion()}{Environment.NewLine}");
    await context.Response.WriteAsync(Environment.NewLine);

    await context.Response.WriteAsync($"System UTC: {DateTime.UtcNow}{Environment.NewLine}");
    await context.Response.WriteAsync($"Process Start: {System.Diagnostics.Process.GetCurrentProcess().StartTime}{Environment.NewLine}");
    await context.Response.WriteAsync(Environment.NewLine);

    //Write HTTP headers
    foreach (var header in context.Request.Headers)
    {
        await context.Response.WriteAsync($"Request Http Header - {header.Key}: {header.Value}{Environment.NewLine}");
    }
}