using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadSceneContoller : MonoBehaviour
{
    [SerializeField] GameObject statusBar;
    [SerializeField] float greenStatusBarHeight = 1f;
    [SerializeField] TextMeshPro percentText;

    private float loadingPercent = - 1;
    private float barStatus = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barStatus += 0.01f;
        percentText.text = (loadingPercent += 1) + " %";
        LoadSceneStatusBar();
    }

    void LoadSceneStatusBar()
    {
        statusBar.transform.localScale = new Vector2(barStatus, greenStatusBarHeight);
    }
}
