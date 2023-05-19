using doan.DTO;
using doan.DTO.Payment;
using doan.DTO.Subscription;
using doan.EF;
using doan.Entities;
using doan.Helpers;
using doan.Interface;
using doan.Services;
using doan.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal;
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
        private readonly IUriService _IUriService;

        public SubscriptionController(ISubscription subscription, UserManager<AppUser> userManager, ApplicationDbContext context, IConfiguration config, IUriService iUriService)
        {
            _subscription = subscription;
            _userManager = userManager;
            _context = context;
            _config = config;
            _IUriService = iUriService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllSubscription([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = await _subscription.getAllSubscription(filter);
            var pagedReponse = PaginationHelper.CreatePagedReponse<SubscriptionView>(result.Item1, result.Item2, result.Item3, _IUriService, route);

            return Ok(new
            {
                status = 200,
                value = new JsonResult(pagedReponse)
            });
        }
        [HttpGet("user/{username}")]
        public async Task<IActionResult> getUserSubscriptionByName([FromRoute(Name = "username")] string username, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = await _subscription.getSubscriptionByUsername(username, filter);
            var pagedReponse = PaginationHelper.CreatePagedReponse<SubscriptionView>(result.Item1, result.Item2, result.Item3, _IUriService, route);

            return Ok(new
                {
                    code = 200,
                    data = new JsonResult(pagedReponse)
                });;
        }
        
        

        [HttpGet("{id}")]
        public async Task<IActionResult> getSubscriptionById([FromRoute(Name = "id")] int id)
        {
            return Ok( new { 
                status = 200,
                value = await _context.Subscriptions.FindAsync(id)
            });
        }
        //[HttpPost]
        //public async Task<IActionResult> createSubscription([FromBody] SubscriptionCreateRequest request )
        //{
        //    var user = await _userManager.FindByNameAsync(request.username);
        //    var productDuration = await _context.ProductDurations.Where(x => x.Id == request.productDurationId)
        //        .Include(b => b.product)
        //        .Include(c => c.duration)
        //        .FirstOrDefaultAsync();
        //    var result = await _subscription.createSubscription(request);
        //    return Ok(result);
        //}

        [HttpPost("createpayment")]
        public async Task<IActionResult> createPayment(SubscriptionCreateRequest request)
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
            var userId = await _userManager.FindByNameAsync(request.username);


            var productDuration = await _context.ProductDurations.Where(b => b.Id == request.productDurationId)
                .Include(b => b.duration)
                .Include(b => b.product)
                .FirstOrDefaultAsync();
            var paypalOrderId = RandomHelper.RandomString(10);


            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Host}";

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
                    return_url = $"{hostname}:3000/payment",
                    cancel_url = $"{hostname}/api/Subscription/fail"
                }
                
            });

            var approvalUrl = payment.links.FirstOrDefault(l => l.rel == "approval_url").href;

      
            var createdInvoice = new Invoice
            {
                paypalId = paypalOrderId,
                paypalIdCore = payment.id,
                Token = payment.token,
                Total = productDuration.price,
                appUser = userId,
                productDuration = productDuration
            };
            await _context.Invoices.AddAsync(createdInvoice);
            await _context.SaveChangesAsync();
            return Ok(approvalUrl);
        }
        [HttpPost("payment")]
        public async Task<IActionResult> SuccessfulPaid([FromBody] PaymentSucessClass request)
        {

            var payment = await _context.Invoices.Where(x=> x.paypalIdCore == request.paymentId && x.Token == request.token)
                .Include(x => x.appUser).FirstOrDefaultAsync();
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
                        username = payment.appUser.UserName
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