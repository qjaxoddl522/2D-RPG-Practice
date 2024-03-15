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

        //대화패널 애니메이션
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim) { //이미 애니메이션 실행중이면 빈 메시지 설정하고 끝냄(나머지 처리는 TypeEffecter에서)
            talk.SetMsg("");
            return;
        }
        else {
            questTalkIndex = questManager.GetquestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        
        if (talkData == null) { //대화 끝남
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id)); //퀘스트 대화 끝나면 진행
            return;
        }

        if (isNpc) {
            talk.SetMsg(talkData.Split('|')[0]);

            //초상화 갱신
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
