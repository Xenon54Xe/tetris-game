using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject waitScreen;
    public GameObject loseScreen;
    public GameObject mainOptionsScreen;
    public GameObject optionsScreen;
    public GameObject changeToucheScreen;
    public GameObject musicScreen;
    public GameObject difficultyScreen;
    public GameObject scoreScreen;
    public GameObject loseScoreText;
    public GameObject bestScoreText;
    public GameObject gameScreen;

    public bool canWait;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChangedListener;
    }

    private void Start()
    {
        canWait = true;
        changeToucheScreen.SetActive(true);
        changeToucheScreen.SetActive(false);
    }

    public void OnGameStateChangedListener(GameStates newState)
    {
        if (GameManager.instance.GameState == GameStates.Waiting)
        {
            waitScreen.SetActive(true);
            bestScoreText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.bestGameScore.ToString();
        }else
        {
            waitScreen.SetActive(false);
        }
        
        if (GameManager.instance.GameState == GameStates.Lose)
        {
            loseScreen.SetActive(true);
            loseScoreText.GetComponent<Text>().text = GameManager.instance.currentGameScore.ToString();
        }else
        {
            loseScreen.SetActive(false);
        }
        
        if (GameManager.instance.GameState == GameStates.Controls)
        {
            optionsScreen.SetActive(true);
        }else
        {
            optionsScreen.SetActive(false);
        }
        
        if (GameManager.instance.GameState == GameStates.ChangeTouche)
        {
            changeToucheScreen.SetActive(true);
        }else
        {
            changeToucheScreen.SetActive(false);
        }
        
        if(GameManager.instance.GameState == GameStates.Music)
        {
            musicScreen.SetActive(true);
        }else
        {
            musicScreen.SetActive(false);
        }
        
        if(GameManager.instance.GameState == GameStates.MainOptions)
        {
            mainOptionsScreen.SetActive(true);
        }else
        {
            mainOptionsScreen.SetActive(false);
        }
        
        if(GameManager.instance.GameState == GameStates.Difficulty)
        {
            difficultyScreen.SetActive(true);
        }else
        {
            difficultyScreen.SetActive(false);
        }
        
        if(GameManager.instance.GameState == GameStates.Score)
        {
            scoreScreen.SetActive(true);
        }else
        {
            scoreScreen.SetActive(false);
        }
        
        if(GameManager.instance.GameState == GameStates.RunGame)
        {
            gameScreen.SetActive(true);
        }else
        {
            gameScreen.SetActive(false);
        }
    }
}
