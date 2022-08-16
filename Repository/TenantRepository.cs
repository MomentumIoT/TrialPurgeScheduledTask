using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace TrialPurgeScheduledTask.Repository;

internal class TenantRepository
{
    private readonly IConfiguration _configuration;
    
    private readonly HttpClient _httpClient;
    
    public TenantRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
        this._httpClient = new HttpClient();
    }

    public async Task CheckForPurgeableTrialAccounts(string token)
    {
        this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        await this._httpClient.PostAsync($"{this._configuration["APIUrl"]}/api/tenant/trial-purge", null);
    }
    
}