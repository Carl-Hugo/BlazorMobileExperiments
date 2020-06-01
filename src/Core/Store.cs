using Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Store : IStore
    {
        private readonly ConcurrentDictionary<Type, List<Action>> _subscribers = new ConcurrentDictionary<Type, List<Action>>();
        private readonly IServiceProvider _serviceProvider;

        public Store(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public TState GetState<TState>() where TState : class
        {
            var value = _serviceProvider.GetService<TState>();
            return value;
        }

        public void SetState<TState>(TState state) where TState : class
        {
            UpdateStateSubscriber<TState>();
        }

        private void UpdateStateSubscriber<TState>() where TState : class
        {
            var type = typeof(TState);
            if (_subscribers.ContainsKey(type))
            {
                var subscribers = _subscribers[type];
                foreach (var subscriber in subscribers)
                {
                    subscriber.Invoke();
                }
            }
        }

        public void Subscribe<TState>(Action stateHasChangedDelegate) where TState : class
        {
            if (stateHasChangedDelegate == null) { throw new ArgumentNullException(nameof(stateHasChangedDelegate)); }

            var type = typeof(TState);
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers.TryAdd(type, new List<Action>());
            }
            _subscribers[type].Add(stateHasChangedDelegate);
        }
    }
}
