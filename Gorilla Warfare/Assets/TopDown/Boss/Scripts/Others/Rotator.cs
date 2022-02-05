using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 10;

    public bool x, y, z;

    Vector3 currentRot;
    void Update()
    {
        currentRot = transform.eulerAngles;
        if (x)
            currentRot.x += rotateSpeed * Time.deltaTime;
        if (y)
            currentRot.y += rotateSpeed * Time.deltaTime;
        if (z)
            currentRot.z += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRot);
    }
}
