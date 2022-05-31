namespace Enemies.State_Machine
{
    public class FinitStateMachine 
    {
        public State currentState { get; private set; }

        public void Initialize(State startingState)
        {
            currentState = startingState;
            currentState.Enter();
        }

        public void ChangeState(State newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }
}
