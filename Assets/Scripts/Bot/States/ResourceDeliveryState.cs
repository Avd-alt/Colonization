using UnityEngine;

public class ResourceDeliveryState : BotStateBase
{
    private Vector3 _basePosition;

    public ResourceDeliveryState(Bot bot, BotMovement botMovement, Vector3 basePosition)
        : base(bot, botMovement)
    {
        _basePosition = basePosition;
    }

    public override void Enter()
    {
        _botMovement.ResourceDelivered += OnResourceDelivered;
    }

    public override void Update()
    {
        _botMovement.MoveToResource(_basePosition, true);
    }

    public override void Exit()
    {
        _botMovement.ResourceDelivered -= OnResourceDelivered;
    }

    private void OnResourceDelivered()
    {
        Resource resource = _bot.GetCurrentResource();

        if (resource != null)
        {
            _bot.GetResourceManager().NotifyResourceDelivered(resource);
            _bot.SetCurrentResource(null);
        }

        _bot.ChangeState(new IdleState(_bot, _botMovement));
    }
}