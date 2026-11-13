using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Book;
using Library.Business.Interfaces.Services;
using Library.Common.Constants;
using Library.Common.Helpers;

namespace LibraryAPI.Endpoints
{
    public static class BookEndpoints
    {
        public static RouteGroupBuilder MapBookEndpoints(
            this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateBookRequest request, IBookService service) =>
                {
                    var result = await service.CreateAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Created("/api/books", result);
                })
                .WithName("CreateBook")
                .WithSummary("Endpoint que permite crear un libro")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapGet("/{id:guid}", async ( Guid id, IBookService service) =>
                {
                    var result = await service.GetByIdAsync(id);

                    if (result.IsFailure)
                        return Results.NotFound(result);

                    return Results.Ok(result);
                })
                .WithName("GetBookById")
                .WithSummary("Endpoint que permite obtener un libro por ID")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async ([AsParameters] PagedRequest request, IBookService service) =>
                {
                    var result = await service.ListAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("ListBooks")
                .WithSummary("Endpoint que permite listar libros")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:guid}", async (Guid id, UpdateBookRequest request, IBookService service) =>
                {
                    var result = await service.UpdateAsync(id, request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("UpdateBook")
                .WithSummary("Endpoint que permite actualizar un libro")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapDelete("/{id:guid}", async (Guid id, IBookService service) =>
                {
                    var result = await service.DeleteAsync(id);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("DeleteBook")
                .WithSummary("Endpoint que permite eliminar lógicamente un libro")
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