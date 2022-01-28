using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    Rigidbody rb;
    public float dashSpeed;
    float dashTime;
    public float startDashTime;
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dashTime = startDashTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
                direction = 1;
            else if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift))
                direction = 2;
            else if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftShift))
                direction = 3;
            else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift))
                direction = 4;
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                    rb.velocity = Vector3.forward * dashSpeed * Time.deltaTime;

                if (direction == 2)
                    rb.velocity = Vector3.left * dashSpeed * Time.deltaTime;

                if (direction == 3)
                    rb.velocity = Vector3.back * dashSpeed * Time.deltaTime;

                if (direction == 4)
                    rb.velocity = Vector3.right * dashSpeed * Time.deltaTime;

            }
        }
    }
}
