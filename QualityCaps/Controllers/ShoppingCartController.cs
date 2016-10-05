using System.Linq;
using System.Web.Mvc;
using QualityCaps.Models;
using QualityCaps.ViewModels;

namespace QualityCaps.Controllers
{
    public class ShoppingCartController : Controller
    {
        readonly ApplicationDbContext _storeDb = new ApplicationDbContext();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                SubTotal = cart.GetSubTotal(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }

        //
        // POST: /Store/RemoveAllItems/5

        public ActionResult RemoveAllItems()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.EmptyCart();
            return RedirectToAction("Index");
        }

        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id,string color)
        {
            // Retrieve the item from the database
            var addedItem = _storeDb.Items
                .Single(item => item.ID == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            if (string.IsNullOrWhiteSpace(color))
                color = "UnSelected";
            int count = cart.AddToCart(addedItem,color);
            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedItem.Name) +
                    " has been added to your shopping cart.",
                CartTotal = cart.GetTotal(),
                SubTotal = cart.GetSubTotal(),
                CartCount = cart.GetCount(),
                ItemCount = count,
                DeleteId = id
            };
            return Json(results);

            // Go back to the main store page for more shopping
           // return RedirectToAction("Index");
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}