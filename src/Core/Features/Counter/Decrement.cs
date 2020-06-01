using AutoMapper;
using FluentValidation;
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
    public class Decrement
    {
        public class Command : IRequest
        {
            public Command(CounterState state)
            {
                State = state;//?? throw new ArgumentNullException(nameof(state));
            }

            public CounterState State { get; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.State).NotNull();
            }
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
                request.State.Count--;
                _store.SetState(request.State);
                return Task.CompletedTask;
            }
        }
    }
}
