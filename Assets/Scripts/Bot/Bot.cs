using UnityEngine;

[RequireComponent(typeof(BotMovement))]
public class Bot : MonoBehaviour
{
    private BotMovement _botMovement;
    private IBotState _currentState;
    private BaseCreator _baseCreator;
    private Resource _currentResource;
    private IResourceDeliveryAnnouncer _resourceDeliveryAnnouncer;
    private Vector3 _basePosition;

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
    public Vector3 GetBasePosition() => _basePosition;
    public void SetBasePosition(Vector3 basePosition) => _basePosition = basePosition;
    public BaseCreator GetBaseCreator() => _baseCreator;
    public void SetBaseCreator(BaseCreator baseCreator) => _baseCreator = baseCreator;
    public IResourceDeliveryAnnouncer GetResourceManager() => _resourceDeliveryAnnouncer;
    public void SetResourceManager(IResourceDeliveryAnnouncer resourceDeliveryAnnouncer) => _resourceDeliveryAnnouncer = resourceDeliveryAnnouncer;
    public Resource GetCurrentResource() => _currentResource;
    public void SetCurrentResource(Resource resource) => _currentResource = resource;
    public bool IsAvailable() => _currentState is IdleState;

    public void GatherResource(Vector3 resourcePosition)
    {
        if (IsAvailable())
        {
            ChangeState(new ResourceGatheringState(this, _botMovement, resourcePosition));
        }
    }
    public void BuildNewBase(Flag flag)
    {
        if (IsAvailable())
        {
            ChangeState(new BaseConstructionState(this, _botMovement, flag));
        }
    }
}