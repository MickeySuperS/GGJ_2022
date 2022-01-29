using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 10;

    Vector3 currentRot;
    void Update()
    {
        currentRot = transform.eulerAngles;
        currentRot.y += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRot);
    }
}
