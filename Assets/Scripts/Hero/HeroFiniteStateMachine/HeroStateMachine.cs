namespace Hero.HeroFiniteStateMachine
{
   public class HeroStateMachine
   {
      public HeroState currentState { get; private set; }

      public void Initialize(HeroState startingState)
      {
         currentState = startingState;
         currentState.Enter();
      }
   
      public void ChangeState(HeroState newState)
      {
         currentState.Exit();
         currentState = newState;
         currentState.Enter();
      }
   }
}
