using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenir.Data;//week 8
using Microsoft.AspNetCore.Http; //week 8
using Microsoft.EntityFrameworkCore;//week 8


namespace QualitySouvenir.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "shoppingCartID";
        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public void AddToCart(Souvenir souvenir, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.ShoppingCartID == ShoppingCartId && c.Souvenir.ID == souvenir.ID);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Souvenir = souvenir,
                    ShoppingCartID = ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            db.SaveChanges();
        }

        public int RemoveFromCart(int id, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.ShoppingCartID == ShoppingCartId && cart.Souvenir.ID == id);
            int itemQuantity = 0;
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemQuantity = cartItem.Quantity;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
            return itemQuantity;
        }

        public int AddItemsToCart(int id, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.ShoppingCartID == ShoppingCartId && cart.Souvenir.ID == id);
            int itemQuantity = 0;
            if (cartItem != null)
            {
                if (cartItem.Quantity > 0)
                {
                    cartItem.Quantity++;
                    itemQuantity = cartItem.Quantity;
                }
                db.SaveChanges();
            }
            return itemQuantity;
        }

        public void EmptyCart(ApplicationDbContext db)
        {
            var cartItems = db.CartItems.Where(cart => cart.ShoppingCartID == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                db.CartItems.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<CartItem> GetCartItems(ApplicationDbContext db)
        {
            List<CartItem> cartItems = db.CartItems.Include(i => i.Souvenir).Where(cartItem => cartItem.ShoppingCartID == ShoppingCartId).ToList();

            return cartItems;

        }

        public int GetQuantity(ApplicationDbContext db)
        {
            int? Quantity =
                (from cartItems in db.CartItems where cartItems.ShoppingCartID == ShoppingCartId select (int?)cartItems.Quantity).Sum();
            return Quantity ?? 0;
        }

        public decimal GetTotal(ApplicationDbContext db)
        {
            decimal? total = (from cartItems in db.CartItems
                              where cartItems.ShoppingCartID == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Souvenir.Price).Sum();
            return total ?? decimal.Zero;
        }

        public decimal GetSubTotal(ApplicationDbContext db)
        {
            decimal? total = (from cartItems in db.CartItems
                              where cartItems.ShoppingCartID == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Souvenir.Price).Sum();

            decimal? subTotal = Convert.ToInt32(total) * ((decimal)0.85);
            return subTotal ?? decimal.Zero;
        }

        public decimal GetGST(ApplicationDbContext db)
        {
            decimal? total = (from cartItems in db.CartItems
                              where cartItems.ShoppingCartID == ShoppingCartId
                              select (int?)cartItems.Quantity * cartItems.Souvenir.Price).Sum();

            decimal? gst = Convert.ToInt32(total) * ((decimal)0.15);
            return gst ?? decimal.Zero;
        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                Guid tempCartId = Guid.NewGuid();
                context.Session.SetString(CartSessionKey, tempCartId.ToString());
            }
            return context.Session.GetString(CartSessionKey).ToString();
        }


    }

}
