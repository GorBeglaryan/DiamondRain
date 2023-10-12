using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameView : AbstractView
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    private NavigationFlow _navigationFlow;
    public UnityEvent OnPause { get; } = new UnityEvent();

    public override void Init()
    {
        _navigationFlow=GameObject.Find("Navigation Flow").GetComponent<NavigationFlow>();
        _navigationFlow.OnScoreValueChanged.AddListener(ShowScore);

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        pauseButton.onClick?.RemoveListener(OnPauseButtonClicked);
    }
    private void OnPauseButtonClicked()
    {
        OnPause?.Invoke();
    }
    private void ShowScore(int value)
    {
        scoreText.text = value.ToString();
    }
}