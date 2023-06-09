using AspNetCoreRateLimit;
using doan.DTO.AppUser;
using doan.EF;
using doan.Entities;
using doan.Interface;
using doan.Mail;
using doan.Repository;
using doan.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("KPTDB");


builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, AppRole>(
    config =>
    {
        config.SignIn.RequireConfirmedEmail = true;
        config.User.RequireUniqueEmail = true;
    })
    .AddErrorDescriber<IdentityErrorDescriber>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddRoles<AppRole>();

builder.Services.AddAuthentication(options =>
    {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
    };
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddHttpClient();
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddScoped<IUserService, UserServiceRepository>();
builder.Services.AddScoped<IAppUser, AppUserRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IDuration, DurationRepository>();
builder.Services.AddScoped<IProductDuration, ProductDurationRepository>();
builder.Services.AddScoped<ISubscription,SubscriptionRepository>();
builder.Services.AddScoped<IuseImageToText, UseImageToTextRepository>();
builder.Services.AddTransient<IEmailSender, SendMailService>();
builder.Services.AddTransient<IValidator<AppUserChangePassword>,AppUserChangePasswordValidate>();
builder.Services.AddScoped<ITypeProduct,TypeProductRepository>();
builder.Services.AddScoped<IProductDuration,ProductDurationRepository>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddScoped<IUseImage, UseImageRepository>();
builder.Services.AddInMemoryRateLimiting();


builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "POST:/api/Product/imagecaptioning",
                Period = "30d",
                Limit = 10,
            }
        };
});

builder.Services.AddOptions();
var mailsettings = configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsettings);



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test", Version = "v1" });
});

builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppUserChangePasswordValidate>());

builder.Services.ConfigureApplicationCookie(o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});


var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
});
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.UseIpRateLimiting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();