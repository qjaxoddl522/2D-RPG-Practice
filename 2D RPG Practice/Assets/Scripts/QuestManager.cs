using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //퀘스트 대화순서 인덱스
    public GameObject[] questObject; //퀘스트 관련 오브젝트

    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>(); //초기화 잊지말기
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("유니티 첫 방문", new int[] {1000, 2000}));

        questList.Add(20, new QuestData("수업료 마련하기", new int[] { 5000, 2000 }));

        questList.Add(30, new QuestData("퀘스트 클리어!", new int[] { 0 }));
    }

    public int GetquestTalkIndex(int id)
    {
        return questId + questActionIndex; //퀘스트 번호 + 퀘스트 대화순서 = 퀘스트 대화 ID
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex]) { //대화한 npcid가 퀘스트 리스트에 존재하는 대화 순서 npcid와 일치하면
            questActionIndex++; //퀘스트 관련 다음 대화 진행
        }

        ControlObject(); //퀘스트 오브젝트 관리

        if (questActionIndex == questList[questId].npcId.Length) {
            NextQuest(); //퀘스트 관련 대화 순서 종료
        }

        return questList[questId].questName;
    }
    public string CheckQuest() //오버로딩된 함수
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0; //다음 퀘스트를 하기 위한 대화 초기화
    }

    public void ControlObject()
    {
        switch (questId) {
            case 10:
                if (questActionIndex == 2) { //퀘스트 대화 끝나면 오브젝트 활성화
                    questObject[0].SetActive(true);
                }
                break;
            case 20:
                if (questActionIndex == 0) { //세이브 불러올 때도 오브젝트 활성화 위함
                    questObject[0].SetActive(true);
                }
                else if (questActionIndex == 1) { //퀘스트 대화 끝나면 오브젝트 비활성화
                    questObject[0].SetActive(false);
                }
                break;
        }
    }
}
