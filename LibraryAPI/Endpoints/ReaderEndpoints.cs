using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Reader;
using Library.Business.Interfaces.Services;
using Library.Common.Constants;
using Library.Common.Helpers;

namespace LibraryAPI.Endpoints
{
    public static class ReaderEndpoints
    {
        public static RouteGroupBuilder MapReaderEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (RegisterReaderRequest request, IReaderService service) =>
                {
                    var result = await service.RegisterAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Created("/api/readers", result);
                })
                .WithName("CreateReader")
                .WithSummary("Endpoint que permite registrar un lector")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapGet("/{id:guid}", async (Guid id, IReaderService service) =>
                {
                    var result = await service.GetByIdAsync(id);

                    if (result.IsFailure)
                        return Results.NotFound(result);

                    return Results.Ok(result);
                })
                .WithName("GetReaderById")
                .WithSummary("Endpoint que permite obtener un lector por ID")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async ([AsParameters] PagedRequest request, IReaderService service) =>
                {
                    var result = await service.ListAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("ListReaders")
                .WithSummary("Endpoint que permite listar lectores")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:guid}", async (Guid id, UpdateReaderRequest request, IReaderService service) =>
                {
                    var result = await service.UpdateAsync(id, request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("UpdateReader")
                .WithSummary("Endpoint que permite actualizar un lector")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapDelete("/{id:guid}", async (Guid id, IReaderService service) =>
                {
                    var result = await service.DeleteAsync(id);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("DeleteReader")
                .WithSummary("Endpoint que permite eliminar lógicamente un lector")
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