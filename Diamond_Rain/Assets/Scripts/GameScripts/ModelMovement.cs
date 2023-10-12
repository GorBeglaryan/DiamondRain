using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;

    private void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        rb.AddForce(GetRandomVector2() * force, ForceMode.Impulse);
        rb.AddTorque(GetRandomVector3() * 10, ForceMode.Impulse);
    }

    private Vector2 GetRandomVector2()
    {
        return new Vector2(Random.Range(-1f, 1f), 0);
    }
    private Vector3 GetRandomVector3()
    {
        return new Vector3
            (
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            );
    }
}
