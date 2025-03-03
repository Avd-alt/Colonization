public class BaseConstructionState : BotStateBase
{
    private Flag _flag;

    public BaseConstructionState(Bot bot, BotMovement botMovement, Flag flag) : base(bot, botMovement)
    {
        _flag = flag;
    }
    public override void Enter()
    {
        _botMovement.TargetBaseAchieved += OnBaseLocationReached;
    }
    public override void Update()
    {
        _botMovement.MoveToFlag(_flag.transform.position);
    }
    public override void Exit()
    {
        _botMovement.TargetBaseAchieved -= OnBaseLocationReached;
    }
    private void OnBaseLocationReached()
    {
        BaseCreator creator = _bot.GetBaseCreator();

        if (creator != null)
        {
            creator.CreateBase(_bot, _flag);
        }

        _bot.ChangeState(new IdleState(_bot, _botMovement));
    }
}