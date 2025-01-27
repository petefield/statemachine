using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine.Tests
{

    public record CatFed : IEvent;

    public record CatTeased : IEvent;
    
    public record CatNotFed : IEvent;

    public enum CatState
    {
        Happy,
        Angry,
        Hungry,
        Dead
    }

    public class Cat() : IStateMachine<CatState>
    {
        public int Hunger = 0;

        public StateMachine<Cat, CatState> StateMachine { get; set; } = new StateMachine<Cat, CatState>();

        public CatState? State { get; set; }

        public void ApplyEvent(IEvent evt)
        {
            if (evt is CatNotFed) Hunger++;
            if (evt is CatFed) Hunger--;
            StateMachine.UpdateState(this, evt);
        }

    }
}
