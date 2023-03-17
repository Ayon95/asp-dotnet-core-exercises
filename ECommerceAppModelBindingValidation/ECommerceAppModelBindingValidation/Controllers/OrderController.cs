using ECommerceAppModelBindingValidation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerceAppModelBindingValidation.Controllers
{
    public class OrderController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("<h1>Welcome to Orders app.</h1>");
        }

        [Route("/order")]
        public IActionResult OrderDetails(Order order)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errorMessagesList = ModelState.Values.SelectMany(value => value.Errors).Select(error => error.ErrorMessage);

                return BadRequest(string.Join("\n", errorMessagesList));
            }

            int orderNumber = new Random().Next(1, 99999);

            order.OrderNumber = orderNumber;

            return Json(new { orderNumber, orderDate = order.OrderDate, products = order.Products, invoicePrice = order.InvoicePrice });
        }
    }
}
