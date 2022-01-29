using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z) - transform.position);

        transform.rotation = lookRot;
    }
}
