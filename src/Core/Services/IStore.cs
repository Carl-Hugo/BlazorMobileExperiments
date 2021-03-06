﻿using System;

namespace Core.Services
{
    public interface IStore
    {
        TState GetState<TState>() where TState : class;
        void SetState<TState>(TState state) where TState : class;
        void Subscribe<TState>(Action stateHasChangedDelegate) where TState : class;
    }
}
