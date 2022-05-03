using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddOcelot();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OcelotAPIGateway", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile($"ocelot.prod.json");
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OcelotAPIGateway v1"));
    builder.Configuration.AddJsonFile($"ocelot.dev.json");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseOcelot();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
