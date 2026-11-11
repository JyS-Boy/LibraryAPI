using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.User;
using Library.Business.Interfaces.Services;
using Library.Common.Constants;
using Library.Common.Helpers;

namespace LibraryAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateUserRequest request, IUserService service) =>
            {
                    var result = await service.CreateAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Created("/api/users", result);
            })
             .WithName("CreateUser")
             .WithSummary("Endpoint que permite crear un usuario bibliotecario")
             .RequireAuthorization(p => p.RequireRole(Roles.Admin))
             .Produces<Result>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status403Forbidden);

            group.MapGet("/{id:guid}", async (Guid id, IUserService service) =>
            {
                    var result = await service.GetByIdAsync(id);

                    if (result.IsFailure)
                        return Results.NotFound(result);

                    return Results.Ok(result);
            })
             .WithName("GetByIdUser")
             .WithSummary("Endpoint que permite obtener un usuario por ID")
             .RequireAuthorization(p => p.RequireRole(Roles.Admin))
             .Produces<Result>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async ([AsParameters] PagedRequest request, IUserService service) =>
            {
                var result = await service.ListAsync(request);

                if (result.IsFailure)
                    return Results.BadRequest(result);

                return Results.Ok(result);
            })
             .WithName("ListUsers")
             .WithSummary("Endpoint que permite listar los usuarios registrados")
             .RequireAuthorization(p => p.RequireRole(Roles.Admin))
             .Produces<Result>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:guid}", async (Guid id, UpdateUserRequest request, IUserService service) =>
            {
                var result = await service.UpdateAsync(id, request);

                if (result.IsFailure)
                    return Results.BadRequest(result);

                return Results.Ok(result);
            })
             .WithName("UpdateUser")
             .WithSummary("Endpoint que permite actualizar un usuario")
             .RequireAuthorization(p => p.RequireRole(Roles.Admin))
             .Produces<Result>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status403Forbidden);

            group.MapDelete("/{id:guid}", async (Guid id, IUserService service) =>
            {
                var result = await service.DeleteAsync(id);

                if (result.IsFailure)
                    return Results.BadRequest(result);

                return Results.Ok(result);
            })
            .WithName("DeleteUser")
            .WithSummary("Endpoint que permite eliminar lógicamente un usuario")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces<Result>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden);

            return group;
        }
    }
}
