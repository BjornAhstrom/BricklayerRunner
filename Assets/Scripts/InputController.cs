using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Vector3 offset;
    Vector3 mainCameraPosition;

    public delegate void ButtonPressed();
    public static event ButtonPressed AButton;
    public static event ButtonPressed BButton;
    public static event ButtonPressed UpArrowButton;
    public static event ButtonPressed DownArrowButton;
    public static event ButtonPressed LeftArrowButton;
    public static event ButtonPressed RightArrowButton;

    private void LateUpdate()
    {
        mainCameraPosition = Camera.main.transform.position;
        transform.position = new Vector3(mainCameraPosition.x + offset.x, mainCameraPosition.y + offset.y);
    }

#if (UNITY_IOS || UNITY_ANDROID)
    private void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (LeftArrowButton != null && hit.collider != null && hit.collider.tag == "LeftArrowButton")
                {
                    //Debug.Log("Left press");
                    LeftArrowButton();
                }
                else if (RightArrowButton != null && hit.collider != null && hit.collider.tag == "RightArrowButton")
                {
                    //Debug.Log("Right press");
                    RightArrowButton();
                }
            }
        }
    }
#elif (UNITY_EDITOR)

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (LeftArrowButton != null && hit.collider != null && hit.collider.tag == "LeftArrowButton")
            {
                //Debug.Log("Left press");
                LeftArrowButton();
            }
            else if (RightArrowButton != null && hit.collider != null && hit.collider.tag == "RightArrowButton")
            {
                //Debug.Log("Right press");
                RightArrowButton();
            }
        }
    }
#endif
}
