using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        public ICollection<Category> GetCategories()
        {
            return UnitOfWork<ICollection<Category>>((s) => s.CreateCriteria<Category>().List<Category>());
        }

        public Category GetCategory(int id)
        {
            return UnitOfWork<Category>((s) => s.Get<Category>(id));
        }

        public void DeleteCategory(int id)
        {
            UnitOfWork((s) =>
            {
                var cat = s.Load<Category>(id);
                s.Delete(cat);
            });
        }

        public void NewCategory(Category category)
        {
            UnitOfWork((s) => { s.Save(category); });
        }

        public void UpdateCategory(Category category)
        {
            UnitOfWork((s) =>
            {
                var merged = s.Merge<Category>(category);
                s.Update(merged);
            });
        }

        public Pageable<Product> GetCategoryProducts(int categoryId, int pageNumber, int pageSize)
        {
            return UnitOfWork<Pageable<Product>>((s) =>
            {
                var category = s.Load<Category>(categoryId);
                return new Pageable<Product>(category.Products.Count, pageNumber, pageSize,
                    category.Products
                        .OrderBy(x => x.Name)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList());
            });
        }

        private void UnitOfWork(Action<ISession> unitOfWork)
        {
            using (var session = SessionHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    unitOfWork(session);
                    transaction.Commit();
                }
            }
        }

        private T UnitOfWork<T>(Func<ISession, T> unitOfWork)
        {
            using (var session = SessionHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var result = unitOfWork(session);
                    transaction.Commit();
                    return result;
                }
            }
        }
    }
}
