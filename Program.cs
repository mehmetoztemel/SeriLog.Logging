using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Serilog Registration Configurations

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs/.txt"
    , rollingInterval: RollingInterval.Day
    , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}], {Message:lj} {RequestMethod} {RequestPath} {RemoteIpAddress}{NewLine}{Exception}")
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Services.AddSerilog();


//Serilog Register From appsettings.json
//builder.Host.UseSerilog((context, configuration) =>
//configuration.ReadFrom.Configuration(context.Configuration));

#endregion


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// This section covers configuring Serilog to log each HTTP request. It is used to customize log messages, determine log levels, and add extra information to diagnostic context.
app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
    };
});

app.MapControllers();

app.Run();
