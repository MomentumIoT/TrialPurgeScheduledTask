using Microsoft.Extensions.Configuration;
using Repository;
using TrialPurgeScheduledTask.Repository;

IConfigurationRoot _configuration;

ConfigureDependencyInjection();

var authenticationRepository = new AuthenticationRepository(_configuration);

var tenantRepository = new TenantRepository(_configuration);

var token = await authenticationRepository.GetAuthenticationToken();

await tenantRepository.CheckForPurgeableTrialAccounts(token);


void ConfigureDependencyInjection()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
#if STAGING
                .AddJsonFile("appsettings.staging.json", optional: false);
#else
        .AddJsonFile("appsettings.json", optional: false);
#endif

    _configuration = builder.Build();
}