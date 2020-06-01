using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Features.Counter
{
    public class CounterTest : BaseTest
    {
        public class IncrementButton : CounterTest
        {
            [Fact]
            public async Task Should_send_an_Increment_Command()
            {
                // Arrange
                ArrangeMediatorMock();
                var sut = _ctx.RenderComponent<Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.First();

                // Act
                await incrementButton.ClickAsync(new MouseEventArgs());

                // Assert
                _mediatorMock.Verify(m => m.Send(It.IsAny<Increment.Command>(), default));
            }

            [Fact]
            public async Task Should_increment_the_count()
            {
                // Arrange
                var sut = _ctx.RenderComponent<Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.First();

                // Act
                await incrementButton.ClickAsync(new MouseEventArgs());

                // Assert
                var diffs = sut.GetChangesSinceFirstRender();
                diffs.ShouldHaveChanges(i => i.ShouldBeAttributeChange(
                    expectedAttrName: "text",
                    expectedAttrValue: "The button was clicked 1 times"
                ));
            }
        }

        public class DecrementButton : CounterTest
        {
            [Fact]
            public async Task Should_send_an_Decrement_Command()
            {
                // Arrange
                ArrangeMediatorMock();
                var sut = _ctx.RenderComponent<Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.Skip(1).First();

                // Act
                await incrementButton.ClickAsync(new MouseEventArgs());

                // Assert
                _mediatorMock.Verify(m => m.Send(It.IsAny<Decrement.Command>(), default));
            }

            [Fact]
            public async Task Should_decrement_the_count()
            {
                // Arrange
                var sut = _ctx.RenderComponent<Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.Skip(1).First();

                // Act
                await incrementButton.ClickAsync(new MouseEventArgs());

                // Assert
                var diffs = sut.GetChangesSinceFirstRender();
                diffs.ShouldHaveChanges(i => i.ShouldBeAttributeChange(
                    expectedAttrName: "text",
                    expectedAttrValue: "The button was clicked -1 times"
                ));
            }
        }

    }
}
