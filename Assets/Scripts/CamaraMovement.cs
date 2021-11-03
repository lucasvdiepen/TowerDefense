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
    private bool cameraIsSetup = false;
    private int mapWidth = 10;
    private int mapHeight = 10;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();

        camera.transform.LookAt(transform);
    }

    private void Update()
    {
        if(cameraIsSetup)
        {
            Movement();
            Rotation();
            Zoom();
        }
    }

    public void UpdateMap(int width, int height)
    {
        mapWidth = width;
        mapHeight = height;

        Vector3 centerPoint = new Vector3(width / 2, 0, height / 2);
        transform.position = new Vector3(centerPoint.x, transform.position.y, centerPoint.z);

        cameraIsSetup = true;
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, mapWidth), transform.position.y, Mathf.Clamp(transform.position.z, 0, mapHeight));
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
