using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Animator portraitAnim;
    public Image portraitImage;
    public Sprite prevPortrait;
    public GameObject scanObject;
    public TypeEffecter talk;
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

        //��ȭ�г� �ִϸ��̼�
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim) { //�̹� �ִϸ��̼� �������̸� �� �޽��� �����ϰ� ����(������ ó���� TypeEffecter����)
            talk.SetMsg("");
            return;
        }
        else {
            questTalkIndex = questManager.GetquestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        
        if (talkData == null) { //��ȭ ����
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id)); //����Ʈ ��ȭ ������ ����
            return;
        }

        if (isNpc) {
            talk.SetMsg(talkData.Split('|')[0]);

            //�ʻ�ȭ ����
            portraitImage.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split('|')[1]));
            portraitImage.color = new Color(1, 1, 1, 1);
            if (prevPortrait != portraitImage.sprite) {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImage.sprite;
            }
        }
        else {
            talk.SetMsg(talkData);

            portraitImage.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
