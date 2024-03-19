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
    public GameObject menuSet;
    public TypeEffecter talk;
    public TextMeshProUGUI questText;
    public GameObject player;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    private void Update()
    {
        //메뉴창 활성화/비활성화
        if (Input.GetButtonDown("Cancel")) {
            if (menuSet.activeSelf) {
                menuSet.SetActive(false);
            }
            else {
                menuSet.SetActive(true);
            }
        }
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
            questText.text = questManager.CheckQuest(id); //퀘스트 대화 끝나면 진행
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

    public void GameSave()
    {
        //PlayerPrefs: 간단한 데이터 저장을 지원하는 클래스
        //저장할 내용
        //플레이어 x, y좌표, 퀘스트 ID, 퀘스트 Action Index
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        //최초 실행
        if (!PlayerPrefs.HasKey("PlayerX")) {
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
