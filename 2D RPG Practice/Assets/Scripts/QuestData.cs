using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData //코드에서 불러서 쓸 것이기 때문에 MonoBehaviour 안씀
{
    public string questName; //퀘스트 이름
    public int[] npcId; //관련된 NPC 배열

    public QuestData(string name, int[] npc) { //생성자
        questName = name;
        npcId = npc;
    }
}
