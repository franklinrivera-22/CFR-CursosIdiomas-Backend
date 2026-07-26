using CursosApp.Database;
using CursosApp.Extensions;
using CursosApp.Services.Categories;
using CursosApp.Services.Checkout;
using CursosApp.Services.Courses;
using CursosApp.Services.Payments;
using CursosApp.Services.Statistics;
using CursosApp.Services.Transactions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IPaymentGatewayService, SandboxPaymentGatewayService>();
builder.Services.AddTransient<ICheckoutService, CheckoutService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();


builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfig(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddControllers();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();