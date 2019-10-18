using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;

    //string levelToLoad;
    private int currentTopScore = 0;
    private float loadDealy = 1f;
    private YieldInstruction delay = new WaitForSeconds(1);

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        //IncreaseTopScore(PlayerController.Instance.playerScore, currentTopScore);
        currentTopScore = PlayerPrefs.GetInt("HighScore");

        ShowTopScore(currentTopScore);
        PlayerController.Instance.playerScore = 0;

        
    }

    void ShowTopScore(int topScore = 0)
    {
        scoreText.text = "Top score: " + topScore;
    }

    //void IncreaseTopScore(int newTopScore, int oldTopScore)
    //{
    //    if (newTopScore > oldTopScore)
    //    {
    //        currentTopScore = newTopScore;
    //    }
    //    else
    //    {
    //        currentTopScore = oldTopScore;
    //    }
    //}

    public void ChangeScene()
    {
        SceneHandler.Instance.ChangeLevelTo("Level1");
    }

    IEnumerator LoadLevelAndChange(string levelName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(levelName);
        async.allowSceneActivation = false;

        yield return delay;//new WaitForSeconds(loadDealy);

        async.allowSceneActivation = true;

    }
}
