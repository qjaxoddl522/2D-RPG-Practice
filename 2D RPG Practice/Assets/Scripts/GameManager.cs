using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Image portraitImage;
    public GameObject scanObject;
    public TextMeshProUGUI talkText;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObj.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetquestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+questTalkIndex, talkIndex);

        if (talkData == null) { //��ȭ ����
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id)); //����Ʈ ��ȭ ������ ����
            return;
        }

        if (isNpc ) {
            talkText.text = talkData.Split('|')[0];

            portraitImage.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split('|')[1]));
            portraitImage.color = new Color(1, 1, 1, 1);
        }
        else {
            talkText.text = talkData;

            portraitImage.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
