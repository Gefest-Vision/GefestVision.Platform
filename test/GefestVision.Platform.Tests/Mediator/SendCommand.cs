using FluentAssertions;
using GefestVision.Platform.CQRS;
using GefestVision.Platform.Tests.Stubs;
using Moq;
using Xunit;

namespace GefestVision.Platform.Tests.Mediator;

public class SendCommand
{
    [Fact]
    public async Task ShouldResolveAndHandle()
    {
        // Arrange
        const int expectedResult = 67;
        var command = new CommandStub();
        var mockHandler = new Mock<ICommandHandler<CommandStub, int>>();
        mockHandler.Setup(e => e.Handle(command, default)).ReturnsAsync(expectedResult);

        var sut = new CQRS.Mediator(_ => mockHandler.Object);

        // Act
        var result = await sut.SendCommand<CommandStub, int>(command);

        // Assert
        mockHandler.Verify(
            e => e.Handle(It.Is<CommandStub>(f => f == command), default),
            Times.Once
        );
        result.Should().Be(expectedResult);
    }

    [Fact]
    public async Task WhenUnableToResolveShouldThrowWithMessageIncludingTypeName()
    {
        // Arrange
        var sut = new CQRS.Mediator(_ => null);

        // Act
        var act = () => sut.SendCommand<CommandStub, int>(new CommandStub());

        // Assert
        await act.Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .Where(e => e.Message.EndsWith(typeof(ICommandHandler<CommandStub, int>)
                .ToString()));
    }
}
