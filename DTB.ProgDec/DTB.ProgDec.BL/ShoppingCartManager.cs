using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.ProgDec.BL.Models;

namespace DTB.ProgDec.BL
{
    public static class ShoppingCartManager
    {
        public static void Checkout(ShoppingCart cart)
        {
            /* For DVD Central, do these things when you checkout
             * Add OrderManager
             * 1) Insert a tblOrder. Get the Order.Id
             * 2)Loop through the Items, and insert a tblOrderITem record
             * with the new Order.Id
             * 3) Remove the items from the cart
             */

            Order order = new Order();
            order.CustomerId = 1;
            OrderManager.Insert(order, cart.Items);
            cart.CheckOut();
        }
        public static void Add(ShoppingCart cart, Models.ProgDec progDec)
        {
            cart.Add(progDec);
        }
        public static void Remove(ShoppingCart cart, Models.ProgDec progDec)
        {
            cart.Remove(progDec);
        }

    }
}
