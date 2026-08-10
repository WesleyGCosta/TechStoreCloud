namespace Api.Models.Response
{
    public record AccessResponse(
        string Type,
        string Token,  
        DateTime ExpireIn,
        string RefreshToken
    );
}
