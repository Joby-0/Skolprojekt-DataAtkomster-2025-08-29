using Configuration;
using Configuration.Options;
using DbContext;
using DbRepos;
using Services;
using Microsoft.EntityFrameworkCore;
using DbModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
//using user secrets in development
var currentDir = Directory.GetCurrentDirectory();
var assembly = System.Reflection.Assembly.Load("Configuration");
builder.Configuration.SetBasePath(Path.Combine(currentDir, "../AppWebApi"))
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddUserSecrets(assembly);

// adding options patterns to read appsettings and user secrets
builder.Services.Configure<AesEncryptionOptions>(
    options => builder.Configuration.GetSection(AesEncryptionOptions.Position).Bind(options));

builder.Services.Configure<JwtOptions>(
    options => builder.Configuration.GetSection(JwtOptions.Position).Bind(options));

// adding options and service for multiple Database connections and their respective DbContexts
builder.Services.Configure<DbConnectionSetsOptions>(
    options => builder.Configuration.GetSection(DbConnectionSetsOptions.Position).Bind(options));

// adding verion info
builder.Services.Configure<VersionOptions>(options =>VersionOptions.ReadFromAssembly(options));

// // Registering database connections service
builder.Services.AddSingleton<DatabaseConnections>();
// adding DbContexts
builder.Services.AddDbContext<MainDbContext>((serviceProvider, options) => 
{ 
    var configuration = serviceProvider.GetRequiredService<IConfiguration>(); 

    var connectionString = configuration.GetConnectionString("SqlServerDocker");
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());

    
    // var connectionString = configuration.GetConnectionString("MySqlDocker");
    // options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
    //     b => b.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}"));

    // var connectionString = configuration.GetConnectionString("PostgreSqlDocker");
    // options.UseNpgsql(connectionString);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Joby Sights API",
#if DEBUG
        Version = "v54.0 DEBUG",
#else
        Version = "v54.0",
#endif
        Description = "This is an API used in Joby's project."
    });
});

//Inject DbRepos and Services
builder.Services.AddScoped<SightDbRepos>();

builder.Services.AddScoped<ISightService, SightService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// for the purpose of this example, we will use Swagger also in production
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seido Friends API v2.0");
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
