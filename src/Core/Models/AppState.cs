using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppState
    {
        public AppState(CounterState counter, ErrorState errorState)
        {
            Counter = counter ?? throw new ArgumentNullException(nameof(counter));
            ErrorState = errorState ?? throw new ArgumentNullException(nameof(errorState));
        }

        public CounterState Counter { get; }
        public ErrorState ErrorState { get; }
    }
}
