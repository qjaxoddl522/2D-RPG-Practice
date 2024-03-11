using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //퀘스트 대화순서 인덱스
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

        if (questActionIndex == questList[questId].npcId.Length) {
            NextQuest(); //퀘스트 관련 대화 순서 종료
        }

        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0; //다음 퀘스트를 하기 위한 대화 초기화
    }
}
