using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoseScreenQuit : MonoBehaviour
{
    public void ButtonLoseQuitEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.Waiting);
    }
}
