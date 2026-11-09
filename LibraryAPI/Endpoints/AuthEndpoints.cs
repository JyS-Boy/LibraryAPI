using Library.Business.DTOs.Request.Auth;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;

namespace LibraryAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/login", async (LoginRequest request, IAuthService service) =>
            {
                var result = await service.LoginAsync(request);

                if (result.IsFailure)
                    return Results.BadRequest(result);

                return Results.Ok(result.Value);
            })
                .WithName("Login")
                .WithSummary("Endpoint que permite a un usuario iniciar sesión")
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            return group;
        }
    }
}
