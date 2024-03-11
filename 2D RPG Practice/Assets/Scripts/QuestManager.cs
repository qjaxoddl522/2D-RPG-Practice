using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //����Ʈ ��ȭ���� �ε���
    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>(); //�ʱ�ȭ ��������
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("����Ƽ ù �湮", new int[] {1000, 2000}));

        questList.Add(20, new QuestData("������ �����ϱ�", new int[] { 5000, 2000 }));
    }

    public int GetquestTalkIndex(int id)
    {
        return questId + questActionIndex; //����Ʈ ��ȣ + ����Ʈ ��ȭ���� = ����Ʈ ��ȭ ID
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex]) { //��ȭ�� npcid�� ����Ʈ ����Ʈ�� �����ϴ� ��ȭ ���� npcid�� ��ġ�ϸ�
            questActionIndex++; //����Ʈ ���� ���� ��ȭ ����
        }

        if (questActionIndex == questList[questId].npcId.Length) {
            NextQuest(); //����Ʈ ���� ��ȭ ���� ����
        }

        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0; //���� ����Ʈ�� �ϱ� ���� ��ȭ �ʱ�ȭ
    }
}
