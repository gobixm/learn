using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public interface IRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        void DeleteCategory(int id);
        void NewCategory(Category category);
        void UpdateCategory(Category category);
        Pageable<Product> GetCategoryProducts(int categoryId, int pageNumber, int pageSize);
    }
}
