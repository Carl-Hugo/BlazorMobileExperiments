using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    /// <summary>
    /// Represents a error message to display to the user.
    /// Each OS should provide an implementation for this service.
    /// </summary>
    public interface IAlertManager
    {
        void Show(string message);
    }
}
