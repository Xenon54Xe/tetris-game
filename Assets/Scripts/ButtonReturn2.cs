using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReturn2 : MonoBehaviour
{
    public void ButtonReturnEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.Waiting);
    }
}
