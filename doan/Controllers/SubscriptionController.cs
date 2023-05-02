using doan.DTO.Subscription;
using doan.EF;
using doan.Entities;
using doan.Helpers;
using doan.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;
using System.Runtime.InteropServices;
using Invoice = doan.Entities.Invoice;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscription _subscription;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public SubscriptionController(ISubscription subscription, UserManager<AppUser> userManager, ApplicationDbContext context, IConfiguration config)
        {
            _subscription = subscription;
            _userManager = userManager;
            _context = context;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> createSubscription([FromBody] SubscriptionCreateRequest request )
        {
            var user = await _userManager.FindByIdAsync(request.userId.ToString());
            var productDuration = await _context.ProductDurations.Where(x => x.Id == request.productDurationId)
                .Include(b => b.product)
                .Include(c => c.duration)
                .FirstOrDefaultAsync();

            
            var result = await _subscription.createSubscription(request);
            return Ok(result);
        }

        [HttpPost("createpayment")]
        public async Task<Payment> createPayment(SubscriptionCreateRequest request)
        {
            var clientID = _config["PaypalSettings:ClientId"];
            var SecretKey = _config["PaypalSettings:SecretKey"];
            var mode = _config["PaypalSettings:Mode"];


            var accessToken = new OAuthTokenCredential(clientID, SecretKey,new Dictionary<string, string>
            {
                {"mode",mode }
            }).GetAccessToken();

            var apiContext = new APIContext(accessToken);
            apiContext.Config = new Dictionary<string, string>
            {
                {"mode",mode }
            };
            var userId = await _userManager.FindByIdAsync(request.userId.ToString());


            var productDuration = await _context.ProductDurations.Where(b => b.Id == request.productDurationId)
                .Include(b => b.duration)
                .Include(b => b.product)
                .FirstOrDefaultAsync();
            var paypalOrderId = RandomHelper.RandomString(10);

            var user = await _userManager.FindByIdAsync(request.userId.ToString());

            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var payment = Payment.Create(apiContext, new Payment
            {
                
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                new Transaction
                {

                    description = $"Transaction description for {paypalOrderId}",
                    invoice_number = paypalOrderId,
                    amount = new Amount()
                    {
                        currency = "USD",
                        total = productDuration.price.ToString(),
                        details = new Details
                        {
                            tax = "0",
                            shipping = "0",
                            subtotal = productDuration.price.ToString(),
                        }
                    },
                    item_list = new ItemList
                    {
                        items = new List<Item>
                        {
                            new Item
                            {
                                name = productDuration.product.Name + productDuration.duration.name,
                                currency = "USD",
                                price = productDuration.price.ToString(),
                                quantity = "1",
                                sku = "sku"
                            }
                        }
                    }
                }
            },
                redirect_urls = new RedirectUrls
                {
                    return_url = $"{hostname}/api/Subscription/success/{paypalOrderId}",
                    cancel_url = $"{hostname}/api/Subscription/fail"
                }

            });
            //var createdPayment = payment.Create(apiContext);
            //var approvalUrl = createdPayment.links.FirstOrDefault(l => l.rel == "approval_url").href;

            var createdInvoice = new Invoice
            {
                paypalId = paypalOrderId,
                Total = productDuration.price,
                appUser = user,
                productDuration = productDuration
            };
            await _context.Invoices.AddAsync(createdInvoice);
            await _context.SaveChangesAsync();
            return payment;
        }
        [HttpGet("success/{IdOrder}")]
        public async Task<IActionResult> SuccessfulPaid([FromRoute] string IdOrder)
        {
            var request = Request.RouteValues.ToList();

            var payment = await _context.Invoices.Where(x=> x.paypalId == IdOrder).FirstOrDefaultAsync();
            if (payment == null)
            {
                return BadRequest( new
                {
                    status = 404,
                    message = "Không tồn tại hóa đơn tương ứng"
                });
            }
            else
            {
                if (payment.isPaid == true)
                {
                    return BadRequest(new
                    {
                        status = 401,
                        message = "Đã cộng rồi nha"
                    });
                }
                else
                {
                    payment.isPaid = true;
                    await _context.SaveChangesAsync();
                    var SubscriptionCreateRequest = new SubscriptionCreateRequest
                    {
                        productDurationId = payment.productDurationId,
                        userId = payment.userId
                    };
                    var result = await _subscription.createSubscription(SubscriptionCreateRequest);
                    return Ok(new
                    {
                        status = 200,
                        message = "Ok rùi nha"
                    });
                }
            }
            return BadRequest(new
            {
                status = 400,
                message = "Có lỗi xảy ra"
            });
        }
        [HttpGet("fail")]
        public async Task<IActionResult> FailPaid()
        {
            var querry = Request.Query.ToList();
            return Ok(querry);
        }
    }
}
