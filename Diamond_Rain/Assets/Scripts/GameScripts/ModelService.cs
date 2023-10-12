using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelService : MonoBehaviour
{
    private const string MODEL_PATH = "Prefabs/Models";

    public void LoadModel(Transform parentTransform, string modelName)
    {
        GameObject modelPrefab = Resources.Load<GameObject>($"{MODEL_PATH}/{modelName}");
        Instantiate(modelPrefab, parentTransform);
    }
}
