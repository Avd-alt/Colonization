using System;
using UnityEngine;
using Zenject;

public class RemoverResource : MonoBehaviour
{
    private Pool _poolResources;

    public event Action<Resource> ResourceAdding;

    [Inject]
    public void Construct(Pool pool)
    {
        _poolResources = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) == true)
        {
            ResourceAdding?.Invoke(resource);
            _poolResources.ReleaseResource(resource);
        }
    }
}