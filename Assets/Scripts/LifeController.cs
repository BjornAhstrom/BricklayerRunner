using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public float distance = 0.8f;

    private List<GameObject> lives = new List<GameObject>();
    //private Vector2 cameraPositon;

    private void Update()
    {
        //UpdateLifeControllerPosition();
    }

    public void InitializeLife(int LifeCount)
    {
        // Letar reda på första livet
        GameObject firstLife = transform.GetChild(0).gameObject;
        lives.Add(firstLife);

        // Om det inte finns något object då ska det skrivas ut ett error meddelande
        if (firstLife == null)
        {
            Debug.LogError("No Lives");
            return;
        }

        // Kopiera firstLife
        for (int i = 0; i < LifeCount -1; i++ )
        {
            GameObject life = Instantiate(firstLife); // Instantiera första livet
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
