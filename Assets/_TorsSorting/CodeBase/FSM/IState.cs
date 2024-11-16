namespace CodeBase.FSM
{
    /// <summary>
    /// implement this interface for create your abstract base state for StateMachine
    ///
    /// if u need use params to enter in state, implement IParametredState for concrete state type(NOT FOR BASE)
    /// (for two end more args use custom struct)
    ///
    /// else implement IDefaultState(NOT FOR BASE, only for concrete state)
    /// </summary>
    
    public interface IState
    {
        public void Exit();
    }
}