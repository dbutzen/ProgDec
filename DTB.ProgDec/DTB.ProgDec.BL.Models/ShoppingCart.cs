using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.ProgDec.BL.Models
{
   public class ShoppingCart
    {
        // This does not apply to dvdcentral app
        // Cost of movies vary, retrieved from tblMovie.Cost
        const double ITEM_COST = 49.99;
        public double TotalCost { get; set; }
        public List<ProgDec> Items { get; set; }
        public int TotalCount { get { return Items.Count; } }

        public ShoppingCart()
        {
            Items = new List<ProgDec>();
        }
        public void Add(ProgDec progDec)
        {
            Items.Add(progDec);
            TotalCost += ITEM_COST;
        }
        public void Remove(ProgDec progDec)
        {
            Items.Remove(progDec);
            TotalCost -= ITEM_COST;
        }
        public void CheckOut()
        {
            Items = new List<ProgDec>();
            TotalCost = 0;

        }
    }
}
