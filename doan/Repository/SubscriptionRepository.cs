using doan.DTO.Subscription;
using doan.EF;
using doan.Entities;
using doan.Helpers;
using doan.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PayPal.Api;
using System.Xml.Schema;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using doan.DTO.Invoice;
using doan.Wrapper;

namespace doan.Repository
{
    public class SubscriptionRepository : ISubscription
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public SubscriptionRepository(UserManager<AppUser> userManager, ApplicationDbContext context, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
        }

        public async Task<int> createSubscription(SubscriptionCreateRequest request)
        {
            var userId = await _userManager.FindByNameAsync(request.username);
            //var productDuration = await _context.ProductDurations.Include.FindAsync(request.productDurationId);
            var productDuration = await _context.ProductDurations.Where(b => b.Id == request.productDurationId)
                .Include(b => b.duration)
                .Include(b => b.product)
                .FirstOrDefaultAsync();

            var checkExistSubscription = await _context.Subscriptions.Where(x => x.AppUser == userId
                && x.product == productDuration.product).FirstOrDefaultAsync();
            if (checkExistSubscription != null)
            {
                if (checkExistSubscription.dueDate < DateTime.Now)
                {
                    checkExistSubscription.dueDate = DateTime.Now.AddDays(productDuration.duration.day);
                }
                else
                {
                    checkExistSubscription.dueDate = checkExistSubscription.dueDate.AddDays(productDuration.duration.day);
                }
                var result = await _context.SaveChangesAsync();
                return result;

            }
            else
            {
                var token = RandomHelper.RandomString(10);
                Subscription toCreateObject = new Subscription
                {
                    AppUser = userId,
                    product = productDuration.product,
                    dueDate = DateTime.Now.AddDays(productDuration.duration.day),
                    token = token
                };
                await _context.Subscriptions.AddAsync(toCreateObject);
                var result = await _context.SaveChangesAsync();
                return result;

            }
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim("type", productDuration.product.Id.ToString()),
            //        new Claim("username", userId.UserName.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(productDuration.duration.day),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            //    SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //var tokenString = tokenHandler.WriteToken(token);
            //Subscription createObject = new Subscription
            //{
            //    AppUser = userId,
            //    productDuration = productDuration,
            //    createDate = DateTime.Now,
            //    token = tokenString
            //};
            //await _context.Subscriptions.AddAsync(createObject);
            //await _context.SaveChangesAsync();
            //return createObject;
        }

        public async Task<int> deleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            _context.Subscriptions.Remove(subscription);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public Task<int> editSubscription(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<SubscriptionView>, PaginationFilter, int)> getAllSubscription(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize,"");

            var result = await _context.Subscriptions.Include(x => x.AppUser).Include(x => x.product)
                .Skip((filter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize).Select(
                sb => new SubscriptionView
                {
                    id = sb.Id,
                    dueDate = sb.dueDate,
                    productName = sb.product.Name,
                    token = sb.token,
                    invoiceViews = _context.Invoices.Where(x => x.appUser == sb.AppUser && x.productDuration.product == sb.product).Select(
                        iv => new InvoiceView
                        {
                            isPaid = iv.isPaid,
                            amount = iv.Total
                        }).ToList()

                }).ToListAsync();
            var count = await _context.Subscriptions.CountAsync();
            return (result, validFilter, count);
        }

        public async Task<(List<SubscriptionView>, PaginationFilter, int)> getSubscriptionByUsername(string username, PaginationFilter filter)
        {
            var user = await _userManager.FindByNameAsync(username);
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, "");

            var result = await _context.Subscriptions.Include(x => x.AppUser).Where(x=>x.AppUser == user).Include(x => x.product)
                .Skip((filter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize).Select(
                sb => new SubscriptionView
                {
                    id = sb.Id,
                    dueDate = sb.dueDate,
                    productName = sb.product.Name,
                    token = sb.token,
                    invoiceViews = _context.Invoices.Where(x => x.appUser == sb.AppUser && x.productDuration.product == sb.product).Select(
                        iv => new InvoiceView
                        {
                            isPaid = iv.isPaid,
                            amount = iv.Total
                        }).ToList()

                }).ToListAsync();
            var count = await _context.Subscriptions.CountAsync();
            return (result, validFilter, count);
        }

        public async Task<SubscriptionView> getSubscriptionsById(int id)
        {
            var result = await _context.Subscriptions.Include(x => x.AppUser).Include(x => x.product).Where(x => x.Id == id).Select(
                           sb => new SubscriptionView
                           {
                               id = sb.Id,
                               dueDate = sb.dueDate,
                               productName = sb.product.Name,
                               token = sb.token,
                               invoiceViews = _context.Invoices.Where(x => x.appUser == sb.AppUser && x.productDuration.product == sb.product).Select(
                                   iv => new InvoiceView
                                   {
                                       isPaid = iv.isPaid,
                                       amount = iv.Total
                                   }).ToList()

                           }).FirstOrDefaultAsync();
            return result;
        }


    }
}
