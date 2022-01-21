using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccelerateScore : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.accelerateBestGameScore.ToString();
    }
}
