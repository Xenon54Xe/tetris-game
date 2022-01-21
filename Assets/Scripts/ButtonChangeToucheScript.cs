using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeToucheScript : MonoBehaviour
{
    public void ButtonChangeToucheEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.ChangeTouche);
    }
}
