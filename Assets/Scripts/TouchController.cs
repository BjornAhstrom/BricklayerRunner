using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch began");
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch moved");
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch ended");
            }
        }
    }
}
