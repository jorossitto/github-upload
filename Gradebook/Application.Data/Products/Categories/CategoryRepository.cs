using System.Collections.Generic;

namespace AppCore.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BusinessDBContext businessDBContext;

        public CategoryRepository(BusinessDBContext businessDBContext)
        {
            this.businessDBContext = businessDBContext;
        }

        public IEnumerable<Category> AllCategories => businessDBContext.Categories;
    }
}
