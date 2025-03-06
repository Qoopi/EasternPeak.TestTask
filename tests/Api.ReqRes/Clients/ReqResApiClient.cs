using Api.ReqRes.Models;
using RestSharp;
using Serilog;
using System.Text.Json;

namespace Api.ReqRes.Clients;

public interface IReqResApiClient
{
    Task<UserListResponse?> GetUserListAsync(int page = 2);
    Task<CreateUserResponse?> CreateUserAsync(CreateUserRequest createUserRequest);
}

public class ReqResApiClient : IReqResApiClient
{
    private readonly RestClient _client;
    private readonly ILogger _logger;

    public ReqResApiClient(ILogger logger)
    {
        _client = new RestClient();
        _logger = logger;
    }

    public async Task<UserListResponse?> GetUserListAsync(int page)
    {
        var url = TestEndpoints.Users.GetUsersList(page);
        var request = new RestRequest(url);
        
        _logger.Information($"Sending GET request to {url}");
        
        var response = await _client.ExecuteAsync(request);
        
        _logger.Information($"Received response with status code: {response.StatusCode}");
        _logger.Information($"Response content: {response.Content}");
        
        if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
        {
            return JsonSerializer.Deserialize<UserListResponse>(response.Content);
        }
        
        return null;
    }

    public async Task<CreateUserResponse?> CreateUserAsync(CreateUserRequest createUserRequest)
    {
        var url = TestEndpoints.Users.CreateUser;
        var request = new RestRequest(url, Method.Post);
        
        request.AddJsonBody(createUserRequest);
        
        _logger.Information($"Sending POST request to {url}");
        _logger.Information($"Request body: {JsonSerializer.Serialize(createUserRequest)}");
        
        var response = await _client.ExecuteAsync(request);
        
        _logger.Information($"Received response with status code: {response.StatusCode}");
        _logger.Information($"Response content: {response.Content}");
        
        if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
        {
            return JsonSerializer.Deserialize<CreateUserResponse>(response.Content);
        }
        
        return null;
    }
} 