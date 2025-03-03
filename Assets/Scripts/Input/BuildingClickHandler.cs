using System;
using UnityEditor;
using UnityEngine;

public class BuildingClickHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskBase;
    [SerializeField] private LayerMask _layerMaskGround;

    private bool _canPlaceMarker = false;

    public event Action<Vector3> FlagPlacementRequested;
    public event Action BuildingAllowed;
    public event Action FlagPlacementDenied;
    public event Action NotEnoughBots;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMaskBase))
        {
            if(hit.collider.TryGetComponent(out Base clickBase) && clickBase.HasAvailableFlags() == false)
            {
                FlagPlacementDenied?.Invoke();
            }
            else if(clickBase.HasEnoughBots() == false)
            {
                NotEnoughBots?.Invoke();
            }
            else
            {
                _canPlaceMarker = true;
                BuildingAllowed?.Invoke();
            }
        }
        else if (_canPlaceMarker == true && Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMaskGround))
        {
            FlagPlacementRequested?.Invoke(hit.point);
            _canPlaceMarker = false;
        }
    }
}