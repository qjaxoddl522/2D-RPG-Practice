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
        if (isAction) { //액션 종료
            isAction = false;
        }
        else { //액션 시작
            isAction = true;
            scanObject = scanObj;
            talkText.text = "이것의 이름은 " + scanObj.name + "이다.";
        }
        talkPanel.SetActive(isAction);
    }
}
