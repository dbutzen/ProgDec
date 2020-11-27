using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;

namespace DTB.ProgDec.MVCUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;
        // GET: ShoppingCart
        public ActionResult Index()
        {
            GetShoppingCart();
            return View(cart);
        }

        //Show cart in sidebar - child action only for partial views
        [ChildActionOnly]
        public ActionResult CartDisplay()
        {
            GetShoppingCart();
            return PartialView(cart);
        }

        public ActionResult RemoveFromCart(int id)
        {
            GetShoppingCart();
            BL.Models.ProgDec progDec = cart.Items.FirstOrDefault(i => i.Id == id); //lambda expression
            ShoppingCartManager.Remove(cart, progDec);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            GetShoppingCart();
            BL.Models.ProgDec progDec = ProgDecManager.LoadById(id);
            ShoppingCartManager.Add(cart, progDec);
            Session["cart"] = cart;
            return RedirectToAction("Index", "ProgDec");
        }

        private void GetShoppingCart()
        {
            if (Session["cart"] == null)
                cart = new ShoppingCart();
            else
                cart = (ShoppingCart)Session["cart"];
        }
        public ActionResult Checkout()
        {
            GetShoppingCart();
            ShoppingCartManager.Checkout(cart);
            return View();
        }
    }
}