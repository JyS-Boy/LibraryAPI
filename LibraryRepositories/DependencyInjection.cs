using Library.Repositories.Implementations;
using Library.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
