using System.Collections.Generic;

namespace AppCore.Data
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}