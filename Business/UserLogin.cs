// Services/UserLogin.cs
using Api;
using Contracts;
using Entites;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class UserLogin : IUserLoginContract
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly WebApplicationSettings _settings;

    public UserLogin(IOptions<WebApplicationSettings> settings, IHttpClientFactory httpClientFactory)
    {
        _settings = settings.Value;
        _httpClientFactory = httpClientFactory;
    }
    public async Task<ViewUsersAndRoleDetails> Post(tblUsers _otblUsers)
    {
        try
        {
            var apiServiceUrl = _settings.ApiServiceUrl;
            var client = _httpClientFactory.CreateClient();
            var myContent = JsonConvert.SerializeObject(_otblUsers);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync($"{apiServiceUrl}/UserLogin", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewUsersAndRoleDetails>(content);
            return result;
            //var client = _httpClientFactory.CreateClient();
            //var apiServiceUrl = _settings.ApiServiceUrl;

            //var myContent = JsonConvert.SerializeObject(_otblUsers);
            //var content = new StringContent(myContent, Encoding.UTF8, "application/json");

            //var response = await client.PostAsync($"{apiServiceUrl}/UserLogin", content);

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseContent = await response.Content.ReadAsStringAsync();
            //    var result = JsonConvert.DeserializeObject<tblUsers>(responseContent);
            //    return result;
            //}
            //else
            //{
            //    throw new HttpRequestException($"POST request failed: {response.StatusCode}");
            //}
        }
        catch (Exception ex)
        {
            // Handle the exception (log it or throw a custom exception)
            throw new HttpRequestException($"POST request failed: {ex.Message}");
        }
    }





}
