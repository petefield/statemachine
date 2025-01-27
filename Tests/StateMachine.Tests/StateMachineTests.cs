namespace StateMachine.Tests;


public class StateMachineTests
{
    private readonly Cat _cat;

    public StateMachineTests()
    {
        _cat = new Cat();
        _cat.StateMachine.AddTransition<CatFed>(CatState.Hungry, CatState.Happy);
        _cat.StateMachine.AddTransition<CatNotFed>(CatState.Happy, CatState.Hungry);
        _cat.StateMachine.AddTransition<CatNotFed>(CatState.Hungry, CatState.Hungry);
        _cat.StateMachine.AddTransition<CatNotFed>(CatState.Hungry, CatState.Dead, (c, e) => c.Hunger > 10);
        _cat.StateMachine.AddTransition<CatTeased>(CatState.Happy, CatState.Angry);        
    }

    [Fact]
    public void UpdateState_WhenValidTransition_Should_UpdateSubjectState()
    {
        _cat.State = CatState.Happy;

        _cat.ApplyEvent(new CatTeased());

        Assert.Equal(CatState.Angry, _cat.State);
    }

    [Fact]
    public void UpdateState_WhenInvalidTransition_Should_ThrowErro()
    {
        _cat.State = CatState.Angry;
        Assert.Throws<Exception>(() => _cat.ApplyEvent(new CatFed()));
    }

    [Fact]
    public void UpdateState_WhenConditionMatched_Should_UpdateState()
    {
        _cat.Hunger = 10;
        _cat.State = CatState.Hungry;
        _cat.ApplyEvent(new CatNotFed());
        Assert.Equal(CatState.Dead, _cat.State);
    }

    [Fact]
    public void UpdateState_WhenConditionNot_Matched_Should_UsedDefaultTransiont()
    {
        _cat.Hunger = 0;
        _cat.State = CatState.Hungry;
        _cat.ApplyEvent(new CatNotFed());
        Assert.Equal(CatState.Hungry, _cat.State);
    }

}