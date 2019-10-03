using System.Collections.Generic;

namespace ACM.BL
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}