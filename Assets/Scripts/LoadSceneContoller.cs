using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadSceneContoller : MonoBehaviour
{
    [SerializeField] GameObject statusBar;
    [SerializeField] float greenStatusBarHeight = 1f;
    [SerializeField] TextMeshPro percentText;

    private float barStatus = 0.001f;
    private float progressMultipliedBy = 100f;
    private float progressDividedBy = 0.9f;
    private YieldInstruction endOfFrame = new WaitForEndOfFrame();

    private void Start()
    {
        StartCoroutine(LoadNewLevelAndChangeScene(PlayerController.Instance.levelToLoad));
    }

    IEnumerator LoadNewLevelAndChangeScene(string leveleName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(leveleName);
        
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / progressDividedBy);

            LoadSceneStatusBar(progress);

            yield return endOfFrame;
        }
    }

    // Ändrar på den gröna bakgrundens x led i progress baren
    void LoadSceneStatusBar(float progress)
    {
        barStatus += progress;
        percentText.text = (progress * progressMultipliedBy) + " %";

        statusBar.transform.localScale = new Vector2(barStatus, greenStatusBarHeight);
    }
}
