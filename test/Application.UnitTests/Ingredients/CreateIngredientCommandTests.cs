using System.Threading;
using System.Xml.Linq;
using Application.Abstractions.Data;
using Application.Ingredients.Create;
using Domain.Ingredients;
using Domain.Shared;
using Domain.Tags;
using FluentAssertions;
using NSubstitute;
using ROP;
using Xunit;

#nullable disable

namespace Application.UnitTests.Ingredients;

//public class CreateIngredientCommandTests
//{
//    private static readonly CreateIngredientCommand Command = new("Ingredient", 1);

//    private readonly CreateIngredientCommandHandler _handler;

//    private readonly IUnitOfWork _unitOfWorkMock;
//    private readonly IIngredientRepository _ingredientRepository;
//    private readonly ITagService _tagService;

//    public CreateIngredientCommandTests()
//    {
//        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
//        _ingredientRepository = Substitute.For<IIngredientRepository>();
//        _tagService = Substitute.For<ITagService>();

//        _handler = new CreateIngredientCommandHandler(
//            _unitOfWorkMock,
//            _ingredientRepository,
//            _tagService);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnError_WhenNameIsEmpty()
//    {
//        // Arrange
//        CreateIngredientCommand invalidCommand = Command with { Name = string.Empty };

//        // Act
//        Result<Guid> result = await _handler.Handle(invalidCommand, default);

//        // Assert
//        result.Errors.Should().ContainSingle()
//            .Which.Should().Be(TextErrors.Empty);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnError_WhenNameIsNull()
//    {
//        // Arrange
//        CreateIngredientCommand invalidCommand = Command with { Name = null };

//        // Act
//        Result<Guid> result = await _handler.Handle(invalidCommand, default);

//        // Assert
//        result.Errors.Should().ContainSingle()
//            .Which.Should().Be(Error.NullValue);
//    }

//    [Theory]
//    [InlineData(0)]
//    [InlineData(-1)]
//    public async Task Handle_Should_ReturnError_WhenQuantityIsNegativeOrZero(decimal quantity)
//    {
//        // Arrange
//        CreateIngredientCommand invalidCommand = Command with { Quantity = quantity };

//        _ingredientRepository.IsNameUniqueAsync(Text.Create(Command.Name).Value)
//            .Returns(true);

//        // Act
//        Result<Guid> result = await _handler.Handle(invalidCommand, default);

//        // Assert
//        result.Errors.Should().ContainSingle()
//            .Which.Should().Be(PDecimalErrors.NegativeOrZero);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnError_WhenIngredientExist()
//    {
//        // Arrange
//        _ingredientRepository.IsNameUniqueAsync(Text.Create(Command.Name).Value)
//            .Returns(false);

//        // Act
//        Result<Guid> result = await _handler.Handle(Command, default);

//        // Assert
//        result.Errors.Should().ContainSingle()
//            .Which.Should().Be(IngredientErrors.NameNotUnique);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnSuccess_WhenCreateSucceeds()
//    {
//        // Arrange
//        _ingredientRepository.IsNameUniqueAsync(Text.Create(Command.Name).Value)
//            .Returns(true);

//        // Act
//        Result<Guid> result = await _handler.Handle(Command, default);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//    }

//    [Fact]
//    public async Task Handle_Should_CreateTagWithSameName_WhenTagNotExists()
//    {
//        // Arrange
//        Text name = Text.Create(Command.Name).Value;

//        _ingredientRepository.IsNameUniqueAsync(name)
//            .Returns(true);

//        //_tagService.GetByNameAsync(name).Returns((Tag) null);

//        // Act
//        Result<Guid> result = await _handler.Handle(Command, default);

//        // Assert
//        //_tagRepository.Received(1).Insert(Arg.Is<Tag>(u => u.Name == name));
//        _ingredientRepository.Received(1).Insert(Arg.Is<Ingredient>(u => u.Name == name));
//    }

//    [Fact]
//    public async Task Handle_Should_NotCreateTag_WhenTagExists()
//    {
//        // Arrange
//        Text name = Text.Create(Command.Name).Value;
//        var tag = Tag.Create(Guid.NewGuid(), name, DateTime.UtcNow);

//        _ingredientRepository.IsNameUniqueAsync(name)
//            .Returns(true);

//        //_tagRepository.GetByNameAsync(name).Returns(tag);

//        // Act
//        await _handler.Handle(Command, default);

//        // Assert
//        //_tagRepository.DidNotReceive().Insert(tag);
//        _ingredientRepository.Received(1).Insert(Arg.Is<Ingredient>(u => u.Name == name));
//    }

//    [Fact]
//    public async Task Handle_Should_CallInsertOnRepository_WhenCreateSucceeds()
//    {
//        // Arrange
//        _ingredientRepository.IsNameUniqueAsync(Text.Create(Command.Name).Value)
//            .Returns(true);

//        // Act
//        Result<Guid> result = await _handler.Handle(Command, default);

//        // Assert
//        _ingredientRepository.Received(1).Insert(Arg.Is<Ingredient>(u => u.Id == result.Value));
//    }

//    [Fact]
//    public async Task Handle_Should_CallUnitOfWork_WhenCreateSucceeds()
//    {
//        // Arrange
//        _ingredientRepository.IsNameUniqueAsync(Text.Create(Command.Name).Value)
//            .Returns(true);

//        // Act
//        await _handler.Handle(Command, default);

//        // Assert
//        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
//    }
//}
