using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;

    private Camera camera;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();

        camera.transform.LookAt(transform);
    }

    private void Update()
    {
        int x = 0;
        int y = 0;
        int rotationY = 0;

        if(Input.GetKey(KeyCode.W))
        {
            y += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            y -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            x += 1;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            rotationY -= 1;
        }

        if(Input.GetKey(KeyCode.E))
        {
            rotationY += 1;
        }

        transform.Translate(new Vector3(x, 0, y) * movementSpeed * Time.deltaTime, Space.Self);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y + (rotationY * rotationSpeed * Time.deltaTime), transform.rotation.z));
    }
}
