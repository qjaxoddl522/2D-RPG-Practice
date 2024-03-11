using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData; //나올 초상화 정보

    public Sprite[] portraitArr; //초상화 이미지 정보

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //일반 대화 데이터
        talkData.Add(1000, new string[] { "안녕?|0", "유니티 고수가 되는 그날까지 화이팅!|2" });
        talkData.Add(2000, new string[] { "힘들지?|1", "그래도 포기하진 마.|0", "노력만큼 정직한 것도 없으니까...|2" });

        talkData.Add(100, new string[] { "평범한 나무상자다..." });
        talkData.Add(200, new string[] { "책상 위 종이엔 얄미운 수인에 대한 이야기가 적혀있다." });

        //퀘스트 대화 데이터(퀘스트 번호 + NPC ID)
        talkData.Add(10 + 1000, new string[] { "처음 보는 얼굴이네?|1", "유니티에 관심 있어?|0", "오른쪽 호수에 가봐! 그 사람이 알려줄거야.|2" });
        talkData.Add(11 + 2000, new string[] { "유니티를 배우러 왔나?|0", "하지만 공짜는 없어.|2", "없으면 돈을 주워서라도 갖고와!|3" });

        //초상화 데이터
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex) //(유닛 id, n번째 대사)
    {
        if (talkIndex == talkData[id].Length) {
            return null;
        }
        else {
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex) //(유닛 id, 초상화 n번째 얼굴)
    {
        return portraitData[id + portraitIndex];
    }
}
