using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBestScore : MonoBehaviour
{
    public void ButtonBestScoreEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.Score);
    }
}
