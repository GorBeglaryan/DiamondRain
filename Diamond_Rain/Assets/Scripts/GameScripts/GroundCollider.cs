using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    [SerializeField] private Transform stoneSpawnPoint;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null)
            return;
        Transform hitTransform = collision.transform;
        if (hitTransform == null)
            return;

        if (hitTransform.gameObject.name.StartsWith("Stone"))
        {
            hitTransform.gameObject.SetActive(false);
            hitTransform.position = stoneSpawnPoint.position;
            hitTransform.gameObject.SetActive(true);
            return;
        }
        Transform parent = hitTransform.parent;
        Destroy(hitTransform.gameObject);
        parent.gameObject.SetActive(false);
    }
}
