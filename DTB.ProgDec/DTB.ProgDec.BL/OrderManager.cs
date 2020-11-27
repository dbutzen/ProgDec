using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.PL;

namespace DTB.ProgDec.BL
{
    public static class OrderManager
    {
        public static void Insert(Order order, List<Models.ProgDec> items)
        {
            // create a tblOrder row
            // loop through the items and create a tblOrderItem row with the new order id

            try
            {
                int results;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //Make a new row
                    tblPDOrder row = new tblPDOrder();
                    
                    //Set the properties
                    row.Id = dc.tblPDOrders.Any() ? dc.tblPDOrders.Max(pd => pd.Id) + 1 : 1;
                    row.CustomerId = 1;

                    // Insert the row
                    dc.tblPDOrders.Add(row);
                    results = dc.SaveChanges();

                    foreach(Models.ProgDec pd in items)
                    {
                        //Make a new row
                        tblPDOrderItem itemrow = new tblPDOrderItem();

                        //Set the properties
                        itemrow.Id = dc.tblPDOrderItems.Any() ? dc.tblPDOrderItems.Max(pdo => pdo.Id) + 1 : 1;
                        itemrow.OrderId = row.Id;
                        itemrow.ItemId = pd.Id;

                        // Insert the row
                        dc.tblPDOrderItems.Add(itemrow);
                        results = dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
