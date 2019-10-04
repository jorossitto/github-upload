using System.Collections.Generic;

namespace Application.Data
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}