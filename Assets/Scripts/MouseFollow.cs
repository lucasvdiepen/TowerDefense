using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public Canvas canvas;
    public float speed = 0.1f;

    void Update()
    {
        if(Input.GetMouseButton(0))
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
}
