using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PayPal.Api;
using SharedLibrary.Configuration.PayPal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly PayPalApiConfiguration Configuration;

        public PaymentController(IOptions<PayPalApiConfiguration> Configuration)
        {
            this.Configuration = Configuration.Value;
        }

        [HttpGet]
        public void Pay()
        {
            // Authenticate with PayPal
            var config = new Dictionary<string, string>();
            config.Add("mode", "sandbox");
            config.Add("clientId", Configuration.ClientID);
            config.Add("clientSecret", Configuration.Secret);

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            var payer = new Payer() { payment_method = "paypal" };
            var redirUrls = new RedirectUrls()
            {
                cancel_url = Configuration.CancelUrl,
                return_url = Configuration.ReturnUrl
            };

            var itemList = new ItemList()
            {
                items = new List<Item>()
                {
                    new Item()
                    {
                         name = "Item Name",
                         currency = "USD",
                         price = "15",
                         quantity = "5",
                         sku = "sku"
                    }
                }
            };

            var details = new Details()
            {
                tax = "15",
                shipping = "10",
                subtotal = "75"
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = "100.00", // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                amount = amount,
                item_list = itemList
            });

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                redirect_urls = redirUrls,
                transactions = transactionList
            };

            var createdPayment = payment.Create(apiContext);

            var links = createdPayment.links.GetEnumerator();
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    // Redirect the customer to link.href
                    Response.Redirect(link.href);
                }
            }
        }

        [HttpGet("success")]
        public dynamic Success(string paymentId, string PayerID, string token)
        {
            // Authenticate with PayPal
            var config = new Dictionary<string, string>();
            config.Add("mode", "sandbox");
            config.Add("clientId", Configuration.ClientID);
            config.Add("clientSecret", Configuration.Secret);

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            // Using the information from the redirect, setup the payment to execute.
            var paymentExecution = new PaymentExecution() { payer_id = PayerID };
            var payment = new Payment() { id = paymentId };

            // Execute the payment.
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return Ok(executedPayment);
        }
    }
}
