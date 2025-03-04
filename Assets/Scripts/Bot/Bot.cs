using UnityEngine;

[RequireComponent(typeof(BotMovement))]
public class Bot : MonoBehaviour
{
    private BotMovement _botMovement;
    private IBotState _currentState;
    private IResourceDeliveryAnnouncer _resourceDeliveryAnnouncer;

    public Vector3 BasePosition { get; private set; }
    public BaseCreator BaseCreator { get; private set; }
    public Resource CurrentResource { get; private set; }
    public bool IsAvailable => _currentState is IdleState;

    private void Awake()
    {
        _botMovement = GetComponent<BotMovement>();
        _currentState = new IdleState(this, _botMovement);
        _currentState.Enter();
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }

    public void ChangeState(IBotState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.Enter();
        }
    }

    public void SetBasePosition(Vector3 basePosition) => BasePosition = basePosition;

    public void SetBaseCreator(BaseCreator baseCreator) => BaseCreator = baseCreator;

    public IResourceDeliveryAnnouncer GetResourceManager() => _resourceDeliveryAnnouncer;

    public void SetResourceManager(IResourceDeliveryAnnouncer resourceDeliveryAnnouncer) => _resourceDeliveryAnnouncer = resourceDeliveryAnnouncer;

    public void SetCurrentResource(Resource resource) => CurrentResource = resource;

    public void GatherResource(Vector3 resourcePosition)
    {
        if (IsAvailable == true)
        {
            ChangeState(new ResourceGatheringState(this, _botMovement, resourcePosition));
        }
    }
    public void BuildNewBase(Flag flag)
    {
        if (IsAvailable == true)
        {
            ChangeState(new BaseConstructionState(this, _botMovement, flag));
        }
    }
}