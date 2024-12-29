using Domain.Categories;
using Domain.Shared;
using Domain.Units;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AppWriteDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<AppWriteDbContext>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        // SEED

        if (!dbContext.Units.Any())
        {
            var units = new List<Unit>
            {
                new(Ulid.Parse("01JFJ8J68D1RY17TM3VXRWZ6A2"), new Text("Kilogramos"), new Text("kg"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68D3P0HMTM0ZT5YH5TP"), new Text("Gramos"), new Text("g"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68D4CKZGGDJH5Y7H6HM"), new Text("Miligramos"), new Text("mg"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68D6RXYCGA5NH6TEGVF"), new Text("Litros"), new Text("l"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68D7Z0D1ZHBSZW2T2DN"), new Text("Mililitros"), new Text("ml"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68D9937PY5SKKPV50W8"), new Text("Tazas"), new Text("tazas"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68D9ARW1HVCHBTPVZHV"), new Text("Cucharaditas"), new Text("cdta"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DACG5ETA7F17AF6WH"), new Text("Cucharadas"), new Text("cda"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DB53TRJ6QYZ6WR8AP"), new Text("Pizca"), new Text("pizca"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DBRMHTA5188ZCS43W"), new Text("Piezas"), new Text("pzas"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DDGTJTXQRDPSY0DQN"), new Text("Chorrito"), new Text("chorrito"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DDJ8BZY2R0NNSQRRC"), new Text("Gotas"), new Text("gotas"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DJR2Q2Y6YK0YXZ2KY"), new Text("Manojo"), new Text("manojo"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DKFYC6WGKYYQJ2WFJ"), new Text("Rama"), new Text("rama"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DMVTFAHT89YRQ12NM"), new Text("Diente"), new Text("diente"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DNC40BM3DTS52KE0M"), new Text("Hoja"), new Text("hoja"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DSHED3SK48FH1G8WT"), new Text("Loncha"), new Text("loncha"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DT6RZVEC0D1DWQGP7"), new Text("Rodaja"), new Text("rodaja"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J68DWBC3CRS39F079H1J"), new Text("Porción"), new Text("porción"), DateTime.UtcNow)
            };

            dbContext.Units.AddRange(units);
            dbContext.SaveChanges();
        }

        if (!dbContext.Categories.Any())
        {
            var categories = new List<Category>
            {
                new(Ulid.Parse("01JFJ8J6B90DFPREG6C1C1BE8R"), new Text("Básicos"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J6B9N4YNT4YCM43BSDNB"), new Text("Lácteos"), DateTime.UtcNow),
                new(Ulid.Parse("01JFJ8J6B9XA0X0GEB5MH9NSTC"), new Text("Carnes"), DateTime.UtcNow)
            };

            dbContext.Categories.AddRange(categories);
            dbContext.SaveChanges();
        }
    }
}
