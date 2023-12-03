using System.Text;
using System.Text.Json.Serialization;
using EcommerceApp.Data;
using EcommerceApp.Mapping;
using EcommerceApp.Models.Domin;
using EcommerceApp.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using Stripe.Terminal;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EcommerceDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("EcommerceDBConnectionString"),ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("EcommerceDBConnectionString")))
);


builder.Services.AddScoped<IProductsRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository > ();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IITemOrderRepository , ItemOrderRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));



//------> Gmail Service


// -----> Identity Framework
//custom feautures
builder.Services.AddIdentityCore<ApplicationUser>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("EcommerceApp")
.AddEntityFrameworkStores<EcommerceDBContext>()
.AddDefaultTokenProviders();





builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequiredLength = 6;
}
);

// ----> Add Authentication 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer  = true,
        ValidateAudience= true,
        ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer= builder.Configuration["JWTToken:Issure"],
        ValidAudience = builder.Configuration["JWTToken:Audience"]  ,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:Key"]))

    });


//--- Corser Policy 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")  // Replace with your React app's URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


//--- cycle error resolve 

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //options.JsonSerializerOptions.WriteIndented = true;
});



// Stripe Payment Integration

builder.Services.Configure<StripeSettingClass>(builder.Configuration.GetSection("Stripe"));

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Value;


builder.Services.AddDataProtection();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowReactApp");
app.Run();



