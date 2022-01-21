using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float MusicVolume = 1;
    public static MusicManager soundBoard;

    public AudioClip defaultClip;
    public AudioClip defaultTwo;
    public AudioClip waiting;
    public AudioClip options;
    public AudioClip gamePlay;
    public AudioClip lose;

    private List<AudioClip> AudioList = new List<AudioClip>();

    private void Awake()
    {
        soundBoard = this;
        GameManager.OnGameStateChanged += OnGameStateChangedListener;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            ChangeVolume(PlayerPrefs.GetFloat("Volume"));
        }
        else
        {
            ChangeVolume(MusicVolume);
        }
    }

    public void ChangeVolume(float newVolume)
    {
        MusicVolume = newVolume;
        PlayerPrefs.SetFloat("Volume", newVolume);
        gameObject.GetComponent<AudioSource>().volume = newVolume;
    }

    public void OnGameStateChangedListener(GameStates newState)
    {
        AudioList.Clear();
        AudioClip moreAudio = Random.Range(0, 2) == 0 ? moreAudio = defaultClip : moreAudio = defaultTwo;
        if (newState == GameStates.Waiting)
        {
            AudioList.Add(waiting);
            AudioList.Add(moreAudio);
        }
        else if(newState == GameStates.Controls || newState == GameStates.MainOptions || newState == GameStates.ChangeTouche || newState == GameStates.Music || newState == GameStates.Difficulty || newState == GameStates.Score)
        {
            AudioList.Add(options);
            AudioList.Add(moreAudio);
        }else if(newState == GameStates.RunGame)
        {
            AudioList.Add(gamePlay);
            AudioList.Add(moreAudio);
        }else if(newState == GameStates.Lose)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            AudioList.Add(lose);
            AudioList.Add(lose);
        }
        else
        {
            Debug.Log("il manque une vérification pour le 'GameState' : " + newState);
        }
    }

    private void Update()
    {
        if (gameObject.GetComponent<AudioSource>().isPlaying is false)
        {
            gameObject.GetComponent<AudioSource>().clip = AudioList[0];
            gameObject.GetComponent<AudioSource>().Play();
            AudioList.Remove(AudioList[0]);
            if(AudioList.Count == 0)
            {
                OnGameStateChangedListener(GameManager.instance.GameState);
            }
        }
    }
}
