using Microsoft.AspNetCore.Mvc;

namespace BankAppControllers.Controllers
{
    public class AccountController : Controller
    {
        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            return Json(new
            {
                AccountNumber = 1001,
                accountHolderName = "Example Name",
                accountBalance = 5000
            });
        }

        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            return File("/mushfiq_rahman_resume.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber:int?}")]
        public IActionResult GetCurrentBalance()
        {
            var account = new
            {
                accountNumber = 1001,
                accountHolderName = "Example Name",
                accountBalance = 5000
            };

            if (!Request.RouteValues.ContainsKey("accountNumber"))
            {
                return NotFound("Account number must be supplied");
            }

            int accountNumber = Convert.ToInt32(Request.RouteValues["accountNumber"]);

            if (accountNumber != account.accountNumber)
            {
                return BadRequest($"Account number should be {account.accountNumber}");
            }

            return Content($"Account balance: {account.accountBalance}");
        }
    }
}
