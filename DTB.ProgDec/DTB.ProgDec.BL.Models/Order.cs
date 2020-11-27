using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.ProgDec.BL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
