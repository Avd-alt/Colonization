public abstract class BotStateBase : IBotState
{
    protected Bot Bot;
    protected BotMovement BotMovement;

    public BotStateBase(Bot bot, BotMovement botMovement)
    {
        Bot = bot;
        BotMovement = botMovement;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
