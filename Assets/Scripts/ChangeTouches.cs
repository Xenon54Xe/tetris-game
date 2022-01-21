using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTouches : MonoBehaviour
{

    private TMP_InputField mainInputField;
    private bool canTip;

    public GameObject[] buttonArray = new GameObject[5];

    private int buttonId;

    private void Awake()
    {
        mainInputField = gameObject.GetComponent<TMP_InputField>();
        mainInputField.onValueChanged.AddListener(delegate { Change(mainInputField); });
    }

    public void SetId(int newId)
    {
        buttonId = newId;
    }

    private void OnEnable()// utiliser cette fonction pour changer les touches affichees sur les boutons
    {
        mainInputField = gameObject.GetComponent<TMP_InputField>();
        StartCoroutine(WaitBeforeTip());
        mainInputField.text = "Change key";

        KeyCode[] keyCodeArray = GameManager.instance.keyCodeArray;

        for(int i = 0; i < keyCodeArray.Length; i++)
        {
            string x = keyCodeArray[i].ToString();
            buttonArray[i].GetComponentInChildren<TextMeshProUGUI>().text = x;//finir
        }
    }

    IEnumerator WaitBeforeTip()
    {
        canTip = false;
        mainInputField.readOnly = true;
        yield return new WaitForSeconds(0.5f);
        mainInputField.readOnly = false;
        canTip = true;
    }

    public void Change(TMP_InputField input)
    {
        if (canTip)
        {
            string newString = input.text;
            int newInt = newString[0];
            int addInt = 65 <= newInt && newInt <= 90 ? addInt = 32 : 97 <= newInt && newInt <= 122 ? addInt = 0 : addInt = 0;
            KeyCode newKeyCode = (KeyCode)newInt + addInt;
            GameManager.instance.ChangeKeyCode(buttonId, newKeyCode);
            char c = System.Convert.ToChar(newInt - 32 + addInt);
            buttonArray[buttonId].GetComponentInChildren<TextMeshProUGUI>().text = c.ToString();
            GameManager.instance.ChangeGameState(GameStates.Controls);
        }
    }
}
