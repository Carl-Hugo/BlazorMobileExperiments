using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Features.Counter
{
    public class CounterTest : IDisposable
    {
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private readonly TestContext _ctx = new TestContext();
        private bool _disposedValue;

        public CounterTest()
        {
            App.ConfigureServices(_ctx.Services);
        }

        public class IncrementButton : CounterTest
        {
            [Fact]
            public async Task Should_send_an_Increment_Command()
            {
                // Arrange
                _ctx.Services.AddSingleton(_mediatorMock.Object);
                var sut = _ctx.RenderComponent<Core.Features.Counter.Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.First();

                // Act
                await incrementButton.ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

                // Assert
                _mediatorMock.Verify(m => m.Send(It.IsAny<Increment.Command>(), default));
            }

            [Fact]
            public async Task Should_increment_the_count()
            {
                // Arrange
                var sut = _ctx.RenderComponent<Core.Features.Counter.Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.First();

                // Act
                await incrementButton.ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

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
                _ctx.Services.AddSingleton(_mediatorMock.Object);
                var sut = _ctx.RenderComponent<Core.Features.Counter.Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.Skip(1).First();

                // Act
                await incrementButton.ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

                // Assert
                _mediatorMock.Verify(m => m.Send(It.IsAny<Decrement.Command>(), default));
            }

            [Fact]
            public async Task Should_decrement_the_count()
            {
                // Arrange
                var sut = _ctx.RenderComponent<Core.Features.Counter.Counter>();
                var buttons = sut.FindAll("Button".Blazorize());
                var incrementButton = buttons.Skip(1).First();

                // Act
                await incrementButton.ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

                // Assert
                var diffs = sut.GetChangesSinceFirstRender();
                diffs.ShouldHaveChanges(i => i.ShouldBeAttributeChange(
                    expectedAttrName: "text",
                    expectedAttrValue: "The button was clicked -1 times"
                ));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
