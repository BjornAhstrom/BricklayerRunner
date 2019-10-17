using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] float greenStatusBarHeight = 1f;

    public string levelToLoad;
    public float updateProgress;

    private YieldInstruction delay = new WaitForSeconds(1.2f);

    private static SceneHandler _instance;

    public static SceneHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("SceneHandler")).GetComponent<SceneHandler>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (SceneManager.GetActiveScene().name == "LoadScene")
        {
            StartCoroutine(LoadNewLevelAndChangeScene(levelToLoad));
        }
    }

    IEnumerator LoadNewLevelAndChangeScene(string levelName)
    {
        yield return null;


        AsyncOperation async = SceneManager.LoadSceneAsync(levelName);
        async.allowSceneActivation = false;

        yield return delay;

        async.allowSceneActivation = true;

    }

    public void ChangeLevelTo(string levelName)
    {
        levelToLoad = levelName;

        SceneManager.LoadScene("LoadScene");
    }
}
