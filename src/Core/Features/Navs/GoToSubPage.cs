using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.Navs
{
    public class GoToSubPage
    {
        public class Command : IRequest
        {
            public Command(string route, string query = null)
            {
                Route = route ?? throw new ArgumentNullException(nameof(route));
                Query = query ?? throw new ArgumentNullException(nameof(query));
            }

            public string Route { get; }
            public string Query { get; }
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
            protected override Task Handle(Command request, CancellationToken cancellationToken)
            {
                var currentShell = Xamarin.Forms.Shell.Current;
                var state = currentShell.CurrentState;
                var query = request.Query == null ? "" : $"?{request.Query}";
                return currentShell.GoToAsync($"{state.Location}/{request.Route}{query}");
            }
        }
    }
}