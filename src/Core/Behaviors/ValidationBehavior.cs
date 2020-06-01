using FluentValidation;
using Core.Features.Errors;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Behaviors
{
    //public class ValidationBehavior<TRequest> : IRequestPreProcessor<TRequest>
    //    where TRequest : IRequest
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;
    //    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }

    //    public Task Process(TRequest request, CancellationToken cancellationToken)
    //    {
    //        var context = new ValidationContext(request);
    //        var failures = _validators
    //            .Select(v => v.Validate(context))
    //            .SelectMany(result => result.Errors)
    //            .Where(f => f != null)
    //            .ToList();

    //        if (failures.Count != 0)
    //        {
    //            throw new ValidationException(failures);
    //        }
    //        return Task.CompletedTask;
    //    }
    //}

    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMediator _mediator;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ExceptionBehavior(IMediator mediator, IEnumerable<IValidator<TRequest>> validators)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                if (request is UnhandledExceptionOccurred.Event)
                {
                    return await next();
                }
                var context = new ValidationContext(request);
                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    await _mediator.Publish(new UnhandledExceptionOccurred.Event(typeof(TRequest).Name, new ValidationException(failures)));
                    return default;
                }
                return await next();
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new UnhandledExceptionOccurred.Event(typeof(TRequest).Name, ex));
                return default;
            }
        }
    }

    //public static class HandlerValidationExtensions
    //{
    //    public static void EnforceRequestValidity<TRequest>(this IEnumerable<IValidator<TRequest>> validators, TRequest request)
    //        where TRequest : IRequest
    //    {
    //        var context = new ValidationContext(request);
    //        var failures = validators
    //            .Select(v => v.Validate(context))
    //            .SelectMany(result => result.Errors)
    //            .Where(f => f != null)
    //            .ToList();

    //        if (failures.Count != 0)
    //        {
    //            throw new ValidationException(failures);
    //        }
    //    }
    //}

    //public class ValidationErrorHandler<TRequest> : AsyncRequestExceptionAction<TRequest>
    //    where TRequest : IRequest
    //{
    //    private readonly IMediator _mediator;
    //    public ValidationErrorHandler(IMediator mediator)
    //    {
    //        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    //    }

    //    protected override Task Execute(TRequest request, Exception exception, CancellationToken cancellationToken)
    //    {
    //        return Task.CompletedTask;
    //    }
    //}
}
