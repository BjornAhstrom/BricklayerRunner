using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    public Vector2 offset;
    private Vector2 mainCameraPosition;

    // Update is called once per frame
    void Update()
    {
        SetScoreTextInTopMiddleOfView();
    }

    void SetScoreTextInTopMiddleOfView()
    {
        mainCameraPosition = Camera.main.transform.position;
        transform.position = new Vector3(mainCameraPosition.x + offset.x, mainCameraPosition.y + offset.y);
    }
}
