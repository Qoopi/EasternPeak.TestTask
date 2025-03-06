namespace Api.ReqRes.Clients;

public static class TestEndpoints
{
    private const string BaseUrl = "https://reqres.in/api";

    public static class Users
    {
        public static string CreateUser => $"{BaseUrl}/users";
        public static string GetUsersList(int page) => $"{BaseUrl}/users?page={page}";
    }
}