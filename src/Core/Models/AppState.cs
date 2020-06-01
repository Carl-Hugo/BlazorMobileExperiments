using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppState
    {
        public AppState(CounterState counter)
        {
            Counter = counter ?? throw new ArgumentNullException(nameof(counter));
        }

        public CounterState Counter { get; }
    }

    public class CounterState
    {
        public int Count { get; set; }
    }

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
