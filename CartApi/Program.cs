using CartApi.Data;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICartRepository, RedisCartRepository>();
builder.Services.AddSingleton<ConnectionMultiplexer>(cm =>
{
    var config = ConfigurationOptions.Parse(configuration["ConnectionString"], true);
    config.ResolveDns = true;
    config.AbortOnConnectFail = true;
    return ConnectionMultiplexer.Connect(config);
});

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Authority = configuration["IdentityUrl"];
        options.TokenValidationParameters.ValidateAudience = false;
        options.RequireHttpsMetadata = false;
        options.Audience = "basket";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
