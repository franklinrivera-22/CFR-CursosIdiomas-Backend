using CursosApp.Database;
using CursosApp.Services.Categories;
using CursosApp.Services.Courses;
using CursosApp.Services.Payments;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IPaymentGatewayService, SandboxPaymentGatewayService>();


builder.Services.AddOpenApi();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
