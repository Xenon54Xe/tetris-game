using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FacileScore : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.facileBestGameScore.ToString();
    }
}
