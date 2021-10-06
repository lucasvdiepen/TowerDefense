using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseFollow : MonoBehaviour
{
    public Canvas canvas;

    private RawImage rawImage;

    private bool isGrabbing = false;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        rawImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if(isGrabbing && Input.GetMouseButton(0))
        {
            //transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToViewportPoint(Input.mousePosition), speed);
            //transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);    

            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera,
                out movePos);

            transform.position = canvas.transform.TransformPoint(movePos);
        }
    }

    public void Select(Texture towerTexture)
    {
        isGrabbing = true;
        rawImage.gameObject.SetActive(true);
        rawImage.texture = towerTexture;
    }

    public void Deselect()
    {
        isGrabbing = false;
        rawImage.gameObject.SetActive(false);
    }
}
