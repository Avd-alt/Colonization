using System;
using UnityEngine;
public class FlagCreator : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private BuildingClickHandler _clickVisualizer;

    private Flag _currentFlag;

    public event Action<Flag> OnFlagSet;

    private void OnEnable()
    {
        _clickVisualizer.FlagPlacementRequested += SetFlag;
    }

    private void OnDisable()
    {
        _clickVisualizer.FlagPlacementRequested -= SetFlag;
    }

    private void SetFlag(Vector3 position)
    {
        if (_currentFlag == null)
        {
            _currentFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
        }
        else
        {
            _currentFlag.transform.position = position;
        }

        OnFlagSet?.Invoke(_currentFlag);
    }
}