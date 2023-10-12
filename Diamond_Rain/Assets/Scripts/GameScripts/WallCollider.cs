using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb == null)
            return;
        rb.AddForce(Vector3.right * rb.velocity.x * -2, ForceMode.Impulse);
    }
}
