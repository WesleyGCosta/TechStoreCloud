using Api.Models.Requests;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Api.Endpoints
{
    internal static class AuthEndpoint
    {
        internal static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
        {

            var group = builder.MapGroup("api/auth").WithTags("Auth").RequireAuthorization();

            group.MapPost("/login", async (IAuthService authService, LoginRequest loginRequest) =>
            {
                var token = await authService.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

                if (token is null) return Results.Unauthorized();

                return Results.Ok(token);
            }).AllowAnonymous();


            group.MapPost("/register", async (UserManager<IdentityUser> userManager, RegisterRequest registerRequest) =>
            {
                var user = new IdentityUser
                { 
                    UserName = registerRequest.Username,
                    Email = registerRequest.Email
                };
                var result = await userManager.CreateAsync(user, registerRequest.Password);
                if (!result.Succeeded)
                    return Results.BadRequest(result.Errors);

                return Results.Ok(registerRequest);
            });

            return builder;
        }
    }
}
