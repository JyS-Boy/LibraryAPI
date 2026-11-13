using Library.Business.DTOs.Request.Loan;
using Library.Business.Interfaces.Services;
using Library.Common.Constants;
using Library.Common.Helpers;

namespace LibraryAPI.Endpoints
{
    public static class LoanEndpoints
    {
        public static RouteGroupBuilder MapLoanEndpoints(
            this RouteGroupBuilder group)
        {
            group.MapPost("/",
                async (CreateLoanRequest request, ILoanService service) =>
                {
                    var result = await service.CreateAsync(request);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Created("/api/loans", result);
                })
                .WithName("CreateLoan")
                .WithSummary("Endpoint que permite registrar un préstamo")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            group.MapGet("/{id:guid}",
                async (Guid id, ILoanService service) =>
                {
                    var result = await service.GetByIdAsync(id);

                    if (result.IsFailure)
                        return Results.NotFound(result);

                    return Results.Ok(result);
                })
                .WithName("GetLoanById")
                .WithSummary("Endpoint que permite obtener un préstamo por ID")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/",
                async (ILoanService service) =>
                {
                    var result = await service.GetAllAsync();

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("ListLoans")
                .WithSummary("Endpoint que permite listar los préstamos")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPost("/{id:guid}/return",
                async (Guid id, ILoanService service) =>
                {
                    var result = await service.ReturnLoanAsync(id);

                    if (result.IsFailure)
                        return Results.BadRequest(result);

                    return Results.Ok(result);
                })
                .WithName("ReturnLoan")
                .WithSummary("Endpoint que permite registrar la devolución de un préstamo")
                .RequireAuthorization(
                    p => p.RequireRole(
                        Roles.Admin,
                        Roles.Librarian))
                .Produces<Result>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden);

            return group;
        }
    }
}