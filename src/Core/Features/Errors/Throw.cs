using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.Errors
{
    public partial class Throw
    {
        public class Command : IRequest
        {
            public Command(Exception exception)
            {
                Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            }

            public Exception Exception { get; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            protected override Task Handle(Command request, CancellationToken cancellationToken)
            {
                throw request.Exception;
            }
        }
    }
}
