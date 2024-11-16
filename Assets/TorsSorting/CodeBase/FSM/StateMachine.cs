using System;
using System.Collections.Generic;

namespace CodeBase.FSM
{
    //TState is a base state for state machine
    public class StateMachine<TBaseState> where TBaseState : class, IState 
    {
        private readonly Dictionary<Type, TBaseState> _defaultStates = new Dictionary<Type, TBaseState>();
        private TBaseState _currentState;

        public void AddState<TNewState>(TNewState state) where TNewState : TBaseState
        {
            Type key = typeof(TNewState);
            
            if (!_defaultStates.TryAdd(key, state))
                throw new ArgumentException($"State of type \"{nameof(key)}\" already added");
        }
        
        public void GoTo<TDefaultState>() where TDefaultState : TBaseState, IDefaultState
        {
            Type stateType = typeof(TDefaultState);  
            
            if (_defaultStates.ContainsKey(stateType) == false) 
                throw new ArgumentException($"Try enter to invalid state of type: \"{nameof(stateType)}\"");

            TDefaultState newState = SwitchState<TDefaultState>();
            newState.Enter();
        }

        public void GoTo<TParametredState, TArgs>(TArgs args) where TParametredState : TBaseState, IParametredState<TArgs>
        {
            Type stateType = typeof(TParametredState);  
            
            if (_defaultStates.ContainsKey(stateType) == false) 
                throw new ArgumentException($"Try enter to invalid state of type: \"{nameof(stateType)}\"");

            TParametredState newState = SwitchState<TParametredState>();
            newState.Enter(args);
        }

        private TNewState SwitchState<TNewState>() where TNewState : TBaseState
        {
            Type key = typeof(TNewState);
            
            TNewState newState = (TNewState)_defaultStates[key];
            _currentState?.Exit();
            _currentState = newState;
            return newState;
        }
    }
}