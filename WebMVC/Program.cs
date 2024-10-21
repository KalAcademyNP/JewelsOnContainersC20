using WebMVC.Infrastructure;
using WebMVC.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpClient, CustomHttpClient>();
builder.Services.AddTransient<ICatalogService, CatalogService>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = "Cookies";
	options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
	{
		options.Authority = configuration["IdentityUrl"];

		options.ClientId = "mvc";
		options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
		options.RequireHttpsMetadata = false;
		options.ResponseType = "code id_token";
		options.SignedOutRedirectUri = configuration["CallbackUrl"];

        options.Scope.Clear();
		options.Scope.Add("openid");
		options.Scope.Add("profile");

		options.MapInboundClaims = false; // Don't rename claim types

		options.SaveTokens = true;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Catalog}/{action=Index}");

app.Run();
