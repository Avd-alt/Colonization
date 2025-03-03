using UnityEngine;

public class ResourceGatheringState : BotStateBase
{
    private Vector3 _resourcePosition;

    public ResourceGatheringState(Bot bot, BotMovement botMovement, Vector3 resourcePosition)
        : base(bot, botMovement)
    {
        _resourcePosition = resourcePosition;
    }

    public override void Enter()
    {
        _botMovement.TargetResourceAchieved += OnResourceReached;
    }

    public override void Update()
    {
        _botMovement.MoveToResource(_resourcePosition, false);
    }

    public override void Exit()
    {
        _botMovement.TargetResourceAchieved -= OnResourceReached;
    }

    private void OnResourceReached()
    {
        float pickupRadius = 2f;

        Collider[] hitColliders = Physics.OverlapSphere(_bot.transform.position, pickupRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent(out Resource resource) && (resource.transform.parent == null || resource.transform.parent == _bot.transform))
            {
                resource.transform.SetParent(_bot.transform);
                resource.transform.localPosition = new Vector3(0f, 0f, 1f);
                resource.Take();
                _bot.SetCurrentResource(resource);
                _bot.ChangeState(new ResourceDeliveryState(_bot, _botMovement, _bot.GetBasePosition()));
                break;
            }
        }
    }
}