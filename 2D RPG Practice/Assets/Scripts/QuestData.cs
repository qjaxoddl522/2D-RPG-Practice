using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData //�ڵ忡�� �ҷ��� �� ���̱� ������ MonoBehaviour �Ⱦ�
{
    public string questName; //����Ʈ �̸�
    public int[] npcId; //���õ� NPC �迭

    public QuestData(string name, int[] npc) { //������
        questName = name;
        npcId = npc;
    }
}
