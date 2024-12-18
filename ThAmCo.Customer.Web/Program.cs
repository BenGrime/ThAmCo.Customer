using ThAmCo.Customer.Web.Services;
// ﻿using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IProductService, ProductsServiceFake>();
}
else
{
    builder.Services.AddHttpClient<ProductsService2>();
    builder.Services.AddTransient<IProductService, ProductsService2>();
    // builder.Services.AddHttpClient<IProductService, ProductsService2>();
}



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();