using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDifficulty : MonoBehaviour
{
    public void ButtonDifficultyEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.Difficulty);
    }
}
