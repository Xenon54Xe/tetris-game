using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownDifficulty : MonoBehaviour
{
    private void OnEnable()
    {
        Color color = Color.blue;
        int diff = GameManager.instance.difficulty;
        if (diff == 0)
        {
            color = Color.HSVToRGB(127 / 360f, 1f, 1f);
        }
        else if (diff == 1)
        {
            color = Color.HSVToRGB(61 / 360f, 1f, 1f);
        }
        else if (diff == 2)
        {
            color = Color.HSVToRGB(0f, 1f, 1f);
        }else if(diff == 3)
        {
            color = Color.HSVToRGB(240 / 360f, 1f, 1f);
        }
        TMP_Dropdown dropdown = gameObject.GetComponent<TMP_Dropdown>();
        dropdown.value = diff;
        SetDropdownColors(dropdown, color);
        dropdown.onValueChanged.AddListener(delegate { DropdownDifficultyEvent(dropdown); });
    }

    private void OnDisable()
    {
        TMP_Dropdown dropdown = gameObject.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.RemoveListener(delegate { DropdownDifficultyEvent(dropdown); });
    }

    public void SetDropdownColors(TMP_Dropdown dropdown, Color color)
    {
        var colors = dropdown.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        colors.selectedColor = color;
        dropdown.colors = colors;
    }

    public void DropdownDifficultyEvent(TMP_Dropdown dropdown)
    {
        GameManager.instance.ChangeGameDifficulty(dropdown.value);
        Color color = Color.blue;
        if (dropdown.value == 0)
        {
            color = Color.HSVToRGB(127 / 360f, 1f, 1f);
        }
        else if(dropdown.value == 1)
        {
            color = Color.HSVToRGB(61 / 360f, 1f, 1f);
        }
        else if(dropdown.value == 2)
        {
            color = Color.HSVToRGB(0f, 1f, 1f);
        }
        else if(dropdown.value == 3)
        {
            color = Color.HSVToRGB(240 / 360f, 1f, 1f);
        }
        SetDropdownColors(dropdown, color);
    }
}
