using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QualityCaps.Configuration;
using QualityCaps.Models;
using RestSharp;
using System.Data.Entity.Validation;
using System.Linq;
using RestSharp.Authenticators;

namespace QualityCaps.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        readonly ApplicationDbContext _storeDb = new ApplicationDbContext();
        readonly AppConfigurations _appConfig = new AppConfigurations();

        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            var previousOrder = _storeDb.Orders.FirstOrDefault(x => x.Username == User.Identity.Name);

            if (previousOrder != null)
                return View(previousOrder);
            else
                return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            string result =  values[8];
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                    order.Username = User.Identity.Name;
                    var currentUserId = User.Identity.GetUserId();
                    var userEmail = _storeDb.Users.Find(currentUserId).Email;
                    order.Email = userEmail;
                    order.OrderDate = DateTime.Now;
                    order.Total = cart.GetTotal();
                    if (order.SaveInfo && !order.Username.Equals(" "))
                    {
                        
                        var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                        var ctx = store.Context;
                        var currentUser = manager.FindById(User.Identity.GetUserId());

                        currentUser.Address = order.Address;
                        currentUser.City = order.City;
                        currentUser.Country = order.Country;
                        currentUser.State = order.State;
                        currentUser.PhoneNumber = order.Phone;
                        currentUser.PostalCode = order.PostalCode;
                        currentUser.FirstName = order.FirstName;

                        //Save this back
                        //http://stackoverflow.com/questions/20444022/updating-user-data-asp-net-identity
                        //var result = await UserManager.UpdateAsync(currentUser);
                        await ctx.SaveChangesAsync();

                        await _storeDb.SaveChangesAsync();
                    }
                    

                    //Save Order
                    _storeDb.Orders.Add(order);
                    await _storeDb.SaveChangesAsync();
                    //Process the order
                    cart = ShoppingCart.GetCart(this.HttpContext);
                    order = cart.CreateOrder(order);



                    CheckoutController.SendOrderMessage(order.FirstName, "New Order: " + order.OrderId,order.ToString(order), _appConfig.OrderEmail);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                
            }
            catch(DbEntityValidationException e)
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = _storeDb.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        private static RestResponse SendOrderMessage(String toName, String subject, String body, String destination)
        {
            RestClient client = new RestClient();
            //fix this we have this up top too
            AppConfigurations appConfig = new AppConfigurations();
            client.BaseUrl = new Uri("https://api.mailgun.net/v2");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              appConfig.EmailApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                appConfig.DomainForApiKey, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", appConfig.FromName + " <" + appConfig.FromEmail + ">");
            request.AddParameter("to", toName + " <" + destination + ">");
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            IRestResponse executor = client.Execute(request);
            return executor as RestResponse;
        }
    }
}