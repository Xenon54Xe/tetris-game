using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficileScore : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.difficileBestGameScore.ToString();
    }
}
