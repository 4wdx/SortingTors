namespace CodeBase.FSM
{
    /// <summary>
    /// for non-args state, implement this for ur concrete state(NOT FOR BASE!)
    /// </summary>
    public interface IDefaultState
    {
        public void Enter();
    }
}