using System;
using System.Web.Http;
using FancyDeliveryApp.Api.Extensions;
using FancyDeliveryApp.Api.Models;
using Infrastructure;
using Infrastructure.Repositories;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using AutoMapper;

namespace FancyDeliveryApp.Api.Controllers
{
    public class CartController : BaseController
    {
        public CartController(IUnitOfWork unitOfWork, IRepository repository) : base(unitOfWork, repository)
        {
        }

        [HttpPost]
        public void Ship([FromBody]string address)
        {
            var cart = GetCartInternal();
            if (cart != null)
            {
                UnitOfWork.BeginTransaction();
                Repository.DeleteCart(cart.Id);
                UnitOfWork.Commit();
            }
        }

        [HttpPost]
        public HttpResponseMessage AddToCart([FromBody]int productId)
        {
            UnitOfWork.BeginTransaction();
            var cart = GetCartInternal();
            var response = Request.CreateResponse();
            if (cart == null)
            {
                cart = new Cart();
                cart.Id = Guid.NewGuid();
                var serverCookie = new CookieHeaderValue("cart_guid", cart.Id.ToString() );                
                response.Headers.AddCookies(new CookieHeaderValue[] { serverCookie });
            }
            var product = Repository.GetProduct(productId);
            var lineItem = cart.LineItems.FirstOrDefault(x => x.Product.Id == productId);

            if(lineItem==null)
            {
                lineItem = new LineItem { Price = product.Price, Product = product };
                cart.LineItems.Add(lineItem);
            }
            
            lineItem.Quantity++;
            lineItem.Price = product.Price;
            Repository.SaveCart(cart);
            UnitOfWork.Commit();
            return response;
        }

        [HttpGet]
        public CartDto GetCart()
        {
            return Mapper.Map<Cart, CartDto>(GetCartInternal());
        }

        private Cart GetCartInternal()
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies("cart_guid").FirstOrDefault();
            if (cookie == null)
            {
                return null;
            }

            return Repository.GetCart(Guid.Parse(cookie["cart_guid"].Value));
        }
    }
}
