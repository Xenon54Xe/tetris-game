using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NormalScore : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.normalBestGameScore.ToString();
    }
}
