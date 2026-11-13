using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Author;
using Library.Business.Interfaces.Services;
using Library.Common.Constants;
using Library.Common.Helpers;

namespace LibraryAPI.Endpoints
{
    public static class AuthorEndpoints
    {
        public static RouteGroupBuilder MapAuthorEndpoints(
            this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateAuthorRequest request, IAuthorService service) =>
                {
                    var result = await service.CreateAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Created("/api/authors", result);
                })
                .WithName("CreateAuthor")
                .WithSummary("Endpoint que permite crear un autor")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapGet("/{id:guid}", async (Guid id, IAuthorService service) =>
                {
                    var result = await service.GetByIdAsync(id);

                    if (result.IsFailure)
                        return Results.NotFound(result);

                    return Results.Ok(result);
                })
                .WithName("GetAuthorById")
                .WithSummary("Endpoint que permite obtener un autor por ID")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async ([AsParameters] PagedRequest request, IAuthorService service) =>
                {
                    var result = await service.ListAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("ListAuthors")
                .WithSummary("Endpoint que permite listar autores")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:guid}", async (Guid id, UpdateAuthorRequest request, IAuthorService service) =>
                {
                    var result = await service.UpdateAsync(id, request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("UpdateAuthor")
                .WithSummary("Endpoint que permite actualizar un autor")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapDelete("/{id:guid}", async (Guid id, IAuthorService service) =>
                {
                    var result = await service.DeleteAsync(id);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("DeleteAuthor")
                .WithSummary("Endpoint que permite eliminar lógicamente un autor")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            return group;
        }
    }
}