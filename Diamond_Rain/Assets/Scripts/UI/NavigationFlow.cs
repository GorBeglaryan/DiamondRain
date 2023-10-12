using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NavigationFlow : MonoBehaviour
{
    [SerializeField] private ViewService viewService;
    [SerializeField] private GameController _gameController;
    [SerializeField] private ParticleSystem rainParticle;
    [SerializeField] private AudioSource gameAudio;

    private AbstractView _currentView;
    private int score = 0;

    public UnityEvent<bool> OnStart { get; } = new UnityEvent<bool>();
    public UnityEvent OnPause { get; } = new UnityEvent();
    public UnityEvent<int> OnScoreValueChanged { get; } = new UnityEvent<int>();

    public void Init()
    {
        Run();
        _gameController.OnGameOver.AddListener(GameOver);
        _gameController.OnScoreValueChanged.AddListener(SetScore);
    }
    private void Run()
    {
        ShowStartView();
    }
    private void ShowStartView()
    {
        AudioInfo.SoundActive = false;
        AudioInfo.MusicActive = false;
        StartView startView = ShowView<StartView>();
        startView.OnPlay.AddListener(StartGame);
        startView.Init();
        rainParticle.Stop();
        gameAudio.Stop();
    }
    private void ShowGameOverView()
    {
        GameOverView gameOverView = ShowView<GameOverView>();
        gameOverView.OnMenu.AddListener(MenuClicked);
        gameOverView.OnRestart.AddListener(StartGame);
        gameOverView.Init();
        rainParticle.Stop();
        gameAudio.Stop();
    }
    private void ShowPauseView()
    {
        PauseView pauseView = ShowView<PauseView>();
        pauseView.OnMenu.AddListener(MenuClicked);
        pauseView.OnRestart.AddListener(RestartGame);
        pauseView.OnResume.AddListener(ResumeGame);
        pauseView.Init();
        rainParticle.Stop();
        gameAudio.Pause();
    }
    private void ShowGameView()
    {       
        GameView gameView = ShowView<GameView>();
        gameView.OnPause.AddListener(PauseGame);
        if (AudioInfo.MusicActive)
        {            
            gameAudio.Play();
        }
       
        rainParticle.Play();      
        gameView.Init();
       
    }
    private TView ShowView<TView>() where TView : AbstractView
    {
        CloseCurrentView();
        TView view = viewService.LoadView<TView>();
        _currentView = view;
        return view;
    }
    private void CloseCurrentView()
    {
        if (_currentView != null)
            Destroy(_currentView.gameObject);
    }
    private void StartGame()
    {
        ShowGameView();
        OnStart?.Invoke(true);
        
    }
    private void PauseGame()
    {
        ShowPauseView();
        OnPause?.Invoke();
    }
    private void RestartGame()
    {
        ShowGameView();
        score = 0;
        OnStart?.Invoke(true);

    }
    private void ResumeGame()
    {
        ShowGameView();
        OnStart?.Invoke(false);
    }
    private void MenuClicked()
    {
        ShowStartView();
        score = 0;
        OnPause?.Invoke();
    }
    private void GameOver()
    {
        ShowGameOverView();
        OnScoreValueChanged?.Invoke(score);
        score = 0;
    }
    private void SetScore(int value)
    {
        score = value > score ? value : score;
        OnScoreValueChanged?.Invoke(value);
    }
}