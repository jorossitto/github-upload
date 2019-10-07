using ACM.BL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Data
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
