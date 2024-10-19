using Duende.IdentityServer.Models;

namespace TokenServiceAPI
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{
				new ApiScope("basket", "Shopping Cart API"),
				new ApiScope("order", "Order API"),
			};

		public static IEnumerable<Client> Clients(IConfiguration config) =>
			new Client[]
			{
	            // interactive client using code flow + pkce
	            new Client
				{
					ClientId = "mvc",
					ClientName = "MVC Client",
					ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

					AllowedGrantTypes = GrantTypes.Hybrid,

					RedirectUris = { $"{config["Mvc"]}/signin-oidc" },
					FrontChannelLogoutUri = $"{config["Mvc"]}/signout-oidc",
					PostLogoutRedirectUris = { $"{config["Mvc"]}/signout-callback-oidc" },

					AllowOfflineAccess = true,
					RequireConsent = false,
					RequirePkce = false,
					AlwaysIncludeUserClaimsInIdToken = true,
					AllowedScopes = { 
						"openid", 
						"profile", 
						"basket",
						"order"
					}
				},
			};
	}
}
