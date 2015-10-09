using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private UnitOfWork unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return unitOfWork.Session; } }

        public ICollection<Category> GetCategories()
        {
            return Session.CreateCriteria<Category>().List<Category>();
        }

        public Category GetCategory(int id)
        {
            return Session.Get<Category>(id);
        }

        public void DeleteCategory(int id)
        {
            var cat = Session.Load<Category>(id);
            Session.Delete(cat);
        }

        public void NewCategory(Category category)
        {
            Session.Save(category);
        }

        public void UpdateCategory(Category category)
        {
            var merged = Session.Merge<Category>(category);
            Session.Update(merged);
        }

        public Pageable<Product> GetCategoryProducts(int categoryId, int pageNumber, int pageSize)
        {
            var category = Session.Load<Category>(categoryId);
            return new Pageable<Product>(category.Products.Count, pageNumber, pageSize,
                category.Products
                    .OrderBy(x => x.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList());
        }

        public void SaveCart(Cart cart)
        {
            Session.SaveOrUpdate(cart);
            Session.Flush();
        }

        public Cart GetCart(Guid id)
        {
            return Session.Get<Cart>(id);
        }

        public Product GetProduct(int id)
        {
            return Session.Get<Product>(id);
        }
    }
}
