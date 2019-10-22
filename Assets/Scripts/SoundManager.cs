using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip jumpOnEnemy;
    //[SerializeField] AudioClip takeLife;
    [SerializeField] AudioClip takeBiggerPlayer;

    AudioSource audioSource;

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("SoundManager")).GetComponent<SoundManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
       
    }

    public void JumpSound()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }

    public void JumpOnEnemySound()
    {
        audioSource.clip = jumpOnEnemy;
        audioSource.Play();
    }

    //public void TakeLifeSound()
    //{
    //    audioSource.clip = takeLife;
    //    audioSource.Play();
    //}

    public void TakeBiggerPlayerSound()
    {
        audioSource.clip = takeBiggerPlayer;
        audioSource.Play();
    }
}
