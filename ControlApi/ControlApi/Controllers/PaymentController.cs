using BusinessLibrary.Services;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class PaymentController : ControllerBase
    {
        private readonly PayPalApiConfiguration Configuration;
        private readonly IFacturaService FacturaService;

        public PaymentController(IOptions<PayPalApiConfiguration> Configuration, IFacturaService FacturaService)
        {
            this.Configuration = Configuration.Value;
            this.FacturaService = FacturaService;
        }

        [HttpPost("Factura/{FacturaId}")]
        public string Pay(Guid FacturaId)
        {
            var Factura = this.FacturaService.GetBuId(FacturaId);

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
                         name = Factura.Descripcion,
                         currency = "USD",
                         price = Factura.Monto.ToString(),
                         quantity = "1",
                         sku = "sku"
                    }
                }
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = Factura.Monto.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                custom = Factura.Id.ToString(),
                description = "Pago Subscripcion " + Factura.Descripcion,
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

            var ret = "";

            var links = createdPayment.links.GetEnumerator();
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    ret = link.href;
                }
            }

            return ret;
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
