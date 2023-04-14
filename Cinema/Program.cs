using Cinema.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Cinema.DataAccess.Repository;
using Cinema.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("default");
var serverVersion = ServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<AppDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion).LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors());
//per impostare la verifica dell'account prima di poter effettuare il login
//occorre inserire come argomento del metodo AddDefaultIdentity l'espressione
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//UnitOfWork si occupa della gestione di tutti i repository

//serve per gestire i casi in cui l'utente prova ad accedere a funzioni che richiedono autenticazione e/o autorizzazione
//https://learn.microsoft.com/en-us/answers/questions/963681/asp-net-mvc-how-unauthorize-access-redirect-user-t

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//middleware for Authentication
//l'autenticazione deve sempre precedere l'autorizzazione

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.UseRequestLocalization("it-IT");
app.Run();
