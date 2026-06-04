using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [Header("スタート")]
    [SerializeField] private Button _startButton;
    [SerializeField] private string _startSceneName;
    [Header("終了")]
    [SerializeField] private Button _exitButton;

    private CancellationTokenSource _cts;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        _startButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                var cts = new CancellationTokenSource();
                StartUp(cts).Forget();
            }).AddTo(this);

        _exitButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                ExitGame();
            }).AddTo(this);
    }

    public async UniTask StartUp(CancellationTokenSource cts)
    {
        await SceneManager.LoadSceneAsync(_startSceneName, LoadSceneMode.Single).ToUniTask();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
