using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    private static SceneHandler _instance;

    public static SceneHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("SceneHandler")).GetComponent<SceneHandler>;
            }
            return _instance;
        }
    }

    private void Awake()
    {
       // if
    }
}
