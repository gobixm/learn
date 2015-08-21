using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    class Repository : IRepository
    {
        public ICollection<Category> GetCategories()
        {
            using (var session = SessionHelper.OpenSession())
            {
                return session.CreateCriteria<Category>().List<Category>();
            }
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Category NewCategory(Category category)
        {
            using (var session = SessionHelper.OpenSession())
            {
                return (Category)session.Save(category);
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var session = SessionHelper.OpenSession())
            {
                session.Update(category);
            }
        }
    }
}