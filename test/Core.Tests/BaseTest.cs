using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
namespace Core
{
    public abstract class BaseTest : IDisposable
    {
        private bool _disposedValue;
        protected readonly TestContext _ctx = new TestContext();
        protected readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public BaseTest()
        {
            App.ConfigureServices(_ctx.Services);
        }

        protected void ArrangeMediatorMock()
        {
            _ctx.Services.AddSingleton(_mediatorMock.Object);
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
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}