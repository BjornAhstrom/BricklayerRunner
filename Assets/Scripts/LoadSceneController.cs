using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] GameObject statusBar;
    [SerializeField] TextMeshPro percentText;

    private float barStatus = 1f;
    int progress = 0;

    private void Start()
    {
        LoadSceneStatusBar();
    }

    // Ändrar på den gröna bakgrundens x led i progress baren
    void LoadSceneStatusBar()
    {
        LeanTween.scaleX(statusBar, 1f, barStatus).setEaseLinear().setOnComplete(LoadSceneStatusBar);
    }

    private void Update()
    {
        progress = (int) (statusBar.transform.localScale.x * 100);

        percentText.text = progress.ToString() + "%";
    }

}
