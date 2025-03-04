using UnityEngine;

public class ResourceDeliveryState : BotStateBase
{
    private Vector3 _basePosition;

    public ResourceDeliveryState(Bot bot, BotMovement botMovement, Vector3 basePosition) : base(bot, botMovement)
    {
        _basePosition = basePosition;
    }

    public override void Enter()
    {
        BotMovement.ResourceDelivered += OnResourceDelivered;
    }

    public override void Update()
    {
        BotMovement.MoveTo(_basePosition, isDeliveringResource: true);
    }

    public override void Exit()
    {
        BotMovement.ResourceDelivered -= OnResourceDelivered;
    }

    private void OnResourceDelivered()
    {
        Resource resource = Bot.CurrentResource;

        if (resource != null)
        {
            Bot.GetResourceManager().NotifyResourceDelivered(resource);
            Bot.SetCurrentResource(null);
        }

        Bot.ChangeState(new IdleState(Bot, BotMovement));
    }
}