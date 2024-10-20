using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TokenServiceAPI.Data;
using TokenServiceAPI.Models;

namespace TokenServiceAPI
{
	internal static class HostingExtensions
	{
		public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddRazorPages();

			var connectionString = builder.Configuration["ConnectionString"];
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
			builder.Services
				.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;

					// see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
					options.EmitStaticAudienceClaim = true;
				})
				.AddDeveloperSigningCredential()
				.AddConfigurationStore(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(migrationsAssembly));
				})
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(migrationsAssembly));
				})
				.AddAspNetIdentity<ApplicationUser>();

			//builder.Services.AddAuthentication()
			//	.AddGoogle(options =>
			//	{
			//		options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

			//		// register your IdentityServer with Google at https://console.developers.google.com
			//		// enable the Google+ API
			//		// set the redirect URI to https://localhost:5001/signin-google
			//		options.ClientId = "copy client ID from Google here";
			//		options.ClientSecret = "copy client secret from Google here";
			//	});

			return builder.Build();
		}

		public static WebApplication ConfigurePipeline(this WebApplication app)
		{
			InitializeDb.InitializeIdentityServerTables(app, app.Configuration);

			app.UseSerilogRequestLogging();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseIdentityServer();
			app.UseAuthorization();

			app.MapRazorPages()
				.RequireAuthorization();

			return app;
		}
	}
}