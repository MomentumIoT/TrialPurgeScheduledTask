
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Repository;

public class AuthenticationRepository
{
    public readonly HttpClient _client;

    private readonly IConfigurationRoot _configuration;

    public AuthenticationRepository(IConfigurationRoot configuration)
    {
        this._configuration = configuration;
        this._client = new HttpClient();
    }

    public async Task<string> GetAuthenticationToken()
    {
        var credentials = new { EmailAddress = this._configuration["EmailAddress"], Password = this._configuration["Password"] };

        var result = await this._client.PostAsync($"{this._configuration["APIUrl"]}/signin", new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json"));

        var token = await result.Content.ReadAsStringAsync();

        return token.Replace("\"", string.Empty);
    }

}