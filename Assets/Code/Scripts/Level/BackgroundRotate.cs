using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotate : MonoBehaviour
{
    [SerializeField] private float rotSpeed;

    void Update()
    {
        transform.Rotate(Vector3.back, rotSpeed * Time.deltaTime);
    }
}
