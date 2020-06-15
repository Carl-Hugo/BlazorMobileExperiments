using AutoMapper;
using FluentValidation;
using Core.Behaviors;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Services;

namespace Core.Features.Counter
{
    public class Increment
    {
        public class Command : IRequest { }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IStore _store;
            public Handler(IStore store)
            {
                _store = store ?? throw new ArgumentNullException(nameof(store));
            }

            protected override Task Handle(Command request, CancellationToken cancellationToken)
            {
                var state = _store.GetState<CounterState>();
                state.Count++;
                _store.SetState(state);
                return Task.CompletedTask;
            }
        }
    }
}
