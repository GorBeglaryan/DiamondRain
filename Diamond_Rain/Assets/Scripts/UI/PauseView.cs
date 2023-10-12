using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseView : AbstractView
{
    [SerializeField] private Button resumeButton, restartButton, menuButton;

    public UnityEvent OnResume { get; } = new UnityEvent();
    public UnityEvent OnRestart { get; } = new UnityEvent();
    public UnityEvent OnMenu { get; } = new UnityEvent();
    public override void Init()
    {
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        menuButton.onClick.AddListener(OnMenuButtonClicked);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        resumeButton.onClick?.RemoveListener(OnResumeButtonClicked);
        restartButton.onClick?.RemoveListener(OnRestartButtonClicked);
        menuButton.onClick?.RemoveListener(OnMenuButtonClicked);
    }
    private void OnResumeButtonClicked()
    {
        OnResume?.Invoke();
    }
    private void OnRestartButtonClicked()
    {
        OnRestart?.Invoke();
    }
    private void OnMenuButtonClicked()
    {
        OnMenu?.Invoke();
    }
}
