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

namespace Core.Features.Errors
{
    public class UnhandledExceptionOccurred
    {
        public class Event : INotification
        {
            public Event(string source, Exception exception)
            {
                Source = source ?? throw new ArgumentNullException(nameof(source));
                Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            }
            public Exception Exception { get; }
            public string Source { get; }
        }

        public class StateUpdater : NotificationHandler<Event>
        {
            private readonly IStore _store;
            public StateUpdater(IStore store)
            {
                _store = store ?? throw new ArgumentNullException(nameof(store));
            }

            protected override void Handle(Event notification)
            {
                var state = _store.GetState<ErrorState>();
                state.Errors.Enqueue(new ErrorState.Error(
                    source: notification.Source,
                    title: "An unhandled exception occured",
                    message: notification.Exception.Message
                ));
                _store.SetState(state);
            }
        }

        public class Toaster : NotificationHandler<Event>
        {
            private readonly IAlertManager _message;
            public Toaster(IAlertManager message)
            {
                _message = message ?? throw new ArgumentNullException(nameof(message));
            }

            protected override void Handle(Event notification)
            {
                _message.Show(notification.Exception.Message);
            }
        }
    }
}
