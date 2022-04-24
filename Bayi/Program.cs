using AppCore.Bayi;
using AppCore.Bayi.Bases;
using AppCore.DataAccess.Configs;
using Bayi.Settings;
using Business.Servis;
using Business.Servis.Base;
using Data.Entity.Context;
using Data.Entity.Repositories;
using Data.Entity.Repositories.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Hesap/GirisIslemi";
        config.AccessDeniedPath = "/Hesap/YetkisizIslem";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        config.SlidingExpiration = true;
    });
builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30); // default: 20 dakika
});

string connectionString = builder.Configuration.GetConnectionString("BayiContext");
ConnectionConfig.ConnectionString = connectionString;
connectionString = @"server=.\\sqlexpress;database=Bayi;trusted_connection=true;";

//Inversion Of Control - IoC
builder.Services.AddScoped<DbContext, BayiContext>();

builder.Services.AddScoped<UrunRepoBase, UrunRepo>();
builder.Services.AddScoped<KullaniciRepoBase, KullaniciRepo>();
builder.Services.AddScoped<KategoriRepoBase, KategoriRepository>();
builder.Services.AddScoped<RolRepoBase, RolRepo>();
builder.Services.AddScoped<HesapDetayRepoBase, HesapDetayiBase>();

builder.Services.AddScoped<IUrunServis, UrunServis>();
builder.Services.AddScoped<IKategoriServis, KategoriServis>();
builder.Services.AddScoped<IKullaniciServis, KullaniciServis>();
builder.Services.AddScoped<IHesapServis, HesapServis>();
builder.Services.AddScoped<IRolServis, RolServis>();
builder.Services.AddScoped<IHesapDetayServis, HesapDetayServis>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

AppSettingsUtilBase appSettingsUtil = new AppSettingsUtil(builder.Configuration);
appSettingsUtil.Bind<AppSettings>();

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

app.UseAuthentication(); //kullanýcý ?

app.UseAuthorization(); // yetki ?

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
