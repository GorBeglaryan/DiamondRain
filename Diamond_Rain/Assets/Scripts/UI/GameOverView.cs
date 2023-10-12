using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameOverView : AbstractView
{
    [SerializeField] private Button restartButton, menuButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private NavigationFlow _navigationFlow;

    public UnityEvent OnRestart { get; } = new UnityEvent();
    public UnityEvent OnMenu { get; } = new UnityEvent();
    public override void Init()
    {
        _navigationFlow = GameObject.Find("Navigation Flow").GetComponent<NavigationFlow>();
        _navigationFlow.OnScoreValueChanged.AddListener(ShowScore);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        menuButton.onClick.AddListener(OnMenuButtonClicked);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        restartButton.onClick?.RemoveListener(OnRestartButtonClicked);
        menuButton.onClick?.RemoveListener(OnMenuButtonClicked);
    }
    private void OnRestartButtonClicked()
    {
        OnRestart?.Invoke();
    }
    private void OnMenuButtonClicked()
    {
        OnMenu?.Invoke();
    }
    private void ShowScore(int value)
    {
        scoreText.text = value.ToString();
    }
}
