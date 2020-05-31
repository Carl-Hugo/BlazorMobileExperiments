﻿using Logic;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TryBlazorMobileBinding
{
    public class MyComponent : ComponentBase
    {
        [Inject]
        public IStore Store { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        public TState GetState<TState>() where TState : class
        {
            return Store.GetState<TState>();
        }

        public void Subscribe<TState>() where TState : class
        {
            Store.Subscribe<TState>(StateHasChanged);
        }

        public Task PublishAsync(object notification, CancellationToken cancellationToken = default)
        {
            return Mediator.Publish(notification, cancellationToken);
        }

        public Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Mediator.Publish(notification, cancellationToken);
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Mediator.Send(request, cancellationToken);
        }

        public Task<object> SendAsync(object request, CancellationToken cancellationToken = default)
        {
            return Mediator.Send(request, cancellationToken);
        }
    }
}
