using ShopApplication.Business.Abstract;
using ShopApplication.DataAccess.Abstract;
using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;

        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            //throw new NotImplementedException();

            var cart = GetCartByUserId(userId);
            if (cart != null) // Kullanıcının bir kartı var mı? Eğer varsa
            {
                var index = cart.CartItems.FindIndex(x => x.ProductId == productId); // Bu productId ile ilişkili olan kayıt var mı?

                if (index < 0) // Bu kayıt daha önce liste içerisinde yok
                {
                    cart.CartItems.Add(new CartItem 
                    { 
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else // index 0 dan büyükse eklenen eleman zaten listede var.
                {
                    cart.CartItems[index].Quantity += quantity; // quantity güncelleme
                }

                _cartDal.Update(cart);
            }
        }

        public void ClearCart(int cartId)
        {
            //throw new NotImplementedException();
            _cartDal.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            //throw new NotImplementedException();

            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var cartId = cart.Id;
                _cartDal.DeleteFromCart(cartId, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            //throw new NotImplementedException();

            return _cartDal.GetByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            //throw new NotImplementedException();

            _cartDal.Create(new Cart()
            {
                UserId = userId,
            });
        }
    }
}
