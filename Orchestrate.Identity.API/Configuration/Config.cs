using System;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Orchestrate.Identity.API.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("testapi", "Test API Service"),
                new ApiResource
                {
                    Name = "api1",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string> {"api1.read", "api1.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                     DisplayName="Role",
                     Name="role",
                     UserClaims = new List<string>{"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // JavaScript Client
                new Client
                {
                    ClientId = "oauthClient",
                    ClientName = "Example client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!
                    AllowedScopes = new List<string> {"api1.read"}
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api1.read", "Read Access to API #1"),
                new ApiScope("api1.write", "Write Access to API #1")
            };
        }
    }
}
