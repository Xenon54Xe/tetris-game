using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOptions : MonoBehaviour
{
    public void ButtonOptionsEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.MainOptions);
    }
}
