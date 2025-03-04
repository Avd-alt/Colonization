public class BaseConstructionState : BotStateBase
{
    private Flag _flag;

    public BaseConstructionState(Bot bot, BotMovement botMovement, Flag flag) : base(bot, botMovement)
    {
        _flag = flag;
    }

    public override void Enter()
    {
        BotMovement.TargetBaseAchieved += OnBaseLocationReached;
    }

    public override void Update()
    {
        BotMovement.MoveTo(_flag.transform.position, isMovingToFlag: true);
    }

    public override void Exit()
    {
        BotMovement.TargetBaseAchieved -= OnBaseLocationReached;
    }

    private void OnBaseLocationReached()
    {
        BaseCreator creator = Bot.BaseCreator;

        if (creator != null)
        {
            Bot.BaseCreator.CreateBase(Bot, _flag);
        }

        Bot.ChangeState(new IdleState(Bot, BotMovement));
    }
}