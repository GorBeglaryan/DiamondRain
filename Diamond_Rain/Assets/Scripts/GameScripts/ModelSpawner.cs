using System.Collections;
using System.Linq;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField] private ModelService modelService;
    [SerializeField] private Transform[] gameModels;

    private float waitSeconds;
    private string[] modelNames = System.Enum.GetNames(typeof(StoneValues)).Take(7).Select(name => name.ToString()).ToArray();
    bool spawnActive = true;
    public void Init()
    {
        ChangeWaitSeconds(0);
        StopSpawn();
        spawnActive = true;
        StartCoroutine(ModelSpawnController());
    }

    private IEnumerator ModelSpawnController()
    {
        while (spawnActive)
        {
            yield return new WaitForSeconds(waitSeconds);
            byte number = (byte)Mathf.Round(Random.Range(0, 7));
            Transform parentTransform = GetEmptyModelContainer();
            if (parentTransform == null)
                continue;
            modelService.LoadModel(parentTransform, modelNames[number]);
            parentTransform.gameObject.SetActive(true);

        }
    }

    private Transform GetEmptyModelContainer()
    {
        for (byte i = 0; i < gameModels.Length; i++)
        {
            if (!gameModels[i].gameObject.activeSelf)
                return gameModels[i];
        }
        return null;
    }

    public void StopSpawn()
    {
        spawnActive = false;
    }
    public void ChangeWaitSeconds(short value)
    {
        waitSeconds = Mathf.Clamp(1 - Mathf.Floor(value / 10) * .065f, .25f, 1);
    }
    public void DestroyAllModels()
    {
        for (byte i = 0; i < gameModels.Length; i++)
        {
            if (gameModels[i].gameObject.activeSelf)
            {
                var diamond = gameModels[i].GetChild(1);
                Destroy(diamond.gameObject);
                gameModels[i].gameObject.SetActive(false);
            }
                
        }
    }
}
