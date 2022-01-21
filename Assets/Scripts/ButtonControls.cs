using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    public void ButtonControlsEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.Controls);
    }
}
