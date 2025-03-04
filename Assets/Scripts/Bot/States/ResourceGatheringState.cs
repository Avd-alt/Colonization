using UnityEngine;

public class ResourceGatheringState : BotStateBase
{
    private Vector3 _resourcePosition;

    public ResourceGatheringState(Bot bot, BotMovement botMovement, Vector3 resourcePosition) : base(bot, botMovement)
    {
        _resourcePosition = resourcePosition;
    }

    public override void Enter()
    {
        BotMovement.TargetResourceAchieved += OnResourceReached;
    }

    public override void Update()
    {
        BotMovement.MoveTo(_resourcePosition, isDeliveringResource: false);
    }

    public override void Exit()
    {
        BotMovement.TargetResourceAchieved -= OnResourceReached;
    }

    private void OnResourceReached()
    {
        float pickupRadius = 2f;

        Collider[] hitColliders = Physics.OverlapSphere(Bot.transform.position, pickupRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent(out Resource resource) && (resource.transform.parent == null || resource.transform.parent == Bot.transform))
            {
                resource.transform.SetParent(Bot.transform);
                resource.transform.localPosition = new Vector3(0,0,1);
                resource.Take();
                Bot.SetCurrentResource(resource);
                Bot.ChangeState(new ResourceDeliveryState(Bot, BotMovement, Bot.BasePosition));
                break;
            }
        }
    }
}