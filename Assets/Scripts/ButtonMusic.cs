using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusic : MonoBehaviour
{
    public void ButtonMusicEvent()
    {
        GameManager.instance.ChangeGameState(GameStates.Music);
    }
}
