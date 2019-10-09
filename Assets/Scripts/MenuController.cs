using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    TextMeshPro scoreText;
    public int playerTopScore;
    int currentTopScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        ShowTopScore(currentTopScore);
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseTopScore(playerTopScore, currentTopScore);
    }

    void ShowTopScore(int topScore)
    {
        scoreText.text = "Top score: " + topScore;
    }

    void IncreaseTopScore(int newTopScore, int oldTopScore)
    {
        if (newTopScore > oldTopScore)
        {
            currentTopScore = playerTopScore;
        }
    }
}
