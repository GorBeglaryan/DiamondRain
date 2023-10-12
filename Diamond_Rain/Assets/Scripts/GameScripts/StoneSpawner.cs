using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private Transform stoneContainer;
    [SerializeField] private ModelService modelService;
    public bool SpawnActive;
    public void Init()
    {
        StartSpawn(10);
    }
    private IEnumerator StoneSpawnerCoroutine(byte count)
    {
        
        while(count > 0)
        {
            if (!SpawnActive)
                break;
            yield return new WaitForSeconds(1);
            modelService.LoadModel(stoneContainer, "Stone");
            count--;
        }
    }
    public void StartSpawn(byte count)
    {
        StartCoroutine(StoneSpawnerCoroutine(count));
    }
    public void DestroyStone()
    {
        if (stoneContainer.childCount > 0)
            Destroy(stoneContainer.GetChild(0).gameObject);
    }
    public void DestroyStones()
    {
        foreach (Transform child in stoneContainer)
            Destroy(child.gameObject);
    }


}
