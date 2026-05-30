using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _activeButton;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _canvasGroupAlphaBeginning = 0.0f;

    private void Start()
    {
        _canvasGroup.alpha = _canvasGroupAlphaBeginning;

        // ボタン落下購読
        _activeButton.OnClickAsObservable().Subscribe(_ =>
        {
            ChangeCanvasGroupAlpha();
        });
    }

    private void ChangeCanvasGroupAlpha()
    {
        var alpha = _canvasGroup.alpha <= 0.0f ? 1.0f : 0.0f;

        _canvasGroupAlphaBeginning = alpha;
    }
}
