using Serilog;
using Serilog.Events;
using Wpm.Web.Dal;
using Wpm.Web.Middleware;
using Wpm.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("PetPhotoClient")
    .ConfigureHttpClient(client =>
    {
        client.Timeout = TimeSpan.FromSeconds(30);
    });

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/wpm-.log", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSqliteWpmDb(builder.Configuration);
builder.Services.AddTransient<EncryptionService>();
builder.Services.AddDataProtection();

var app = builder.Build();
app.Services.EnsureWpmDbIsCreated();


app.UseMiddleware<ErrorLoggingMiddleware>();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();