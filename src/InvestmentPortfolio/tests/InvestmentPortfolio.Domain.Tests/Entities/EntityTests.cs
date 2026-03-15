using FluentAssertions;
using InvestmentPortfolio.Domain.Entities;

namespace InvestmentPortfolio.Domain.Tests.Entities;

public class EntityTests
{
    private class TestEntity : Entity
    {
        public TestEntity() : base() { }
        public TestEntity(Guid id) : base(id) { }
    }

    [Fact]
    public void Entity_WhenCreated_ShouldHaveNonEmptyId()
    {
        var entity = new TestEntity();

        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Entity_WhenCreatedWithSpecificId_ShouldHaveCorrectId()
    {
        var id = Guid.NewGuid();
        var entity = new TestEntity(id);

        entity.Id.Should().Be(id);
    }

    [Fact]
    public void Entity_WhenCreatedWithEmptyId_ShouldThrowException()
    {
        var act = () => new TestEntity(Guid.Empty);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*id*");
    }

    [Fact]
    public void Entity_WhenTwoEntitiesHaveSameId_ShouldBeEqual()
    {
        var id = Guid.NewGuid();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        entity1.Should().Be(entity2);
    }

    [Fact]
    public void Entity_WhenTwoEntitiesHaveDifferentIds_ShouldNotBeEqual()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        entity1.Should().NotBe(entity2);
    }
}
