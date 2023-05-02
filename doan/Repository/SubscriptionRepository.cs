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

        public async Task<Subscription> createSubscription(SubscriptionCreateRequest request)
        {
            var userId = await _userManager.FindByIdAsync(request.userId.ToString());
            //var productDuration = await _context.ProductDurations.Include.FindAsync(request.productDurationId);
            var productDuration = await _context.ProductDurations.Where(b => b.Id == request.productDurationId)
                .Include(b => b.duration)
                .Include(b => b.product)
                .FirstOrDefaultAsync();

            var checkExistSubscription = await _context.Subscriptions.Where(x => x.AppUser == userId
                && x.productDuration.product == productDuration.product).FirstOrDefaultAsync();
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
                await _context.SaveChangesAsync();
                return checkExistSubscription;

            }
            else
            {
                var token = RandomHelper.RandomString(10);
                Subscription toCreateObject = new Subscription
                {
                    AppUser = userId,
                    productDuration = productDuration,
                    dueDate = DateTime.Now.AddDays(productDuration.duration.day),
                    token = token
                };
                await _context.Subscriptions.AddAsync(toCreateObject);
                await _context.SaveChangesAsync();
                return toCreateObject;

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

        public async Task<bool> deleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            _context.Subscriptions.Remove(subscription);
            var result = await _context.SaveChangesAsync();
            return (result == 1 ? true : false);
        }

        public Task<bool> editSubscription(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Subscription>> getAllSubscription()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> getSubscriptionsById(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            return subscription;
        }


        public async Task<List<Subscription>> getSubscriptionsByUserId(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            List<Subscription> listSubscription = await _context.Subscriptions.Where(x => x.AppUser == user).ToListAsync();
            return listSubscription;
        }
    }
}
