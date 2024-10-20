using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using TokenServiceAPI.Data;

namespace TokenServiceAPI
{
	public static class InitializeDb
	{
		public static void InitializeIdentityServerTables(WebApplication app, IConfiguration config)
		{
			using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
				var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
				context.Database.Migrate();

				if (!context.Clients.Any()) 
				{
					foreach (var client in Config.Clients(config)) 
					{
						context.Clients.Add(client.ToEntity());
					}
					context.SaveChanges();
				}

				if (!context.ApiScopes.Any()) 
				{ 
					foreach(var apiScope in Config.ApiScopes)
					{
						context.ApiScopes.Add(apiScope.ToEntity());
					}
					context.SaveChanges();
				}

				if (!context.IdentityResources.Any())
				{
					foreach (var idResource in Config.IdentityResources)
					{
						context.IdentityResources.Add(idResource.ToEntity());
					}
					context.SaveChanges();
				}

			}
		}
	}
}
