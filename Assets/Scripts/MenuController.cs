using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;

    private AsyncOperation async;
    private int currentTopScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        IncreaseTopScore(PlayerController.Instance.playerScore, currentTopScore);
        ShowTopScore(currentTopScore);
    }

    void ShowTopScore(int topScore = 0)
    {
        scoreText.text = "Top score: " + topScore;
        PlayerController.Instance.playerScore = 0;
    }

    void IncreaseTopScore(int newTopScore, int oldTopScore)
    {
        if (newTopScore > oldTopScore)
        {
            currentTopScore = newTopScore;
        }
        else
        {
            currentTopScore = oldTopScore;
        }
    }

    public void ChangeScene()
    {
       // Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene("Level1");
    }
}
