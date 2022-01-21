using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderVolume : MonoBehaviour
{
    public GameObject sliderValue;

    private void Start()
    {
        gameObject.GetComponent<Slider>().value = MusicManager.soundBoard.MusicVolume;
        sliderValue.GetComponent<TextMeshProUGUI>().text = ((int)(MusicManager.soundBoard.MusicVolume * 100)).ToString();
    }

    public void SliderVolumeEvent()
    {
        float newVolume = gameObject.GetComponent<Slider>().value;
        MusicManager.soundBoard.ChangeVolume(newVolume);
        sliderValue.GetComponent<TextMeshProUGUI>().text = ((int)(newVolume * 100)).ToString();
    }
}
