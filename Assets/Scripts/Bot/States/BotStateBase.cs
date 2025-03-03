public abstract class BotStateBase : IBotState
{
    protected Bot _bot;
    protected BotMovement _botMovement;

    public BotStateBase(Bot bot, BotMovement botMovement)
    {
        _bot = bot;
        _botMovement = botMovement;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
