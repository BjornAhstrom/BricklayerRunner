using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    public float distance = 0.8f;

    private List<GameObject> lives = new List<GameObject>();


    private void Start()
    {
        InitializeLife(PlayerController.Instance.startLives);
    }

    private void Update()
    {
        UpdateRemovedLives();
    }

    void UpdateRemovedLives()
    {
        if (PlayerController.Instance.playerDied)
        {
            RemoveLives();
            PlayerController.Instance.playerDied = false;
            //InitializeLife(PlayerController.Instance.startLives);
            //SceneManager.LoadScene("Level1");
        }
    }

    public void InitializeLife(int lifeCount)
    {
        // Letar reda på första livet
        GameObject firstLife = transform.GetChild(0).gameObject;
        lives.Add(firstLife);

        // If there is no object, print out a error message
        if (firstLife == null)
        {
            Debug.LogError("No Lives");
            return;
        }

        // Kopiera firstLife
        for (int i = 0; i < lifeCount -1; i++ )
        {
            GameObject life = Instantiate(firstLife);
            lives.Add(life);
            life.transform.parent = transform;
            Vector3 pos = life.transform.position;
            pos.x += distance * (i + 1);
            life.transform.position = pos;
        }
    }

    public bool RemoveLives()
    {
        if ( lives.Count < 1)
        {
            return false;
        }

        GameObject lastLife = lives[lives.Count -1];
        lives.RemoveAt(lives.Count -1);

        Destroy(lastLife);
        return true;
    }
}
