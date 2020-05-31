using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Features.Navs
{
    public class GoToPage
    {
        public class Command : IRequest
        {
            public Command(string route)
            {
                Route = route ?? throw new ArgumentNullException(nameof(route));
            }

            public string Route { get; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Route).NotEmpty();
            }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly INavigationService _navigationService;
            public Handler(INavigationService navigationService)
            {
                _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            }

            protected override Task Handle(Command request, CancellationToken cancellationToken)
            {
                return _navigationService.GoToAsync(request.Route);
            }
        }
    }
}
