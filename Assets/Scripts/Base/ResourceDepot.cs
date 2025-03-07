using UnityEngine;

public class ResourceDepot : MonoBehaviour
{
    [SerializeField] private RemoverResource _remover;

    public int QuantityResources { get; private set; }

    private void OnEnable()
    {
        _remover.ResourceAdding += AddResource;
    }

    private void OnDisable()
    {
        _remover.ResourceAdding -= AddResource;
    }

    public void TakeResources(int quantity)
    {
        QuantityResources -= quantity;
    }

    private void AddResource(Resource resource)
    {
        QuantityResources++;
    }
}