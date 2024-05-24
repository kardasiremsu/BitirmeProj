using BitirmeProj.Data;
using System;
using BitirmeProj.Services;
using BitirmeProj.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{  
   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddSingleton<IUserSessionService, UserSessionService>();
// Add Identity services

// Add other services and configurations

// Configure UserManager
builder.Services.AddScoped<UserManager<RegisterViewModel>>();
builder.Services.AddIdentity<RegisterViewModel, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!"; // Define allowed characters
    options.User.RequireUniqueEmail = false;
    options.Password.RequiredLength = 6; // Minimum username length
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();


builder.Services.AddTransient<IEmailSender>(provider =>
{
    var smtpServer = builder.Configuration["SmtpSettings:SmtpServer"];
    var smtpPort = int.Parse(builder.Configuration["SmtpSettings:SmtpPort"]);
    var smtpUsername = builder.Configuration["SmtpSettings:SmtpUsername"];
    var smtpPassword = builder.Configuration["SmtpSettings:SmtpPassword"];
    return new EmailSender(smtpServer, smtpPort, smtpUsername, smtpPassword);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();
