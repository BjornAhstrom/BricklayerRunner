using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Level2 : MonoBehaviour
{
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Level 2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPressedButton()
    {
        SceneHandler.Instance.ChangeLevelTo("MainMenu");
    }
}
