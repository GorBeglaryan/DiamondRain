using UnityEngine;

public class LightRotation : MonoBehaviour
{
    private Transform modelTransform;
   
    private void LateUpdate()
    {
        modelTransform = transform.parent.GetChild(1);        
        if (modelTransform == null)
            return;
        transform.position = new Vector3(modelTransform.position.x*2, modelTransform.position.y*2, Camera.main.transform.position.z);
        transform.rotation = Quaternion.LookRotation(modelTransform.position - transform.position);
    }
}
