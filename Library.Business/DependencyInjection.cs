using Library.Business.Implementations.Services;
using Library.Business.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(
            this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IReaderService, ReaderService>();
            // Se implementarán más adelante
            //services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}