using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;


namespace Library.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
