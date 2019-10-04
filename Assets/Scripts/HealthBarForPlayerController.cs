using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarForPlayerController : MonoBehaviour
{
    public Vector3 offset;
    public float greenStatusBarHeight = 1f;

    private Transform bar;
    private Vector3 mainCameraPosition;

    void Start()
    {
        bar = transform.Find("Bar");
    }

    private void Update()
    {
        HealthBarToTopLeft();
    }

    public void SetStatusOnHealthBar(float status)
    {
        bar.localScale = new Vector3(status, greenStatusBarHeight);
    }

    void HealthBarToTopLeft()
    {
        mainCameraPosition = Camera.main.transform.position;
        transform.position = new Vector3(mainCameraPosition.x + offset.x, mainCameraPosition.y + offset.y, offset.z);
    }
}
