using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode[]> OnActionTouchesChanged;
    public static event Action<GameStates> OnGameStateChanged;
    public static event Action<float> OnGameSpeedChanged;

    private KeyCode rotateRight = KeyCode.E;
    private KeyCode rotateLeft = KeyCode.A;
    private KeyCode moveRight = KeyCode.D;
    private KeyCode moveLeft = KeyCode.Q;
    private KeyCode moveDown = KeyCode.S;

    public int difficulty = 0;

    public static GameManager instance;

    private bool canChangeScreen;

    public float bestGameScore = 0;
    public float currentGameScore;
    public float facileBestGameScore = 0;
    public float normalBestGameScore = 0;
    public float difficileBestGameScore = 0;
    public float accelerateBestGameScore = 0;

    public float gameSpeed = 1;

    public GameStates GameState;

    public KeyCode[] keyCodeArray = new KeyCode[5];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");

        if (PlayerPrefs.HasKey("FacileScore"))
        {
            facileBestGameScore = PlayerPrefs.GetFloat("FacileScore");
        }
        if (PlayerPrefs.HasKey("NormalScore"))
        {
            normalBestGameScore = PlayerPrefs.GetFloat("NormalScore");
        }
        if (PlayerPrefs.HasKey("DifficileScore"))
        {
            difficileBestGameScore = PlayerPrefs.GetFloat("DifficileScore");
        }
        if (PlayerPrefs.HasKey("AccelerateScore"))
        {
            accelerateBestGameScore = PlayerPrefs.GetFloat("AccelerateScore");
        }

        keyCodeArray[0] = rotateRight;
        keyCodeArray[1] = rotateLeft;
        keyCodeArray[2] = moveRight;
        keyCodeArray[3] = moveLeft;
        keyCodeArray[4] = moveDown;

        if (PlayerPrefs.HasKey("Key 0"))
        {
            for (int i = 0; i < keyCodeArray.Length; i++)
            {
                KeyCode x = (KeyCode)PlayerPrefs.GetInt("Key " + i.ToString());
                keyCodeArray[i] = x;
            }
        }

        OnActionTouchesChanged?.Invoke(keyCodeArray);

        bestGameScore = PlayerPrefs.GetFloat("BestScore");

        ChangeGameState(GameStates.Waiting);

        float newGameSpeed = difficulty == 0 || difficulty == 3 ? newGameSpeed = 1 : difficulty == 1 ? newGameSpeed = 1.5f : newGameSpeed = 2;
        ChangeGameSpeed(newGameSpeed);
    }

    private void Update()
    {
        if(GameState != GameStates.RunGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeGameState(GameStates.Waiting);
            }
        }
    }

    public void ChangeGameDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
        float newGameSpeed = difficulty == 0 || difficulty == 3 ? newGameSpeed = 1 : difficulty == 1 ? newGameSpeed = 1.5f : newGameSpeed = 2;
        ChangeGameSpeed(newGameSpeed);
        PlayerPrefs.SetInt("Difficulty", newDifficulty);
    }

    public void ChangeGameState(GameStates newState)
    {
        GameState = newState;
        OnGameStateChanged?.Invoke(newState);
        if(newState == GameStates.Lose)
        {
            StartCoroutine(WaitBeforeChangeScreenInLoseScreen());
        }
    }

    public void ChangeGameSpeed(float newSpeed)
    {
        gameSpeed = newSpeed;
        OnGameSpeedChanged?.Invoke(newSpeed);
    }

    public void SetScore(float newScore)
    {
        currentGameScore = newScore;

        if(newScore > bestGameScore)
        {
            bestGameScore = newScore;
        }

        if(difficulty == 0 && newScore > facileBestGameScore)
        {
            facileBestGameScore = newScore;
            PlayerPrefs.SetFloat("FacileScore", newScore);
        }
        else if (difficulty == 1 && newScore > normalBestGameScore)
        {
            normalBestGameScore = newScore;
            PlayerPrefs.SetFloat("NormalScore", newScore);
        }
        else if(difficulty == 2 && newScore > difficileBestGameScore)
        {
            difficileBestGameScore = newScore;
            PlayerPrefs.SetFloat("DifficileScore", newScore);
        }else if(difficulty == 3 && newScore > accelerateBestGameScore)
        {
            accelerateBestGameScore = newScore;
            PlayerPrefs.SetFloat("AccelerateScore", newScore);
        }

        float lastBestScore = PlayerPrefs.GetFloat("BestScore");
        if(bestGameScore > lastBestScore)
        {
            PlayerPrefs.SetFloat("BestScore", bestGameScore);
        }
    }

    public void ChangeKeyCode(int id, KeyCode newKeyCode)
    {
        keyCodeArray[id] = newKeyCode;

        for(int i = 0; i < keyCodeArray.Length; i++)
        {
            int x = (int)keyCodeArray[i];
            PlayerPrefs.SetInt("Key " + i.ToString(), x);
        }

        OnActionTouchesChanged?.Invoke(keyCodeArray);
    }

    IEnumerator WaitBeforeChangeScreenInLoseScreen()
    {
        yield return new WaitForSeconds(6);
        if (GameState == GameStates.Lose)
        {
            ChangeGameState(GameStates.Waiting);
        }
    }
}

public enum GameStates
{
    Waiting,
    RunGame,
    Lose,
    MainOptions,
    Controls,
    ChangeTouche,
    Music,
    Difficulty,
    Score
}
