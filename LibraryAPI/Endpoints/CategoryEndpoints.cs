using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Category;
using Library.Business.Interfaces.Services;
using Library.Common.Constants;
using Library.Common.Helpers;

namespace LibraryAPI.Endpoints
{
    public static class CategoryEndpoints
    {
        public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryRequest request, ICategoryService service) =>
                {
                    var result = await service.CreateAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Created("/api/categories", result);
                })
                .WithName("CreateCategory")
                .WithSummary("Endpoint que permite crear una categoría")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapGet("/{id:guid}", async (Guid id, ICategoryService service) =>
                {
                    var result = await service.GetByIdAsync(id);

                    if (result.IsFailure)
                        return Results.NotFound(result);

                    return Results.Ok(result);
                })
                .WithName("GetCategoryById")
                .WithSummary("Endpoint que permite obtener una categoría por ID")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async ([AsParameters] PagedRequest request, ICategoryService service) =>
                {
                    var result = await service.ListAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("ListCategories")
                .WithSummary("Endpoint que permite listar categorías")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:guid}", async (Guid id, UpdateCategoryRequest request, ICategoryService service) =>
                {
                    var result = await service.UpdateAsync(id, request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("UpdateCategory")
                .WithSummary("Endpoint que permite actualizar una categoría")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapDelete("/{id:guid}", async (Guid id, ICategoryService service) =>
                {
                    var result = await service.DeleteAsync(id);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("DeleteCategory")
                .WithSummary("Endpoint que permite eliminar lógicamente una categoría")
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