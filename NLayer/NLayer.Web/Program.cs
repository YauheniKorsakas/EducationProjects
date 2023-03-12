using NLayer.Core.Models;
using NLayer.Infrastructure;
using NLayer.Root;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.DatabaseSection))
    .Configure<DatabaseOptions>(opt => opt.ConnectionString = builder.Configuration.GetConnectionString(DatabaseOptions.ConnectionStringName));
builder.Services.RegisterInfrastructure(new DatabaseOptions {
    ConnectionString = builder.Configuration.GetConnectionString(DatabaseOptions.ConnectionStringName),
    OperationTimeout = builder.Configuration.GetSection(DatabaseOptions.DatabaseSection).GetValue<int>("OperationTimeout"),
    RetryCount = builder.Configuration.GetSection(DatabaseOptions.DatabaseSection).GetValue<int>("RetryCount")
});
builder.Services.RegisterBusiness();
builder.Services.RegisterDomain();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    await DatabaseInitializator.InitializeAndSeedAsync(services.GetRequiredService<ShopContext>());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
