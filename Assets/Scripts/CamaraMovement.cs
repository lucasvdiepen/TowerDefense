using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public float zoomSpeed = 10f;

    public float minCamaraDistance = 0.5f;
    public float maxCameraDistance = 5f;

    private Camera camera;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();

        camera.transform.LookAt(transform);
    }

    private void Update()
    {
        Movement();
        Rotation();
        Zoom();
    }

    public void UpdateCenter(Vector3 centerPoint)
    {
        transform.position = new Vector3(centerPoint.x, transform.position.y, centerPoint.z);
    }

    public void UpdateMaxZoomDistance()
    {

    }

    private void Movement()
    {
        int x = 0;
        int y = 0;

        if (Input.GetKey(KeyCode.W))
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

        //Should not be able to move outside the map

        transform.Translate(new Vector3(x, 0, y) * movementSpeed * Time.deltaTime, Space.Self);
    }

    private void Rotation()
    {
        int rotationY = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            rotationY += 1;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotationY -= 1;
        }

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (rotationY * rotationSpeed * Time.deltaTime), transform.eulerAngles.z);
    }

    private void Zoom()
    {
        //Determine zoom direction
        int scrollDirection = 0;

        if(Input.mouseScrollDelta.y > 0) scrollDirection = 1;
        if(Input.mouseScrollDelta.y < 0) scrollDirection = -1;

        if(scrollDirection != 0)
        {
            Vector3 newCameraPosition = camera.transform.position + camera.transform.forward * zoomSpeed * scrollDirection *  Time.deltaTime;
            float distance = Vector3.Distance(newCameraPosition, transform.position);
            Debug.Log(distance);

            if (distance >= minCamaraDistance && distance <= maxCameraDistance)
            {
                camera.transform.position = newCameraPosition;
            }
        }
    }
}
