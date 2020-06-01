using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class ErrorState
    {
        public Queue<Error> Errors { get; } = new Queue<Error>();

        public class Error
        {
            public Error(string source, string title, string message)
            {
                Source = source ?? throw new ArgumentNullException(nameof(source));
                Title = title ?? throw new ArgumentNullException(nameof(title));
                Message = message ?? throw new ArgumentNullException(nameof(message));
            }

            public string Source { get; }
            public string Title { get; }
            public string Message { get; }
        }
    }
}
