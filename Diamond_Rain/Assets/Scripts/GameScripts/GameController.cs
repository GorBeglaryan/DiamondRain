using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private NavigationFlow _navigationFlow;
    [SerializeField] private BoxMovement _boxMovement;
    [SerializeField] private BoxFloorColliderScript _boxFloorColliderScript;
    [SerializeField] private ModelSpawner _modelSpawner;
    [SerializeField] private StoneSpawner _stoneSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private Transform modelContainer, stoneContainer;
    [SerializeField] private GameObject box;
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip explosionSound;


    private short score = 0;
    private bool isPLaying = false, isGameOver = false;
    private byte bombSpawnerValue = 1;
    public UnityEvent OnGameOver { get; } = new UnityEvent();
    public UnityEvent<int> OnScoreValueChanged { get; } = new UnityEvent<int>();
    private void Start()
    {
        _navigationFlow.OnStart.AddListener(StartGame);
        _navigationFlow.OnPause.AddListener(PauseGame);
        _boxFloorColliderScript.OnBoxEntered.AddListener(ChangeScore);
        _navigationFlow.Init();
    }

    private void Update()
    {
        if (isPLaying)
        {
            _boxMovement.UpdateFrame();
            return;
        }
        if (isGameOver)
        {
            score = 0;
            _stoneSpawner.SpawnActive = false;
            _stoneSpawner.DestroyStones();
            _modelSpawner.StopSpawn();
            _modelSpawner.DestroyAllModels();
            modelContainer.gameObject.SetActive(false);
            stoneContainer.gameObject.SetActive(false);
            box.SetActive(false);
            OnGameOver?.Invoke();
            isGameOver = false;
        }


    }
    private void StartGame(bool isNewGame)
    {
        modelContainer.gameObject.SetActive(true);
        stoneContainer.gameObject.SetActive(true);       
        _modelSpawner.Init();
        _stoneSpawner.SpawnActive = true;
        _stoneSpawner.StartSpawn((byte)(7 + bombSpawnerValue));
        box.SetActive(true);
        _boxMovement.Init();
        isPLaying = true;
        if (!isNewGame)
        {
            OnScoreValueChanged?.Invoke(score);
            return;
        }
        score = 0;

    }
    private void PauseGame()
    {
        _stoneSpawner.SpawnActive = false;
        modelContainer.gameObject.SetActive(false);
        stoneContainer.gameObject.SetActive(false);
        _stoneSpawner.DestroyStones();
        _modelSpawner.StopSpawn();
        isPLaying = false;
        box.SetActive(false);
    }
    private void ChangeScore(short value)
    {
        if (value < -5)
        {
            StartCoroutine(ExplossionCoroutine());
            if(AudioInfo.SoundActive)
                gameAudio.PlayOneShot(explosionSound, 1.0f);
            return;
        }
        else
        {
            score += value;
        }
        OnScoreValueChanged?.Invoke(score);
        if (score < 0)
        {
            isPLaying = false;
            isGameOver = true;
            return;
        }
        ControlBadSpawn(score);
        _modelSpawner.ChangeWaitSeconds(score);
    }
    private void ControlBadSpawn(int value)
    {
        byte temp = (byte)Mathf.FloorToInt(value / 15);

        if (temp > bombSpawnerValue)
        {
            _stoneSpawner.StartSpawn(1);
            bombSpawnerValue = temp;
            _bombSpawner.OnSpawnBomb?.Invoke();
        }
        if (temp < bombSpawnerValue)
        {
            bombSpawnerValue = temp;
            _stoneSpawner.DestroyStone();
        }
    }
    private IEnumerator ExplossionCoroutine()
    {
        yield return new WaitForSeconds(0.65f);
        isPLaying = false;
        isGameOver = true;   
    }

}