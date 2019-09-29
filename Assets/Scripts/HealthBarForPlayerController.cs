using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarForPlayerController : MonoBehaviour
{
    public Vector3 offset;
    public CameraFollow camera;
    public float greenStatusBarHeight = 1f;

    private Transform bar;
    private Vector3 mainCameraPosition;

    // Start is called before the first frame update
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
        transform.position = new Vector3(camera.transform.position.x + offset.x, camera.transform.position.y + offset.y, offset.z);
    }
}
