using UnityEngine;
using DG.Tweening;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    private const string FlagPlacementMessage = "ÂÛÁÅÐÈÒÅ ÌÅÑÒÎ ÄËß ÓÑÒÀÍÎÂÊÈ ÔËÀÃÀ";
    private const string NoAvailableFlagsMessage = "ÍÅÂÎÇÌÎÆÍÎ ÏÎÑÒÐÎÈÒÜ ÁÀÇÓ, ÒÀÊ ÊÀÊ Ó ÁÀÇÛ ÍÅÒ ÑÂÎÁÎÄÍÛÕ ÔËÀÃÎÂ";
    private const string NotEnoughBotsMessage = "ÍÅÂÎÇÌÎÆÍÎ ÏÎÑÒÐÎÈÒÜ ÁÀÇÓ, ÒÀÊ ÊÀÊ Ó ÂÀÑ 1 ÁÎÒ";

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private BuildingClickHandler _clickVisualizer;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private float _fadeDuration = 1.5f;
    private float _minAlpha = 0;
    private float _maxAlpha = 1;

    private void Start()
    {
        _canvasGroup.alpha = _minAlpha;
    }

    private void OnEnable()
    {
        _clickVisualizer.NotEnoughBots += () => ShowFadeEffect(NotEnoughBotsMessage);
        _clickVisualizer.BuildingAllowed += () => ShowFadeEffect(FlagPlacementMessage);
        _clickVisualizer.FlagPlacementDenied += () => ShowFadeEffect(NoAvailableFlagsMessage);
    }

    private void OnDisable()
    {
        _clickVisualizer.NotEnoughBots -= () => ShowFadeEffect(NotEnoughBotsMessage);
        _clickVisualizer.BuildingAllowed -= () => ShowFadeEffect(FlagPlacementMessage);
        _clickVisualizer.FlagPlacementDenied -= () => ShowFadeEffect(NoAvailableFlagsMessage);
    }

    private void ShowFadeEffect(string message)
    {
        _textMeshPro.text = message;
        _canvasGroup.DOFade(_maxAlpha, _fadeDuration)
            .OnComplete(() => _canvasGroup.DOFade(_minAlpha, _fadeDuration));
    }
}