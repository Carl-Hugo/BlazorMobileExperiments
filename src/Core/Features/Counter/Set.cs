using AutoMapper;
using Core.Models;
using Core.Services;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.Counter
{
    public class Set
    {
        public class Command : IRequest
        {
            public Command(int value)
            {
                Value = value;
            }

            public int Value { get; }
        }

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
                state.Count = request.Value;
                _store.SetState(state);
                return Task.CompletedTask;
            }
        }
    }
}
