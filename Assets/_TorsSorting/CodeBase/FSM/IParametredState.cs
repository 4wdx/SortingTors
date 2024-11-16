namespace CodeBase.FSM
{
    /// <summary>
    /// for state with enter args, implement this for ur concrete state(NOT FOR BASE!)
    /// recommendation: create custom struct for args 
    /// </summary>
    public interface IParametredState<TArgs>
    {
        public void Enter(TArgs args);
    }
}