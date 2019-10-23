using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    Vector2 mainCamera;

    public Vector2 offset;
    public float distance = 0.8f;

    private List<GameObject> lives = new List<GameObject>();

    private void Start()
    {
        InitializeLife(PlayerController.Instance.startLives);
    }

    private void Update()
    {
       SetLifeControllerToFollowMainCamera();
      //  UpdateRemovedLives();
    }

    //void UpdateRemovedLives()
    //{
    //    if (PlayerController.Instance.playerDied)
    //    {
    //        RemoveLives();
    //        PlayerController.Instance.playerDied = false;
    //    }
    //}

    public void InitializeLife(int lifeCount)
    {
        // Looking for first life
        GameObject firstLife = transform.GetChild(0).gameObject;
        lives.Add(firstLife);

        // If there is no object, print out a error message
        if (firstLife == null)
        {
            Debug.LogError("No Lives");
            return;
        }

        // Copy firstLife
        for (int i = 0; i < lifeCount -1; i++ )
        {
            GameObject life = Instantiate(firstLife);
            lives.Add(life);
            life.transform.parent = transform;
            Vector3 pos = firstLife.transform.position;
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

    void SetLifeControllerToFollowMainCamera()
    {
        mainCamera = Camera.main.transform.position;
        transform.position = new Vector2(mainCamera.x + offset.x, mainCamera.y + offset.y);
    }
}
