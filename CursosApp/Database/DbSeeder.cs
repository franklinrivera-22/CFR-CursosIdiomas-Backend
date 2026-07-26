using CursosApp.Constants;
using CursosApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CursosApp.Database
{
    public class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;

            var context = provider.GetRequiredService<AppDbContext>();
            var userManager = provider.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = provider.GetRequiredService<RoleManager<RoleEntity>>();
            await context.Database.EnsureCreatedAsync();

            await SeedRolesAsync(roleManager);
            await SeedAdminAsync(userManager);
            await SeedCategoriesAndCoursesAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<RoleEntity> roleManager)
        {
            string[] roles = { RolesConstant.ADMIN, RolesConstant.NORMAL_USER };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new RoleEntity
                    {
                        Name = role,
                        Description = $"Rol {role}"
                    });
                }
            }
        }

        private static async Task SeedAdminAsync(UserManager<UserEntity> userManager)
        {
            const string adminEmail = "cfr@cursos.com";
            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                var admin = new UserEntity
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Administrador",
                    LastName = "General"
                };

                var result = await userManager.CreateAsync(admin, "Cfr1234*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, RolesConstant.ADMIN);
                }
            }
        }

        private static async Task SeedCategoriesAndCoursesAsync(AppDbContext context)
        {
            if (await context.Categories.AnyAsync()) return; 

            var now = DateTime.Now;

            var ingles = new CategoryEntity { Id = Guid.NewGuid().ToString(), Name = "Inglés", Description = "Cursos de idioma inglés", CreatedDate = now, UpdatedDate = now };
            var frances = new CategoryEntity { Id = Guid.NewGuid().ToString(), Name = "Francés", Description = "Cursos de idioma francés", CreatedDate = now, UpdatedDate = now };
            var aleman = new CategoryEntity { Id = Guid.NewGuid().ToString(), Name = "Alemán", Description = "Cursos de idioma alemán", CreatedDate = now, UpdatedDate = now };

            await context.Categories.AddRangeAsync(ingles, frances, aleman);

            var courses = new List<CourseEntity>
            {
                new CourseEntity { Id = Guid.NewGuid().ToString(), Title = "Inglés Básico A1", Description = "Fundamentos del inglés: saludos, presente simple y vocabulario esencial.", Level = "A1", Price = 45.00m, DurationHours = 30, ImageUrl = "https://img.freepik.com/psd-premium/nivel-a1-concepto-ingles-nivel-elemental-principiante-renderizacion-3d-aislada-sobre-fondo-transparente_823159-21418.jpg", IsActive = true, CategoryId = ingles.Id, CreatedDate = now, UpdatedDate = now },
                new CourseEntity { Id = Guid.NewGuid().ToString(), Title = "Inglés Intermedio B1", Description = "Conversación fluida, tiempos verbales y comprensión auditiva.", Level = "B1", Price = 65.00m, DurationHours = 45, ImageUrl = "https://th.bing.com/th/id/OIP.L6AgX6xZWuncj90FIam49AHaE0?w=202&h=131&c=7&r=0&o=7&pid=1.7&rm=3", IsActive = true, CategoryId = ingles.Id, CreatedDate = now, UpdatedDate = now },
                new CourseEntity { Id = Guid.NewGuid().ToString(), Title = "Inglés Avanzado C1", Description = "Dominio del idioma para contextos académicos y profesionales.", Level = "C1", Price = 90.00m, DurationHours = 60, ImageUrl = "https://www.bing.com/images/search?view=detailV2&ccid=CCbQQXQR&id=87F053A27622C4E0CBB320A4304113519BC64EB1&thid=OIP.CCbQQXQRQvpb2klK6osbtAHaE0&mediaurl=https%3a%2f%2fimg.freepik.com%2fpsd-premium%2fc1-conceito-de-nivel-de-ingles-c1-nivel-avancado-renderizacao-3d-isolada-em-fundo-transparente_823159-21057.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.0826d041741142fa5bda494aea8b1bb4%3frik%3dsU7Gm1ETQTCkIA%26pid%3dImgRaw%26r%3d0&exph=407&expw=626&q=Ingles+C1+imagen&FORM=IRPRST&ck=7F89212E8982B58C1C3172E306B848B0&selectedIndex=0&itb=0", IsActive = true, CategoryId = ingles.Id, CreatedDate = now, UpdatedDate = now },
                new CourseEntity { Id = Guid.NewGuid().ToString(), Title = "Francés Básico A1", Description = "Primeros pasos en francés: pronunciación y frases cotidianas.", Level = "A1", Price = 50.00m, DurationHours = 30, ImageUrl = "https://www.bing.com/images/search?view=detailV2&ccid=OvvfaHp5&id=D2C1BAF7D2ED7D71248B845DDBC60AA1B49BCCAC&thid=OIP.OvvfaHp5X2jIlYpsUOMNdgHaHa&mediaurl=https%3a%2f%2fdspace.ups.edu.ec%2f382a271e-6065-4ad6-bc56-300a8fe81bbf%2f809f2ed5-c946-4ab9-9291-5afb7f05d116.None&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.3afbdf687a795f68c8958a6c50e30d76%3frik%3drMybtKEKxttdhA%26pid%3dImgRaw%26r%3d0&exph=1201&expw=1201&q=Frances+a1+imagen&FORM=IRPRST&ck=3C5AFAA15D665329F5B08E1D9342D8C0&selectedIndex=9&itb=0", IsActive = true, CategoryId = frances.Id, CreatedDate = now, UpdatedDate = now },
                new CourseEntity { Id = Guid.NewGuid().ToString(), Title = "Francés Intermedio B2", Description = "Gramática avanzada y expresión escrita en francés.", Level = "B2", Price = 75.00m, DurationHours = 50, ImageUrl = "https://www.shutterstock.com/image-illustration/b2-french-level-concept-upper-600nw-2441167381.jpg", IsActive = true, CategoryId = frances.Id, CreatedDate = now, UpdatedDate = now },
                new CourseEntity { Id = Guid.NewGuid().ToString(), Title = "Alemán Básico A2", Description = "Vocabulario, casos gramaticales y conversación inicial en alemán.", Level = "A2", Price = 60.00m, DurationHours = 40, ImageUrl = "https://th.bing.com/th/id/OIP.t5-M3YPMIDdW1Wt8vaJjtQHaEK?w=202&h=113&c=7&r=0&o=7&pid=1.7&rm=3", IsActive = true, CategoryId = aleman.Id, CreatedDate = now, UpdatedDate = now }
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
}