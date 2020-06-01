using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class XamarinNavigationService : INavigationService
    {
        public Task GoToAsync(string route)
        {
            var currentShell = Xamarin.Forms.Shell.Current;
            return currentShell.GoToAsync($"//{route}");
        }

        public Task GoToSubPageAsync(string route)
        {
            var currentShell = Xamarin.Forms.Shell.Current;
            var state = currentShell.CurrentState;
            return currentShell.GoToAsync($"{state.Location}/{route}");
        }
    }
}
