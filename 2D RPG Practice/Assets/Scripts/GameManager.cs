using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public GameObject scanObject;
    public TextMeshProUGUI talkText;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if (isAction) { //�׼� ����
            isAction = false;
        }
        else { //�׼� ����
            isAction = true;
            scanObject = scanObj;
            talkText.text = "�̰��� �̸��� " + scanObj.name + "�̴�.";
        }
        talkPanel.SetActive(isAction);
    }
}
