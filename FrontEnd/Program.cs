using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Duración de la cookie
        options.SlidingExpiration = true; // Renueva la cookie si el usuario está activo
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Duración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necesario para GDPR
});

#region DI
builder.Services.AddHttpClient<IServiceHelper, ServiceHelper>();
builder.Services.AddScoped<IServiceHelper, ServiceHelper>();
builder.Services.AddScoped<IDepartmentHelper, DepartmentHelper>();
builder.Services.AddScoped<ISecurityHelper, SecurityHelper>();
builder.Services.AddScoped<IRegisterHelper, RegisterHelper>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");


app.Run();
