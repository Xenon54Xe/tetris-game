using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlay : MonoBehaviour
{
    public void ButtonPlayEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.RunGame);
    }
}
