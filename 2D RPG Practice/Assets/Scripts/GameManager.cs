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
        //�޴�â Ȱ��ȭ/��Ȱ��ȭ
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
            questText.text = questManager.CheckQuest(id); //����Ʈ ��ȭ ������ ����
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

    public void GameSave()
    {
        //PlayerPrefs: ������ ������ ������ �����ϴ� Ŭ����
        //������ ����
        //�÷��̾� x, y��ǥ, ����Ʈ ID, ����Ʈ Action Index
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        //���� ����
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
