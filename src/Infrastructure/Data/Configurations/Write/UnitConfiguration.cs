using Domain.Recipes;
using Domain.Tags;
using Domain.Units;
using Infrastructure.Data.Converters;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName(nameof(Unit.Name))
                .HasMaxLength(50)
                .HasColumnName(nameof(Unit.Name).ToLower())
                .IsRequired());

        builder.ComplexProperty(
            u => u.Symbol,
            b => b.Property(e => e.Value).HasColumnName(nameof(Unit.Symbol))
                .HasMaxLength(10)
                .HasColumnName(nameof(Unit.Symbol).ToLower())
                .IsRequired());

        builder.HasMany<RecipeIngredient>()
            .WithOne()
            .HasForeignKey(rt => rt.UnitId);

        //builder.HasData(
        //    new Unit { Id = new Ulid("082fc8d7-4923-46b0-ace3-86d00a2ae239"), Name = "Kilogramos", Symbol = "kg" },
        //    new Unit { Id = new Ulid("b4d55092-1ec0-4723-863f-ec00b66f5f60"), Name = "Gramos", Symbol = "g" },
        //    new Unit { Id = new Ulid("3373e4d7-0d6a-4e0c-a541-94d05edcd103"), Name = "Miligramos", Symbol = "mg" },
        //    new Unit { Id = new Ulid("c3fa5ac6-e5c8-4476-b6b1-10d8fcf2bd84"), Name = "Litros", Symbol = "l" },
        //    new Unit { Id = new Ulid("7e9b5279-233f-4ce8-8b33-f999ff5be481"), Name = "Mililitros", Symbol = "ml" },
        //    new Unit { Id = new Ulid("08bac3db-8ca2-4d03-b971-8566aeb916f1"), Name = "Tazas", Symbol = "tazas" },
        //    new Unit { Id = new Ulid("61b53f0b-099b-40f6-a63d-f675b1afe7b8"), Name = "Cucharaditas", Symbol = "cdta" },
        //    new Unit { Id = new Ulid("afd03506-688b-41fa-90fe-4b009ad19389"), Name = "Cucharadas", Symbol = "cda" },
        //    new Unit { Id = new Ulid("c01a2713-308f-421b-b11e-125cba0e9c47"), Name = "Pizca", Symbol = "pizca" },
        //    new Unit { Id = new Ulid("1e55fb99-cbf5-4d4f-bb54-a30502d0e98c"), Name = "Piezas", Symbol = "pzas" },
        //    new Unit { Id = new Ulid("6baaf217-d702-435d-a809-c2ed285256f6"), Name = "Chorrito", Symbol = "chorrito" },
        //    new Unit { Id = new Ulid("b5397a00-c2ba-4a45-8ec7-5dcb3a8405f2"), Name = "Gotas", Symbol = "gotas" },
        //    new Unit { Id = new Ulid("8d191e6c-48ed-4f66-90b3-34f9c2048734"), Name = "Manojo", Symbol = "manojo" },
        //    new Unit { Id = new Ulid("fbb9f085-1343-493c-bc7e-0e1e475afeec"), Name = "Rama", Symbol = "rama" },
        //    new Unit { Id = new Ulid("b83a4a16-6c5d-43b7-960d-9b55d5f682ae"), Name = "Diente", Symbol = "diente" },
        //    new Unit { Id = new Ulid("201aa095-f406-4d2b-af8f-59f52cd7b7ba"), Name = "Hoja", Symbol = "hoja" },
        //    new Unit { Id = new Ulid("928ba0ad-6ec5-42cb-870f-815c826bcb8f"), Name = "Loncha", Symbol = "loncha" },
        //    new Unit { Id = new Ulid("406aa78e-7ff3-49da-85db-71f52e767133"), Name = "Rodaja", Symbol = "rodaja" },
        //    new Unit { Id = new Ulid("62c796d8-8082-42fb-9651-37e421134bd2"), Name = "Porción", Symbol = "porción" }
        //);
    }
}
