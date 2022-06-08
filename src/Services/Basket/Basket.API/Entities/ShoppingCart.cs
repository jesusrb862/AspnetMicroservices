using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
           
        }

        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                // Items.Sum(item=>item.Price + item.Quantity);
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }            
        }       
    }
}
