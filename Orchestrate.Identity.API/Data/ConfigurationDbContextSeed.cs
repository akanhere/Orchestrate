using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.Configuration;
using Orchestrate.Identity.API.Configuration;

namespace Orchestrate.Identity.API.Data
{
    public class ConfigurationDbContextSeed
    {
        public ConfigurationDbContextSeed()
        {

        }
        public static async Task SeedAsync(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var api in Config.GetApis())
                {
                    context.ApiResources.Add(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in Config.GetApiScopes())
                {
                    context.ApiScopes.Add(apiScope.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }
    }
    
}