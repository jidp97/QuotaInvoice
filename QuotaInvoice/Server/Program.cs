using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuotaInvoice.Server.Data;
using QuotaInvoice.Shared.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DevelopmentConnection")));
builder.Services.AddDefaultIdentity<ApplicationUser>(configureOptions: options => 
    { 
        options.User = new UserOptions 
        { 
            RequireUniqueEmail = true 
        };
        options.Password = new PasswordOptions
        {
            RequireDigit = false,
            RequireLowercase = false,
            RequireNonAlphanumeric = false,
            RequireUppercase = false,
            RequiredLength = 6
        }; 
    }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = builder.Configuration["JwtIssuer"],
                           ValidAudience = builder.Configuration["JwtAudience"],
                           IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: builder.Configuration["JwtSecurityKey"] ?? throw new NullReferenceException(message: "Empty JwtSecurityKey")))
                       };
                   });
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
    config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
});
builder.Services.AddMvc().AddNewtonsoftJson(options =>
                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
