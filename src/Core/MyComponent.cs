using Core;
using Core.Models;
using Core.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class MyComponent : ComponentBase
    {
        [Inject]
        public IStore Store { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        protected virtual TState GetState<TState>() where TState : class
        {
            Subscribe<TState>();
            return Store.GetState<TState>();
        }

        protected virtual void Subscribe<TState>() where TState : class
        {
            Store.Subscribe<TState>(StateHasChanged);
        }

        protected virtual Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Mediator.Publish(notification, cancellationToken);
        }

        protected virtual Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Mediator.Send(request, cancellationToken);
        }

        protected virtual Task GoToAsync(string route)
        {
            return SendAsync(new Features.Navs.GoToPage.Command(route));
        }
    }
}
