// Services/UserLogin.cs
using Api;
using Contracts;
using Entites;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class Employee : IEmployeeContract
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly WebApplicationSettings _settings;

    public Employee(IOptions<WebApplicationSettings> settings, IHttpClientFactory httpClientFactory)
    {
        _settings = settings.Value;
        _httpClientFactory = httpClientFactory;
    }
    public async Task<string> Post(tblEmployees _oTblEmployees)
    {
        try
        {
            var apiServiceUrl = _settings.ApiServiceUrl;
            var client = _httpClientFactory.CreateClient();
            var myContent = JsonConvert.SerializeObject(_oTblEmployees);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync($"{apiServiceUrl}/Employee", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        catch (Exception ex)
        {
            // Handle the exception (log it or throw a custom exception)
            throw new HttpRequestException($"POST request failed: {ex.Message}");
        }
    }

    public async Task<List<tblEmployees>> Get(string search)
    {
        try
        {
            var apiServiceUrl = _settings.ApiServiceUrl;
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{apiServiceUrl}/Employee?search={search}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<tblEmployees>>(content);
                return result;
            }
            else
            {

                throw new Exception($"Failed to fetch employees: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<tblEmployees>();
        }
    }

    public async Task<tblEmployees> GetEmployeeById(int Id)
    {
        try
        {
            var apiServiceUrl = _settings.ApiServiceUrl;
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{apiServiceUrl}/Employee/GetEmployeeById/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<tblEmployees>(content);
                return result;
            }
            else
            {
                throw new Exception($"Failed to fetch employee: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task<string> Put(tblEmployees _oTblEmployees)
    {
        try
        {
            var apiServiceUrl = _settings.ApiServiceUrl;
            var client = _httpClientFactory.CreateClient();
            var myContent = JsonConvert.SerializeObject(_oTblEmployees);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync($"{apiServiceUrl}/Employee", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        catch (Exception ex)
        {
            // Handle the exception (log it or throw a custom exception)
            throw new HttpRequestException($"POST request failed: {ex.Message}");
        }
    }
    public async Task<string> Delete(int Id)
    {
        var apiServiceUrl = _settings.ApiServiceUrl;
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"{apiServiceUrl}/Employee/Delete/{Id}");
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }



}
