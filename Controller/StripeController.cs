using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace EcommerceApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {


        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession( [FromBody] double totalPrice  )
        {

  
            // Create a Product 
      
                var productOptions = new ProductCreateOptions
                {
                    Name = "Wacth",
                    Description = "A placeholder product for dynamic pricing",
                };
                var productService = new ProductService();
                var product = productService.Create(productOptions);



                // Create a Price dynamically based on the total amount
                var priceOptions = new PriceCreateOptions
                {
                    UnitAmount = (long)(totalPrice * 100), // Convert the total amount to cents
                    Currency = "usd", // Use your desired currency
                    Product = product.Id
                };

                var priceService = new PriceService();
                var price = priceService.Create(priceOptions);




            var options = new SessionCreateOptions
            {

                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>()
                  {
                      new SessionLineItemOptions
                      {

                          Price = price.Id, // Replace with your actual price ID
                          Quantity = 1,
                      }
                  },
          
                Mode = "payment",
                SuccessUrl = "http://localhost:3000/success",
                CancelUrl = "http://localhost:3000/cancel",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { sessionId = session.Id });
        }
    }
}
