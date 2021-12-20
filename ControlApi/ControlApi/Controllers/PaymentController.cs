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
using SharedLibrary.DataTypes.Pago;
using SharedLibrary.Error;
using SharedLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PayPalApiConfiguration Configuration;
        private readonly IStore<Factura> FacturaStore;
        private readonly IPagoStore Store;

        public PaymentController(IOptions<PayPalApiConfiguration> Configuration, IStore<Factura> FacturaStore, IPagoStore Store)
        {
            this.Configuration = Configuration.Value;
            this.FacturaStore = FacturaStore;
            this.Store = Store;
        }

        [HttpPost("Factura/{FacturaId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Pay(Guid FacturaId)
        {
            var Factura = this.FacturaStore.GetById(FacturaId, new string[] { "Pago" });
            if (Factura == null) throw new ApiError("Factura no encontrada", (int)HttpStatusCode.NotFound);
            if (Factura.Pago != null) throw new ApiError("Factura pagada", (int)HttpStatusCode.BadRequest);

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
                item_list = itemList,
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

            return Ok(new { Url = ret });
        }

        [HttpGet("success")]
        public void Success(string paymentId, string PayerID, string token)
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

            if (executedPayment.state != "approved") throw new ApiError("Pago no aprobado", (int)HttpStatusCode.BadRequest);

            foreach (var transaction in executedPayment.transactions)
            {
                if (Guid.TryParse(transaction.custom, out var FacturaId))
                {
                    try
                    {
                        var NewPago = new Pago() { Id = Guid.NewGuid(), FacturaId = FacturaId, Monto = float.Parse(transaction.amount.total) };

                        this.Store.Create(NewPago);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }

            Response.Redirect("http://localhost:4200/pago-exitoso");
        }
    }
}
