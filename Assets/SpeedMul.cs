using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedMul : MonoBehaviour
{
    TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        ChangeSpeedShow(GameManager.instance.gameSpeed);
        GameManager.OnGameSpeedChanged += ChangeSpeedShow;
    }

    private void OnDisable()
    {
        GameManager.OnGameSpeedChanged -= ChangeSpeedShow;
    }

    public void ChangeSpeedShow(float newSpeed)
    {
        newSpeed = Mathf.Round(10 * newSpeed) / 10;
        text.text = "x" + newSpeed.ToString();
    }
}
